using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Core;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        // Config
        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1.5f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;

        // String const
        private const string PLAYER_TAG = "Player";

        // Initialize Variables
        int sceneIndex;

        // Start is called before the first frame update
        void Start()
        {    
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_TAG))
            {
                StartCoroutine(Transition());
            }             
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            GameObject player1 = GameObject.FindWithTag(PLAYER_TAG);
            player1.GetComponent<PlayerController>().enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad); //Load scene asyncronous in the background

            GameObject player2 = GameObject.FindWithTag(PLAYER_TAG);
            player2.GetComponent<PlayerController>().enabled = false;

            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdateThePlayer(otherPortal);

            savingWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(fadeInTime);

            player2.GetComponent<PlayerController>().enabled = true;
            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            Portal[] otherPortals = FindObjectsOfType<Portal>();
            foreach(Portal portal in otherPortals)
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }

        private void UpdateThePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag(PLAYER_TAG);
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}