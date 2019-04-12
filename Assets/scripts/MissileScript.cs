using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour {

    public Transform RocketPrefab;

    public void AutoAim(Vector3 initialPosition, Collider2D Enemy)
    {
        var shotTransform = Instantiate(RocketPrefab) as Transform;
        shotTransform.position = initialPosition;
        StartCoroutine(AutoAim( 50, Enemy, shotTransform));
    }

    IEnumerator AutoAim( float speed, Collider2D Enemy, Transform shotTransform)
    {
        yield return new WaitForSeconds(0.5f);

        float timeToReachTargetPosition = Enemy ? Vector3.Distance(shotTransform.position, Enemy.transform.position) / speed : 1; 
        

        float time = 0.0f;
        while (time < 1)
        {
            time += Time.deltaTime / timeToReachTargetPosition;

            if (Enemy && shotTransform)
            {
                shotTransform.transform.right = Enemy.transform.position - shotTransform.transform.position;
                shotTransform.transform.position = Vector3.Lerp(shotTransform.position, Enemy.transform.position, time);
            }
            yield return null;
        }
    }
}
