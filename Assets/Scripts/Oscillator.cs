using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startinPosition;
    [SerializeField] Vector3 movementVector; //determines how far we want to move the obstacle
    [SerializeField] [Range(0,1)] float movementFactor; // a number to multiply it by the movementVector for controlling the movement
                                                        // using "Range" gives us a slider :)
    [SerializeField] float period; //period of the sine wave


    // Start is called before the first frame update
    void Start()
    {
        startinPosition = transform.position;
        //Debug.Log(startinPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } // avioding Nan (Not a number caused by dividing a number by 0) error
        float cycles = Time.time / period; // if 10 sec has elapsed and the period is 2 sec, we've had 5 cycles by now
                                           // continually growing ver time

        const float tau = Mathf.PI * 2;//for working with sine wave tau = 2 * pi = 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // this gives us a sin wave as the time passes => from -1 to 1
        //Debug.Log(rawSinWave);

        movementFactor = (rawSinWave + 1f) / 2f; // -1 < rawSinWave < 1 => 0 < movementFactor < 1

        Vector3 offset = movementVector * movementFactor; // we add this to the position of the object
        transform.position = startinPosition + offset;
    }
}
