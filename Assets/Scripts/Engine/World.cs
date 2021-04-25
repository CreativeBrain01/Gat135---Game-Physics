using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public BoolData collision;
    public BoolData wrap;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFPS;
    public StringData trueFPS;

    static World instance;
    public static World Instance { get { return instance; } }

    public List<Body> bodies { get; set; } = new List<Body>();

    public float fixedDeltaTime { get { return 1/fixedFPS.value; } }

    private void Awake()
    {
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    float timeAccumulator = 0;

    float nextFPSUpdate = 0;
    float fpsUpdateTime = 0.25f;

    Vector2 size;

    void Update()
    {
        float dt = Time.deltaTime;

        nextFPSUpdate += dt;
        if (nextFPSUpdate >= fpsUpdateTime)
        {
            nextFPSUpdate = 0;
            trueFPS.value = Mathf.Floor(1 / dt).ToString();
        }

        if (!simulate.value) return;

        GravitationalForce.ApplyForce(bodies, gravitation.value);

        bodies.ForEach(body => body.shape.color = Color.red);

        timeAccumulator += dt;
        while (timeAccumulator >= fixedDeltaTime)
        {
            timeAccumulator -= dt;
            bodies.ForEach(body => body.Step(dt));
            bodies.ForEach(body => Integrator.ImplicitEuler(body, dt));

            bodies.ForEach(body => body.shape.color = Color.white);

            if (collision.value)
            {
                Collision.CreateContacts(bodies, out List<Contact> contacts);
                contacts.ForEach(contact => { contact.bodyA.shape.color = Color.red; contact.bodyB.shape.color = Color.red; });
                ContactSolver.Resolve(contacts);
            }
        }

        if (wrap.value)
        {
            bodies.ForEach(body => body.position = Utilities.Wrap(body.position, -size, size));
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}