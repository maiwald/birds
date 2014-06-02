using UnityEngine;
using System.Linq;
using System.Collections;

public class Main : MonoBehaviour {

    public Transform bird;
	public int numberOfBirds;

	public float circleTimeout = 0;
	public bool circling = false;


	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        for (int i = 0; i < numberOfBirds; i++) {
            Instantiate(bird);
        }
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
}
