using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Vector3 speed = new Vector3(50, 50, 0);
    public float mobileSpeed;
    private Weapon[] weapons;
    public bool damagePlayer;
    private Vector3 movement;
    private Rigidbody rigidbodyComponent;
    public bool CanMove;
    Vector3 startPosition;
    float dist;
    float leftBorder;
    float rightBorder;
    float topBorder;
    float bottomBorder;


    public void Awake()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        int layerMask = 1 << 8;

        RaycastHit hit;

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask);

        if (hit.collider != null && hit.collider.enabled && GetComponentInChildren<Weapon>().enabled)
        {
            GetComponentInChildren<Weapon>().Attack();
            SoundEffectsHelper.Instance.MakePlayerShotSound();
        }


        //if (Input.GetButton("Fire1")) 


        var HpCounter = FindObjectOfType<ResourceManager>();
        Health playerHealth = GetComponent<Health>();
        HpCounter.HpBarSchema(playerHealth.hp);
        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftBorder, rightBorder), Mathf.Clamp(transform.position.y, topBorder, bottomBorder));
    }
    void FixedUpdate()
    {
        PlayerMove();
    }

    void OnDestroy()
    {
       UIInteractions.GameOver();
    }

    public void PlayerMove()
    {
        Vector3 targetPosition = transform.position;

        if (ResourceManager.Fuel > 0 && !ResourceManager.delay)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButton(0))
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) != null ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, 0, targetPosition.z), mobileSpeed * Time.deltaTime);
            }
        }
        else ResourceManager.StartDelay();

        
        if(Physics.OverlapSphere(targetPosition, 1f) == null) ResourceManager.Thrust();
        
        


    }
}


