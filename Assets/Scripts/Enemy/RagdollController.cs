using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class RagdollController : MonoBehaviour
    {
        [Tooltip("Ragdoll's rigidbodies")]
        [SerializeField] private Rigidbody[] rigidbodies;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
            StartCoroutine(DisableAnimations());
        }
        IEnumerator DisableAnimations()
        {
            _animator.SetTrigger("Death");
            yield return new WaitForSeconds(0.35f);
            _animator.enabled = false;
            SwitchKinematicState(false);
        }
    }
}
