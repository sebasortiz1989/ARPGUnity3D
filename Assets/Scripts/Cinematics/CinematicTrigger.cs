using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        // Initialize variables
        bool startSequencePlayed;

        // String const
        private const string PLAYER_TAG = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_TAG) && !startSequencePlayed)
            {
                GetComponent<PlayableDirector>().Play();
                startSequencePlayed = true;
            }
        }
    }
}