using System.Collections;
using Enemy;
using ObjectsPool;
using UnityEngine;

[RequireComponent(typeof(PoolObject), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [Header("bullet settings")]
    [SerializeField] private float speed = 5f;
    [Tooltip("the time after which the bullet will be deactivated")]
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 3;
    
    private Transform _transform;
    private PoolObject _pool;
    private Vector3 _direction;
    
    #region Mono
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _pool = GetComponent<PoolObject>();
    }
    private void OnEnable()
    {
        StartCoroutine(CountdownToDestruction());
    }
    private void Update()
    {
        Move();
    }
    #endregion  
    /// <summary>
    /// Sets move direction
    /// </summary>
    /// <param name="direction"> Vector to tap position </param>
    public void SetDirection(Vector3 direction)
    {
        _direction = (transform.position - direction).normalized;
    }

    void Move()
    {
        _transform.position += -_direction * Time.deltaTime * speed;
    }
    IEnumerator CountdownToDestruction()
    {
        yield return new WaitForSeconds(lifetime);
        DestroyBullet();
    }
    private void DestroyBullet()
    {
        _pool.ReturnToPool();
        StopAllCoroutines();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable)) damageable.Hit(damage);
        DestroyBullet();
    }
}