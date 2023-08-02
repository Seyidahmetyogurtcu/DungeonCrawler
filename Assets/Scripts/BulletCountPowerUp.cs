using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCountPowerUp : MonoBehaviour
{
    public float UpgradeAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAttack>().bulletCount += UpgradeAmount;
            other.GetComponent<PlayerAttack>().currentBulletCount += UpgradeAmount;
            Destroy(gameObject);

        }

    }
}
