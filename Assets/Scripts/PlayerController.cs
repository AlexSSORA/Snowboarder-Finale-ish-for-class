using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostSpeed = 60f;      // Set a higher value for boost
    [SerializeField] float baseSpeed = 30f;       // Normal speed
    [SerializeField] float boostDuration = 1.5f;  // Duration of boost
    [SerializeField] float boostCooldown = 3f;    // Cooldown between boosts

    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;

    bool canMove = true;
    bool isBoosting = false;
    bool canBoost = true;  // Whether the player can boost or not

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();

        // Debug message to check if SurfaceEffector2D is found
        if (surfaceEffector2D == null)
        {
            Debug.LogError("No SurfaceEffector2D found! Make sure it's attached to the ground.");
        }
    }

    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    public void DisableControls()
    {
        canMove = false;
    }

    // Handle player boost and speed control
    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canBoost && !isBoosting)  // Boost when pressing space
        {
            Debug.Log("Spacebar pressed. Boosting!");  // Debug log to confirm spacebar press
            StartCoroutine(ActivateBoost());
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    // Coroutine to handle boosting and cooldown
    IEnumerator ActivateBoost()
    {
        // Start boosting
        Debug.Log("Boost started!");
        isBoosting = true;
        surfaceEffector2D.speed = boostSpeed * 2;  // Double the speed during boost
        yield return new WaitForSeconds(boostDuration);

        // Stop boosting and start cooldown
        surfaceEffector2D.speed = baseSpeed;
        isBoosting = false;
        canBoost = false;

        Debug.Log("Boost ended. Cooldown started.");
        // Wait for the cooldown period
        yield return new WaitForSeconds(boostCooldown);
        canBoost = true;
        Debug.Log("Cooldown ended. Boost ready again.");
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }
}

