using UnityEngine;


public class Enemy : MonoBehaviour
{
    public bool hasSpawn;
    private Move moveScript;
    private EnemyWeapon[] weapons;
    private PolygonCollider2D coliderComponent;
    private SpriteRenderer rendererComponent;
    public float Force;



    void Awake()
    {
        weapons = GetComponentsInChildren<EnemyWeapon>();
        coliderComponent = GetComponent<PolygonCollider2D>();
        rendererComponent = GetComponent<SpriteRenderer>();
    }
    //TO DO : LASER CANON SHOT 
    
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
        //EnemyBehavior();
        //Scan();
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
    //private void EnemyBehavior()
    //{
    //    int layerMask = 1 << 9;

    //    RaycastHit hit;

    //    Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask);

    //    if (hit.collider != null && hit.collider.enabled && GetComponentInChildren<Weapon>().enabled)
    //    {
    //        GetComponentInChildren<EnemyWeapon>().Attack(true);
    //        SoundEffectsHelper.Instance.MakeEnemyShotSound();
    //    }
    //}

    //private void Scan()
    //{
    //    int layerMask = 1 << 9;

    //    var possibleIntersections = Physics.OverlapSphere(transform.position, 500, layerMask);
    //    if(possibleIntersections != null)
    //    {
    //        Avoid();
    //    }
    //}

    //private void Avoid()
    //{
    //    Rigidbody Rgb = GetComponent<Rigidbody>();

    //    if (Random.Range(0, 1) != 1)
    //    {
    //        Rgb.AddForce(Vector3.left * Force, ForceMode.Impulse);
    //    }
    //    else
    //    {
    //        Rgb.AddForce(Vector3.right * Force, ForceMode.Impulse);
    //    }
    //}
    
}