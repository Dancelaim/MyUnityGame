using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SwipeController : MonoBehaviour
{

    public float MAX_SWIPE_TIME = 0.5f;
    public const float MIN_SWIPE_DISTANCE = 0.17f;
    private string swipeAction;
    public GameObject Ship;
    private Player player;
    public bool debugWithArrowKeys = true;
    Vector2 startPos;
    float startTime;
    private static GameObject localShip;
    public Camera Camera;
    private Vector3 screenBounds;
    private float objectWidth;
    private float objectHeight;
    bool notEnoughFuel = false;
    public float avoidDistance;
    static float localAvoidDistance;
    private void Start()
    {
        localAvoidDistance = avoidDistance;
        player = Ship.GetComponent<Player>();
        localShip = Ship;
        screenBounds = Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 100));
        objectWidth = transform.GetComponentInChildren<BoxCollider>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponentInChildren<BoxCollider>().bounds.extents.z; //extents = size of height / 2
        
    }
    public void Update()
    {
        EngineController EngContrl = GetComponent<EngineController>();
        var touches = InputHelper.GetTouches();
        if (touches.Count > 0)
        {
            swipeAction = null;
            Touch t = touches[0];

            if (t.phase == TouchPhase.Began)
            {
                startPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
                startTime = Time.time;
            }
            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Stationary)
            {
                if (Time.time - startTime > MAX_SWIPE_TIME)
                {
                    player.isMoving = true;
                }
                else
                {
                    player.isMoving = false;
                    Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

                    Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

                    if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
                        return;

                    if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                    {
                        if (swipe.x > 0)
                        {
                            swipeAction = "swipedRight";
                        }
                        else
                        {
                            swipeAction = "swipedLeft";
                        }
                    }
                    //else
                    //{
                    //    if (swipe.y > 0)
                    //    {
                    //        swipeAction = "swipedUp";
                    //    }
                    //    else
                    //    {
                    //        swipeAction = "swipedDown";
                    //    }
                    //}
                }
                
            }
        }

        if (swipeAction != null && ResourceManager.Fuel > 10 && !ResourceManager.delay)
        {
            player.isAvoiding = true;
            StartCoroutine(ResourceManager.AvoidThrust(10));
            StopCoroutine(Avoid(1));
            switch (swipeAction)
            {
                case "swipedRight":
                    StartCoroutine(Avoid(localAvoidDistance));
                    EngContrl.AvoidEffect(true);
                    swipeAction = null;
                    break;
                case "swipedLeft":
                    StartCoroutine(Avoid(-localAvoidDistance));
                    EngContrl.AvoidEffect(false);
                    swipeAction = null;
                    break;
                //case "swipedUp":
                //    StartCoroutine(Avoid(0,Distance));
                //    swipeAction = null;
                //    break;
                //case "swipedDown":
                //    StartCoroutine(Avoid(0,-Distance));
                //    swipeAction = null;
                //    break;
            }
        }
        else if(ResourceManager.Fuel <= 10)
        { //TO DO : Show warning Message or Alert picture of insufficient fuel
            ResourceManager.StartDelay();
        }
    }
    public static IEnumerator Avoid(float Horizontal)
    {
        for (int i = 0; i < localAvoidDistance; i++)
        {
            if (Horizontal > 0)
            {
                localShip.transform.position = Vector3.Lerp(localShip.transform.position, localShip.transform.right * Horizontal, Time.deltaTime);
            }
            else
            {
                localShip.transform.position = Vector3.Lerp(localShip.transform.position, localShip.transform.right * Horizontal, Time.deltaTime);

            }
            yield return new WaitForSeconds(0.01f);
        }


        localShip.GetComponent<Player>().isAvoiding = false;
    }
    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectHeight, screenBounds.x - objectHeight);
        viewPos.y = Mathf.Clamp(viewPos.z, 0 - objectWidth, screenBounds.z - objectWidth);

        viewPos.z = viewPos.y;
        viewPos.y = -100;
        transform.position = viewPos;
    }
}