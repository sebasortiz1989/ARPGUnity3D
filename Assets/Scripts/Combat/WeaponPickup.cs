using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        // Config
        [SerializeField] Weapon weapon;

        // String const
        private const string PLAYER_TAG = "Player";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_TAG))
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }

}