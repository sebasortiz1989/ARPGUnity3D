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
        [SerializeField] float weaponDamage = 3;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTransform = null;

        // Cached Component References
        Animator anim;

        // String const
        private const string ATTACK_TRIGGER = "attack";
        private const string STOP_ATTACK_TRIGGER = "stopAttack"; 

        // Initialize variables
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            SpawnWeapon();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            if (target != null && !target.IsDead())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
                bool inRange = Vector3.Distance(transform.position, target.transform.position) < weaponRange;
                if (inRange)
                {
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                }
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // This will trigger the Hit() event
                TriggetAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggetAttack()
        {
            anim.ResetTrigger(STOP_ATTACK_TRIGGER);
            anim.SetTrigger(ATTACK_TRIGGER);
        }

        // Animation Event
        public void Hit()
        {
            // This makes to only attack once
            //playerAnim.ResetTrigger(ATTACK_TRIGGER);
            //Cancel();

            if (target != null) target.TakeDamage(weaponDamage);
        }
        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            anim.ResetTrigger(ATTACK_TRIGGER);
            anim.SetTrigger(STOP_ATTACK_TRIGGER);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) 
                return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return combatTarget != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        private void SpawnWeapon()
        {
            if (weaponPrefab != null)
                Instantiate(weaponPrefab, handTransform);
        }
    }
}
