using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class ProceduralEyes : MonoBehaviour
{
    public bool preview = true;
    public float maxZFar = -5;
    public List<Eye> Eyes = new List<Eye>();

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (Eye a in Eyes)
        {
            if (a.eye != null) Gizmos.DrawWireSphere(a.eye.position, a.radius);
        }
    }

    void Update()
    {
        DrawEyes();
    }

    void OnDrawGizmos()
    {
        if (preview) DrawEyes();
    }

    void DrawEyes()
    {
        foreach (Eye a in Eyes)
        {
            Vector2 controller;
            float lerpVal = (maxZFar - transform.position.z) / maxZFar;
            controller = Vector2.Lerp(a.eye.localPosition, transform.localPosition, lerpVal / 2);

            var leftPos = Vector2.ClampMagnitude(controller - (Vector2)a.eye.localPosition, a.radius);//Normalization
            if (Quaternion.Angle(a.eye.rotation, transform.rotation) > 60)
            {
                leftPos.x *= -1;
            }

            a.eye.GetChild(0).localPosition = leftPos;//The first child is the pupil 
        }
    }
}

[System.Serializable]
public struct Eye
{
    public Transform eye;
    public float radius;
}