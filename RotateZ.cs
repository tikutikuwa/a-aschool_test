using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateZ : MonoBehaviour
{
    public float rotSpeedZ;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotSpeedZ);
    }
}