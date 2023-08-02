using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float UpgradeAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAttack>().playerAttackDamage += UpgradeAmount;
            other.GetComponent<PlayerProjectileScript>().damage += UpgradeAmount;
            Destroy(gameObject);

        }

    }
}
