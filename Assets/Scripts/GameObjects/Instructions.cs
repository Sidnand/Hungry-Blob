using UnityEngine;
using UnityEngine.UI;

public class TapText : MonoBehaviour {

    private void Start () {
        // What instructions to show on start of game.
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer) gameObject.GetComponent<Text> ().text = "Tap...";

        else if (Application.platform == RuntimePlatform.WindowsEditor ||
                 Application.platform == RuntimePlatform.OSXEditor ||
                 Application.platform == RuntimePlatform.LinuxEditor) gameObject.GetComponent<Text> ().text = "Press Space...";
    }

}
