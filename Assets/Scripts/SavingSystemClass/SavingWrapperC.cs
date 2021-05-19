using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SavingC
{
    public class SavingWrapperC : MonoBehaviour
    {
        private const string defaultSaveFile = "save";
        private SavingSystemC _savingSystem;

        private void Awake()
        {
            _savingSystem = GetComponent<SavingSystemC>();
        }

        void Update()
        {
            SaveLoadKeyStroke();
        }

        private void SaveLoadKeyStroke()
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.L))
            {
                _savingSystem.Load(defaultSaveFile);
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.S))
            {
                _savingSystem.Save(defaultSaveFile);
            }
        }
    }
}