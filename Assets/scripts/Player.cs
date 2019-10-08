using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float mobileSpeed;
    private Weapon[] weapons;
    public bool damagePlayer;
    private Vector3 movement;
    private Rigidbody rigidbodyComponent;
    public bool CanMove;
    Vector3 startPosition;
    public float rotatingSpeed = 0.1F;
    bool routineFinished;
    public Vector3 direction;
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
        FindObjectOfType<ResourceManager>().HpBarSchema(GetComponent<Health>().hp);
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
                Vector3 screenPosition = Input.mousePosition;
                screenPosition.z = 150;
                targetPosition = Input.mousePosition != null ? Camera.main.ScreenToWorldPoint(screenPosition) : Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                direction = targetPosition - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                targetRotation.z = targetRotation.x = 0;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,rotatingSpeed * Time.deltaTime );
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, mobileSpeed * Time.deltaTime);
                ResourceManager.Thrust();
            }
        }
        else ResourceManager.StartDelay();

       
            


        if (Physics.OverlapSphere(targetPosition, 1f) == null) ResourceManager.Thrust();

    }
}


