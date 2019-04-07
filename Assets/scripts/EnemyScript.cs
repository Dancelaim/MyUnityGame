using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
    private bool hasSpawn;
    private MoveScript moveScript;
    private WeaponScript[] weapons;
    private PolygonCollider2D coliderComponent;
    private SpriteRenderer rendererComponent;
   

    void Awake()
    {
        // Retrieve the weapon only once
        weapons = GetComponentsInChildren<WeaponScript>();

        // Retrieve scripts to disable when not spawn
        moveScript = GetComponent<MoveScript>();

        coliderComponent = GetComponent<PolygonCollider2D>();

        rendererComponent = GetComponent<SpriteRenderer>();
    }

    // 1 - Disable everything
    void Start()
    {
        hasSpawn = false;
       
        moveScript.enabled = false;

        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
    }

    void Update()
    {
        // 2 - Check if the enemy has spawned.
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
            // Auto-fire
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                    SoundEffectsHelper.Instance.MakeEnemyShotSound();
                }
            }

            // 4 - Out of the camera ? Destroy the game object.
            if (!rendererComponent.IsVisibleFrom(Camera.main))
            {
                if (gameObject.tag == "Alien")
                {
                    var counter = FindObjectOfType<ResourceManager>();
                    if (counter)
                    {
                        counter.MissedCounter();
                    }
                }
                Destroy(gameObject);
            }
        }
        
    }
    
    private void Spawn()
    {
        hasSpawn = coliderComponent.enabled = moveScript.enabled = true;
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}