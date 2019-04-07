using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int hp = 1;
    public bool isEnemy;
    public bool isWreckage;
    public bool isPlayer;
    public int reward = 100;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            if (shot.isEnemyShot && isPlayer)
            {
                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
            else if(!shot.isEnemyShot && isEnemy)
            {
                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyScript enemy = isEnemy ? this.GetComponent<EnemyScript>() : collision != null ? collision.gameObject.GetComponent<EnemyScript>() : null;
        HealthScript enemyHealth = enemy ? enemy.GetComponent<HealthScript>() : null;
   
        PlayerScript player = isPlayer ? this.GetComponent<PlayerScript>() : collision != null ? collision.gameObject.GetComponent<PlayerScript>() : null;
        HealthScript playerHealth = player ? player.GetComponent<HealthScript>() : null;

        WreckageScript wreckage = isWreckage ? this.GetComponent<WreckageScript>() : collision != null ? collision.gameObject.GetComponent<WreckageScript>() : null;
        HealthScript wreckageHealth = wreckage ? wreckage.GetComponent<HealthScript>() : null;

        var HpCounter = FindObjectOfType<ResourceManager>();

        string collisionType = enemy & player ? "playerEnemy" : player & wreckage ? "playerWreckage" : wreckage & enemy ? "enemyWreckage" : null;

        if (collisionType != null)
        {
            switch (collisionType)
            {
                case "enemyWreckage":
                    enemyHealth.Damage(wreckageHealth.hp);
                    break;
                case "playerWreckage":
                    playerHealth.Damage(wreckageHealth.hp);
                    break;
                case "playerEnemy":
                    if (enemyHealth.hp < 0) break;
                    var dmg = enemyHealth.hp;
                    enemyHealth.Damage(playerHealth.hp);
                    playerHealth.Damage(dmg);
                    break;
            }
        }
    }

    public void Damage(int damageCount)
    {
        hp -= Mathf.Abs(damageCount);

        if (hp <= 0)
        {
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            SoundEffectsHelper.Instance.MakeExplosionSound();
            Destroy(gameObject);
            if (gameObject.tag == "Alien")
            {
                var counter = FindObjectOfType<ResourceManager>();
                if (counter)
                {
                    counter.ScoreCounter(reward);
                }
            }
        }
    }


}