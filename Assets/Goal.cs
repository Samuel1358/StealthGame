using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private string goal;
    private TasksObserver tasksObserver;

    private void Start()
    {
        tasksObserver = FindObjectOfType<TasksObserver>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MenuOptions.LoadScene(goal);
        }
    }
}
