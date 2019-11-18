using UnityEngine;


public class Move : MonoBehaviour
{
    
    public float speed = 100;
    public Vector2 direction;
    private Vector2 movement;
    private Rigidbody rigidbodyComponent;

    void FixedUpdate()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

        rigidbodyComponent.velocity = movement;
    }

}