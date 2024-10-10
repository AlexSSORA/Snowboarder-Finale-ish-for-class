using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDectector : MonoBehaviour
{
    //Creates a Variable to change delay of Reset
    [SerializeField] float loadDelay = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;

    bool hasCrashed = false;

    //Adds a Function to Reset the Player after crashing
    void OnTriggerEnter2D(Collider2D other)
    {
        //When the Player's head touches the ground
        if(other.tag == "Ground" && !hasCrashed)
        {
            hasCrashed = true;

            FindObjectOfType<PlayerController>().DisableControls();

            //Activates Particle Effect when player crashes
            crashEffect.Play();

            GetComponent<AudioSource>().PlayOneShot(crashSFX);

            //Adds a delay before resetting the level
            Invoke("ReloadScene", loadDelay);
        }
    }

    //Creates a Variable to Reset the Level
    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
