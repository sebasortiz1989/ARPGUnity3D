using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        // Config
        [SerializeField] float chaseDistance = 8f;

        // String const
        private const string PLAYER_TAG = "Player";

        // Cached reference
        GameObject player;
        Health health;
        Fighter fighter;

        // Initialize Variables
        float distanceToPlayer;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag(PLAYER_TAG);
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
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
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance / 2);
        }
    }
}