using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public float UpgradeAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().maxHealth += UpgradeAmount;
            other.GetComponent<PlayerHealth>().currentHealth = other.GetComponent<PlayerHealth>().maxHealth;
            Destroy(gameObject);

        }

    }
}
