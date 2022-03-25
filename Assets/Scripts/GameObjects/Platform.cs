using UnityEngine;

public class Platform : MonoBehaviour {

    [SerializeField] Sprite[] sprites;

    SpriteRenderer sp;
    GameObject generate;

    // Enemies y position.
    float enemyPosY;

    // # of food items / platform.
    int items = 4;

    // The chance food food to spawn ( / 10).
    int foodChance = 4;

    Vector2 enemyPos;

    private void Start () {
        sp = GetComponent<SpriteRenderer> ();

        enemyPosY = transform.position.y + 0.4f;
        enemyPos = new Vector3 (transform.position.x, enemyPosY, transform.position.z);
        generate = GameObject.Find ("Generate");

        EnemyGenerate ();
        FoodGenerate ();
        ChangeColor();
    }

    private void Update () {
        OffScreen ();
    }

    /// <summary>
    /// Generates the enemy.
    /// </summary>
    private void EnemyGenerate() {
        // Don't want to generate enemy on starting platform
        if (gameObject.name != "Start Platform") {
            GenerateEnemy.Instance.Generate (enemyPos, gameObject);
        }
    }

    /// <summary>
    /// Generates food items.
    /// </summary>
    private void FoodGenerate() {
        if (gameObject.name != "Start Platform") {
            for (var i = 0; i < items; i++) {
                GameObject child = GetChild ("Item " + i.ToString ());

                // Hide all sprites
                child.GetComponent<SpriteRenderer> ().enabled = false;
                child.GetComponent<BoxCollider2D> ().size = new Vector2 (0, 0);

                var rand = Random.Range (0, 11);
                if (rand > foodChance) {
                    // Only show sprites that are chosen
                    child.GetComponent<SpriteRenderer> ().enabled = true;
                    child.transform.parent = transform;
                    GenerateFood.Instance.Generate (child);
                }
            }
        }
    }

    /// <summary>
    /// Resets the enemy.
    /// </summary>
    private void ResetEnemy () {
        Vector2 pos = generate.GetComponent<GeneratePlatforms>().GetPos ();
        transform.position = pos;

        Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform> (true);
        foreach (Transform t in ts) {
            if (!t.gameObject.activeSelf) t.gameObject.SetActive (true);
            if (t.gameObject.tag == "Enemy") generate.GetComponent<GenerateEnemy> ().Animate (t.gameObject, gameObject);
        }
    }

    /// <summary>
    /// Gets child game object.
    /// </summary>
    /// <param name="child">Child name</param>
    /// <returns>The child object</returns>
    private GameObject GetChild (string child) {
        Transform[] ts = gameObject.transform.GetComponentsInChildren<Transform> (true);

        foreach (Transform t in ts) {
            if (t.gameObject.name == child) return t.gameObject;
        }

        return null;
    }

    /// <summary>
    /// Changes platforms color.
    /// </summary>
    private void ChangeColor() {
        var newColor = new Color (Random.Range(0, 0.9f), Random.Range (0, 0.9f), Random.Range (0, 0.9f));
        sp.color = newColor;
    }

    /// <summary>
    /// Checks if platform is offscreen.
    /// </summary>
    private void OffScreen() {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint (transform.position);
        var onScreen = screenPoint.x > -1;

        if (!onScreen) {
            if (gameObject.name != "Start Platform") {
                FoodGenerate ();
                ResetEnemy ();
                ChangeColor ();
             // Only if platform if Starting platform.
            } else gameObject.SetActive (false);
        }
    }

}
