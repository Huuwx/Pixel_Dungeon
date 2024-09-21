using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [SerializeField] Image _healthBarFill;


    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        if (currentHealth > 0) 
        {
            PlayerController.Instance.TakeDamageAnimation();
        }
        else
        {
            currentHealth = 0;
            PlayerController.Instance.Dead();
        }
    }

    private void UpdateHealthBar()
    {
        float targetFillAmount = currentHealth / maxHealth;
        _healthBarFill.fillAmount = targetFillAmount;
    }
}
