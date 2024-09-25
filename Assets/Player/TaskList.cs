using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject taskUI;
    [SerializeField] RectTransform container;

    [Header("List")]
    public List<Task> taskList;
    private List<TaskUI> taskUIList = new List<TaskUI>();

    [SerializeField] int tasksLimit;

    private void Start()
    {
        while (taskList.Count > tasksLimit)
        {
            taskList.RemoveAt(0);
        }

        for (int i = 0; i < taskList.Count; i++)
        {
            var instance = Instantiate(taskUI, container);
            //Debug.Log(instance.GetComponent<TaskUI>());
            taskUIList.Add(instance.GetComponent<TaskUI>());

            taskUIList[i].description = taskList[i].description;
            taskUIList[i].sprite = taskList[i].sprite;

            taskUIList[i].Assign();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Verify();
        }
    }

    public void Verify()
    {
        for (int i = 0; i < taskList.Count; i++)
        {
            if (taskList[i].done)
            {
                taskUIList[i].gameObject.SetActive(false);
            }
        }
    }
}
