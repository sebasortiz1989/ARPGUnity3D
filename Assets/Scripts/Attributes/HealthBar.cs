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
        [SerializeField] Canvas _canvas;

        private Vector3 _scale;

        private void Awake()
        {
            _scale = _currentHealthBar.rectTransform.localScale;
            _canvas.enabled = false;
        }

        void Update()
        {
            _scale.x = _health.GetFractionOfHealth();

            if (Mathf.Approximately(_scale.x, 0) || Mathf.Approximately(_scale.x, 1))
            {
                _canvas.enabled = false;
                return;
            }

            if (!_canvas.enabled)
                _canvas.enabled = true;
           
            _currentHealthBar.rectTransform.localScale = _scale;
        }
    }
}