using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectible collectible))
        {
            collectible.Collect();
        }
    }
}
