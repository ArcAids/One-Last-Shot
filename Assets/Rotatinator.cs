using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Rotatinator : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 axis;

    void FixedUpdate()
    {
        transform.Rotate(axis*speed *Time.fixedDeltaTime);
    }
}
