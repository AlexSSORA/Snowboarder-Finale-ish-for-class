using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float timeElapsed;
    public TextMeshProUGUI timeText;  // Reference to your Timer Text(TMP)
    public int checkpointCount = 0;    // Counter for the checkpoints
    public TextMeshProUGUI checkpointText;  // Reference to the Checkpoint Text(TMP)
    private bool levelCleared = false;  // Track if the level has been cleared

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timeElapsed = 0f;
        checkpointCount = 0; // Initialize checkpoint count
        UpdateCheckpointText();  // Initialize the checkpoint text
        ResetText(); // Set initial text values
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeText != null)
        {
            timeText.text = "Time: " + timeElapsed.ToString("F2") + " s";
        }

        // Check if time exceeds 15 seconds to restart level
        if (timeElapsed > 15f && !levelCleared)
        {
            RestartLevel();
        }

        // Change the checkpoint text color based on elapsed time
        if (timeElapsed > 15f)
        {
            ChangeCheckpointTextColor(Color.red);  // Set text color to red if time exceeds
        }
    }

    // Method to increase the checkpoint count
    public void AddCheckpoint()
    {
        if (timeElapsed <= 15f) // Only increment if time is within limits
        {
            checkpointCount++;
            UpdateCheckpointText();
            ChangeCheckpointTextColor(Color.white);  // Reset text color to white when crossing a checkpoint
            ResetTimer(); // Reset the timer after crossing a checkpoint
        }
        levelCleared = true;  // Mark the level as cleared when a checkpoint is reached
    }

    // Restart the level
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
        timeElapsed = 0f; // Reset timer
        checkpointCount = 0; // Reset checkpoint count
        UpdateCheckpointText(); // Update checkpoint text after reload
        ChangeCheckpointTextColor(Color.white); // Reset text color on restart
        levelCleared = false; // Reset level cleared status
    }

    // Reset the timer after passing a checkpoint
    private void ResetTimer()
    {
        timeElapsed = 0f; // Reset timer
    }

    // Update the checkpoint counter text (UI)
    private void UpdateCheckpointText()
    {
        if (checkpointText != null)
        {
            checkpointText.text = "Checkpoints: " + checkpointCount;
        }
    }

    // Method to change the checkpoint text color
    public void ChangeCheckpointTextColor(Color color)
    {
        if (checkpointText != null)
        {
            checkpointText.color = color;  // Update the text color
        }
    }

    // Reset text values
    private void ResetText()
    {
        if (timeText != null) timeText.text = "Time: 0.00 s"; // Set initial time text
        if (checkpointText != null) checkpointText.text = "Checkpoints: 0"; // Set initial checkpoint text
    }
}
