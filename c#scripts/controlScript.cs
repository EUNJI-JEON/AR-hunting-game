using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlScript : MonoBehaviour {

	AudioSource audio;
	public AudioClip[] clips;

	void Start()
	{
		audio = GetComponent<AudioSource>();
		StartCoroutine(introJingle());

	}

	private void playSound(int sound){
		audio.clip = clips[sound];
		audio.Play();
	}

	private IEnumerator introJingle(){
		yield return new WaitForSeconds(3f);
		playSound(0);
		StartCoroutine(quack());
	}
	//introJingle 사운드 끝날 때까지 기다렸다가 끝나면 quack 사운드
	private IEnumerator quack(){
		yield return new WaitForSeconds(1.8f);
		playSound(1);
		StartCoroutine(dog());
	}

	private IEnumerator dog(){
		yield return new WaitForSeconds(0.5f);
		playSound(2);
		StartCoroutine(gunShot());
	}

	private IEnumerator gunShot(){
		yield return new WaitForSeconds(0.4f);
		playSound(3);
	}

	public void ChangeScene(){
		SceneManager.LoadScene("Main");
	}
}
