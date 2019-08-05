using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform shotPrefab;
    public float shootingRate;
    public int Ammo;
    private int FireArms;
    private float ReloadTime;
    public float Reload;
    private bool AttackFinished;
    bool AttackStarted;

    private void Awake()
    {
        FireArms = Ammo;
        AttackFinished = true;
    }
    void Update()
    {
        if (ReloadTime > 0 && AttackFinished) ReloadTime -= Time.deltaTime;
        if (ReloadTime <= 0) FireArms = Ammo;
    }
    public void Attack()
    {
        if (ReloadTime <= 0 && !AttackStarted) StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {
        AttackStarted = true;
        for (int i = 0; i < FireArms; i++)
        {
                AttackFinished = false;
                var shotTransform = Instantiate(shotPrefab);
                shotTransform.position = transform.position + new Vector3(2.1f, -0.82f, 1);
                Move move = shotTransform.gameObject.GetComponent<Move>();
                if (move) move.direction = this.transform.right;
                yield return new WaitForSeconds(shootingRate);
        }
        ReloadTime = Reload;
        FireArms = Ammo;
        AttackFinished = true;
        AttackStarted = false;
    }
}

