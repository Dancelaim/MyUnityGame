using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public static Vector3 targetPos;
    static bool IsLocked;
    EngineController EngContrl;
    bool isBusy;
    public static float busyTimer = 1f;
    private void Start()
    {
        IsLocked = false;
        EngContrl = GetComponent<EngineController>();
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
                LockTarget(hit.collider.transform.position);
            }
        }
    }
    public static void LockTarget(Vector3 NewTarget)
    {
        targetPos = NewTarget;

        if (targetPos != null)
        {
            IsLocked = true;
        }
        else
        {
            IsLocked = false;
        }
    }
    public void LeadTarget()
    {
        if (!isBusy)
        {
            Vector3 direction = targetPos - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            EngContrl.ThrustEngineEffect(targetRotation.eulerAngles, transform.rotation.eulerAngles);
            FindObjectOfType<ResourceManager>().Thrust();

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 300 * Time.deltaTime);
        }
    }
}
