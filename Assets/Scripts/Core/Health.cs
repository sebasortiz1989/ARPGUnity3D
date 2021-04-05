using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        // Config
        [SerializeField] float healthPoints = 100f;

        // String const
        private const string DIE_TRIGGER = "die";

        // Initialize Variables
        public bool isDead;

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
