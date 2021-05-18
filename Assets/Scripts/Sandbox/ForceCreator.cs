using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCreator : Action
{
    public GameObject original;
    public FloatData size;
    public FloatData forceMagnitude;
    public ForceModeData forceMode;

    public override eActionType actionType { get { return eActionType.Force; } }

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
            if (gameObject.TryGetComponent<PointEffector>(out PointEffector effector))
            {
                effector.forceMagnitude = forceMagnitude.value;
                effector.shape.size = size.value;
                effector.forceMode = forceMode.value;

                World.Instance.forces.Add(effector);
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
