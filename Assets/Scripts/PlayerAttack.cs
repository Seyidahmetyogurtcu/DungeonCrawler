using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackSphere;

    public float attackRange;
    public float playerAttackDamage;

    public LayerMask enemyLayer;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
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
