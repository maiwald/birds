using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public Transform bird;
	public int numberOfBirds;


	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        for (int i = 0; i < numberOfBirds; i++) {
            Instantiate(bird);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
