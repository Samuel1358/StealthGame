using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Cleaner : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform pathTrack;
    private Vector3[] waypoints;
    //private int moveId = 0;

    [SerializeField] private float waitTime;
    private float wtt;

    private int rand;

    private void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        //radiusView = GetComponent<RadiusView>();

        //radiusView.SetRadius(angryDistance * 40);

        // Move 1
        if (pathTrack != null)
        {
            waypoints = new Vector3[pathTrack.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = pathTrack.GetChild(i).position;
                waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
            }
            transform.position = waypoints[0];
            //moveId = 1;
            rand = Random.Range(0, waypoints.Length);
            navMeshAgent.destination = waypoints[rand];
        }

        wtt = waitTime;
    }

    private void Update()
    {
        //Debug.Log(rand);
        if (Vector3.Distance(transform.position, waypoints[rand]) <= 0.05f)
        {
            if (waitTime <= 0)
            {
                rand = Random.Range(0, waypoints.Length);
                navMeshAgent.destination = waypoints[rand];
                waitTime = wtt;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
