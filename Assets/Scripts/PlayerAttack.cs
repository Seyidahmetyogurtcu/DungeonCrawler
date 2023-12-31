using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackSphere;

    public float attackRange;
    public float playerAttackDamage;
    public float bulletCount;
    public float reloadSpeed;
    private float reloadTimer = 0f;

    public float currentBulletCount;
    

    public LayerMask enemyLayer;

    public GameObject shot;

    private bool isMelee = false;
    private bool isReloading = false;

    private void Start()
    {
        currentBulletCount = bulletCount;
    }
    private void Update()
    {
        Inputs();
        Reload();
    }
    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isMelee)
            {
                isMelee = false;
            }
            else
            {
                isMelee = true;
            }
        }


        if (Input.GetButtonDown("Fire1"))
        {
            if (isMelee)
            {
                Attack();
            }
            else
            {
                Shoot();
            }

        }
        if (currentBulletCount <= 0)
        {
            isReloading = true;
        }
    }
    private void Shoot()
    {
        if (currentBulletCount > 0)
        {
            Instantiate(shot, transform.position, transform.rotation);
            currentBulletCount--;
        }
    }
    private void Reload()
    {
        if (isReloading)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadSpeed)
            {
                currentBulletCount = bulletCount;
                reloadTimer = 0f;
                isReloading= false;
            }
        }
    }
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackSphere.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(playerAttackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackSphere == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackSphere.position, attackRange);
    }
}
