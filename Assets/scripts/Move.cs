using UnityEngine;


public class Move : MonoBehaviour
{
    
    public float speed = 100;
    public Vector3 direction;
    private Rigidbody rigidbodyComponent;

    void FixedUpdate()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

        rigidbodyComponent.velocity = direction;
    }

}