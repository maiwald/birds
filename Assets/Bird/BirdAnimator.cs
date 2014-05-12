using UnityEngine;
using System.Collections;

public class BirdAnimator : MonoBehaviour {

	public Sprite[] sprites;
	public int framesPerSecond = 15;

	private SpriteRenderer spriteRenderer;
    private int randomizedFramesPerSecond;
    private int baseTime;

	// Use this for initialization
	void Start () {
		spriteRenderer = renderer as SpriteRenderer;
        randomizedFramesPerSecond = Random.Range(framesPerSecond - 3, framesPerSecond + 3);
        baseTime = Random.Range(0, Random.Range(framesPerSecond - 3, framesPerSecond + 3));
        Debug.Log(baseTime);
	}
	
	// Update is called once per frame
	void Update () {
        int index = (int)(baseTime + Time.timeSinceLevelLoad * randomizedFramesPerSecond);
		index = index % sprites.Length;
		spriteRenderer.sprite = sprites[ index ];
	}
}
