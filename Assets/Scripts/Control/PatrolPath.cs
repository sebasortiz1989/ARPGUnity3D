using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        // String const
        private const float wayPointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(GetWaypoint(i), wayPointGizmoRadius);

                Gizmos.color = Color.white;
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        private int GetNextIndex(int i)
        {
            if (i < transform.childCount - 1)
                return i + 1;
            else
                return 0;
        }

        private Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}