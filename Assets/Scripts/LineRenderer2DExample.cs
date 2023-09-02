using UnityEngine;

public class LineRenderer2DExample : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer.positionCount = 2; // Set the number of points

        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(0, 0, 0);
        positions[1] = new Vector3(2, 2, 0);

        lineRenderer.SetPositions(positions);
    }
}
