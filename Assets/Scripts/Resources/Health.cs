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
        // String const
        private const string DIE_TRIGGER = "die";

        // Public variables
        public bool isDead;

        // Initialize variables
        float initialHealth;
        float healthPoints = -1;

        private void Awake()
        {
            if (healthPoints < 0)
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            initialHealth = healthPoints;
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
