using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTrack : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 startPoint = transform.GetChild(0).position;
        Vector3 actualPoint = startPoint;

        Gizmos.color = Color.yellow;
        foreach (Transform waypoint in transform)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(actualPoint, waypoint.position);
            actualPoint = waypoint.position;
            Gizmos.color = Color.white;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(actualPoint, startPoint);
        Gizmos.color = Color.white;
    }
}
