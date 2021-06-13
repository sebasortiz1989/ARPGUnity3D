using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        // Cached references
        Health health;
        Text healthText;

        // String const
        private const string PLAYER_TAG = "Player";

        private void Awake()
        {
            healthText = GetComponent<Text>();      
        }

        private void Start()
        {
            health = GameObject.FindWithTag(PLAYER_TAG).GetComponent<Health>();
        }

        void Update()
        {
            //healthText.text = (String.Format("{0:0}%", health.GetPercentage()));
            healthText.text = health.HealthPoints + "/" + health.MaxHealthPoints + " (" + Mathf.Round(health.GetPercentage()) + "%)";
        }
    }
}