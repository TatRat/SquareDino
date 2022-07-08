using UnityEngine;

namespace Enemy.HealthBars
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;
        private Transform _transform;
        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
        private void Start()
        {
            _camera = Camera.main;
        }
        private void LateUpdate()
        {
            _transform.LookAt(_camera.transform);
        }
    }
}