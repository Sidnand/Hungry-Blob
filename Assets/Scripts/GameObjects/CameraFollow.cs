using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // Gameobject that camera will follow.
    [SerializeField] Transform target;

    private void Update () {
        // Offset of camera.
        var offSetX = 4;

        transform.position = new Vector3 (target.position.x + offSetX, transform.position.y, transform.position.z);
    }

}
