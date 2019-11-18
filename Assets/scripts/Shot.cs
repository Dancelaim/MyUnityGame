using UnityEngine;

public class Shot : MonoBehaviour
{
    CircleCollider2D shotCollider;
    private SpriteRenderer rendererComponent;
    public int damage;
    public bool isEnemyShot;

    public float speed = 100;
    public Vector3 direction;
    private Rigidbody rigidbodyComponent;
 
    void FixedUpdate()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        rigidbodyComponent.velocity = transform.forward * speed;
    }
}
