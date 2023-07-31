using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{
    public GameObject virtualCam;
    public GameObject gates;

    // Passive and Active state variables
    private enum RoomState { Passive, Active, Clear }
    [SerializeField] private RoomState currentState = RoomState.Passive;

    private List<GameObject> enemies = new List<GameObject>(); // Use a List to store the enemies within the room

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find all enemies objects within the room
            enemies = FindEnemiesWithinRoom();

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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentState == RoomState.Active)
            {
                // Find all enemies objects within the room
                enemies = FindEnemiesWithinRoom();

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
                    Debug.Log("Hello");
                }
            }
            else
            {
                Debug.Log("Goodbye");
                gates.SetActive(false);
            }
        }
    }

    private void SwitchToActiveState()
    {
        if (currentState == RoomState.Passive)
        {
            // Enable ShooterEnemy scripts for all objects with tag "Enemy"
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

    private List<GameObject> FindEnemiesWithinRoom()
    {
        List<GameObject> enemiesWithinRoom = new List<GameObject>();

        // Assuming there's a collider2D for the room, get its bounds
        Collider2D roomCollider = GetComponent<Collider2D>();
        if (roomCollider != null)
        {
            // Get all colliders within the room's bounds
            Collider2D[] colliders = Physics2D.OverlapBoxAll(roomCollider.bounds.center, roomCollider.bounds.size, 0f);

            // Check each collider if it has the tag "Enemy" and add to the list if true
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    enemiesWithinRoom.Add(collider.gameObject);
                }
            }
        }

        return enemiesWithinRoom;
    }

    private bool AreEnemiesRemaining()
    {
        return enemies.Count > 0;
    }
}
