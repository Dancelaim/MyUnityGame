//using UnityEngine;
//using System.Collections;

//public class CameraControll : MonoBehaviour
//{

//    public GameObject player;

//    public Vector3 offset;
//    public float speed = 0.125f;
//    void LateUpdate()
//    {
//        Vector3 targetPosition = player.transform.position + offset;
//        targetPosition.y = 0;
//        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
//    }
//}
