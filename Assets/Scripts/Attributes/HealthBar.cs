using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image _currentHealthBar;
        [SerializeField] Health _health;

        private Vector3 _scale;

        private void Awake()
        {
            _scale = _currentHealthBar.rectTransform.localScale;
        }

        void Update()
        {
            _scale.x = _health.GetFractionOfHealth();
            _currentHealthBar.rectTransform.localScale = _scale;
        }
    }
}