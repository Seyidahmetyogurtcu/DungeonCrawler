using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedPowerUp : MonoBehaviour
{
    public float UpgradeAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().moveSpeed += UpgradeAmount;
            Destroy(gameObject);

        }

    }
}
