using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class WreckageScript : MonoBehaviour
{
    private bool hasSpawn;
    private MoveScript moveScript;
    private Collider2D coliderComponent;
    private SpriteRenderer rendererComponent;

    void Awake()
    {

        // Retrieve scripts to disable when not spawn
        moveScript = GetComponent<MoveScript>();

        coliderComponent = GetComponent<Collider2D>();

        rendererComponent = GetComponent<SpriteRenderer>();
    }

    // 1 - Disable everything
    void Start()
    {
        hasSpawn = false;

        // Disable everything
        // -- collider
        coliderComponent.enabled = false;
        // -- Moving
        moveScript.enabled = false;
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
            if (!rendererComponent.IsVisibleFrom(Camera.main))
            {
                Destroy(gameObject);
            }
        }
    }

    // 3 - Activate itself.
    private void Spawn()
    {
        hasSpawn = true;

        // Enable everything
        // -- Collider
        coliderComponent.enabled = true;
        // -- Moving
        moveScript.enabled = true;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        ShotScript shot = collision.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            moveScript.direction = new Vector2(1,Random.Range(-3.0f, 3.0f));
        }
    }

}