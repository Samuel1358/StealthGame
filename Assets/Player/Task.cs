using JetBrains.Annotations;
using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] private Color color;

    [Header("UI")]
    [TextArea]
    public string description;
    public Sprite sprite;
    public bool done;

    [Header("Functionality")]
    [SerializeField] private float interactableDistance;
    public bool interactable;
    [SerializeField] private GameObject model;

    private void Start()
    {
        model.GetComponent<Renderer>().material.color = color;
    }

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

    public void Complite()
    {
        done = true;
        model.SetActive(false);
    }
}
