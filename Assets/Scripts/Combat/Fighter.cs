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
        [SerializeField] float weaponRange = 2f;
        [SerializeField] [Range(0, 3)] float timeBetweenAttacks = 1f;

        // Cached Component References
        Animator playerAnim;

        // String const
        private const string ATTACK_TRIGGER = "attack";

        // Initialize variables
        Transform target;
        float timeSinceLastAttack = 1f;

        // Start is called before the first frame update
        void Start()
        {
            playerAnim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
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
                    AttackBehaviour();
                }
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                playerAnim.SetTrigger(ATTACK_TRIGGER);
                timeSinceLastAttack = 0;
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

        // Animation Event
        public void Hit()
        {
            // This makes to only attack once
            //playerAnim.ResetTrigger(ATTACK_TRIGGER);
            //Cancel();
        }
    }
}
