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

        // Initialize Variables
        public float healthPoints;

        // Public variables
        public bool isDead;

        private void Awake()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            Debug.Log(healthPoints);
            if (healthPoints == 0 && !isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            Debug.Log("Die " + name);           
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
