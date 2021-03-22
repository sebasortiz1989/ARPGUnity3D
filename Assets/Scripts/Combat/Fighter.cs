using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        // Config
        [SerializeField] float weaponRange = 2;

        // Initialize variables
        Transform target;

        // Update is called once per frame
        void Update()
        {
            if (target != null)
            {
                GetComponent<Mover>().MoveTo(target.position);
                if (Vector3.Distance(transform.position, target.position) < weaponRange)
                {
                    GetComponent<Mover>().Stop();
                }
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        public void CancelAttack()
        {
            target = null;
        }
    }
}
