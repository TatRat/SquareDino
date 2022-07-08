using UnityEngine;
using UnityEngine.UI;

namespace Enemy.HealthBars
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthbar;
        /// <summary>
        /// Updates healthbar value
        /// </summary>
        /// <param name="percent"> current percentage of maximum health </param>
        public void UpdateBar(float percent)
        {
            healthbar.fillAmount = percent;
        }
        /// <summary>
        /// Disables healthbar
        /// </summary>
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
