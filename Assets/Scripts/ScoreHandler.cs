using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour {

	// Use this for initialization
	float timer, originalY;
	GameObject scoreBoard;
	void Start () {
		timer = 0;
		originalY = transform.position.y;
		scoreBoard = GameObject.Find("Bomberman2_score");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > 2.0f){
			Destroy(gameObject);
			scoreBoard.GetComponent<Score>().score += 100;
		}
		if(transform.position.y - originalY <= 0.5f)
			transform.position = new Vector2(transform.position.x, transform.position.y + 0.7f*Time.deltaTime);
	}
}
