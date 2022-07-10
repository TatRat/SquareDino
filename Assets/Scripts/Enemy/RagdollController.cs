using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class RagdollController : MonoBehaviour
    {
        [Tooltip("Ragdoll's rigidbodies")]
        [SerializeField] private Rigidbody[] rigidbodies;
        private Collider _collider;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
        }
        private void Start()
        {
            SwitchKinematicState(true);
        }
        private void SwitchKinematicState(bool state)
        {
            for(int i = 0; i < rigidbodies.Length; i++) rigidbodies[i].isKinematic = state;
        }
        /// <summary>
        /// Activates Ragdoll, Disables Animator
        /// </summary>
        public void ActivateRagdoll()
        {
            _animator.enabled = false;
            _collider.enabled = false;
            SwitchKinematicState(false);
        }
    }
}
