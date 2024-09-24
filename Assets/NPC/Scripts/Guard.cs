using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Guard : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [Header("Move")]
    [SerializeField] private Transform pathTrack;
    //[SerializeField] private float speed = 8;
    //[SerializeField] private float turnSpeed = 90;
    //[SerializeField] private float waitTime;

    private Vector3[] waypoints;
    private int id;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (pathTrack != null)
        {
            waypoints = new Vector3[pathTrack.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = pathTrack.GetChild(i).position;
                waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
            }
            transform.position = waypoints[0];
            id = 1;
        }
    }

    private void Update()
    {
        FollowPath();
    }

    public void FollowPath()
    {
        Vector3 targetPoint = waypoints[id];

        while (true)
        {
            navMeshAgent.destination = targetPoint;
            if (Vector3.Distance(transform.position, targetPoint) <= 0.05f)
            {
                
                if (id == waypoints.Length - 1)
                {
                    id = 0;
                }
                else
                {
                    id ++;
                }
            }
            break;
        }
    }
}

