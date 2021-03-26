using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        // Config
        [SerializeField] float chaseDistance = 8f;
        [SerializeField] float timeToWaitSuspiciously = 6f;

        // String const
        private const string PLAYER_TAG = "Player";

        // Cached reference
        GameObject player;
        Health health;
        Fighter fighter;
        Mover mover;
        NavMeshAgent navMeshAgent;
        ActionScheduler actionScheduler;

        // Initialize Variables
        float distanceToPlayer;
        Vector3 guardLocation;
        Quaternion guardRotation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag(PLAYER_TAG);
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            actionScheduler = GetComponent<ActionScheduler>();
            guardLocation = transform.position;
            guardRotation = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;
            AttackIfPlayerInRange();
        }

        private void AttackIfPlayerInRange()
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= chaseDistance && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < timeToWaitSuspiciously)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            fighter.Cancel();
            mover.StartMoveAction(guardLocation);
            if (navMeshAgent.velocity.magnitude < 0.05f && transform.rotation != guardRotation)
            {
                transform.rotation = guardRotation;
            }
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}