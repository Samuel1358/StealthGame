using UnityEngine;

public class SpotView : MonoBehaviour
{
    [Header("View")]
    public Light spotLight;
    private float viewAngle;
    private Color originalLightColor;    
    [SerializeField] private float viewDistance;
    [SerializeField] private float toleranceTime;
    private float toltt;
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

        toltt = toleranceTime;
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            spotLight.color = Color.red;

            if (toleranceTime <= 0)
            {
                // DERROTA
                MenuOptions.LoadScene("Title");
            }
            else
            {
                toleranceTime -= Time.deltaTime;
            }
        }
        else
        {
            spotLight.color = originalLightColor;

            if (toleranceTime < toltt)
            {
                toleranceTime += Time.deltaTime;
            }
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

    public void SetRange(float value)
    {
        spotLight.range = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward * viewDistance);
        Gizmos.color = Color.white;
    }
}
