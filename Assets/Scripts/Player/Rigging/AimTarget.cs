using System;
using  UnityEngine;

namespace Player.Rigging
{
    public class AimTarget : MonoBehaviour
    {
        [SerializeField] private Transform aimTargetTransform;
        [SerializeField] private Vector3 aimTargetDefaultOffset;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        public void SetAimTargetPosition(Vector3 position)
        {
            aimTargetTransform.position = position;
        }

        public void ResetAimTargetPosition()
        {
            aimTargetTransform.position = aimTargetDefaultOffset + _transform.position;
        }
    }
}