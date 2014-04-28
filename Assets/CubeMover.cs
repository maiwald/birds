using UnityEngine;
using System.Collections;

public class CubeMover : MonoBehaviour {

	private Camera camera;
	private Vector3 position;

	// Use this for initialization
	void Start () {
		camera = Camera.main;
		position = transform.position;
	}

	// Update is called once per frame
	void Update () {
		Vector3 followPosition = Input.mousePosition;

		logPosition("object world", position);
		logPosition("mouse world", camera.ScreenToWorldPoint(followPosition));
		logPosition("screen point", followPosition);
		logPosition("screen object", camera.WorldToScreenPoint(position));

		gameObject.transform.position = new Vector3(
			camera.ScreenToWorldPoint(followPosition).x,
			camera.ScreenToWorldPoint(followPosition).y,
			0);
	}

	void logPosition(string message, Vector3 p) {
		Debug.Log (message + " " + p.x + ", " + p.y + ", " + p.z);
	}
}
