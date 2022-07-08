using UnityEngine;

namespace Player
{
    public interface IInput
    {
        public delegate void PlayerInput(Vector2 touchPosition);
        public event PlayerInput OnPlayerInput;
    }
}