using UnityEngine;
using System.Collections;

public class BirdAnimator : MonoBehaviour {

	public Sprite[] flying;
	public Sprite[] turning;
	public Sprite[] approaching;
	public int framesPerSecond = 15;

	private Sprite[] currentSprites;
	private SpriteRenderer spriteRenderer;
	private int randomizedFramesPerSecond;
    private int baseTime;

	// Use this for initialization
	void Start () {
		spriteRenderer = renderer as SpriteRenderer;
		currentSprites = flying;
        randomizedFramesPerSecond = Random.Range(framesPerSecond - 3, framesPerSecond + 3);
        baseTime = Random.Range(0, Random.Range(framesPerSecond - 3, framesPerSecond + 3));
	}

	// Update is called once per frame
	void Update () {
        int index = (int)(baseTime + Time.timeSinceLevelLoad * randomizedFramesPerSecond);
		index = index % currentSprites.Length;
		spriteRenderer.sprite = currentSprites[ index ];

		if (currentSprites == turning && index == currentSprites.Length - 1) {
			currentSprites = approaching;
		}
	}

	public void StartApproach() {
		currentSprites = approaching;	
	}

	void StartFlying() {
		currentSprites = flying;
	}
}
