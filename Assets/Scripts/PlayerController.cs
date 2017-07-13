using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float keyBoardSpeed;
	public float touchSpeed;

	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();

		winText.text = "";
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		float touchX = 0;
		float touchZ = 0;
		foreach (Touch touch in Input.touches) {
			touchX += touch.deltaPosition.x;
			touchZ += touch.deltaPosition.y;
		}

		Vector3 movement;

		if (moveHorizontal != 0 || moveVertical != 0) {
			movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			movement *= keyBoardSpeed;
		} else {
			movement = new Vector3 (-touchX, 0.0f, -touchZ);
			movement *= touchSpeed;
		}

		rb.AddForce (movement);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count += 1;
			SetCountText ();
		}
	}

	void SetCountText () {
		countText.text = "Count: " + count.ToString ();

		if (count >= 12) {
			winText.text = "You Win!";
		}
	}
}
