using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairBot : MonoBehaviour
{
    public GameObject MotherBoard;
    float LifeTime;

    private void Awake()
    {
        LifeTime = 10;
    }

    void Update()
    {
        LifeTime -= Time.deltaTime;
        transform.RotateAround(MotherBoard.transform.position, new Vector3(0,0,1), 100 * Time.deltaTime);
        Restore();
        if (LifeTime < 0) Destroy(gameObject);
    }

    void Restore()
    {
        Health Hp = MotherBoard.GetComponent<Health>();
        Player Pl = MotherBoard.GetComponent<Player>();
        if (Hp.hp < 5)
        {
            int MissedHp = 5 - Hp.hp;
            if (LifeTime > 4)
            {
                StartCoroutine(HpRestoration(MissedHp, Hp));
            }
        }
        Pl.RestoreTemperature();

    }

    IEnumerator HpRestoration(int MissedHp, Health Hp)
    {
        for (int hp = 0; hp < MissedHp; hp++)
        {
            Hp.hp += 1;
            yield return new WaitForSeconds(2f);
        }
    }
    
}
