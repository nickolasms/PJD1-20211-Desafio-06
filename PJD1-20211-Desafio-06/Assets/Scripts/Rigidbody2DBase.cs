using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigidbody2DBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Transform tf;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }
}
