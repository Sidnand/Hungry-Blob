using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance { get; set; }

    // Score text on main game.
    [SerializeField] Text mainGameScoreText;
    
    // Current player score.
    public int score = 0;

    // Player's high score.
    [HideInInspector] public int highScore;

    private void Awake () {
        Instance = this;
    }

    private void Start () {
        score = 0;

        if (!PlayerPrefs.HasKey("Skin")) {
            PlayerPrefs.SetString ("Skin", "Default");
        }

        // If the user watched an ad the previous round.
        // The users score that round is saved in "Temp Score".
        // Retrieve it and store it into the score variable.
        if (PlayerPrefs.HasKey("Temp Score")) {
            SetScore (PlayerPrefs.GetInt ("Temp Score"));
        } else {
            SetTempScore ();
            SetScore (PlayerPrefs.GetInt ("Temp Score"));
        }

        PlayerPrefs.SetInt ("Temp Score", 0);
        highScore = PlayerPrefs.GetInt ("High Score");
    }

    /// <summary>
    /// Updates the score.
    /// </summary>
    /// <param name="amount">Amount to add score by</param>
    public void UpdateScore(int amount) {
        score += amount;
        mainGameScoreText.text = score.ToString ();
    }

    /// <summary>
    /// Saves the score in local storage.
    /// </summary>
    public void SaveScore () {
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt ("High Score", score);
        }
    }

    /// <summary>
    /// Sets the temp score.
    /// </summary>
    public void SetTempScore () {
        PlayerPrefs.SetInt ("Temp Score", score);
    }

    /// <summary>
    /// Displays the score at the neginning if user watched ad the last round.
    /// </summary>
    /// <param name="tempScore">The tempScore int</param>
    public void SetScore (int tempScore) {
        score = tempScore;
        mainGameScoreText.GetComponent<Text> ().text = score.ToString ();
    }

}
