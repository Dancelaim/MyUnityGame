using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float paralax;
    private void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;

        offset.x = transform.position.x  / paralax ;
        offset.y = transform.position.z  / paralax;

        mat.mainTextureOffset = offset;

    }

}


