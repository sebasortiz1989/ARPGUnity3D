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

        public float range = 2f;
        public float timeBetweenAttacks = 1f;
        public float damage = 3;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (weaponPrefab == null) return;
            Instantiate(weaponPrefab, handTransform);
            animator.runtimeAnimatorController = weaponOverride;
        }
    }
}

