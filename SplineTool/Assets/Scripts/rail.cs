using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

public enum PlayMode
{
    Linear,
    Catmull,
}

[ExecuteInEditMode]
public class rail : MonoBehaviour
{
    public  Transform[] Nodes;

    private void Update()
    {
        Nodes = GetComponentsInChildren<Transform>();
    }

    public Vector3 linearPos(int seg,float ratio)
    {
        Vector3 p1 = Nodes[seg].position;
        Vector3 p2 = Nodes[seg + 1].position;

        return Vector3.Lerp(p1, p2, ratio);
    }

    public Quaternion Orientation(int seg, float ratio)
    {
        Quaternion q1 = Nodes[seg].rotation;
        Quaternion q2 = Nodes[seg+1].rotation;

        return Quaternion.Lerp(q1, q2, ratio);
    }
    public Vector3 CatmullPosition(int seg,float ratio)
    {
        Vector3 p1, p2, p3, p4;

        if(seg==0)
        {
            p1 = Nodes[seg].position;
            p2 = p1;
            p3 = Nodes[seg + 1].position;
            p4 = Nodes[seg + 2].position;

        }
        else if(seg == Nodes.Length -2)
        {
            p1 = Nodes[seg - 1].position;
            p2 = Nodes[seg].position;
            p3 = Nodes[seg + 1].position;
            p4 = p3;
        }
        else 
        {
            p1 = Nodes[seg - 1].position;
            p2 = Nodes[seg].position;
            p3 = Nodes[seg + 1].position;
            p4 = Nodes[seg + 2].position;
        }
      

        float t2 = ratio * ratio;
        float t3 = t2 * ratio;

        float x = 0.5f * ((2.0f * p2.x) + (-p1.x + p3.x) * ratio + 
            (2.0f * p1.x - 5.0f * p2.x + 4 * p3.x - p4.x) * t2 + (-p1.x + 3.0f * p2.x - 3.0f * p3.x + p4.x) * t3);

        float y = 0.5f * ((2.0f * p2.y) + (-p1.y + p3.y) * ratio +
            (2.0f * p1.y - 5.0f * p2.y + 4 * p3.y - p4.y) * t2 + (-p1.y + 3.0f * p2.y - 3.0f * p3.y + p4.y) * t3);

        float z = 0.5f * ((2.0f * p2.z) + (-p1.z + p3.z) * ratio +
            (2.0f * p1.z - 5.0f * p2.z + 4 * p3.z - p4.z) * t2 + (-p1.z + 3.0f * p2.z - 3.0f * p3.z + p4.z) * t3);
        return new Vector3(x, y, z);
    }
    public Vector3 PositionOnRail(int seg,float ratio,PlayMode mode)
    {
        switch(mode)
        {
            default:
            case PlayMode.Linear:
                return linearPos(seg, ratio);

            case PlayMode.Catmull:
                return CatmullPosition(seg, ratio);
        }
    }
    private void OnDrawGizmos()
    {
        if(Nodes.Length>1)
        {
            for(int i =0;i<Nodes.Length-1;i++)
            {
                Handles.DrawDottedLine(Nodes[i].position, Nodes[i + 1].position, 3.0f);
            }
        }
    }

}
