using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using RPG.Resources;

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

        // Initialize variables
        float enemyHealthPercent;

        private void Awake()
        {
            fighter = GameObject.FindWithTag(PLAYER_TAG).GetComponent<Fighter>();       
            healthText = GetComponent<Text>(); 
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
                enemyHealthPercent = enemyHealth.GetPercentage();
                healthText.text = (String.Format("{0:0}%", enemyHealthPercent));
            }
        }
    }
}