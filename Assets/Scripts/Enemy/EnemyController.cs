using System.Collections;
using Enemy.HealthBars;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(DamageBlinkEffect), typeof(RagdollController))]
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [Header("Stages settings")]
        [SerializeField] private StageManager stageManager;
        [SerializeField] private int stageNumber; 
        [Space] [Header("Health settings")] [Tooltip("maximum amount of health")]
        [SerializeField] private int healPoint = 10;
        [Tooltip("Time after death before deactivation")]
        [SerializeField] private float timeToDeactivate = 4f;
        [Header("Healtbar")]
        [SerializeField] private HealthBar healthBar;
        
        private DamageBlinkEffect _damageBlinkEffect;
        private RagdollController _ragdollController;
        private int _maxHealth;
        private bool _isAlive = true;
        private void Awake()
        {
            _damageBlinkEffect = GetComponent<DamageBlinkEffect>();
            _ragdollController = GetComponent<RagdollController>();
            _maxHealth = healPoint;
        }
        private void Start()
        {
            stageManager.InitializeEnemiesOnScene(stageNumber);
            healthBar.UpdateBar((float)healPoint/_maxHealth);
        }
        /// <summary>
        /// calculates remaining health, updates healthbar and causes injury effects
        /// </summary>
        /// <param name="damage"> damage done </param>
        public void Hit(int damage)
        {
            if(!_isAlive) return;
            healPoint -= damage;
            healthBar.UpdateBar((float)healPoint/_maxHealth);
            _damageBlinkEffect.StartBlinking();
            if(healPoint <= 0) Die();
        }
        private void Die()
        {
            _isAlive = false;
            stageManager.OnEnemyDie(stageNumber);
            _ragdollController.ActivateRagdoll(); 
            healthBar.Disable();
            StartCoroutine(DeactivateBody());
        }

        IEnumerator DeactivateBody()
        {
            yield return new WaitForSeconds(timeToDeactivate);
            gameObject.SetActive(false);
        }
    }
}
