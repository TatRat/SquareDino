using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        public event Action OnMovementStart;
        public event Action OnMovementEnd;
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        /// <summary>
        /// Moves Player to the next waypoint
        /// </summary>
        /// <param name="destinationCoordinates"> waypoint coordinates</param>
        public void Move(Vector3 destinationCoordinates)
        {
            OnMovementStart?.Invoke();
            _navMeshAgent.SetDestination(destinationCoordinates);
            StartCoroutine(CheckPlayerState());
        }
        IEnumerator CheckPlayerState()
        {
            do
            {
                yield return null;
            } while (_navMeshAgent.remainingDistance > 0.1f);
            _navMeshAgent.ResetPath();
            OnMovementEnd?.Invoke();
        }
    }
}
