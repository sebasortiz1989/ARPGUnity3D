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
        [SerializeField] float arrowSpeed = 15f;
        [SerializeField] bool isHoming;
        [SerializeField] GameObject hitEffect = null;

        // String const
        private const string ENEMY_TAG = "Enemy";

        // Initialize variables
        float damage = 0;
        Health target;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
            Destroy(gameObject, 5f);
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) { return; }

            if (isHoming && !target.IsDead())
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

            return target.transform.position + Vector3.up;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (target.IsDead()) return;

            if (other.GetComponent<Health>() == target || other.CompareTag(ENEMY_TAG))
            {
                target.TakeDamage(damage);
                if (hitEffect != null)
                    Instantiate(hitEffect, other.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}