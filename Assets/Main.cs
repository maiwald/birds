using UnityEngine;
using System.Linq;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class Main : MonoBehaviour {

    public Transform bird;
	public int numberOfBirds;

	public float circleTimeout = 0;
	public bool circling = false;

	private bool idle = true;
	private Thread t;

	private SerialPort arduino = new SerialPort("COM3", 9600);

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        for (int i = 0; i < numberOfBirds; i++) {
            Instantiate(bird);
        }
		
		arduino.Open ();

		t = new Thread (new ThreadStart (ArduinoHandler));
		t.Start ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

		circling = (Time.realtimeSinceStartup - circleTimeout) > 4;
	}

	void OnDestroy() {
		this._shouldKillHandler = true;
		t.Join ();
	}

	public void waitForApproach() {
		circleTimeout = Time.realtimeSinceStartup;
	}
	
	private bool _shouldKillHandler = false;
	private void ArduinoHandler()
	{
		while (t.IsAlive && !_shouldKillHandler) {
			try {
				this.idle = !(arduino.ReadLine ().Equals ("0"));
			} catch (System.Exception) {}
		}
	}
	
	public bool IsIdle() {
		return this.idle;
	}
}
