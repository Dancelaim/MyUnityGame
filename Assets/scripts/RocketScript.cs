using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

    public Transform RocketPrefab;

    public void AutoAim(Vector3 initialPosition, Collider2D Enemy)
    {
        StartCoroutine(AutoAim(initialPosition, 50, Enemy));
    }

    IEnumerator AutoAim(Vector3 initialPosition, float speed, Collider2D Enemy)
    {
        var shotTransform = Instantiate(RocketPrefab) as Transform;
        shotTransform.position = initialPosition;
        float timeToReachTargetPosition = Vector3.Distance(initialPosition, Enemy.transform.position) / speed;

        float time = 0.0f;
        while (time < 1)
        {
            time += Time.deltaTime / timeToReachTargetPosition;
            
            if (Enemy && shotTransform)
            {
                Vector3 dir = Enemy.transform.position - shotTransform.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                shotTransform.transform.position = Vector3.Lerp(initialPosition, Enemy.transform.position, time);
            }

            yield return null;
        }
    }
    
}
