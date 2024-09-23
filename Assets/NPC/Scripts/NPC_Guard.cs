using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Guard : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private List<Transform> pathTrack;
    [SerializeField] private float speed = 8;
    [SerializeField] private float turnSpeed = 90;
    [SerializeField] private float waitTime;   

    private void Start()
    {
        if (pathTrack != null)
        {
            Vector3[] waypoints = new Vector3[pathTrack.Count];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = pathTrack[i].position;
                waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
            }

            StartCoroutine(FollowPath(waypoints));
        }        
    }


    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int id = 1;
        Vector3 targetPoint = waypoints[id];
        transform.LookAt(targetPoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            if (transform.position == targetPoint)
            {
                id = (id + 1) % waypoints.Length;
                targetPoint = waypoints[id];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(Turn(targetPoint));
            }
            yield return null;
        }
    }

    IEnumerator Turn(Vector3 lookTarget)
    {
        Vector3 dirLook = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirLook.z, dirLook.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }
}
