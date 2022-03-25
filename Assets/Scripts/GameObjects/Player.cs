using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    Rigidbody2D rb;
    Animator anime;

    [SerializeField] GameObject instructions;
    [SerializeField] GameObject maxJumpTextObject;
    [SerializeField] GameObject jumpsLeftTextObject;

    // Distance travel / frame rate.
    float dis = 5;

    // Jump force.
    float thrust = 30;

    // Number of platforms jumped on.
    int currentPlatformNumber = 0;

    int maxPlatformNumber = 3;

    // Maximum number of jumps.
    int maxJumps = 12;

    // Players current jump number.
    int currentJump = 0;

    private void Start () {
        rb = GetComponent<Rigidbody2D> ();
        anime = GetComponent<Animator> ();

        jumpsLeftTextObject.GetComponent<Text> ().text = "Jumps Left: " + (maxJumps - currentJump).ToString();
    }

    private void Update () {
        if (GameManager.Instance.start) {
            // anime.Play ("Move Christmas");
            anime.Play ("Move Default");

            // Set controls.
            if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer) MobileControls ();
            else if (Application.platform == RuntimePlatform.WindowsEditor) DesktopControls ();

            // Makes player faster / frame.
            if (dis < 10) dis += 0.06f * Time.deltaTime;
        }

    }

    private void FixedUpdate () {
        if (GameManager.Instance.start) {
            Gravity ();
            Move ();
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            if (CheckColTop(collision)) {
                SoundManager.Instance.PlayOneShot (SoundManager.Instance.enemyTop);
                collision.gameObject.SetActive (false);
                ScoreManager.Instance.UpdateScore (3);
            } else {
                End ();
            }
        }

        if (collision.gameObject.tag == "Bottom Collider") End ();

        if (collision.gameObject.tag == "Platform") {
            if (CheckColTop(collision)) {
                currentJump = 0;

                if (currentPlatformNumber <= maxPlatformNumber) {
                    currentPlatformNumber++;
                } else if (currentPlatformNumber > maxPlatformNumber) {
                    if (maxJumps > 2) {
                        maxJumps -= 1;
                        ShowMaxJump ();
                    }

                    currentPlatformNumber = 0;
                }

                jumpsLeftTextObject.GetComponent<Text> ().text = "Jumps Left: " + (maxJumps - currentJump).ToString ();
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.tag == "Food Item") {
            SoundManager.Instance.PlayOneShot (SoundManager.Instance.pickUp);
            collision.gameObject.SetActive (false);

            ScoreManager.Instance.UpdateScore (1);
        }
    }

    /// <summary>
    /// Shows the max jump amount
    /// </summary>
    private void ShowMaxJump() {
        maxJumpTextObject.GetComponent<Text> ().text = "Max Jumps: " + maxJumps.ToString ();
        maxJumpTextObject.SetActive (true);

        Animator maxJumpTextObjectAnime = maxJumpTextObject.GetComponent<Animator> ();
        maxJumpTextObjectAnime.Play ("InOut");

        if (maxJumpTextObjectAnime.GetCurrentAnimatorStateInfo (0).IsName ("InOut")) {
            maxJumpTextObject.SetActive (false);
        }
    }

    /// <summary>
    /// Makes player move.
    /// </summary>
    private void Move () {
        float x = transform.position.x + dis * Time.deltaTime;
        transform.position = new Vector3 (x, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Makes object fall.
    /// </summary>
    private void Gravity () {
        rb.velocity = rb.velocity + Vector2.down;
    }

    /// <summary>
    /// Makes player jump.
    /// </summary>
    private void Jump () {
        SoundManager.Instance.PlayOneShot (SoundManager.Instance.jump);

        rb.velocity = rb.velocity + Vector2.up * thrust;
    }

    private bool AbleToJump() {
        if (currentJump < maxJumps) {
            currentJump++;
            jumpsLeftTextObject.GetComponent<Text> ().text = "Jumps Left: " + (maxJumps - currentJump).ToString ();
            return true;
        } else return false;
    }

    /// <summary>
    /// Controls for mobile devices.
    /// </summary>
    private void MobileControls () {
        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
            if (AbleToJump()) {
                HideInstructions ();
                Jump ();
            }
        }
    }

    /// <summary>
    /// Controls for desktop devices.
    /// </summary>
    private void DesktopControls () {
        if (Input.GetKeyDown (KeyCode.Space)) {
            if (AbleToJump ()) {
                HideInstructions ();
                Jump ();
            }
        }
    }

    /// <summary>
    /// Hides the instructions text.
    /// </summary>
    private void HideInstructions() {
        if (instructions.activeSelf) {
            ShowMaxJump ();
            instructions.SetActive (false);
        }
    }

    /// <summary>
    /// If player collides at top of a gameobject.
    /// </summary>
    /// <param name="collision">Collisions collider</param>
    /// <returns>If collide for not</returns>
    private bool CheckColTop (Collision2D collision) {
        if (collision.contacts.Length > 0) {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector3.Dot (contact.normal, Vector3.up) > 0.5) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    /// <summary>
    /// When the player dies.
    /// </summary>
    private void End (GameObject enemy = null) {
        SoundManager.Instance.PlayOneShot (SoundManager.Instance.death);
        gameObject.SetActive (false);
        if (enemy != null) {
            enemy.SetActive (false);
        }
        GameManager.Instance.EndGame ();
    }

}
