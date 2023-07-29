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
    private float shotTimer; // Time interval between shots

    public GameObject shot;
    private Transform player;

    private enum EnemyState { Wandering, Attacking }
    private EnemyState currentState;

    public float maxRaycastDistance = 20f;
    private LayerMask playerLayerMask;

    // Variables for wandering behavior
    private Vector2 wanderTarget;
    private float wanderTimer = 0f;
    public float wanderDuration = 2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerLayerMask = LayerMask.GetMask("Player");
        numberOfShots = totalNumberOfShots;
        shotTimer = timeBetweenShots; // Initialize the shotTimer to allow the first shot.

        currentState = EnemyState.Wandering;
        wanderTarget = transform.position;
    }

    private void Update()
    {
        HandleState();
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Wandering:
                Wander();
                CheckPlayerVisibility();
                break;

            case EnemyState.Attacking:
                Attack();
                CheckPlayerVisibility();
                break;
        }
    }

    private void Wander()
    {
        // Move towards the wander target
        transform.position = Vector2.MoveTowards(transform.position, wanderTarget, speed * Time.deltaTime);

        // Check if the enemy reached the wander target
        if (Vector2.Distance(transform.position, wanderTarget) <= 0.1f)
        {
            // Generate a new random wander target
            wanderTimer += Time.deltaTime;
            if (wanderTimer >= wanderDuration)
            {
                wanderTarget = GetRandomWanderTarget();
                wanderTimer = 0f;
            }
        }
    }

    private Vector2 GetRandomWanderTarget()
    {
        // Generate a random position within a certain radius around the current position
        float wanderRadius = 2f; // You can adjust this to control the wandering area
        Vector2 randomDirection = Random.insideUnitCircle.normalized * wanderRadius;
        return (Vector2)transform.position + randomDirection;
    }

    private void Attack()
    {
        // Move towards the player
        if (Vector2.Distance(transform.position, player.position) < nearDistance)
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
        }

        // Shoot if allowed and the shotTimer reaches 0
        if (shotTimer <= 0f)
        {
            Shoot();
            shotTimer = timeBetweenShots; // Reset the shotTimer after shooting
        }
        else
        {
            shotTimer -= Time.deltaTime; // Decrease the shotTimer
        }
    }

    private void CheckPlayerVisibility()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, maxRaycastDistance, playerLayerMask);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            currentState = EnemyState.Attacking;
        }
        else
        {
            currentState = EnemyState.Wandering;
        }
    }

    private void Shoot()
    {
        if (numberOfShots > 0)
        {
            numberOfShots -= 1;
            Instantiate(shot, transform.position, Quaternion.identity);
        }
        else
        {
            Reload();
        }
    }

    private void Reload()
    {
        numberOfShots = totalNumberOfShots;
        shotTimer = timeBetweenShots; // Reset the shotTimer after reloading
    }
}
