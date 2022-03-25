using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickManager : MonoBehaviour {

    /// <summary>
    /// Start the game when button is clicked.
    /// </summary>
    public void StartGame() {
        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        GameManager.Instance.StartGame ();
    }

    /// <summary>
    /// Reloads the game.
    /// </summary>
    public void ReloadGame() {
        // User didn't watch an ad that round.
        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        DontDestroy.Instance.displayContinueOption = true;
        SceneManager.LoadScene (0);
    }

    /// <summary>
    /// Shows rewarded video ads.
    /// </summary>
    public void Ad() {
        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        AdManager.Instance.ShowRewaredVideoAd ();
    }

    /// <summary>
    /// Exits a given panel
    /// </summary>
    /// <param name="panel">Panel to exit</param>
    public void ExitPanel(GameObject panel) {
        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        panel.SetActive (false);
    }

    /// <summary>
    /// Shows panel
    /// </summary>
    /// <param name="panel">Panel to show</param>
    public void ShowPanel(GameObject panel) {
        SoundManager.Instance.PlayOneShot (SoundManager.Instance.click);
        panel.SetActive (true);
    }

}
