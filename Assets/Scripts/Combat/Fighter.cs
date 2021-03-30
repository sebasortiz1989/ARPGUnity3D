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
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        // Cached Component References
        Animator anim;

        // String const
        private const string ATTACK_TRIGGER = "attack";
        private const string STOP_ATTACK_TRIGGER = "stopAttack"; 

        // Initialize variables
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon = null;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            EquipWeapon(defaultWeapon);
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
                bool inRange = Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
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
            if (timeSinceLastAttack >= currentWeapon.GetTimeBetweenAttacks())
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
            if (target != null) target.TakeDamage(currentWeapon.GetDamage());
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

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            weapon.Spawn(handTransform, anim);
        }
    }
}
