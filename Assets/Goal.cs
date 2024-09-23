using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private string goal;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MenuOptions.LoadScene(goal);
        }
    }
}
