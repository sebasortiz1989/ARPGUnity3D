using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (weaponPrefab != null) 
            {
                Transform handTransform;

                if (isRightHanded) 
                    handTransform = rightHand;
                else
                    handTransform = leftHand;

                Instantiate(weaponPrefab, handTransform); 
            }
            if (weaponOverride != null) { animator.runtimeAnimatorController = weaponOverride; }      
        }

        public float GetRange() { return range; }
        public float GetDamage() { return damage; }
        public float GetTimeBetweenAttacks() { return timeBetweenAttacks; }
    }
}

