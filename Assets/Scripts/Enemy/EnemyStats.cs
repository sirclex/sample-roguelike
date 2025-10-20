using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    // Current stats
    [HideInInspector]
    float currentMoveSpeed;

    [HideInInspector]
    float currentHealth;

    [HideInInspector]
    float currentDamage;

    public float despawnDistance = 30f;
    Transform player;

    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerStats>().transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.OnEnemyKilled();
        }
    }

    private void ReturnEnemy()
    {
        EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
        transform.position = player.position + spawner.relativeSpawnPoints[Random.Range(0, spawner.relativeSpawnPoints.Count)].position;
    }
}
