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
    private float idleTime = 5f;
    EngineController EngContrl;
    bool movingInProgress;
    private void Start()
    {
        EngContrl = GetComponent<EngineController>();
    }
    void Update()
    {
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
        }
        else
        {
            idleTime = 5f;
            EngContrl.MainEngineEffect(1);
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
        StopCoroutine(SwipeController.Avoid(1));
         
        Vector3 targetPosition;

        if (ResourceManager.Fuel > 0 && !ResourceManager.delay && isMoving)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetMouseButton(0))
            {
                TargetController.busyTimer = 0.75f;

                Vector3 screenPosition = Input.mousePosition;
                screenPosition.z = 100;
                targetPosition = Input.mousePosition != null ? Camera.main.ScreenToWorldPoint(screenPosition) : Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                direction = targetPosition - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                targetRotation.z = targetRotation.x = 0;

                int playerLayerMask = 1 << 9;
                int EnemyLayerMask = 1 << 8;
                if (Physics.OverlapSphere(targetPosition, 4.5f, playerLayerMask | EnemyLayerMask).Length < 1)
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
                        EngContrl.ThrustEngineEffect(targetRotation.eulerAngles, transform.rotation.eulerAngles);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotatingSpeed * Time.deltaTime);
                        FindObjectOfType<ResourceManager>().Thrust();

                        EngContrl.MainEngineEffect(enginePower);
                        if (!movingInProgress) StartCoroutine(FullThrottle(targetPosition, distance));
                    }
            }
        }
        else if (!isAvoiding && isMoving)
        {
            ResourceManager.StartDelay();
        }
    }
    public IEnumerator FullThrottle(Vector3 targetPosition,float distance)
    {
        yield return new WaitForSeconds(0.35f);
        movingInProgress = true;
        for (int i = 0; i < distance; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, mobileSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
            movingInProgress = false;
        }
        
    }
}


