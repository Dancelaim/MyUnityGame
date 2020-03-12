using UnityEngine;


public class EnemyPirate : MonoBehaviour
{
    public bool hasSpawn;
    private AI ai;
    public int rotatingSpeed;
    EngineController EngContrl;
    public int angleSpeed;
    public int rotationSpeed;
    private void Start()
    {
        EngContrl = GetComponent<EngineController>();
    }


    void Awake()
    {
        ai = new AI(this, rotatingSpeed, angleSpeed);
        ai.GetTask();

    }
    //TO DO : LASER CANON SHOT 
    private void Update()
    {
        ai.GetTask();
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime, Space.Self);
    }

    public void RotationThrustEffect(Vector3 targetRotation, Vector3 playerRotation)
    {
        EngContrl.ThrustEngineEffect(targetRotation, playerRotation);
    }

    public void Attack()
    {
        EnemyWeapon[] weapons = GetComponentsInChildren<EnemyWeapon>();
        foreach (EnemyWeapon weapon in weapons)
        {
            weapon.Attack();
        }
    }

}