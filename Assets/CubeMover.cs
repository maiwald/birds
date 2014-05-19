using UnityEngine;
using System.Collections;

public class CubeMover : MonoBehaviour {

	private Camera mainCamera;

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log (col.gameObject.name);
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Trigger: " + collider2D.isTrigger);

        mainCamera = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		Vector3 followPosition = Input.mousePosition;

		gameObject.transform.position = new Vector3(
            mainCamera.ScreenToWorldPoint(followPosition).x,
            mainCamera.ScreenToWorldPoint(followPosition).y,
			0);
	}
}
