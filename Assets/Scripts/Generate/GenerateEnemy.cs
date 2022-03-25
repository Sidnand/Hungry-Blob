using UnityEngine;

public class GenerateEnemy : MonoBehaviour {

    public static GenerateEnemy Instance { get; set; }

    // Enemies prefab.
    [SerializeField] GameObject enemy;

    private void Awake () {
        Instance = this;
    }

    /// <summary>
    /// Generates the enemy.
    /// </summary>
    /// <param name="pos">Position of the enemy</param>
    /// <param name="platform">Parent platform</param>
    public void Generate (Vector3 pos, GameObject platform) {
        GameObject o = Instantiate (enemy, pos, Quaternion.identity);
        o.transform.parent = platform.transform;

        Animate (o, platform);
    }

    /// <summary>
    /// Animates enemy.
    /// </summary>
    /// <param name="o">Enemy game object</param>
    public void Animate (GameObject o, GameObject platform) {
        string[] animations = { "Blue Walk", "Green Walk", "Red Walk" };
        // string[] animations = { "Blue Walk Christmas", "Green Walk Christmas", "Red Walk Christmas" };

        int randIndex = Random.Range (0, animations.Length);
        o.GetComponent<Animator> ().Play (animations[randIndex]);
    }

}
