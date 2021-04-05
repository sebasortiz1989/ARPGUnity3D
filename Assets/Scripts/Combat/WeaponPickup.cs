using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using System;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        // Config
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime = 10f;

        // String const
        private const string PLAYER_TAG = "Player";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_TAG))
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }
        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }
    }

}