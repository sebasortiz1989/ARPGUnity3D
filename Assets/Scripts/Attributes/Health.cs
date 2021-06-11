using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        // Config
        [SerializeField] float _regenerationPercentageHealth = 5;
        [SerializeField] TakeDamageEvent takeDamage;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>{}
        
        // String const
        private const string DIE_TRIGGER = "die";

        // Public variables
        public bool isDead;

        // Initialize variables
        private float _maxHealth;
        
        public float _healthPoints = -1;

        private BaseStats _baseStatsFromPlayer;

        public float MaxHealthPoints { get => _maxHealth; set => _maxHealth = value; }
        public float HealthPoints { get => _healthPoints; set => _healthPoints = value; }

        private void Awake()
        {
            _baseStatsFromPlayer = GetComponent<BaseStats>();

            if (_healthPoints < 0)
                _healthPoints = _baseStatsFromPlayer.GetStat(Stat.Health);

            _maxHealth = _healthPoints;
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
            return 100 * GetFractionOfHealth();
        }

        public float GetFractionOfHealth()
        {
            return (_healthPoints / _maxHealth);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            //Debug.Log(gameObject.name + " took damage: " + damage);
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);

            takeDamage.Invoke(damage);

            if (_healthPoints == 0 && !isDead)
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

            _healthPoints = Mathf.Max(_healthPoints, regenHealthPoints);
            _maxHealth = _baseStatsFromPlayer.GetStat(Stat.Health);
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
            return _healthPoints;
        }

        public void RestoreState(object state)
        {
            _healthPoints = (float)state;
            if (_healthPoints == 0 && !isDead)
            {
                Die();
            }
        }
    }
}
