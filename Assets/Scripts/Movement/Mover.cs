using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Control;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float enemyRunSpeed = 4f;
        [SerializeField] float enemyWalkSpeed = 2f;
        [SerializeField] float maxNavPathLength = 40f;

        // Cached Component References
        NavMeshAgent navMeshAgent;
        Animator anim;
        Health health;
        AIController aiController;

        // String const
        private const string SPEED_BLEND_VALUE = "fowardSpeed";
        private const string PLAYER_TAG = "Player";
        private const string ENEMY_TAG = "Enemy";

        // Initialize Variables
        float runSpeed = 5.662f;
        float walkSpeed = 3f;
        Vector3 velocity;
        Vector3 localVelocity;
        bool walkOrRun;
        private static bool rPressed;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            health = GetComponent<Health>();

            if (gameObject.CompareTag(ENEMY_TAG))
                aiController = GetComponent<AIController>();
        }

        // Update is called once per frame
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            if (health.IsDead()) return;

            if (gameObject.CompareTag(PLAYER_TAG))
                ChangePlayerWalkRunSpeed();

            if (gameObject.CompareTag(ENEMY_TAG))
                ChangeEnemyWalkRunSpeed();

            UpdateAnimator();
        }

        private void ChangeEnemyWalkRunSpeed()
        {
            if (aiController.AttackIfPlayerInRange())
                navMeshAgent.speed = enemyRunSpeed;
            else
                navMeshAgent.speed = enemyWalkSpeed;
        }

        private void ChangePlayerWalkRunSpeed()
        {
            // If Control is pressed
            if (Input.GetKeyDown(KeyCode.LeftControl))
                walkOrRun = true;
            else if (Input.GetKeyUp(KeyCode.LeftControl))
                walkOrRun = false;

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

            // Change speed
            if (walkOrRun)
                navMeshAgent.speed = runSpeed;
            else
                navMeshAgent.speed = walkSpeed;
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                float distance = Vector3.Distance(path.corners[i], path.corners[i + 1]);
                total += distance;
            }

            return total;
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

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().Warp(position.ToVector());
        }
    }
}