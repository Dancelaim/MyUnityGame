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
    public float rotatingSpeed = 0.1F;
    bool routineFinished;
    public Vector3 direction;
    public bool isMoving;
    public bool isAvoiding;
    public ParticleSystem[] EngineSystems;
    private float idleTime = 5f;
    void Update()
    {
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
        }
        else
        {
            idleTime = 5f;
            EngineController(1);
        }
        

        int layerMask = 1 << 8;

        RaycastHit hit;

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask);

        if (hit.collider != null && hit.collider.enabled && GetComponentInChildren<Weapon>().enabled)
        {
            //StartCoroutine(SwipeController.Avoid(target: hit.collider ,rotatingSpeed : rotatingSpeed));
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
        StopCoroutine(SwipeController.Avoid());

        Vector3 targetPosition = transform.position;

        if (ResourceManager.Fuel > 0 && !ResourceManager.delay && isMoving)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetMouseButton(0))
            {
                Vector3 screenPosition = Input.mousePosition;
                screenPosition.z = 100;
                targetPosition = Input.mousePosition != null ? Camera.main.ScreenToWorldPoint(screenPosition) : Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                direction = targetPosition - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                targetRotation.z = targetRotation.x = 0;

                int layerMask = 1 << 9;
                if (Physics.OverlapSphere(targetPosition, 1.5f, layerMask).Length < 1)
                {
                    float enginePower = 1;
                    var distance = Vector3.Distance(transform.position, targetPosition);
                    if (distance > 15)
                    {
                        enginePower = 2.5f;
                    }
                    else if (distance < 15 && distance > 5)
                    {
                        enginePower = 2f;
                    }
                    else if (distance < 10)
                    {
                        enginePower = 1.5f;
                    }

                    EngineController(enginePower);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotatingSpeed * Time.deltaTime);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, mobileSpeed * Time.deltaTime);
                    ResourceManager.Thrust();
                }
            }
        }
        else if (!isAvoiding && isMoving)
        {
            ResourceManager.StartDelay();
        }
    }
    public void EngineController(float enginePower)
    {
        foreach (ParticleSystem engSys in EngineSystems)
        {
            engSys.startLifetime = enginePower;
        }
    }
}


