using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string DEFAULT_SAVE_FILE = "save";

        void Update()
        {
            SaveLoadKeyStroke();
        }

        private void SaveLoadKeyStroke()
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(DEFAULT_SAVE_FILE);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(DEFAULT_SAVE_FILE);
        }
    }
}