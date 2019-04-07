using UnityEngine;

public class ShotScript : MonoBehaviour
{
    CircleCollider2D shotCollider;
    private SpriteRenderer rendererComponent;
    public int damage;
    public bool isEnemyShot;

    void Awake()
    {
        rendererComponent = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!rendererComponent.IsVisibleFrom(Camera.main))
        {
            Destroy(gameObject);
        }
    }
}
