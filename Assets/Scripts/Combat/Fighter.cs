using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // Config
        [SerializeField] float weaponRange = 2;

        // Initialize variables
        Transform target;

        // Update is called once per frame
        void Update()
        {
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            if (target != null)
            {
                GetComponent<Mover>().MoveTo(target.position);
                bool inRange = Vector3.Distance(transform.position, target.position) < weaponRange;
                if (inRange)
                {
                    GetComponent<Mover>().Cancel();
                    Debug.Log("Attacking");
                }
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
