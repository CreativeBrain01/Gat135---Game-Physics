using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Integrator
{
    public static void ExplicitEuler(Body body, float dt)
    {
        body.position += (body.velocity * dt);
        body.velocity += (body.acceleration * dt);
        body.velocity *= 1f / (1f + (body.damping * dt));
    }

    public static void ImplicitEuler(Body body, float dt)
    {
        //body.acceleration = body.force / body.mass;
        body.velocity += body.acceleration * dt;
        body.position += body.velocity * dt;
        body.velocity *= 1f / (1f + (body.damping * dt));
    }
}
