using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;

    protected PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual protected void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    virtual protected void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
