using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Music : MonoBehaviour {

	// Use this for initialization
	float timer;
	bool flag;
	void Start () {
		flag = false;
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
		if(timer > 0.7f && !flag)
		{
			gameObject.GetComponent<AudioSource>().Play();
			flag = true;
		}
	}
}
