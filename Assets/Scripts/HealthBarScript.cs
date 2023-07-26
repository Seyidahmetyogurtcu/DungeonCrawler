using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image HealthBarTotal;
    [SerializeField] private Image HealthBarCurrent;
    void Start()
    {
        HealthBarTotal.fillAmount = playerHealth.currentHealth / 10;
    }

    void Update()
    {
        HealthBarCurrent.fillAmount = playerHealth.currentHealth / 10;
    }
}
