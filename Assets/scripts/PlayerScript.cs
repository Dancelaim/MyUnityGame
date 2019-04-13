using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(50, 50);
    public float mobileSpeed;
    private WeaponScript[] weapons;
    public bool damagePlayer;
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    public bool CanMove;
    Vector3 startPosition;
    float Temp = 0;
    public float burnRate = 5;
    public float restoreRate = 5;
    bool delay = false;


    public void Awake()
    {
        startPosition = transform.position;

    }

    void Update()
    {
        bool shoot = false;
       
        if (Input.GetButton("Fire1")) shoot = true;

        var HpCounter = FindObjectOfType<ResourceManager>();
        HealthScript playerHealth = GetComponent<HealthScript>();
        HpCounter.HpBarSchema(playerHealth.hp);

        if (shoot)
        {
            weapons = GetComponentsInChildren<WeaponScript>();

            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.CanAttack)
                {
                    weapon.Attack(false);
                    SoundEffectsHelper.Instance.MakePlayerShotSound();
                }
            }

        }

        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0f, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.95f, 0f, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.05f, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.95f, dist)).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );

    }
    void FixedUpdate()
    {
        PlayerMove();
    }

    void OnDestroy()
    {
        var gameOver = FindObjectOfType<UIInteractions>();
        gameOver.ShowButtons();
    }

    public void PlayerMove()
    {
        Vector3 targetPosition = transform.position;

        RestoringTemperature();

        if (Temp <= 100 && !delay)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButton(0))
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) != null ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RemainingFuel();
            }
        }
        else StartDelay();


        if (targetPosition != transform.position && !Physics2D.OverlapCircle(targetPosition, 0.05f) || !EventSystem.current.IsPointerOverGameObject())
        {
            this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, 0), mobileSpeed * Time.deltaTime);
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

        counter.TemperatureBarWarning(delay);
    }

    private void EmergencyTemperatureRestore()
    {
        if (Temp < 105 && Temp > 0)
            Temp -= 20 * Time.deltaTime;

        var counter = FindObjectOfType<ResourceManager>();
        if (counter)
        {
            counter.TempRemainsCounter(Temp);
        }
    }

    public void RestoringTemperature()
    {
        if(Temp < 105 && Temp > 0)
            Temp -= restoreRate * Time.deltaTime;

        var counter = FindObjectOfType<ResourceManager>();
        if (counter)
        {
            counter.TempRemainsCounter(Temp);
        }
    }

    public void RemainingFuel()
    {
        
        Temp += burnRate * Time.deltaTime;

        var counter = FindObjectOfType<ResourceManager>();
        if (counter)
        {
            counter.TempRemainsCounter(Temp);
        }
    }
}



