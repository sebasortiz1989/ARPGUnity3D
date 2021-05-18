using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        // Config
        [SerializeField] float arrowSpeed = 15f;
        [SerializeField] bool isHoming;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 0.2f;

        // String const
        private const string ENEMY_TAG = "Enemy";

        // Initialize variables
        float damage = 0;
        Health target;
        GameObject instigator = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, maxLifeTime);
        }

        void Update()
        {
            if (target == null) { return; }

            if (isHoming && !target.IsDead())
                transform.LookAt(GetAimLocation());

            transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;

            return target.transform.position + Vector3.up;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (target.IsDead()) return;

            if (other.GetComponent<Health>() == target || other.CompareTag(ENEMY_TAG))
            {
                target.TakeDamage(instigator, damage);

                arrowSpeed = 0;

                if (hitEffect != null)
                {
                    Instantiate(hitEffect, other.transform.position, Quaternion.identity);
                }

                foreach(GameObject toDestroy in destroyOnHit)
                {
                    Destroy(toDestroy);
                }
                    
                Destroy(gameObject, lifeAfterImpact);
            }
        }
    }
}