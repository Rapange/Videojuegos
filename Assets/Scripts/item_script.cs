using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_script : MonoBehaviour {

	// Use this for initialization
	public Transform exp_item;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void explode(){
		Instantiate(exp_item,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
