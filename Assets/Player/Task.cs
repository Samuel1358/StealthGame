using JetBrains.Annotations;
using UnityEngine;

public class Task : MonoBehaviour
{
    [TextArea]
    public string description;
    public Sprite sprite;
    public bool done;

}
