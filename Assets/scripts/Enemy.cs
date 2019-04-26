using UnityEngine;


public class Enemy : MonoBehaviour
{
    private bool hasSpawn;
    private Move moveScript;
    private Weapon[] weapons;
    private PolygonCollider2D coliderComponent;
    private SpriteRenderer rendererComponent;
   

    void Awake()
    {
        
        weapons = GetComponentsInChildren<Weapon>();

       
        moveScript = GetComponent<Move>();

        coliderComponent = GetComponent<PolygonCollider2D>();

        rendererComponent = GetComponent<SpriteRenderer>();
    }

    
    void Start()
    {
        hasSpawn = false;
       
        moveScript.enabled = false;

        foreach (Weapon weapon in weapons)
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
           
            foreach (Weapon weapon in weapons)
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
        foreach (Weapon weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}