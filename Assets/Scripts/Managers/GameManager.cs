using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; set; }

    // When the game has started.
    [HideInInspector] public bool start = false;
    
    [SerializeField] GameObject startMenu;

    [SerializeField] GameObject endMenu;
    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;
    [SerializeField] GameObject continueAd;

    private void Awake () {
        Instance = this;
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame () {
        startMenu.SetActive (false);
        start = true;
    }

    /// <summary>
    /// Ends the game.
    /// </summary>
    public void EndGame() {
        /*if (DontDestroy.Instance.displayContinueOption) {
            continueAd.SetActive (true);
        } else if (!DontDestroy.Instance.displayContinueOption) {
            continueAd.SetActive (false);
        }*/

        ScoreManager.Instance.SaveScore ();

        scoreText.text = "Score: " + ScoreManager.Instance.score.ToString();
        highScoreText.text = "High Score: " + ScoreManager.Instance.highScore.ToString ();

        start = false;
        endMenu.SetActive (true);
    }

}
