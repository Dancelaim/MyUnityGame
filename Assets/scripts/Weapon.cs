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
    public GameObject ship;
    public float particleExecutionTime;
    public ParticleSystem LaserEffect;

    private void Awake()
    {
        FireArms = Ammo;
        AttackFinished = true;
        ReloadTime = Reload;
    }
    void Update()
    {
        if (ReloadTime > 0 ) ReloadTime -= Time.deltaTime;

        int layerMask = 1 << 8;
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask);
        if (hit.collider != null && hit.collider.enabled && GetComponentInChildren<Weapon>().enabled)
        {
            Attack();
        }
    }
    public void Attack()
    { 
        if (ReloadTime <= 0)
        {
            StartCoroutine(Fire());
            ReloadTime = Reload;
        }
    }
    IEnumerator Fire()
    {
        LaserEffect.Play();
        yield return new WaitForSeconds(particleExecutionTime);
        var shotTransform = Instantiate(shotPrefab);
        Shot shot = shotTransform.gameObject.GetComponent<Shot>();
        shot.transform.rotation = this.transform.rotation;
        shot.direction = this.transform.forward;
        shotTransform.position = this.transform.position;
        SoundEffectsHelper.Instance.MakePlayerShotSound();
        LaserEffect.Stop();
        ReloadTime = Reload;
    }

}

