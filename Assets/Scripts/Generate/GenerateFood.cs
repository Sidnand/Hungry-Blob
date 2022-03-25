using UnityEngine;

public class GenerateFood : MonoBehaviour {

    public static GenerateFood Instance { get; set; }

    // Array of sprites of food items.
    [SerializeField] Sprite[] items;

    private void Start () {
        Instance = this;
    }

    /// <summary>
    /// Adds sprite to GameObject.
    /// </summary>
    /// <param name="point">GameObject of sprite</param>
    public void Generate(GameObject point) {
        point.GetComponent<SpriteRenderer> ().sprite = items[Pick ()];
        Vector2 s = point.GetComponent<SpriteRenderer> ().sprite.bounds.size;
        point.GetComponent<BoxCollider2D> ().size = s;
    }

    /// <summary>
    /// Picks a random food sprite.
    /// </summary>
    /// <returns>Sprite index</returns>
    public int Pick () {
        var index = Random.Range (0, items.Length);
        return index;
    }

}
