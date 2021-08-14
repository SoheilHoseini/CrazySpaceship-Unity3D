using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float xAngle, yAngle, zAngle;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, yAngle, 0);
    }
}
