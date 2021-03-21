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
    float runSpeed = 5.662f;
    float walkSpeed = 3f;
    Vector3 velocity;
    Vector3 localVelocity;
    bool walkOrRun;
    bool rPressed;

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
        ChangeWalkRunSpeed();
        UpdateAnimator();
    }

    private void ChangeWalkRunSpeed()
    {
        // If R is toggled
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            if (!walkOrRun)
            {
                if (Input.GetKeyDown(KeyCode.R))
                    rPressed = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.R))
                    rPressed = false;
            }

            if (rPressed)
                walkOrRun = true;
            else
                walkOrRun = false;
        }

        // If Control is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
            walkOrRun = true;
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            walkOrRun = false;

        // Change speed
        if (walkOrRun)
            playerNavMeshAgent.speed = runSpeed;
        else
            playerNavMeshAgent.speed = walkSpeed;
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
