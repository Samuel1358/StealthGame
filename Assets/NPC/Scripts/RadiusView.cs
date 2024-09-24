using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RadiusView : MonoBehaviour
{
    [Header("View")]
    public Light radiusLight;
    //[SerializeField] private float viewDistance;
    [SerializeField] private LayerMask viewBlockLayer;
    private Color originalLightColor;

    private Transform player;

    private void Start()
    {
        if (radiusLight == null)
        {
            radiusLight = transform.GetChild(0).GetComponent<Light>();
        }

        if (radiusLight != null)
        {
            originalLightColor = radiusLight.color;
            player = FindObjectOfType<Player>().transform;
        }
    }

    public void ChangeColor(Color color)
    {
        radiusLight.color = color;
    }

    public void ResetColor()
    {
        radiusLight.color = originalLightColor;       
    }

    public void SetRadius(float value)
    {
        radiusLight.spotAngle = value;
    }
}
