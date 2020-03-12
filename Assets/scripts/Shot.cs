using UnityEngine;

public class Shot : MonoBehaviour
{
    public int damage;
    public float speed;
    public Vector3 direction;
    private Rigidbody rigidbodyComponent;
    public bool isEnemyShot;
    public float liveTime = 3f;
 
    void FixedUpdate()
    {
        liveTime -= Time.deltaTime;
        if (liveTime < 0) Destroy(gameObject);

        rigidbodyComponent = GetComponent<Rigidbody>();
        rigidbodyComponent.velocity = direction * speed;
    }
}
