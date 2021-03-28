using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        // Initialize variables
        public bool startSequencePlayed;

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

        public object CaptureState()
        {
            return startSequencePlayed;
        }

        public void RestoreState(object state)
        {
            startSequencePlayed = (bool)state;
        }
    }
}