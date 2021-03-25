using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        // Config
        [SerializeField] float chaseDistance = 5f;

        // String const
        private const string PLAYER_TAG = "Player";

        // Initialize Variables
        GameObject player;
        float distanceToPlayer;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag(PLAYER_TAG);
        }

        // Update is called once per frame
        void Update()
        {
            DistanceToPlayer();
        }

        private void DistanceToPlayer()
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= chaseDistance)
            {
                Debug.Log(this.name + " Starts chasing the " + player.name);
            }
        }
    }
}