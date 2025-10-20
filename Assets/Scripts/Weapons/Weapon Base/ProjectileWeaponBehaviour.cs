using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;

    // Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected float currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual protected void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) // Left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        } else if (dirx == 0 && diry < 0) // Down
        {
            scale.y = scale.y * -1;
        } else if (dirx == 0 && diry > 0) // Up
        {
            scale.x = scale.x * -1;
        } else if (dirx > 0 && diry > 0) // Right Up
        { 
            rotation.z = 0f;
        } else if (dirx > 0 && diry < 0) // Right Down
        {
            rotation.z = -90f;
        } else if (dirx < 0 && diry > 0) // Left Up
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        } else if (dirx < 0 && diry < 0) // Left Down
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

            transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentDamage);
                ReducePierce();
            }
        } else if (collision.gameObject.TryGetComponent(out BreakableProps breakable))
        {
            breakable.TakeDamage(currentDamage);
            ReducePierce();
        }
    }

    void ReducePierce()
    {
        currentPierce -= 1;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
