using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyPrefabs;

    [SerializeField] private GameObject[] EliteEnemyPrefabs;

    [SerializeField] public float SpawnerInterval;

    private Vector2 parentPosition;

    //Handle Reset Event
    private void OnEnable()
    {
        ResetManager.OnReset += HandleReset;
    }

    private void OnDisable()
    {
        ResetManager.OnReset -= HandleReset;
    }

    private void HandleReset(int resetCount)
    {
        // Do something in response to the scene reset.
        SpawnEnemiesBatch(resetCount);
        Debug.Log("RoomScript: Scene Reset! Reset Count: " + resetCount);
    }

    void Start()
    {
        parentPosition = transform.parent.position;
    }

    private void SpawnEnemiesBatch(int resetCount)
    {
        int enemiesToSpawn = Mathf.Min(2 * resetCount, 20);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            bool eliteChance = Mathf.Min(resetCount, 35) >= Random.Range(1, 41);

            while (true)
            {
                spawnPosition = new Vector3(parentPosition.x + Random.Range(-15f, 15f), parentPosition.y + Random.Range(-15f, 15f), 0);

                // Check if there are any colliders within a radius of 1 around the spawn position
                Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 1f);
                bool isValidSpawnPosition = true;

                foreach (Collider2D collider in colliders)
                {
                        isValidSpawnPosition = false;
                        break;
                   
                }

                // If the spawn position is valid, break the loop
                if (isValidSpawnPosition)
                {
                    break;
                }
            }

            Debug.Log(eliteChance);
            if (eliteChance)
            {
                Instantiate(EliteEnemyPrefabs[Random.Range(0, EliteEnemyPrefabs.Length)], spawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], spawnPosition, Quaternion.identity);
            }
        }
    }

    /*
   void Start()
   {
       StartCoroutine(spawnEnemy(SpawnerInterval, EnemyPrefab));    
   }
   private IEnumerator spawnEnemy(float interval, GameObject enemy)
   {
       yield return new WaitForSeconds(interval);
       GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-20f,20), Random.Range(40f, 45f), 0),  Quaternion.identity);
       StartCoroutine(spawnEnemy(interval, enemy));
   }
   */
}

