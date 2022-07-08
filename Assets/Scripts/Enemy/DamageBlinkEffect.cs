using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class DamageBlinkEffect : MonoBehaviour
    {
        [Tooltip("duration of the effect")]
        [SerializeField] private float blinkDuration = 10f;
        [Tooltip("effect strength")]
        [SerializeField] private float blinkIntensity = 10f;
        private float _blinkTimer;
        private SkinnedMeshRenderer _skinnedMeshRenderer;
        private void Start()
        {
            _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }
        /// <summary>
        /// causes the effect of taking damage
        /// </summary>
        public void StartBlinking()
        {
            _blinkTimer = blinkDuration;
            StartCoroutine(Blinker());
        }
        IEnumerator Blinker()
        {
            while (_blinkTimer > 0)
            {
                yield return null;
                _blinkTimer -= Time.deltaTime;
                _skinnedMeshRenderer.material.color = Color.white * (Mathf.Clamp01(_blinkTimer/blinkDuration) * blinkIntensity + 1);
            }
        }
    }
}
