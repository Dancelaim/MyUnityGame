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
    private Rigidbody2D rigidbodyComponent;
    public bool CanMove;
    Vector3 startPosition;
    public float Temp = 0;
    public float burnRate = 5;
    public float restoreRate = 5;
    bool delay = false;
    float dist;
    float leftBorder;
    float rightBorder;
    float topBorder;
    float bottomBorder;


    public void Awake()
    {
        startPosition = transform.position;
        dist = (transform.position - Camera.main.transform.position).z;
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0f, dist)).x;
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0f, dist)).x;
        topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.1f, dist)).y;
        bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1f, dist)).y;
    }

    void Update()
    {
        int layerMask = 1 << 8;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-2f, 0, 1), new Vector3(1, 0, 0), Mathf.Infinity, layerMask);

        if (hit.collider != null && hit.collider.enabled && GetComponentInChildren<Weapon>().enabled)
        {
            GetComponentInChildren<Weapon>().Attack(false);
            SoundEffectsHelper.Instance.MakePlayerShotSound();
        }


        //if (Input.GetButton("Fire1")) 


        var HpCounter = FindObjectOfType<ResourceManager>();
        Health playerHealth = GetComponent<Health>();
        HpCounter.HpBarSchema(playerHealth.hp);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftBorder, rightBorder), Mathf.Clamp(transform.position.y, topBorder, bottomBorder));
    }
    void FixedUpdate()
    {
        PlayerMove();
    }

    void OnDestroy()
    {
        GameObject.Find("MainCanvas").GetComponent<UIInteractions>().GameOver();
    }

    public void PlayerMove()
    {
        Vector3 targetPosition = transform.position;

        RestoreTemperature();

        if (Temp <= 100 && !delay)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButton(0))
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) != null ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
        }
        else StartDelay();


        if (9.2f > targetPosition.y && targetPosition.y > -9)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, 0), mobileSpeed * Time.deltaTime);
            if(!Physics2D.OverlapCircle(targetPosition, 0.0001f))Heating();
        }
        


    }

    private void StartDelay()
    {
        var counter = FindObjectOfType<ResourceManager>();
        if (Temp >= 15)
        {
            delay = true;
            EmergencyTemperatureRestore();
        }
        else delay = false;

        counter.TemperatureBarWarning(ResourceManager.TempStatus.High);
    }

    private void EmergencyTemperatureRestore()
    {
        if (Temp < 105 && Temp > 0)
            Temp -= 20 * Time.deltaTime;

        FindObjectOfType<ResourceManager>().TempRemainsCounter(Temp);
    }

    public void RestoreTemperature()
    {
        var counter = FindObjectOfType<ResourceManager>();

        if (Temp < 105 && Temp > 0)
            Temp -= restoreRate * Time.deltaTime;

        if (Temp < 35)
            counter.TemperatureBarWarning(ResourceManager.TempStatus.Normal);

        counter.TempRemainsCounter(Temp);

    }

    public void Heating()
    {
        var counter = FindObjectOfType<ResourceManager>();

        Temp += burnRate * Time.deltaTime;

        if (Temp > 65) counter.TemperatureBarWarning(ResourceManager.TempStatus.Warning);

        counter.TempRemainsCounter(Temp);

    }
}


