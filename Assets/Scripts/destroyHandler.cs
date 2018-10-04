using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyHandler : MonoBehaviour {

	// Use this for initialization
	public float timer;
	public bool run;
	public bool drop, givesScore;
	bool scoreGiven;
	public Transform item_bomb, item_powa, item_boot, score;
	void Start () {
		if(run) autoDestroy();
		scoreGiven = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void autoDestroy(){
		print(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
		if(drop) Invoke("getItem",this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - timer);
		if(givesScore && !scoreGiven){
			Invoke("getScore",this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - timer);
			scoreGiven = true;
		}
		Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - timer);
	}
	
	public void getItem(){
		if(Random.Range(0,500) < 250) return;
		int nItem = Random.Range(0,3);
		if(nItem == 0) Instantiate(item_bomb,transform.position,Quaternion.identity);
		else if(nItem == 1) Instantiate(item_powa,transform.position,Quaternion.identity);
		else if(nItem == 2) Instantiate(item_boot,transform.position,Quaternion.identity);
	}
	
	public void getScore(){
		Instantiate(score,transform.position,Quaternion.identity);
	}
}
