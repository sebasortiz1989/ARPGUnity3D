using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SavingC
{
    public class SavingSystemC : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            print("Saving to " + saveFile);
        }

        public void Load(string saveFile)
        {
            print("Loading from " + saveFile);
        }
    }
}

