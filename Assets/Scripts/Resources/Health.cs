using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        // Config
        [SerializeField] float _regenerationPercentageHealth = 5;
        
        // String const
        private const string DIE_TRIGGER = "die";

        // Public variables
        public bool isDead;

        // Initialize variables
        private float initialHealth;
        
        public float healthPoints = -1;

        private BaseStats _baseStatsFromPlayer;

        public float InitialHealth { get => initialHealth; set => initialHealth = value; }

        private void Awake()
        {
            _baseStatsFromPlayer = GetComponent<BaseStats>();

            if (healthPoints < 0)
                healthPoints = _baseStatsFromPlayer.GetStat(Stat.Health);

            initialHealth = healthPoints;
        }

        private void Start()
        {
            if (_baseStatsFromPlayer != null)
            {
                _baseStatsFromPlayer.onLevelUp += RegenerateHealth;
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / initialHealth);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0 && !isDead)
            {           
                Die();
                AwardExperience(instigator);
            }
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = Mathf.Round(_baseStatsFromPlayer.GetStat(Stat.Health) * (GetPercentage() + _regenerationPercentageHealth) / 100);
            
            if (regenHealthPoints > _baseStatsFromPlayer.GetStat(Stat.Health))
                regenHealthPoints = _baseStatsFromPlayer.GetStat(Stat.Health);

            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
            initialHealth = _baseStatsFromPlayer.GetStat(Stat.Health);
        }

        private void AwardExperience(GameObject instigator)
        {
            try
            {
                Experience instigatorExperience = instigator.GetComponent<Experience>();
                if (instigatorExperience == null) { return; }
                instigatorExperience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
            }
            finally { }
        }

        private void Die()
        {
            if (isDead) return;           
            GetComponent<Animator>().SetTrigger(DIE_TRIGGER);
            StartCoroutine(DieAgain());
            isDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        IEnumerator DieAgain()
        {
            yield return new WaitForSeconds(0.5f);
            GetComponent<Animator>().SetTrigger(DIE_TRIGGER);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints == 0 && !isDead)
            {
                Die();
            }
        }
    }
}
