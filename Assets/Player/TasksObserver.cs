using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksObserver : MonoBehaviour
{
    [SerializeField] GameObject interactableLabel;

    private Task[] tasks;
    private Player player;
    private TaskList taskList;

    [Header("Conclusão")]
    [SerializeField] private GameObject exit;
    [SerializeField] private Transform exitPosition;
    [SerializeField] private GameObject finalMensage;
    public bool conclued;
    private int doneds;
    private int inativos;

    private void Start()
    {
        tasks = FindObjectsOfType<Task>();
        player = FindObjectOfType<Player>();
        taskList = FindObjectOfType<TaskList>();
    }

    private void Update()
    {
        if (!conclued)
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

                    if (Input.GetKey(KeyCode.E))
                    {
                        task.Complite();
                        taskList.Verify();
                    }
                }
                else
                {
                    task.interactable = false;
                    inativos++;
                }

                if (task.done)
                {
                    doneds++;
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

            if (doneds == tasks.Length)
            {
                conclued = true;
                Instantiate(exit, exitPosition);
                interactableLabel.SetActive(false);
                finalMensage.SetActive(true);
            }

            inativos = 0;
            doneds = 0;
        }
    }
}
