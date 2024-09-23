using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    private void Start()
    {
        /*var meshRenderer = GetComponent<MeshRenderer>();
        var material = meshRenderer.material;

        material.color = Color.ra*/
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
