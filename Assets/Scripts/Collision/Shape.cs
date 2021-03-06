using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shape : MonoBehaviour
{
    public enum eType
    {
        Circle,
        Box
    }

    public abstract eType type { get; }
    public abstract float mass { get; }
    public abstract float size { get; set; }
    public float density { get; set; } = 1;

    public Color color { set => sr.material.color = value; }
    public AABB aabb { get => new AABB(transform.position, Vector2.one * size);  }

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
}
