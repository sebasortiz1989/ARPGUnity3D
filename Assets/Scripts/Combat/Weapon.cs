using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController weaponOverride = null;
        [SerializeField] float range = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float damage = 3f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Transform handTransform = GetTranform(rightHand, leftHand);

                Instantiate(weaponPrefab, handTransform);
            }
            if (weaponOverride != null) { animator.runtimeAnimatorController = weaponOverride; }      
        }

        private Transform GetTranform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;

            if (isRightHanded)
                handTransform = rightHand;
            else
                handTransform = leftHand;
            return handTransform;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTranform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, damage);
        }

        public bool HasProjectile() { return projectile != null; }
        public float GetRange() { return range; }
        public float GetDamage() { return damage; }
        public float GetTimeBetweenAttacks() { return timeBetweenAttacks; }
    }
}

