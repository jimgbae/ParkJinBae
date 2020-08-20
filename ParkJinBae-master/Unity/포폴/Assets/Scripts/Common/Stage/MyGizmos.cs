﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public enum Type
    {
        Type_NORMAL,
        Type_WAYPOINT,
        Type_PLAYERPOINT
    }
    private const string wayPointFile = "Enemy";
    public Type type = Type.Type_NORMAL;

    public Color _color = Color.yellow;
    public float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        if (type == Type.Type_NORMAL)
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
        else if (type == Type.Type_WAYPOINT)
        {
            Gizmos.color = _color;
            Gizmos.DrawIcon(transform.position + Vector3.up * 1.0f, wayPointFile, true);
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
        else
        {
            Gizmos.color = _color;
            Gizmos.DrawCube(transform.position, new Vector3(0.5f,0.5f,0.5f));
        }
    }
}
