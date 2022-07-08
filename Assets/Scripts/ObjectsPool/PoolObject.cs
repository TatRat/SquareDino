using UnityEngine;

namespace ObjectsPool
{
    public class PoolObject : MonoBehaviour
    {
        /// <summary>
        /// Returns Object to pool
        /// </summary>
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
