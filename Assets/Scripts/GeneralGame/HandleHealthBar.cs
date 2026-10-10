using UnityEngine;
using UnityEngine.UI;

public class HandleHealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;

    public void UpdateHealthBar(float _maxHealth, float _currentHealth)
    {

        healthBar.fillAmount = _currentHealth / _maxHealth;
    }
}
