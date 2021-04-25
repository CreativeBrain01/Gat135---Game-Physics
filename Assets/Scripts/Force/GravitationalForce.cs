using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalForce : MonoBehaviour
{
    public static void ApplyForce(List<Body> bodies, float G)
    {
        for (int i = 0; i < bodies.Count - 1; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                Vector2 dir = bodyA.position - bodyB.position;
                float mag = Mathf.Max(dir.sqrMagnitude, 1);

                float force = G * ((bodyA.mass * bodyB.mass) / mag);

                Vector2 normDir = dir.normalized;

                bodyA.AddForce(-normDir * force, Body.eForceMode.Force);
                bodyB.AddForce(normDir * force, Body.eForceMode.Force);
            }
        }
    }
}