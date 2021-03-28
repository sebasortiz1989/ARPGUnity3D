using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        // Config
        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;

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
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdateThePlayer(otherPortal);

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            Portal[] otherPortals = FindObjectsOfType<Portal>();
            foreach(Portal portal in otherPortals)
            {
                if (portal == this) continue;
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