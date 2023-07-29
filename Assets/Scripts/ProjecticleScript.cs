using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecticleScript : MonoBehaviour
{
    public float speed;
    public float damage;
    public float timeOfBullet;

    private Transform player;
    private Transform selfTransform;

    private Vector2 target;
    private Vector3 direction;
    private Vector3 playerPosition;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);


        selfTransform = GetComponent<Transform>();
        playerPosition = player.position;
        direction = (playerPosition - selfTransform.position).normalized;
    }
    void Update()
    {
        selfTransform.position += direction * speed * Time.deltaTime;


        StartCoroutine(bulletTime(timeOfBullet));

    }
    private IEnumerator bulletTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyProjectile();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
            other.GetComponent<PlayerHealth>().TakeDamage(damage);

        }
        else if (other.CompareTag("Enemy") || other.CompareTag("Enemy Bullet") || other.CompareTag("Player Bullet"))
        {

        }
        else
        {
            DestroyProjectile();
        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
