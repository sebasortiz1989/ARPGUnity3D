using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject _targetToDestroy = null;
        // Update is called once per frame
        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (_targetToDestroy != null)
                    Destroy(_targetToDestroy);
                else
                    Destroy(gameObject);
            }
        }
    }
}