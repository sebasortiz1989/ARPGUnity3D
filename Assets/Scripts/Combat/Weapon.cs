using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Resources;
using System;

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
        [SerializeField] float percentageBonus = 0f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (weaponPrefab != null)
            {
                Transform handTransform = GetTranform(rightHand, leftHand);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = weaponName;
            }

            var overRideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (weaponOverride != null) 
            { 
                animator.runtimeAnimatorController = weaponOverride; 
            }
            else if (overRideController != null)
            {
               animator.runtimeAnimatorController = overRideController.runtimeAnimatorController;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null) 
            { 
                oldWeapon = leftHand.Find(weaponName); 
            }
            if (oldWeapon == null) { return; }

            oldWeapon.name = "DESTROY WEAPON";
            Destroy(oldWeapon.gameObject);
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

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float _calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTranform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, _calculatedDamage);
        }

        public float GetPercentageBonus() { return percentageBonus; }
        public bool HasProjectile() { return projectile != null; }
        public float GetRange() { return range; }
        public float GetDamage() { return damage; }
        public float GetTimeBetweenAttacks() { return timeBetweenAttacks; }
    }
}

