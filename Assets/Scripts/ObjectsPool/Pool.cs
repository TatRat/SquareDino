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
        private bool TryGetElement(out PoolObject element)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].isActiveAndEnabled)
                {
                    element = _pool[i];
                    element.gameObject.SetActive(true);
                    return true;
                }
            }
            element = null;
            return false;
        }
        /// <summary>
        /// Returns GameObject from the pool
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public PoolObject GetFreeElement()
        {
            if (TryGetElement(out PoolObject element)) return element;
            if (autoExpand) return CreateElement(true);
            if(_pool.Count < maxCapacity) return CreateElement(true);
            throw new Exception("pool is out of objects");
        }
        /// <summary>
        /// Returns an object from the pool, sets its postition
        /// </summary>
        /// <param name="position"> object position </param>
        /// <returns> Pool object </returns>
        public PoolObject GetFreeElement(Vector3 position)
        {
            PoolObject element = GetFreeElement();
            element.transform.position = position;
            return element;
        }
        /// <summary>
        /// Returns an object from the pool, sets its postition and rotation
        /// </summary>
        /// <param name="position">object position</param>
        /// <param name="rotation">object rotation</param>
        /// <returns>Pool object</returns>
        public PoolObject GetFreeElement(Vector3 position, Quaternion rotation)
        {
            PoolObject element = GetFreeElement(position);
            element.transform.rotation = rotation;
            return element;
        }
    }
}
