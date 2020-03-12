using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private EnemyPirate owner;
    private Task currentTask;
    private Collider target;
    private int rotatingSpeed;
    private int angleSpeed;
    private bool isAimed;
    private Health playerHealth;

    public AI(EnemyPirate owner, int rotatingSpeed, int angleSpeed)
    {
        this.owner = owner;
        this.rotatingSpeed = rotatingSpeed;
        this.angleSpeed = angleSpeed;
        currentTask = Task.Searching;

    }
    public EnemyPirate Owner
    { set { owner = value; } }
    public int RotatingSpeed
    { set { rotatingSpeed = value; } }
    public Task CurrentTask
    { set { currentTask = value; } }
    public void GetTask()
    {
        switch ((int)currentTask)
        {
            case 1:
                AcquireTarget();
                break;
            case 2:
                Attack();
                break;
            case 3:
                Avoid();
                break;
            default:
                Idle();
                break;
        }
    }
    private void AcquireTarget()
    {
        int playerLayerMask = 1 << 9;
        Collider[] targets = Physics.OverlapSphere(owner.transform.position, 50, playerLayerMask);
        if (targets.Length > 0)
        {
            target = targets[0];
        }
        playerHealth = target.GetComponent<Health>();
        currentTask = Task.Attacking;
    }
    private void Attack()
    {
        int layerMask = 1 << 9;
        RaycastHit hit;
        Physics.Raycast(owner.transform.position, owner.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask);

        if (!hit.collider)
        {
            Aim();
        }
        else
        {
            owner.Attack();
        }
        
        

    }
    private void Avoid()
    {

    }
    private void Idle()
    {
       // //Implement after Player stealth implementation
    }

    public void Aim()
    {
        Vector3 targetdirection;
        ParticleSystem leftEngEffect = GameObject.Find("LeftThrust").GetComponentInChildren<ParticleSystem>();
        ParticleSystem rightEngEffect = GameObject.Find("RightThurst").GetComponentInChildren<ParticleSystem>();

        targetdirection = target.transform.position - owner.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetdirection);

        Quaternion direction = Quaternion.RotateTowards(owner.transform.rotation, targetRotation, rotatingSpeed * Time.deltaTime);

        if (!leftEngEffect.IsAlive() && !rightEngEffect.IsAlive())
        {
            owner.RotationThrustEffect(targetRotation.eulerAngles, owner.transform.rotation.eulerAngles);

        }

        if (direction != owner.transform.rotation && leftEngEffect.time > 0.2f || rightEngEffect.time > 0.2f)
        {
            owner.transform.rotation = direction;
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, target.transform.position, angleSpeed * Time.deltaTime);
        }

    }

    public enum Task
    {
        Idle = 0
        , Searching = 1
        , Attacking = 2
        , Avoiding = 3
    }
}

