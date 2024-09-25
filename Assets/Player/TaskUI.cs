using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public string description;
    public Sprite sprite;

    private Image image;
    private TMP_Text textMeshPro;

    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        textMeshPro = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();

        //Assign();
    }

    public void Assign()
    {
        image.sprite = sprite;
        textMeshPro.text = description;
    }
}
