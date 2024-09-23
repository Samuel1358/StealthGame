using UnityEngine;

public class SpotView : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private Light spotLight;
    private float viewAngle;
    private Color originalLightColor;    
    [SerializeField] private float viewDistance;
    [SerializeField] private LayerMask viewBlockLayer;

    private Transform player;

    private void Start()
    {
        if (spotLight == null)
        {
            spotLight = transform.GetChild(0).GetComponent<Light>();
        }
        
        if (spotLight != null)
        {
            viewAngle = spotLight.spotAngle;
            originalLightColor = spotLight.color;

            player = FindObjectOfType<Player>().transform;
        }
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            spotLight.color = Color.red;
        }
        else
        {
            spotLight.color = originalLightColor;
        }
    }

    private bool CanSeePlayer()
    {
        if (spotLight != null)
        {
            if (Vector3.Distance(transform.position, player.position) <= viewDistance)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, dir);
                if (angle < viewAngle / 2f)
                {
                    if (!Physics.Linecast(transform.position, player.position, viewBlockLayer))
                    {
                        return true;
                    }
                }
            }
        }
        return false;

        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward * viewDistance);
        Gizmos.color = Color.white;
    }
}
