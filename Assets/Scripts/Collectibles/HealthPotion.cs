using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectible
{
    public int healthAmount;
    public void Collect()
    {
        PlayerStats player = FindFirstObjectByType<PlayerStats>();
        player.Heal(healthAmount);
        Destroy(gameObject);
    }
}
