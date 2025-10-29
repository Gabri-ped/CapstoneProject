using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
public class VisionCone : MonoBehaviour
{
    public float viewDistance = 10f;
    public float viewAngle = 45f;
    public int horizontalRayCount = 20;
    public int verticalRayCount = 10;
    public LayerMask targetLayer;
    public bool isVisible;

    void Update()
    {
        CastCone();
    }

    public bool CastCone()
    {
        float halfAngle = viewAngle / 2;
        isVisible = false;

        for (int v = 0; v <= verticalRayCount; v++)
        {
            float pitch = -halfAngle + (viewAngle / verticalRayCount) * v;



            for (int h = 0; h <= horizontalRayCount; h++)
            {
                float yaw = -halfAngle + (viewAngle / horizontalRayCount) * h;

                Vector3 dir = Quaternion.Euler(pitch, yaw, 0) * transform.forward;


                if (Physics.Raycast(transform.position, dir, out RaycastHit hit, viewDistance, targetLayer))
                {
                    return isVisible = true;
                }

            }

        }
        return isVisible;
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
        float halfAngle = viewAngle / 2;
        for (int v = 0; v <= verticalRayCount; v++)
        {
            float pitch = -halfAngle + (viewAngle / verticalRayCount) * v;
            for (int h = 0; h <= horizontalRayCount; h++)
            {
                float yaw = -halfAngle + (viewAngle / horizontalRayCount) * h;
                Vector3 dir = Quaternion.Euler(pitch, yaw, 0) * transform.forward;
                Gizmos.DrawRay(transform.position, dir * viewDistance);
            }
        }
    }
}


