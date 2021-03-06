using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText _damageTextPrefab = null;

        public void Spawn(float damageAmount)
        {
            DamageText instance = Instantiate<DamageText>(_damageTextPrefab, transform);
            instance.SetValue(damageAmount);
            Destroy(instance.gameObject, 1.2f);
        }
    }
}