using System.Collections.Generic;
using UnityEngine;

public class AuraBehaviour : MeleeWeaponBehaviour
{
    List<GameObject> markedEnemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !markedEnemies.Contains(collision.gameObject))
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentDamage);
                markedEnemies.Add(collision.gameObject);
            }
        } else if (collision.gameObject.TryGetComponent(out BreakableProps breakable))
        {
            breakable.TakeDamage(currentDamage);
        }
    }
}
