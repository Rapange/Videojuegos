using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenHandler : MonoBehaviour {

	// Use this for initialization
	Color color;
	public float waitTime, speed;
	public Transform Bomberman;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		waitTime -= Time.deltaTime;
		if(waitTime < 0.0f)
		{
			color = gameObject.GetComponent<SpriteRenderer>().color;
			color.a -= speed*Time.deltaTime;
			gameObject.GetComponent<SpriteRenderer>().color = color;
			if(color.a <= 0.0f){
				//Destroy(gameObject);
				if(Bomberman != null) Bomberman.GetComponent<Player>().canPlay = true;
			}
		}
		
		
	}
}
