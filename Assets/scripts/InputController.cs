using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SwipeManager))]
public class InputController : MonoBehaviour
{
    public Player Player;
    public int ForceToAdd;


    void Start()
    {
        SwipeManager swipeManager = GetComponent<SwipeManager>();
        swipeManager.onSwipe += HandleSwipe;
        swipeManager.onLongPress += HandleLongPress;

    }

    void HandleSwipe(SwipeAction swipeAction)
    {
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (Player != null)
        {
            switch (swipeAction.direction)
            {
                case SwipeDirection.Up:
                    Player.GetComponent<Rigidbody>().AddForce(new Vector3(ForceToAdd, 0, 0), ForceMode.Impulse);
                    break;
                case SwipeDirection.Right:
                    Player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -ForceToAdd), ForceMode.Impulse);
                    break;
                case SwipeDirection.Down:
                    Player.GetComponent<Rigidbody>().AddForce(new Vector3(-ForceToAdd, 0, 0), ForceMode.Impulse);
                    break;
                case SwipeDirection.Left:
                    Player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, ForceToAdd), ForceMode.Impulse);
                    break;
                case SwipeDirection.UpRight:
                    Player.GetComponent<Rigidbody>().AddForce(new Vector3(ForceToAdd, 0, -ForceToAdd), ForceMode.Impulse);
                    break;
                case SwipeDirection.DownRight:
                    Player.GetComponent<Rigidbody>().AddForce(new Vector3(-ForceToAdd, 0, -ForceToAdd), ForceMode.Impulse);
                    break;
                case SwipeDirection.DownLeft:
                    Player.GetComponent<Rigidbody>().AddForce(new Vector3(-ForceToAdd, 0, ForceToAdd), ForceMode.Impulse);
                    break;
                case SwipeDirection.UpLeft:
                    Player.GetComponent<Rigidbody>().AddForce(new Vector3(ForceToAdd, 0, ForceToAdd), ForceMode.Impulse);
                    break;
            }
        }
    }

    void HandleLongPress(SwipeAction swipeAction)
    {
    }
}