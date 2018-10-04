using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Magnet : MonoBehaviour {

	// Use this for initialization
	public int strength, direction;
	void Start () {
		
	}
	
	Vector2 get_next(int j){
		if(direction == 1) return new Vector2(transform.position.x,transform.position.y+j*0.8f);
		if(direction == 2) return new Vector2(transform.position.x+j*0.8f,transform.position.y);
		if(direction == 3) return new Vector2(transform.position.x,transform.position.y-j*0.8f);
		return new Vector2(transform.position.x-j*0.8f, transform.position.y);
	}
	
	Vector2 attract(){
		if(direction == 1) return new Vector2(0.0f,-2.0f);
		if(direction == 2) return new Vector2(-2.0f,0.0f);
		if(direction == 3) return new Vector2(0.0f,2.0f);
		return new Vector2(2.0f,0.0f);
	}
	
	GameObject isCollision(float x, float y, ref GameObject[] walls){
		for(int i = 0; i < walls.Length; i++){
			if(Math.Abs(walls[i].transform.position.x - x) < 0.005 && Math.Abs(walls[i].transform.position.y - y) < 0.005f)
				return walls[i];
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
		GameObject[] d_walls = GameObject.FindGameObjectsWithTag("Wall_d");
		GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
		GameObject[] magnets = GameObject.FindGameObjectsWithTag("Magnet");
		
		GameObject[] all = new GameObject[walls.Length+d_walls.Length+bombs.Length+magnets.Length];
		for(int i = 0; i < walls.Length;i++)
			all[i] = walls[i];
		for(int i = 0; i < d_walls.Length;i++)
			all[i+walls.Length] = d_walls[i];
		for(int i = 0; i < bombs.Length; i++)
			all[i+walls.Length+d_walls.Length] = bombs[i];
		for(int i = 0; i < magnets.Length; i++)
			all[i+walls.Length+d_walls.Length+bombs.Length] = magnets[i];
		
		//Do not attract if there is an object between the bomb
		for(int i = 0; i < bombs.Length; i++){
			
			if(Vector2.Distance(bombs[i].transform.position,transform.position) <= 0.8f){
				bombs[i].gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				//bombs[i].gameObject.GetComponent<Bomb_script>().reposition();
			}
			for(int j = 2; j <= strength; j++){
				if((Vector2)bombs[i].transform.position == get_next(j)){
					bombs[i].gameObject.GetComponent<Rigidbody2D>().velocity = attract();
				}
			}
		}
	}
}
