using UnityEngine;


public class Enemy : MonoBehaviour
{
    public bool hasSpawn;
    private Move moveScript;
    private EnemyWeapon[] weapons;
    private PolygonCollider2D coliderComponent;
    private SpriteRenderer rendererComponent;
   

    void Awake()
    {
        weapons = GetComponentsInChildren<EnemyWeapon>();
        coliderComponent = GetComponent<PolygonCollider2D>();
        rendererComponent = GetComponent<SpriteRenderer>();
    }

    
    void Start()
    {
        hasSpawn = false;
        foreach (EnemyWeapon weapon in weapons)
        {
            weapon.enabled = coliderComponent.enabled = false;
        }
    }

    void Update()
    {
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
           
            foreach (EnemyWeapon weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                    SoundEffectsHelper.Instance.MakeEnemyShotSound();
                }
            }

          
            if (!rendererComponent.IsVisibleFrom(Camera.main))
            {
                Destroy(gameObject);
            }
        }
        
    }
    
    private void Spawn()
    {
        hasSpawn = true;
        foreach (EnemyWeapon weapon in weapons)
        {
            weapon.enabled = coliderComponent.enabled  = true;
        }
    }
}