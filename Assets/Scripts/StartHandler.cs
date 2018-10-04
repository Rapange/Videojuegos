using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHandler : MonoBehaviour {

	// Use this for initialization
	public float speed, timeInMiddle;
	GameObject master;
	void Start () {
		master = GameObject.Find("Master");
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < 6.0f && timeInMiddle > 0.0f)
			timeInMiddle -= Time.deltaTime;			
		else transform.position = new Vector2(transform.position.x - speed*Time.deltaTime, transform.position.y);
		
		if(transform.position.x < -7.0f)
		{
			master.GetComponents<AudioSource>()[0].Play();
			Destroy(gameObject);
		}
	}
}
