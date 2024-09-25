using JetBrains.Annotations;
using UnityEngine;

public class Task : MonoBehaviour
{
    [Header("UI")]
    [TextArea]
    public string description;
    public Sprite sprite;
    public bool done;

    [Header("Functionality")]
    [SerializeField] private float interactableDistance;
    public bool interactable;


    public bool VerifyDistanceToPlayer(Player player)
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= interactableDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
