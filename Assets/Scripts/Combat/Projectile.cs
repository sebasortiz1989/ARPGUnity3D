using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 1f;
    
    Health target;

    // Update is called once per frame
    void Update()
    {
        if (target == null) { return; }
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
    }

    public void SetTarget(Health target)
    {
        this.target = target;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) return target.transform.position;

        return target.transform.position + Vector3.up * 1.25f ;
    }
}
