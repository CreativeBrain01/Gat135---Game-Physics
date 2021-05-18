using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : Action
{
    public GameObject original;
    public float speed = 100;
    public FloatData damping;
    public FloatData size;
    public FloatData density;
    public FloatData restitution;
    public BodyEnumData bodyType;

    public override eActionType actionType { get { return eActionType.Creator; } }

    bool action { get; set; } = false;
    bool oneTime { get; set; } = false;

    public override void StartAction()
    {
        action = true;
        oneTime = true;
    }

    public override void StopAction()
    {
        action = false;
    }

    void Update()
    {
        if (action && (oneTime || Input.GetKey(KeyCode.LeftControl)))
        {
            oneTime = false;
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            GameObject gameObject = Instantiate(original, position, Quaternion.identity);
            if (gameObject.TryGetComponent<Body>(out Body body))
            {
                body.damping = damping.value;
                body.shape.size = size.value;
                body.shape.density = density.value;
                body.restitution = restitution.value;
                body.type = (Body.eType)bodyType.value;


                Vector2 force = Random.insideUnitSphere.normalized * speed;
                body.AddForce(force);
                World.Instance.bodies.Add(body);
            }
        } /*else if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Shape>())
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }*/
    }
}
