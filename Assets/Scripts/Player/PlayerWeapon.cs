using System.Collections;
using ObjectsPool;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Pool))]
    public class PlayerWeapon : MonoBehaviour
    {
        [Tooltip("the place where the bullets are flying from")]
        [SerializeField] private Transform weaponPoint;
        [SerializeField] private float cooldownTime = 0.2f;
        private bool _isReadyToShoot = true;
        private Pool _pool;
        private void Awake()
        {
            _pool = GetComponent<Pool>();
        }
        /// <summary>
        /// Spawns bullet
        /// </summary>
        /// <param name="attackPoint"></param>
        public void Attack(Vector3 attackPoint)
        {
            if(!_isReadyToShoot) return;
            _pool.GetFreeElement(weaponPoint.position, Quaternion.identity).GetComponent<Bullet>().SetDirection(attackPoint);
            StartCoroutine(Cooldown());
        }
        
        public void OffPossibilityToShoot()
        {
            _isReadyToShoot = false;
            StopCoroutine(Cooldown());
        }
        public void OnPossibilityToShoot()
        {
            _isReadyToShoot = true;
        }
        IEnumerator Cooldown()
        {
            _isReadyToShoot = false;
            yield return new WaitForSeconds(cooldownTime);
            _isReadyToShoot = true;
        }
    }
}
