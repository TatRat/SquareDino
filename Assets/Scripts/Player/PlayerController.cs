using UnityEngine;
namespace Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerAnimationController), typeof(PlayerWeapon))]
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("links to waypoints")]
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private StageManager stageManager;
        
        private IInput _inputManager;
        private PlayerMovement _playerMovement;
        private PlayerWeapon _playerWeapon;
        private PlayerAnimationController _playerAnimationController;
        private Camera _camera;
        private bool _isStarted = false;
        
        private void Awake()
        {
            Caching();
            Subscriptions();
        }
        private void Start()
        {
            _camera = Camera.main;
        }
        private void Caching()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAnimationController = GetComponent<PlayerAnimationController>();
            _playerWeapon = GetComponent<PlayerWeapon>();
            _inputManager = GetComponent<IInput>();
        }
        private void Subscriptions()
        {
            _playerMovement.OnMovementStart += () => _playerAnimationController.SetRunAnimation();
            _playerMovement.OnMovementStart += () => _playerWeapon.OffPossibilityToShoot();
            _playerMovement.OnMovementEnd += () => _playerAnimationController.SetIdleAnimation();
            _playerMovement.OnMovementEnd += () => _playerWeapon.OnPossibilityToShoot();
            stageManager.OnStageChange += number => _playerMovement.Move(waypoints[number].position);
            _inputManager.OnPlayerInput += PlayersActions;
        }
        /// <summary>
        /// Gets direction to shoot
        /// </summary>
        /// <param name="touchPosition"></param>
        void PlayersActions(Vector2 touchPosition)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!_isStarted)
            {
                _isStarted = true;
                _playerMovement.Move(waypoints[0].position);
            }
            else if (Physics.Raycast(ray, out RaycastHit hit)) _playerWeapon.Attack(hit.point);
            else _playerWeapon.Attack(10 * ray.direction + _camera.transform.position);
        }
    }
}
