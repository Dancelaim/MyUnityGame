﻿using UnityEngine;

public class Wreckage : MonoBehaviour
{
    private bool hasSpawn;
    private Move moveScript;
    private Collider2D coliderComponent;
    private SpriteRenderer rendererComponent;

    void Awake()
    {

        
        moveScript = GetComponent<Move>();

        coliderComponent = GetComponent<Collider2D>();

        rendererComponent = GetComponent<SpriteRenderer>();
    }

   
    void Start()
    {
        hasSpawn = false;

       
        coliderComponent.enabled = false;
        
        moveScript.enabled = false;
    }

    void Update()
    {
        
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
            if (!rendererComponent.IsVisibleFrom(Camera.main))
            {
                Destroy(gameObject);
            }
        }
    }

   
    private void Spawn()
    {
        hasSpawn = true;

       
        coliderComponent.enabled = true;
        
        moveScript.enabled = true;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Shot shot = collision.gameObject.GetComponent<Shot>();
        if (shot != null)
        {
            moveScript.direction = new Vector2(1,Random.Range(-3.0f, 3.0f));
        }
    }

}