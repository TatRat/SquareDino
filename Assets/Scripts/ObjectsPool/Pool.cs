using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectsPool
{
    public class Pool : MonoBehaviour
    {
        [Tooltip("Prefab for spawn")] [SerializeField] private PoolObject prefab;
        [Tooltip("GameObject for storing bullets on stage")]
        [SerializeField] private Transform container;
        [Space][Header("Pool capacity Settings")]
        [SerializeField] private int minCapacity;
        [SerializeField] private int maxCapacity;
        [Tooltip("if the value is true, it automatically expands the size of the pool")]
        [SerializeField] private bool autoExpand;

        private List<PoolObject> _pool;

        private void Start()
        {
            CreatePool();
        }

        private void OnValidate()
        {
            if(autoExpand) maxCapacity = Int32.MaxValue;
        }
        private void CreatePool()
        {
            _pool = new List<PoolObject>(minCapacity);
            for (int i = 0; i < minCapacity; i++) CreateElement();
        }
        private PoolObject CreateElement(bool isActiveByDefault = false)
        {
            PoolObject createdObject = Instantiate(prefab, container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }
        private bool TryGetElement(out PoolObject element, bool isOverloaded = false)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].isActiveAndEnabled)
                {
                    element = _pool[i];
                    if(!isOverloaded) element.gameObject.SetActive(true);
                    return true;
                }
            }
            element = null;
            return false;
        }
        /// <summary>
        /// Returns GameObject from the pool
        /// </summary>
        /// <param name="isOverloaded"> was an overloaded method called? </param>
        /// <returns> PoolObject from the pool</returns>
        /// <exception cref="Exception"> pool is out of objects </exception>
        public PoolObject GetFreeElement(bool isOverloaded = false)
        {
            if (TryGetElement(out PoolObject element, isOverloaded)) return element;
            if (autoExpand) return CreateElement(!isOverloaded);
            if(_pool.Count < maxCapacity) return CreateElement(!isOverloaded);
            throw new Exception("pool is out of objects");
        }
        /// <summary>
        /// Returns an object from the pool, sets its position
        /// </summary>
        /// <param name="position"> object position </param>
        /// <param name="isOverloaded"> was an overloaded method called? </param>
        /// <returns> PoolObject from the pool </returns>
        public PoolObject GetFreeElement(Vector3 position, bool isOverloaded = false)
        {
            PoolObject element = GetFreeElement(true);
            element.transform.position = position;
            if(!isOverloaded) element.gameObject.SetActive(true);
            return element;
        }
        /// <summary>
        /// Returns an object from the pool, sets its position and rotation
        /// </summary>
        /// <param name="position">object position</param>
        /// <param name="rotation">object rotation</param>
        /// <returns>Pool object</returns>
        public PoolObject GetFreeElement(Vector3 position, Quaternion rotation)
        {
            PoolObject element = GetFreeElement(position, true);
            element.transform.rotation = rotation;
            element.gameObject.SetActive(true);
            return element;
        }
    }
}
