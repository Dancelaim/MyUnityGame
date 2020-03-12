using System.Collections;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    public Transform shotPrefab;
    public float shootingRate;
    public float reloadTime;
    private float shootCooldown;
    public int Ammo;
    private int FireArms;
    private bool AttackStarted;
    void Update()
    {
        if (shootCooldown >= 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if(shootCooldown<0 && !AttackStarted) StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        AttackStarted = true;
        for (int i = 0; i < FireArms; i++)
        {
            var shotTransform = Instantiate(shotPrefab);
            Shot shot = shotTransform.gameObject.GetComponent<Shot>();
            shot.direction = this.transform.forward;
            shotTransform.position = transform.position;
            yield return new WaitForSeconds(shootingRate);
        }
        shootCooldown = reloadTime;
        FireArms = Ammo;
        AttackStarted = false;
    }
}

