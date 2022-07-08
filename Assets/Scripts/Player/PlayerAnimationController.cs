using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        /// <summary>
        /// Turns on the Run animation
        /// </summary>
        public void SetRunAnimation()
        {
            _animator.SetTrigger("Run");
        }
        /// <summary>
        /// Turns off the Run animation
        /// </summary>
        public void SetIdleAnimation()
        {
            _animator.SetTrigger("Stop");
        }
    }
}