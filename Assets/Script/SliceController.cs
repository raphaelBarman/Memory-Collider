using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliceController : MonoBehaviour {
	private RectTransform canvasRect;
	public GameObject image;

	void createBorders(RectTransform canvasRect) {
		GameObject borders = new GameObject ();
		borders.name = "Borders";
		borders.transform.SetParent (canvasRect.transform);
		for (int dir = 1; dir > -2; dir -= 2) {
			for (int sel = 0; sel < 2; sel++) {
				GameObject border = new GameObject ();
				border.name = "border";
				border.transform.SetParent (borders.transform);
				border.transform.position = canvasRect.position + new Vector3 (sel*dir*(canvasRect.rect.width /2f + 5f), (1-sel)*dir*(canvasRect.rect.height/2f + 5f), 0);
				border.AddComponent<BoxCollider2D> ();
				border.GetComponent<BoxCollider2D> ().size = new Vector2 ((canvasRect.rect.width*(1-sel) + 10 * sel), (canvasRect.rect.height*sel + 10 * (1-sel)));
			}

		}

	
	}

	void spawnImages(RectTransform canvasRect, string[] filenames) {
		GameObject images = new GameObject ();
		images.name = "Images";
		images.transform.SetParent (canvasRect.transform);
    float available_size = (1-(filenames.Length+1)*0.01f)*Mathf.Min(canvasRect.rect.width, canvasRect.rect.height);
    float min_width = available_size/8;
    float max_width = available_size/2;

    //SquaresTree sqt = new SquaresTree(canvasRect.rect.width, canvasRect.rect.height, max_width, min_width);
    SquaresTree sqt = new SquaresTree(canvasRect.rect.width, canvasRect.rect.height, 600, 70, canvasRect.position);

		for (int i = 0; i < filenames.Length; i++) {
        SquareCell sqc = sqt.getSquare();
        float size = sqc.side;
        GameObject img = Instantiate (image, sqc.center, Quaternion.identity);
        img.name = string.Format ("image{0}", i);
        img.transform.SetParent (images.transform);
        img.GetComponent<RectTransform> ();
        (img.GetComponent<ImageController>()).SetSize (size, size);
		}
	}
	// Use this for initialization
	void Start () {
		RectTransform canvasRect = GetComponent<RectTransform> ();
		createBorders (canvasRect);
		spawnImages (canvasRect, new string[20]);
    new SquaresAssigner(100, 100, 50, 10, new Vector2(0,0));
    //SquaresTree sq = new SquaresTree(8, 8, 4, 1);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
