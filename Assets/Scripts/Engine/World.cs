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
    public VectorField vectorField;

    static World instance;
    public static World Instance { get { return instance; } }

    public List<Body> bodies { get; set; } = new List<Body>();
    public List<Spring> springs { get; set; } = new List<Spring>();
    public List<Force> forces { get; set; } = new List<Force>();

    public float fixedDeltaTime { get { return 1 / fixedFPS.value; } }

    AABB aabb;
    public AABB AABB { get => aabb; }

    BroadPhase broadPhase = new QuadTree();

    private void Awake()
    {
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
        aabb = new AABB(Vector2.zero, size * 2);
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

        springs.ForEach(spring => spring.Draw());
        if (!simulate.value) return;

        GravitationalForce.ApplyForce(bodies, gravitation.value);
        forces.ForEach(force => bodies.ForEach(body => force.ApplyForce(body)));
        springs.ForEach(spring => spring.ApplyForce());
        bodies.ForEach(body => vectorField.ApplyForce(body));

        bodies.ForEach(body => body.shape.color = Color.red);

        timeAccumulator += dt;
        while (timeAccumulator >= fixedDeltaTime)
        {
            timeAccumulator -= dt;
            bodies.ForEach(body => body.Step(dt));
            bodies.ForEach(body => Integrator.ImplicitEuler(body, dt));
            bodies.ForEach(body => body.shape.color = Color.white);

            if (collision)
            {
                broadPhase.Build(AABB, bodies);

                Collision.CreateBroadPhaseContacts(broadPhase, bodies, out List<Contact> contacts);
                Collision.CreateNarrowPhaseContacts(ref contacts);
                contacts.ForEach(contact => Collision.UpdateContactInfo(ref contact));

                //Collision.CreateContacts(bodies, out List<Contact> contacts);
                ContactSolver.Resolve(contacts);
                contacts.ForEach(contact => { contact.bodyA.shape.color = Color.red; contact.bodyB.shape.color = Color.red; });
            }
        }

        broadPhase.Draw();

        if (wrap.value)
        {
            bodies.ForEach(body => body.position = Utilities.Wrap(body.position, -size, size));
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}