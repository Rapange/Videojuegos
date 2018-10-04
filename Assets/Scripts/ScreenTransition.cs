using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransition : MonoBehaviour {

	// Use this for initialization
	Color color;
	public float waitTime, speed;
	public bool start,midStart;
	GameObject UIMaster;
	void Start () {
		UIMaster = GameObject.Find("UI Master");
	}
	
	// Update is called once per frame
	void Update () {
		if(start){
			color = gameObject.GetComponent<SpriteRenderer>().color;
			color.a += 2.0f;
			gameObject.GetComponent<SpriteRenderer>().color = color;
			if(color.a >= 1.0f){
				start = false;
				midStart = true;
			}
		}
		else if(midStart){
			color = gameObject.GetComponent<SpriteRenderer>().color;
			color.a -= speed*Time.deltaTime;
			gameObject.GetComponent<SpriteRenderer>().color = color;
			if(color.a <= 0.0f){
				midStart = false;
			}
		}
		else{
			color = gameObject.GetComponent<SpriteRenderer>().color;
			color.a = 0.0f;
			gameObject.GetComponent<SpriteRenderer>().color = color;
		}
	}
	
	
}
