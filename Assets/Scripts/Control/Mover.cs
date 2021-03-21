using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    // Config

    // Cached Component References
    NavMeshAgent playerNavMeshAgent;
    Animator playerAnim;

    // String const
    private const string SPEED_BLEND_VALUE = "fowardSpeed";

    // Initialize Variables
    Vector3 velocity;
    Vector3 localVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerNavMeshAgent = GetComponent<NavMeshAgent>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToCursor();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        velocity = GetComponent<NavMeshAgent>().velocity;
        localVelocity = transform.InverseTransformDirection(velocity);
        playerAnim.SetFloat(SPEED_BLEND_VALUE, Mathf.Abs(localVelocity.z));
    }

    private void MoveToCursor()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInformation;
            bool hasHit = Physics.Raycast(ray, out hitInformation);
            if (hasHit)
            {
                playerNavMeshAgent.destination = hitInformation.point;
            }
        }
    }
}
