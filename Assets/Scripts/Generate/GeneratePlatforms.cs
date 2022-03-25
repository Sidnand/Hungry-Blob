using UnityEngine;

public class GeneratePlatforms : MonoBehaviour {

    static public GeneratePlatforms Instance { get; set; }

    // Starting platform gameobject.
    [SerializeField] GameObject startingPlatform;

    // Platform prefab.
    [SerializeField] GameObject platform;
    
    SpriteRenderer sp;
    Camera cam;

    // Height of the camera
    float camHeight;

    // Minimum y pos
    float yMin;

    // Maximum y pos
    float yMax;

    // Number of platforms
    int numPlatforms = 4;

    // Gap between platforms
    int gap = 4;

    // Last platforms position
    Vector3 lastPlatformPos;

    private void Start () {
        sp = startingPlatform.GetComponent<SpriteRenderer> ();
        cam = Camera.main;

        camHeight = 2 * cam.orthographicSize;
        yMin = (-camHeight / 2);
        yMax = (camHeight / 2) - 3;

        lastPlatformPos = startingPlatform.transform.position;

        Generate ();
    }

    /// <summary>
    /// Generates the platforms.
    /// </summary>
    public void Generate() {
        for (var i = 0; i < numPlatforms; i++) {
            Vector2 pos = GetPos ();
            Instantiate (platform, pos, Quaternion.identity);
        }
    }

    /// <summary>
    /// Gets the next platforms position.
    /// </summary>
    /// <returns>Position</returns>
    public Vector2 GetPos() {
        // Random y pos
        var randY = Random.Range (yMin, yMax);

        // W of platform
        var x = lastPlatformPos.x + sp.bounds.size.x + gap;

        // Position of platform
        var pos = new Vector2 (x, randY);

        lastPlatformPos = pos;
        return pos;
    }

}
