using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        // Cached references
        Health enemyHealth;
        Text healthText;
        Fighter fighter;

        // String const
        private const string PLAYER_TAG = "Player";

        private void Awake()
        {  
            healthText = GetComponent<Text>(); 
        }

        private void Start()
        {
            fighter = GameObject.FindWithTag(PLAYER_TAG).GetComponent<Fighter>();
        }

        void Update()
        {
            enemyHealth = fighter.GetTarget();
            if (enemyHealth == null) 
            {
                healthText.text = "N/A";
            }
            else
            {
                healthText.text = enemyHealth.HealthPoints + " (" + Mathf.Round(enemyHealth.GetPercentage()) + "%)";
            }
        }
    }
}