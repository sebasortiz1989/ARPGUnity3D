using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        // Config

        // Cached Component References
        NavMeshAgent navMeshAgent;
        Animator anim;
        Health health;

        // String const
        private const string SPEED_BLEND_VALUE = "fowardSpeed";
        private const string PLAYER_TAG = "Player";

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
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            ChangeWalkRunSpeed();
            UpdateAnimator();
        }

        private void ChangeWalkRunSpeed()
        {
            if (gameObject.CompareTag(PLAYER_TAG))
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
                    navMeshAgent.speed = runSpeed;
                else
                    navMeshAgent.speed = walkSpeed;
            }
        }

        private void UpdateAnimator()
        {
            velocity = GetComponent<NavMeshAgent>().velocity;
            localVelocity = transform.InverseTransformDirection(velocity);
            anim.SetFloat(SPEED_BLEND_VALUE, Mathf.Abs(localVelocity.z));
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}