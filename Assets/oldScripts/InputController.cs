//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//[RequireComponent(typeof(SwipeManager))]
//public class InputController : MonoBehaviour
//{
//    public PlayerScript Player; // Perhaps your playerscript?


//    void Start()
//    {
//        SwipeManager swipeManager = GetComponent<SwipeManager>();
//        swipeManager.onSwipe += HandleSwipe;
//        swipeManager.onLongPress += HandleLongPress;

//    }

//    void HandleSwipe(SwipeAction swipeAction)
//    {
//        if (Player != null)
//        {
//            switch (swipeAction.direction)
//            {
//                case SwipeDirection.Up:
//                    break;
//                case SwipeDirection.Right:
//                    break;
//                case SwipeDirection.Down:
//                    break;
//                case SwipeDirection.Left:
//                    break;
//                case SwipeDirection.UpRight:
//                    break;
//                case SwipeDirection.DownRight:
//                    break;
//                case SwipeDirection.DownLeft:
//                    break;
//                case SwipeDirection.UpLeft:
//                    break;
//            }
//        }
//    }

//    void HandleLongPress(SwipeAction swipeAction)
//    {
//    }
//}