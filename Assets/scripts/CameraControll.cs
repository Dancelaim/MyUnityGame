using UnityEngine;
using System.Collections;

public class CameraControll : MonoBehaviour
{

    public GameObject player;      

    private Vector3 offset;         

    
    void Start()
    {
       
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 fixedPosition = player.transform.position + offset;
        fixedPosition.y = 50;
        transform.position = fixedPosition;
    }
}
