using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        // Config
        [SerializeField] int sceneToLoad;

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
                SceneManager.LoadScene(sceneToLoad);
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }
}