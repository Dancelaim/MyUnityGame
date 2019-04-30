using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp = 1;
    public bool isEnemy;
    public bool isWreckage;
    public bool isPlayer;
    public int reward = 100;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Shot shot = otherCollider.gameObject.GetComponent<Shot>();
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
        Enemy enemy = isEnemy ? this.GetComponent<Enemy>() : collision != null ? collision.gameObject.GetComponent<Enemy>() : null;
        Health enemyHealth = enemy ? enemy.GetComponent<Health>() : null;
   
        Player player = isPlayer ? this.GetComponent<Player>() : collision != null ? collision.gameObject.GetComponent<Player>() : null;
        Health playerHealth = player ? player.GetComponent<Health>() : null;

        Wreckage wreckage = isWreckage ? this.GetComponent<Wreckage>() : collision != null ? collision.gameObject.GetComponent<Wreckage>() : null;
        Health wreckageHealth = wreckage ? wreckage.GetComponent<Health>() : null;

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