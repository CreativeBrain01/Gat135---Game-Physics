using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuadTreeNode
{
    AABB aabb;
    int capacity;
    List<Body> bodies;
    bool subdivided = false;

    QuadTreeNode northeast;
    QuadTreeNode northwest;
    QuadTreeNode southeast;
    QuadTreeNode southwest;

    public QuadTreeNode(AABB aabb, int capacity)
    {
        this.aabb = aabb;
        this.capacity = capacity;

        bodies = new List<Body>();
    }

    public void Insert(Body body)
    {
        if (!aabb.Contains(body.shape.aabb)) return;

        if (bodies.Count < capacity)
        {
            bodies.Add(body);
        } else
        {
            if (!subdivided)
            {
                Subdivide();
            }

            northeast.Insert(body);
            northwest.Insert(body);
            southeast.Insert(body);
            southwest.Insert(body);
        }
    }

    private void Subdivide()
    {
        float xo = aabb.extents.x * 0.5f;
        float yo = aabb.extents.y * 0.5f;

        northeast = new QuadTreeNode(new AABB(new Vector2(aabb.center.x - xo, aabb.center.y + yo), aabb.extents), capacity);
        northwest = new QuadTreeNode(new AABB(new Vector2(aabb.center.x + xo, aabb.center.y + yo), aabb.extents), capacity);
        southeast = new QuadTreeNode(new AABB(new Vector2(aabb.center.x - xo, aabb.center.y - yo), aabb.extents), capacity);
        southwest = new QuadTreeNode(new AABB(new Vector2(aabb.center.x + xo, aabb.center.y - yo), aabb.extents), capacity);

        subdivided = true;
    }

    public void Draw()
    {
        aabb.Draw(Color.white);

        northeast?.Draw();
        southeast?.Draw();
        northwest?.Draw();
        southwest?.Draw();
    }

    public void Query(AABB aabb, List<Body> bodies)
    {
        if (!this.aabb.Contains(aabb)) return;

        bodies.AddRange(this.bodies.Where(body => body.shape.aabb.Contains(aabb)));

        /*foreach (Body body in this.bodies)
        {
            if (body.shape.aabb.Contains(aabb))
            {
                bodies.Add(body);
            }
        }*/

        if (subdivided)
        {
            northeast.Query(aabb, bodies);
            northwest.Query(aabb, bodies);
            southeast.Query(aabb, bodies);
            southwest.Query(aabb, bodies);
        }
    }
}
