using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTest : MonoBehaviour
{
    public GameObject target;

    public float radius;
    [Range(0, 180)]
    public float angleLimit;

    public float targetAngle;

    public float direction;

    private void Update()
    {
        Vector3 dirToTarget = (target.transform.position - this.transform.position).normalized;
        targetAngle = Vector3.Angle(this.transform.forward, dirToTarget);

        if(targetAngle > angleLimit)
        {
            Debug.Log("Out of bounds!");
        }

        Vector3 localDirToTarget = (target.transform.position - this.transform.forward).normalized;
        direction = localDirToTarget.x;
    }

    public Vector3 dirFromAngle(float angleInDegrees)
    {
        angleInDegrees += transform.eulerAngles.y;
        float radians = angleInDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, this.transform.forward*5f);
    }
}
