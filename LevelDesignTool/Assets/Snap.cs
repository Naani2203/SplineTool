using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    Vector3 pos;
    public static float gridSnap;

    private void LateUpdate()
    {
        pos.x = Mathf.Floor(transform.position.x / gridSnap) * gridSnap;
        pos.y = Mathf.Floor(transform.position.y / gridSnap) * gridSnap;
        pos.z = Mathf.Floor(transform.position.z / gridSnap) * gridSnap;
    }
}
