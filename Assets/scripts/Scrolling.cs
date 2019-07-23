using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Scrolling : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    public bool isLinkedToCamera = false;
    public bool isLooping = false;
    private List<SpriteRenderer> backgroundPart;
    public GameObject SpaceShip;
    Vector3 Movespeed = new Vector3(0, 0, 0);
    bool RoutineStarted = false;
    void Start()
    {
       if (isLooping)
        {           
            backgroundPart = new List<SpriteRenderer>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();                
                if (r != null)
                {
                    backgroundPart.Add(r);
                }
            }            
            backgroundPart = backgroundPart.OrderBy(
              t => t.transform.position.x
            ).ToList();
        }
    }

    void Update()
    {
        if (SpaceShip.transform.position.x >= Camera.main.ViewportToWorldPoint(new Vector3(0.15f, 0f, 0f)).x)
        {
            Scroll(false);
        }
        else
        {
            Scroll(true);
        }
    }

    public void Scroll(bool inertion)
    {
        if(!RoutineStarted && inertion)
        {
            RoutineStarted = true;
            StartCoroutine(SmoothScrollRoutine(-1));
        }
        else if (!RoutineStarted && Movespeed.x < speed.x)
        {
            RoutineStarted = true;
            StartCoroutine(SmoothScrollRoutine(1));
        }

           Vector3 movement = new Vector3(
              Movespeed.x * direction.x,
              Movespeed.y * direction.y,
              0);

        movement *= Time.deltaTime;
        transform.Translate(movement);


        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }


        if (isLooping)
        {
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();
            if (firstChild != null)
            {
                if (firstChild.transform.position.x < Camera.main.transform.position.x)
                {
                    if (firstChild.IsVisibleFrom(Camera.main) == false)
                    {
                        SpriteRenderer lastChild = backgroundPart.LastOrDefault();
                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);
                        firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y, firstChild.transform.position.z);
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
    }

    IEnumerator SmoothScrollRoutine(int inertion)
    {
        if (inertion > 0)
        {
            while (speed.x > Movespeed.x) 
            {
                Movespeed.x += 0.5f;
                yield return new WaitForSeconds(0.05f);
            }
           
        }
        else
        {
            while (Movespeed.x > 0) 
            {
                Movespeed.x -= 0.5f;
                yield return new WaitForSeconds(0.1f);
            };
            
        }
        RoutineStarted = false;
    }

}

