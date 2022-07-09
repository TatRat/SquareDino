using System;
using UnityEngine;

namespace Player
{
    public interface IInput
    {
        public event Action<Vector2> OnPlayerInput;
    }
}