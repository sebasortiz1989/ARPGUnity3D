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
        bool isDeath;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            Debug.Log(healthPoints);
            if (healthPoints == 0 && !isDeath)
            {
                Die();
            }
        }

        private void Die()
        {
            isDeath = true;
            anim.SetTrigger(DIE_TRIGGER);
        }
    }
}
