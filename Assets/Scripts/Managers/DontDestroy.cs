using UnityEngine;

public class DontDestroy : MonoBehaviour {

    public static DontDestroy Instance { get; set; }

    // If use watched an ad the last round.
    [HideInInspector] public bool displayContinueOption = true;

    // Whether to allow user to change level or not.
    [HideInInspector] public bool displayChangeLevel = true;

    private void Awake () {
        DontDestroyOnLoad (this);

        if (Instance == null) {
            Instance = this;
        } else {
            DestroyObject (gameObject);
        }
    }

}
