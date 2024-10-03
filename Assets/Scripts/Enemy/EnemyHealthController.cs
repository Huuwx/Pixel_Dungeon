using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] Image _healthBarFill;
    [SerializeField] EnemyController _enemyController;
    [SerializeField] private Transform _healthBarTransform;
    public Camera _camera;

    void Awake()
    {
        currentHealth = maxHealth;
        _camera = Camera.main;
    }

    void Update()
    {
        _healthBarTransform.rotation = _camera.transform.rotation;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        if (currentHealth > 0)
        {
            _enemyController.HitAnimation();
        }
        else if (currentHealth <= 0)
        {
            if (gameObject.tag == "Orc")
            {
                if (PlayerPrefs.GetInt("Progress_2") < PlayerPrefs.GetInt("Target_2"))
                {
                    Debug.Log("CONG DIEM");
                    TaskManager.Instance.SetProgress_2();
                }
            }
            else if (gameObject.tag == "Skeleton")
            {
                if (PlayerPrefs.GetInt("Progress_1") < PlayerPrefs.GetInt("Target_1"))
                {
                    Debug.Log("CONG DIEM");
                    TaskManager.Instance.SetProgress_1();
                }
            }
            _enemyController.Dead();
        }
    }

    private void UpdateHealthBar()
    {
        float targetFillAmount = currentHealth / maxHealth;
        _healthBarFill.fillAmount = targetFillAmount;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
}
