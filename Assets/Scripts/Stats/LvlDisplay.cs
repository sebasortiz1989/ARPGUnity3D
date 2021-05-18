using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LvlDisplay : MonoBehaviour
    {
        // Cached references
        BaseStats baseStats;
        Text levelText;

        // String const
        private const string PLAYER_TAG = "Player";

        private void Awake()
        {
            levelText = GetComponent<Text>();
        }

        private void Start()
        {
            baseStats = GameObject.FindWithTag(PLAYER_TAG).GetComponent<BaseStats>();
        }

        void Update()
        {         
            levelText.text = (String.Format("{0:0}", baseStats.CalculateLevel()));
        }
    }
}

