using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip[] bgmClipArray;
	public AudioClip[] seClipArray;
	private AudioSource mBGMAudioSource;
	private AudioSource[] mSEAudioSourceArray;
	private static SoundManager sInstance;

	// Use this for initialization
	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		mBGMAudioSource = gameObject.AddComponent<AudioSource> ();
		mBGMAudioSource.loop = true;
		mSEAudioSourceArray = new AudioSource[seClipArray.Length];
		for (int i = 0; i < mSEAudioSourceArray.Length; i++) {
			mSEAudioSourceArray[i] = gameObject.AddComponent<AudioSource> ();
			mSEAudioSourceArray[i].clip = seClipArray[i];
		}
	}

	public static SoundManager Instance{
		get{
			return sInstance;
		}
	}

	public void PlayBGM(int bgmClipId){
		mBGMAudioSource.Stop();
		mBGMAudioSource.clip = bgmClipArray[bgmClipId];
		mBGMAudioSource.Play();
	}

	public void PlaySE (int seClipId) {
		mSEAudioSourceArray[seClipId].Play();
	}

}
