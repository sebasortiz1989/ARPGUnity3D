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

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (weaponPrefab != null) { Instantiate(weaponPrefab, handTransform); }
            if (weaponOverride != null) { animator.runtimeAnimatorController = weaponOverride; }      
        }

        public float GetRange() { return range; }
        public float GetDamage() { return damage; }
        public float GetTimeBetweenAttacks() { return timeBetweenAttacks; }
    }
}

