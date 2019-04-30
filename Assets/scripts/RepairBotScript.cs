using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairBotScript : MonoBehaviour
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
        HealthScript HpScript = MotherBoard.GetComponent<HealthScript>();
        PlayerScript PlScript = MotherBoard.GetComponent<PlayerScript>();
        if (HpScript.hp < 5)
        {
            int MissedHp = 5 - HpScript.hp;
            if (LifeTime > 4)
            {
                StartCoroutine(HpRestoration(MissedHp, HpScript));
            }
        }
        PlScript.RestoreTemperature();

    }

    IEnumerator HpRestoration(int MissedHp, HealthScript HpScript)
    {
        for (int hp = 0; hp < MissedHp; hp++)
        {
            HpScript.hp += 1;
            yield return new WaitForSeconds(2f);
        }
    }
    
}
