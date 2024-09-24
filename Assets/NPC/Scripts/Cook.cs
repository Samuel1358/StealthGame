using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(RadiusView))]
public class Cook : MonoBehaviour
{
    [SerializeField] private List<int> rotine;
    private int rotineId = 0;
    [SerializeField] private int action = 0;

    [Tooltip("Velocidade enquanto segue a rotina normalmente")]
    [SerializeField] private int normalSpeed = 5;
    [Tooltip("Velocidade enquanto persegue o jogador")]
    [SerializeField] private int runSpeed = 8;

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

    [Header("Follow player [4]")]
    [Tooltip("Distância para começar a perseguir")]
    [SerializeField] private float angryDistance;
    [Tooltip("Distância para continuar perseguindo")]
    [SerializeField] private float followDistance;
    private Player player;

    // RadiusView
    private RadiusView radiusView;

    private void Start()
    {
        SetAction(rotine[rotineId]);

        navMeshAgent = GetComponent<NavMeshAgent>();
        radiusView = GetComponent<RadiusView>();

        radiusView.SetRadius(angryDistance * 40);

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

        // Follow player
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        switch(action)
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
                FollowPlayer();
                break;
        }

        if (action != 4)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= angryDistance)
            {
                action = 4;
                navMeshAgent.speed = runSpeed;

                radiusView.ChangeColor(Color.red);
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

    private void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= followDistance)
        {
            navMeshAgent.destination = player.transform.position;
            if (Vector3.Distance(transform.position, player.transform.position) <= 0.05f)
            {
                //
            }
        }
        else
        {
            //SetAction(rotine[rotineId]);
            NextRotine();
            navMeshAgent.speed = normalSpeed;

            radiusView.ResetColor();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward * angryDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.forward * followDistance);
        Gizmos.color = Color.white;
    }
}
