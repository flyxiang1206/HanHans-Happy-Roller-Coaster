using UnityEngine;
//using UnityEditor;

public enum Playmode
{
    Linear, Catmull
};

[ExecuteInEditMode]
public class Rail : MonoBehaviour
{

    public Transform[] nodes;


    private void Start()
    {

        foreach (Transform node in nodes)
        {
            node.GetComponent<Transform>();
        }

    }

    public Vector3 PositionOnRail(int seg, float ratio, Playmode mode)
    {
        switch (mode)
        {
            default:
            case Playmode.Linear:
                return LinearPosition(seg, ratio);
            case Playmode.Catmull:
                return CatmullPosition(seg, ratio);
        }
    }

    public Vector3 LinearPosition(int seg, float ratio)
    {
        Vector3 p1 = nodes[seg].position;
        Vector3 p2 = nodes[seg + 1].position;

        /*
        if (seg >= nodes.Length - 1)
        {
            p1 = nodes[seg].position;
            p2 = nodes[0].position;
        }
        */

        return Vector3.Lerp(p1, p2, ratio);
    }

    public Vector3 CatmullPosition(int seg, float ratio)
    {
        Vector3 p1, p2, p3, p4;

        if (seg == 0)
        {
            p1 = nodes[seg].position;
            p2 = p1;
            p3 = nodes[seg + 1].position;
            p4 = nodes[seg + 2].position;
        }
        else if (seg == nodes.Length - 2)
        {
            p1 = nodes[seg - 1].position;
            p2 = nodes[seg].position;
            p3 = nodes[seg + 1].position;
            p4 = p3;
        }
        else
        {
            p1 = nodes[seg - 1].position;
            p2 = nodes[seg].position;
            p3 = nodes[seg + 1].position;
            p4 = nodes[seg + 2].position;
        }

        float t2 = ratio * ratio;
        float t3 = t2 * ratio;

        float x =
            0.5f * ((2.0f * p2.x)
            + (-p1.x + p3.x) * ratio
            + (2.0f * p1.x - 5.0f * p2.x + 4 * p3.x - p4.x) * t2
            + (-p1.x + 3.0f * p2.x - 3.0f * p3.x + p4.x) * t3);

        float y =
            0.5f * ((2.0f * p2.y)
            + (-p1.y + p3.y) * ratio
            + (2.0f * p1.y - 5.0f * p2.y + 4 * p3.y - p4.y) * t2
            + (-p1.y + 3.0f * p2.y - 3.0f * p3.y + p4.y) * t3);

        float z =
            0.5f * ((2.0f * p2.z)
            + (-p1.z + p3.z) * ratio
            + (2.0f * p1.z - 5.0f * p2.z + 4 * p3.z - p4.z) * t2
            + (-p1.z + 3.0f * p2.z - 3.0f * p3.z + p4.z) * t3);

        return new Vector3(x, y, z);
    }

    public Quaternion Orientation(int seg, float ratio)
    {
        Quaternion q1 = nodes[seg].rotation;
        Quaternion q2 = nodes[seg + 1].rotation;

        /*
        if (seg >= nodes.Length - 1)
        {
            q1 = nodes[seg].rotation;
            q2 = nodes[0].rotation;
        }
        */

        return Quaternion.Lerp(q1, q2, ratio);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            //if (i == nodes.Length - 1)
            //    Handles.DrawDottedLine(nodes[i].position, nodes[0].position, 3.0f);
            //else
           // Handles.DrawDottedLine(nodes[i].position, nodes[i + 1].position, 3.0f);
        }
    }
}

