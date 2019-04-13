using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    private bool hasSpawn;
    private MoveScript moveScript;
    private WeaponScript[] weapons;
    private PolygonCollider2D coliderComponent;
    private SpriteRenderer rendererComponent;
   

    void Awake()
    {
        
        weapons = GetComponentsInChildren<WeaponScript>();

       
        moveScript = GetComponent<MoveScript>();

        coliderComponent = GetComponent<PolygonCollider2D>();

        rendererComponent = GetComponent<SpriteRenderer>();
    }

    
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
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
           
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                    SoundEffectsHelper.Instance.MakeEnemyShotSound();
                }
            }

          
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