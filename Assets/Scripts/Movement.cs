using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // I think the "Drag" section in rigidbody component controls the movement of the player and slows it down
    //Drag can be used to slow down an object. The higher the drag the more the object slows down.

    [SerializeField] AudioClip mainEngineSound; //we use AudioClip to use different sounds for one game object
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            ////force and move the object towards the direction which is determines by the coordinates given 
            //rb.AddRelativeForce(0, 1, 0);
            //or wr can use Vector3 which is a 3D vector
            //mainThrust determines the amount of power imposed to the rocket
            //Time.deltaTime is to make it frame independent
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            //without the "if", when we keep the space button down, unity will play the sound over & over and its messy
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSound);
            }
            
        }
        else
        {
            audioSource.Stop();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    //this method will rotate the spaceship to left and right, frame independently
    //use "Ctrl + M" to create a method from a selected line
    private void ApplyRotation(float rotationThisFrame)
    {
        //when we take control of the rocket's rotation, the system should live it to us to not have a conflict there
        //and when we are finished it should take control again

        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(rotationThisFrame * Vector3.forward * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics system can take over after we're done (I guess :)  )

        
    }
}
