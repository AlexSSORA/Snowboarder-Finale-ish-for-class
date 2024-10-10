using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem finishEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate Particle Effect when the player hits the finish line
            finishEffect.Play();

            // Play the audio effect for the finish line
            GetComponent<AudioSource>().Play();

            // Increment checkpoint count
            GameManager.Instance.AddCheckpoint();  // Call to increment checkpoints
        }
    }
}
