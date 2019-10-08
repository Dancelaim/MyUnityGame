using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float paralax;
    public GameObject ship;
    Player playerScript;
    Vector2 oldDirection;
    Vector2 offsetDirection;
    private void LateUpdate()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        //TO DO : SMOOTH PARTICLE SYSTEM THAT WILL STOP WITH BACKGROUND.
        offset.x = Mathf.SmoothStep(offset.x, ship.transform.forward.x * paralax ,Time.deltaTime);
        offset.y = Mathf.SmoothStep(offset.y, ship.transform.forward.z * paralax , Time.deltaTime);

        mat.mainTextureOffset = offset;
    }
}


