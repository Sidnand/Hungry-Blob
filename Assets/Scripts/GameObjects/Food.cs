using UnityEngine;

public class Food : MonoBehaviour {

    Animator anime;

    private void Start () {
        anime = GetComponent<Animator> ();

        if (gameObject.name == "Item 0") anime.Play ("Float1", -1, Random.Range (0.0f, 1.0f));
        if (gameObject.name == "Item 1") anime.Play ("Float2", -1, Random.Range (0.0f, 1.0f));
        if (gameObject.name == "Item 2") anime.Play ("Float3", -1, Random.Range (0.0f, 1.0f));
        if (gameObject.name == "Item 3") anime.Play ("Float4", -1, Random.Range (0.0f, 1.0f));
    }

}
