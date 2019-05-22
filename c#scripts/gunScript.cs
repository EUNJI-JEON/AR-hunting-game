using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour {

	AudioSource audio;
	public static gunScript instance;

	//raycast go through the instance on the script and call the final sound
	void Awake(){
		if(instance == null){
			instance =this;
		}
	}

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}

	public void fireSound(){
		audio.Play();
	}
	

}
