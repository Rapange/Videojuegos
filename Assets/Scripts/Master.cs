using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour {

	// Use this for initialization
	public int switches, enemies;
	public Transform door1, door2, door3, door4, entrance1, entrance2, enemy;

	public bool win;
	
	int totalEnemies;
	void Start () {
		totalEnemies = enemies;
	}
	
	// Update is called once per frame
	void Update () {
		if(switches <= 0 && enemies <= 0){
			door1.GetComponent<Animator>().SetTrigger("open");
			door2.GetComponent<Animator>().SetTrigger("open");
			door3.GetComponent<Animator>().SetTrigger("open");
			door4.GetComponent<Animator>().SetTrigger("open");

			door1.GetComponent<BoxCollider2D>().isTrigger = true;
			door2.GetComponent<BoxCollider2D>().isTrigger = true;
			door3.GetComponent<BoxCollider2D>().isTrigger = true;
			door4.GetComponent<BoxCollider2D>().isTrigger = true;
			switches--;
			win = true;
		}
	}
	
	public void SpawnEnemies(){
		for(int i = enemies; i < totalEnemies; i++){
			Instantiate(enemy,new Vector2(5.6f,0.8f+i*2.4f),Quaternion.identity);
		}
		enemies = totalEnemies;
	}
}
