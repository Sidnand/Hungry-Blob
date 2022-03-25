using UnityEngine;

public class Enemy : MonoBehaviour {

    SpriteRenderer sp;

    // Travel distance.
    float dis;

    private void Start () {
        sp = GetComponent<SpriteRenderer> ();

        SetDis ();
        Flip ();
    }

    private void Update () {
        if (GameManager.Instance.start) {
            Move ();
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Enemy Collider") {
            dis *= -1;
            Flip ();
        }
    }

    /// <summary>
    /// Sets a random distance travel.
    /// </summary>
    private void SetDis () {
        int rand = 0;
        while (rand < 2 && rand > -2) rand = Random.Range (-4, 4);
        dis = rand;
    }

    /// <summary>
    /// Flips direciton of sprite.
    /// </summary>
    private void Flip () {
        if (dis > 0) sp.flipX = true;
        else if (dis < 0) sp.flipX = false;
    }

    /// <summary>
    /// Makes enemy move.
    /// </summary>
    private void Move () {
        float x = transform.position.x + dis * Time.deltaTime;
        transform.position = new Vector3 (x, transform.position.y, transform.position.z);
    }

}
