using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public Transform shotPrefab;


    public float shootingRate = 0.25f;

    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }
    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            
            var shotTransform = Instantiate(shotPrefab) as Transform;

            
            if (isEnemy == true) {

                var pos = transform.position + new Vector3(-1.2f, 0, 1);

                shotTransform.position = pos;
            }
            else
            {
                var pos = transform.position + new Vector3(2.1f, -1.1f, 1);

                shotTransform.position = pos;
            }

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right; 
            }
        }
    }
    
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
