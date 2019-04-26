using UnityEngine;
using System.Collections.Generic;

public class Spawn : MonoBehaviour
{
    public GameObject[] Objects;
    private Vector2 position;
    public Camera cam;
    public int maxEnemies;
    public int maxWreckages;

    void Update()
    {
        int EnemyCount = GameObject.FindGameObjectsWithTag("Alien").Length;
        int WreckageCount = GameObject.FindGameObjectsWithTag("Wreckage").Length;
        int totalObjectsOnScene = EnemyCount + WreckageCount;

        if (totalObjectsOnScene < (maxWreckages + maxEnemies)) SpawnObject(EnemyCount, WreckageCount);
    }

    void SpawnObject(int EnCount, int WreCount)
    {
        position = new Vector2(cam.orthographicSize * cam.aspect + Random.Range(3, 20), Random.Range(-9, 9));
        

        if (!Physics2D.OverlapCircle(position,3))
        {
            if (EnCount < maxEnemies)
            {
                Instantiate(Objects[0], position, transform.rotation);
            }
            else if (WreCount < maxWreckages)
            {
                Instantiate(Objects[1], position, transform.rotation);
            }
        }
        
    }
}