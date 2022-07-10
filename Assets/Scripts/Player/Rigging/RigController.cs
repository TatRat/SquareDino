using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player.Rigging
{
    [RequireComponent(typeof(AimTarget))]
    public class RigController : MonoBehaviour
    {
        [SerializeField] private Rig weaponAimRigLayer;
        private AimTarget _aimTarget;

        private void Awake()
        {
            _aimTarget = GetComponent<AimTarget>();
        }

        private void Start()
        {
            weaponAimRigLayer.weight = 0;
        }

        public void OnRun()
        {
            weaponAimRigLayer.weight = 0;
        }

        public void OnStop()
        {
            _aimTarget.ResetAimTargetPosition();
            weaponAimRigLayer.weight = 1;
        }
    }
}
