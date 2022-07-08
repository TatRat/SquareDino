using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour, IInput
    {
        public event IInput.PlayerInput OnPlayerInput;
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                if(touch.phase == TouchPhase.Began) OnPlayerInput?.Invoke(touch.position);
            }
        }
    }
}
