using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ImageController : MonoBehaviour {
    private static int numInstances = 0;
    private static string[] files = new string[] {"/home/raph/metallica-1.jpg", "/home/raph/metallica-45.jpg", "/home/raph/metallica-3.jpg", "/home/raph/metallica-24.jpg"};
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Vector3 defaultRot;
    public float maxSpeed = 50f;

	// Use this for initialization
	void Start () {
		defaultRot = transform.eulerAngles;
		rb = GetComponent<Rigidbody2D> ();
    float dirX = (float) Random.Range(0, 1);
    dirX = 2f*dirX-1f;
    float dirY = (float) Random.Range(0, 1);
    dirY = 2f*dirY-1f;
		rb.AddForce(new Vector2 (dirX*Random.Range(0f, maxSpeed*50), dirY*Random.Range(0f, maxSpeed*50)));

		Image img = GetComponent<Image> ();
		img.enabled = true;
		img.sprite = IMG2Sprite.instance.LoadNewSprite(files[numInstances % files.Length]);
    Vector3 scale;
    if (img.sprite.texture.width > img.sprite.texture.height) {
        scale = new Vector3(1f, (float)img.sprite.texture.height / (float)img.sprite.texture.width, 1f);
    } else {
        scale = new Vector3((float)img.sprite.texture.width / (float)img.sprite.texture.height, 1f, 1f);
    }
		img.transform.localScale = scale;
		bc = GetComponent<BoxCollider2D> ();
		bc.transform.localScale = scale;
		RectTransform r = GetComponent<RectTransform> ();
    r.sizeDelta -= new Vector2(20, 20);
		numInstances += 1;
	}

	public void SetSize(float width, float height){
		RectTransform r = GetComponent<RectTransform> ();
		r.sizeDelta = new Vector2 (width, height);
		GetComponent<BoxCollider2D> ().size = new Vector2 (width, height);
	}

	public void SetPos(Vector3 pos) {
		RectTransform r = GetComponent<RectTransform> ();
		r.transform.position = pos;
	}

	void FixedUpdate() {
		//rb.AddForce (new Vector3 (dir, 0, 0));
	}
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = defaultRot;
		rb.velocity = new Vector2 (Mathf.Min (rb.velocity.x, maxSpeed), Mathf.Min (rb.velocity.y, maxSpeed));
	}


}
