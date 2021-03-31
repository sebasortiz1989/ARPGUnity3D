using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 1f;
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target == null) { return; }
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) return target.position;

        return target.position + Vector3.up * 1.25f ;
    }
}
