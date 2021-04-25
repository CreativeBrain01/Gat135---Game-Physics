using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactSolver
{
    public static void Resolve(List<Contact> contacts)
    {
        foreach (var contact in contacts)
        {
            Body bodyA = contact.bodyA;
            Body bodyB = contact.bodyB;

            //separation
            float TIM = bodyA.inverseMass + bodyB.inverseMass; //total inverse mass
            Vector2 separation = (contact.normal * contact.depth) / TIM;
            contact.bodyA.position += separation * bodyA.inverseMass;
            contact.bodyB.position -= separation * bodyB.inverseMass;

            //Collision impulse
            Vector2 relativeVelocity = bodyA.velocity - bodyB.velocity;
            float normalVelocity = Vector2.Dot(relativeVelocity, contact.normal);

            if (normalVelocity > 0) continue;

            float restitution = (bodyA.restitution + bodyB.restitution) * 0.5f;
            float impulseMagnitude = -(1.0f + restitution) * normalVelocity / TIM;

            Vector2 impulse = contact.normal * impulseMagnitude;
            bodyA.AddForce(bodyA.velocity + (impulse * bodyA.inverseMass), Body.eForceMode.Velocity);
            bodyB.AddForce(bodyB.velocity - (impulse * bodyB.inverseMass), Body.eForceMode.Velocity);

        }
    }
}
