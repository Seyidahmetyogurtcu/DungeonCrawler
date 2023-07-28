using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public float speed;
    public float reloadSpeed;
    public float timeBetweenShots;
    public float stoppingDistance;
    public float nearDistance;
    public float totalNumberOfShots;
    private float numberOfShots;


    public GameObject shot;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        numberOfShots = totalNumberOfShots;

        SpawnBullet();
    }
    void Update()
    {
        Movement();
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

    }
    private void Movement()
    { if (Vector2.Distance(transform.position, player.position) < nearDistance) 
        { 
        transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }   
     else if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
     else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > nearDistance)
        {
            transform.position = this.transform.position;
        }}
    private void SpawnBullet()
    {
    if(numberOfShots > 0) 
        {
            numberOfShots -= 1;
            Instantiate(shot, transform.position, Quaternion.identity);
            StartCoroutine(shotDelay(timeBetweenShots));
        }
    else
        {
            StartCoroutine(reload(reloadSpeed));
        }
    }
    private IEnumerator reload(float time)
    {   
        yield return new WaitForSeconds(time); 
        numberOfShots = totalNumberOfShots;
        SpawnBullet();
    }
    private IEnumerator shotDelay(float time)
    {   
        yield return new WaitForSeconds(time);
        SpawnBullet();
    }
}
