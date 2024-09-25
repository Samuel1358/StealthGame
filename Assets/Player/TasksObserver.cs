using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksObserver : MonoBehaviour
{
    [SerializeField] GameObject interactableLabel;

    private Task[] tasks;
    private Player player;

    private int inativos;

    private void Start()
    {
        tasks = FindObjectsOfType<Task>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        foreach (var task in tasks)
        {
            if (task.VerifyDistanceToPlayer(player))
            {
                task.interactable = true;
                #pragma warning disable CS0618
                if (interactableLabel.active == false)
                {
                    interactableLabel.SetActive(true);
                }
            }
            else
            {
                task.interactable = false;
                inativos++;
            }
        }

        if (inativos == tasks.Length)
        {
            #pragma warning disable CS0618
            if (interactableLabel.active == true)
            {
                interactableLabel.SetActive(false);
            }
        }

        inativos = 0;
    }
}
