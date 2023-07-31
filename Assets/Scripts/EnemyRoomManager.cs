﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{
    public GameObject virtualCam;
    public GameObject gates;

    // Passive and Active state variables
    private enum RoomState { Passive, Active, Clear}
    [SerializeField] private RoomState currentState = RoomState.Passive;

    public GameObject[] enemies;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //find all enemies objects
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            virtualCam.SetActive(true);

            if (AreEnemiesRemaining())
            {
                gates.SetActive(true);
                SwitchToActiveState();
            }

            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = RoomState.Passive;
            virtualCam.SetActive(false);

        }
    }

    private void FixedUpdate()
    {
        if(currentState == RoomState.Active)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // Check if there are any enemies remaining in the room
            if (AreEnemiesRemaining())
            {
                // If there are enemies remaining, keep the gates active
                gates.SetActive(true);
            }
            else
            {
                // If all enemies are defeated, disable the gates
                gates.SetActive(false);
                currentState = RoomState.Clear;
            }

        }
        else
        {
            gates.SetActive(false);
        }
    }

    private void SwitchToActiveState()
    {
        if (currentState == RoomState.Passive)
        {
            // Enable ShooterEnemy scripts for all objects with tag "Enemy"
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                ShooterEnemy shooterEnemy = enemy.GetComponent<ShooterEnemy>();
                if (shooterEnemy != null)
                {
                    shooterEnemy.enabled = true;
                }
            }

            currentState = RoomState.Active;
        }
    }



    private bool AreEnemiesRemaining()
    {
        return enemies.Length > 0;
    }
}