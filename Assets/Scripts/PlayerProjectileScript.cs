using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileScript : MonoBehaviour
{
    public float speed;
    public float damage;
    public float timeOfBullet;

    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed * transform.up;
        StartCoroutine(bulletTime(timeOfBullet));
    }
    
    private IEnumerator bulletTime(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyProjectile();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            DestroyProjectile();
            other.GetComponent<EnemyHealth>().TakeDamage(damage);

        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
