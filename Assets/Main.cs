using UnityEngine;
using System.Linq;
using System.Collections;

public class Main : MonoBehaviour {

    public Transform bird;
	public int numberOfBirds;

	public float circleTimeout = 0;
	public bool circling = false;

	public Vector3 TopLeft;
	public Vector3 TopRight;
	public Vector3 BottomRight;
	public Vector3 BottomLeft;

	public int trackedJoint = (int) KinectWrapper.NuiSkeletonPositionIndex.Spine;
	private GameObject obstacle;

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        for (int i = 0; i < numberOfBirds; i++) {
            Instantiate(bird);
        }

		obstacle = GameObject.Find ("Obstacle");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

		circling = (Time.realtimeSinceStartup - circleTimeout) > 8;
	}

	public void waitForApproach() {
		circleTimeout = Time.realtimeSinceStartup;
	}

	public bool IsIdle() {
		return Input.GetMouseButton (0);
	}
}
