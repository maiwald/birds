using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioClip clip_idle_1;
	public AudioClip clip_idle_2;

	public AudioClip clip_agitated_1;
	public AudioClip clip_agitated_2;
	public AudioClip clip_agitated_3_background;

	private AudioSource idle_1;
	private AudioSource idle_2;
	
	private AudioSource agitated_1;
	private AudioSource agitated_2;
	private AudioSource agitated_3_background;

	private Main main;

	// Use this for initialization
	void Start () {
		main = Camera.main.GetComponent ("Main") as Main;

		idle_1 = AddAudio (clip_idle_1, 0.5f);
		idle_2 = AddAudio (clip_idle_2, 0.5f);

		agitated_1 = AddAudio (clip_agitated_1, 0.4f);
		agitated_2 = AddAudio (clip_agitated_2, 0.3f);
		agitated_3_background = AddAudio (clip_agitated_3_background);

	}
	
	// Update is called once per frame
	void Update () {
		if (main.IsIdle ()) {
			if (!idle_1.isPlaying) fadeIn(idle_1, 0.5f);
			if (!idle_2.isPlaying) fadeIn(idle_2, 0.5f);
			
			if (agitated_1.isPlaying) fadeOut(agitated_1);
			if (agitated_2.isPlaying) fadeOut(agitated_2);
			if (agitated_3_background.isPlaying) fadeOut(agitated_3_background);
		} else {
			if (idle_1.isPlaying) fadeOut(idle_1);
			if (idle_2.isPlaying) fadeOut(idle_2);
			
			if (!agitated_1.isPlaying && (Time.time % 3f > 2.9f)) {
				fadeIn(agitated_1, 0.4f);
			}
			if (!agitated_2.isPlaying && (Time.time % 4f > 3.9f)) {
				fadeIn(agitated_2, 0.3f);
			}
			if (!agitated_3_background.isPlaying) {
				fadeIn(agitated_3_background, 1f);
			}
		}
	}

	void fadeIn (AudioSource audioSource, float limit = 1f) {
		if (!audioSource.isPlaying)
						audioSource.Play ();

		if (audioSource.volume < limit) {
						audioSource.volume += Time.deltaTime;
				}


	}

	void fadeOut (AudioSource audioSource) {
		if (audioSource.volume > 0) {
						audioSource.volume -= Time.deltaTime;	
				} else {
						audioSource.Stop ();
				}
	}

	AudioSource AddAudio(AudioClip clip, float volume = 1.0f) {
		AudioSource audioSource = (AudioSource) gameObject.AddComponent("AudioSource");
		audioSource.clip = clip;
		audioSource.volume = volume;
		return audioSource;
	}
}
