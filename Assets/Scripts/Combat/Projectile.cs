using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        // Config
        [SerializeField] float arrowSpeed = 1f;

        // String const
        private const string ENEMY_TAG = "Enemy";

        // Initialize variables
        float damage = 0;
        Health target;

        // Update is called once per frame
        void Update()
        {
            if (target == null) { return; }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;

            return target.transform.position + Vector3.up * 1.25f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() == target || other.CompareTag(ENEMY_TAG))
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}