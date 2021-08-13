using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;//to prevent moving the player or playing SFX while we loading the next scene
    bool collisionDisabled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    //God mode 
    private void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKey(KeyCode.C))
        {
            //isTransitioning = !isTransitioning;  => this is another easy way
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) return; //not to do any of the below things


        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrachSequence();          
                break;
        }
    }

    private void StartSuccessSequence()
    {
        //emit the particles
        successParticles.Play();

        isTransitioning = true;
        audioSource.Stop();//stop all sounds after success
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrachSequence()
    {
        
        isTransitioning = true;
        audioSource.Stop();//stop all sounds after success
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();

        //if we disable the script, the rocket will no longer be under control
        GetComponent<Movement>().enabled = false;

        //before call the given method, it waits as long as the given secs
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndx = currentSceneIndx + 1;

        if(nextSceneIndx == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndx = 0;
        }

        SceneManager.LoadScene(nextSceneIndx);      
    }

    void ReloadLevel()
    {
        //SceneManager.LoadScene(0);
        //or we can write:
        int currentSceneIndx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndx);
    }
}

