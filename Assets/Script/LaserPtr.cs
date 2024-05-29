using UnityEngine;

public class LaserPtr: MonoBehaviour
{
    public float _laserLength;
    private LineRenderer _lineRenderer;
    private GameObject obi;

    public void Start()
    {
        obi = GameObject.FindGameObjectWithTag("Player");
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false ;  
    }

    public void render()
    {
        _lineRenderer.enabled = true;
      
        Vector3 endPos = transform.position + (obi.transform.position * _laserLength);
        _lineRenderer.SetPositions(new Vector3[] {transform.position,endPos });
    }

    public void setEnable()
    {
        _lineRenderer.enabled = false;
    }
}