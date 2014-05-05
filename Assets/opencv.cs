using UnityEngine;
using System.Collections;
using OpenCvSharp;


public class opencv : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CvMat src = new CvMat("lenna.png", LoadMode.GrayScale);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
