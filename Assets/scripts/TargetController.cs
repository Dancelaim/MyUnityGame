using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public static Collider Target;
    static bool IsLocked;
    EngineController EngContrl;
    bool isBusy;
    public static float busyTimer = 1f;
    private void Start()
    {
        IsLocked = false;
        EngContrl = GetComponent<EngineController>();
    }

    public static void LockTarget(Collider NewTarget)
    {
        Target = NewTarget;

        if (Target != null)
        {
            IsLocked = true;
        }
        else
        {
            IsLocked = false;
        }
    }
    private void Update()
    {
        if (busyTimer > 0)
        {
            isBusy = true;
            busyTimer -= Time.deltaTime;
        }
        else
        {
            isBusy = false;
        }

        if (IsLocked) LeadTarget();

        if (Input.GetMouseButton(0))
        {
            Ray ray;
            RaycastHit hit;
            int enemyLayerMask = 1 << 8;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayerMask))
            {
                LockTarget(hit.collider);
            }
        }
    }
    public void LeadTarget()
    {
        if (!isBusy)
        {
            var direction = Target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.z = targetRotation.x = 0;

            EngContrl.ThrustEngineEffect(targetRotation.eulerAngles, transform.rotation.eulerAngles);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 300 * Time.deltaTime);
            FindObjectOfType<ResourceManager>().Thrust();
        }
        
    }
}
