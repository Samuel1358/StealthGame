using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(RadiusView), typeof(SpotView))]
public class Gatekeeper : MonoBehaviour
{
    [SerializeField] private List<int> rotine;
    private int rotineId = 0;
    [SerializeField] private int action = 0;

    [SerializeField] private float radiusViewDistance;
    private RadiusView radiusView;
    private SpotView spotView;

    [SerializeField] private float toleranceTime;
    private float toltt;

    private NavMeshAgent navMeshAgent;

    [Header("Move 1 [1]")]
    [SerializeField] private Transform pathTrack1;
    private Vector3[] waypoints1;
    private int moveId1 = 0;

    [Header("Move 2 [2]")]
    [SerializeField] private Transform pathTrack2;
    private Vector3[] waypoints2;
    private int moveId2 = 0;

    [Header("Wait [3]")]
    [SerializeField] private float waitTime;
    private float wtt;

    [Header("Drowse [4]")]
    [SerializeField] private float drowseTime;
    private float dtt;
    [SerializeField] private float drowseSpeed;

    // Player
    private Player player;

    private void Start()
    {
        action = 0;

        SetAction(rotine[rotineId]);

        navMeshAgent = GetComponent<NavMeshAgent>();
        radiusView = GetComponent<RadiusView>();
        spotView = GetComponent<SpotView>();

        radiusView.SetRadius(radiusViewDistance * 40);

        // Move 1
        if (pathTrack1 != null)
        {
            waypoints1 = new Vector3[pathTrack1.childCount];
            for (int i = 0; i < waypoints1.Length; i++)
            {
                waypoints1[i] = pathTrack1.GetChild(i).position;
                waypoints1[i] = new Vector3(waypoints1[i].x, transform.position.y, waypoints1[i].z);
            }
            transform.position = waypoints1[0];
            moveId1 = 1;
        }

        // Move 2
        if (pathTrack2 != null)
        {
            waypoints2 = new Vector3[pathTrack2.childCount];
            for (int i = 0; i < waypoints2.Length; i++)
            {
                waypoints2[i] = pathTrack2.GetChild(i).position;
                waypoints2[i] = new Vector3(waypoints2[i].x, transform.position.y, waypoints2[i].z);
            }
        }

        // Wait
        wtt = waitTime;

        //Player
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        switch (action)
        {
            case 1:
                FollowPath1();
                break;
            case 2:
                FollowPath2();
                break;
            case 3:
                Wait();
                break;
            case 4:
                spotView.enabled = false;
                spotView.spotLight.enabled = false;
                Drowse();
                break;
        }

        if (action != 4)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= radiusViewDistance)
            {
                radiusView.ChangeColor(Color.red);

                if (toleranceTime <= 0)
                {
                    // DERROTA
                    MenuOptions.LoadScene("Title");
                }
                else
                {
                    toleranceTime -= Time.deltaTime;
                }
            }
            else if (toleranceTime < toltt)
            {
                toleranceTime += Time.deltaTime;
            }
        }
    }

    private void SetAction(int value)
    {
        this.action = value;
    }

    private void NextRotine()
    {
        if (rotineId == rotine.Count - 1)
        {
            rotineId = 0;
        }
        else
        {
            rotineId++;
        }

        SetAction(rotine[rotineId]);
    }

    private void FollowPath1()
    {
        Vector3 targetPoint = waypoints1[moveId1];

        while (true)
        {
            navMeshAgent.destination = targetPoint;
            if (Vector3.Distance(transform.position, targetPoint) <= 0.05f)
            {

                if (moveId1 == waypoints1.Length - 1)
                {
                    moveId1 = 0;
                    NextRotine();
                }
                else
                {
                    moveId1++;
                }
            }
            break;
        }
    }

    private void FollowPath2()
    {
        Vector3 targetPoint = waypoints2[moveId2];

        while (true)
        {
            navMeshAgent.destination = targetPoint;
            if (Vector3.Distance(transform.position, targetPoint) <= 0.05f)
            {

                if (moveId2 == waypoints2.Length - 1)
                {
                    moveId2 = 0;
                    NextRotine();
                }
                else
                {
                    moveId2++;
                }
            }
            break;
        }
    }

    private void Wait()
    {
        if (waitTime <= 0)
        {
            NextRotine();
            waitTime = wtt;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void Drowse()
    {
        if (drowseTime <= 0)
        {
            StartCoroutine(WakeUp());

            spotView.enabled = true;
            spotView.spotLight.enabled = true;
            NextRotine();
            drowseTime = wtt;
        }
        else
        {
            if (radiusView.radiusLight.spotAngle > 40)
            {
                radiusView.SetRadius(radiusView.radiusLight.spotAngle - (drowseSpeed * Time.fixedDeltaTime));
            }

            drowseTime -= Time.deltaTime;
        }
    }

    IEnumerator WakeUp()
    {
        while (true)
        {
            if (radiusView.radiusLight.spotAngle < radiusViewDistance * 40)
            {
                radiusView.SetRadius(radiusView.radiusLight.spotAngle + (1 * Time.fixedDeltaTime));
            }
            else
            {
                yield return null;
            }
        }
    }
}
