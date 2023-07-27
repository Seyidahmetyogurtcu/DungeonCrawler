using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;

    [SerializeField] public float SpawnerInterval;

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
}
