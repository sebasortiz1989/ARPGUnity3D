using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        // Config
        [SerializeField] float healthPoints = 100f;

        // Cached Component References
        Animator anim;

        // String const
        private const string DIE_TRIGGER = "die";

        // Initialize Variables
        bool isDead;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
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
            isDead = true;
            anim.SetTrigger(DIE_TRIGGER);
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}
