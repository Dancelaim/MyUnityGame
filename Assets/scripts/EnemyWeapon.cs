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
    ParticleSystem animation;

    private void Start()
    {
       animation = GetComponentInChildren<ParticleSystem>();
    }
    void Update()
    {
        if (shootCooldown >= 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        if (!AttackStarted) animation.Stop();
    }
    public void Attack()
    {
        if (shootCooldown<0 && !AttackStarted) StartCoroutine(Fire());
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
            animation.Play();
            yield return new WaitForSeconds(shootingRate);
        }
        shootCooldown = reloadTime;
        FireArms = Ammo;
        AttackStarted = false;
    }
}

