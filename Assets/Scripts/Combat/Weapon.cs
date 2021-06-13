using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] AudioClip _weaponStrikeSound = null;
        public void OnHit()
        {
            if (_weaponStrikeSound != null)
                AudioSource.PlayClipAtPoint(_weaponStrikeSound, transform.position);
        }
    }
}
