using UnityEngine;
using Unity;
public class LaserPtr: MonoBehaviour
{
    public float _laserLength;
    private LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 endPos = transform.position + (transform.right * _laserLength);
        _lineRenderer.SetPositions(new Vector3[] {transform.position,endPos });
    }
}