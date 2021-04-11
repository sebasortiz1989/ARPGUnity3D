using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class XPDisplay : MonoBehaviour
    {
    // Cached references
    Experience experience;
    Text experienceText;

    // String const
    private const string PLAYER_TAG = "Player";

    private void Awake()
    {
            experience = GameObject.FindWithTag(PLAYER_TAG).GetComponent<Experience>();
            experienceText = GetComponent<Text>();
    }

    void Update()
        {
            experienceText.text = (String.Format("{0:0}", experience.GetExperiencePoints()));
        }
    }
}

