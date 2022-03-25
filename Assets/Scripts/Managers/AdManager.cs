using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {

    public static AdManager Instance { get; set; }

    // Gets the panel to show that ads unplayable
    [SerializeField] GameObject unplayableAdPanel;

    // Game id for ads.
    private string gameID;

    private void Awake () {
        DontDestroyOnLoad (this);

        if (Instance == null) {
            Instance = this;
        } else {
            DestroyObject (gameObject);
        }
    }

    private void Start () {
        if (Application.platform == RuntimePlatform.Android) {
            gameID = "1590911";
        } else if (Application.platform == RuntimePlatform.IPhonePlayer) {
            gameID = "1590912";
        } else {
            gameID = "unexpected_platform";
        }
        
        Advertisement.Initialize (gameID);
    }

    /// <summary>
    /// Adds to rounds.
    /// </summary>
    /// <returns>If user in on second round</returns>
    public bool CheckContinue() {
        if (DontDestroy.Instance.displayContinueOption) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// Shows rewarded video ads.
    /// </summary>
    public void ShowRewaredVideoAd() {
        if (Advertisement.isInitialized) {
            if (Advertisement.IsReady ()) {
                Advertisement.Show ("", new ShowOptions () { resultCallback = HandleShowResult });
            } else if (!Advertisement.IsReady ()) {
                unplayableAdPanel.SetActive (true);
            } else {
                unplayableAdPanel.SetActive (true);
            }
        }
    }

    /// <summary>
    /// Handles the results.
    /// </summary>
    /// <param name="result">Return from ads</param>
    private void HandleShowResult (ShowResult result) {
        switch (result) {
            case ShowResult.Finished:
            DontDestroy.Instance.displayContinueOption = false;
            ScoreManager.Instance.SetTempScore ();
            SceneManager.LoadScene (0);
            break;

            case ShowResult.Skipped:
            PlayerPrefs.SetInt ("Temp Score", 0);
            ScoreManager.Instance.SetTempScore ();
            SceneManager.LoadScene (0);
            break;

            case ShowResult.Failed:
            PlayerPrefs.SetInt ("Temp Score", 0);
            ScoreManager.Instance.SetTempScore ();
            SceneManager.LoadScene (0);
            break;
        }
    }

}
