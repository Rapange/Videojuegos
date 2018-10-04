using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

	// Use this for initialization
	int direction;
	public float m_speed;
	public Animator anim;
	public Transform death;
	GameObject master;
	bool dead;
	
	void Start () {
		direction = 3;
		dead = false;
		master = GameObject.Find("Master");
	}
	
	// Update is called once per frame
	void Update () {
		if(direction == 1) transform.position = new Vector2(transform.position.x, transform.position.y+m_speed*Time.deltaTime);
		else if(direction == 2) transform.position = new Vector2(transform.position.x+m_speed*Time.deltaTime, transform.position.y);
		else if(direction == 3) transform.position = new Vector2(transform.position.x, transform.position.y-m_speed*Time.deltaTime);
		else if(direction == 4) transform.position = new Vector2(transform.position.x-m_speed*Time.deltaTime, transform.position.y);
	}
	
	void OnCollisionStay2D(Collision2D col)
	{
		int nDirection;
		while((nDirection = Random.Range(1,5)) == direction){}
		direction = nDirection;
		anim.SetInteger("direction",direction);
		
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("Explosion"))
		{
			if(!dead){
				master.GetComponent<Master>().enemies--;
				Instantiate(death,transform.position,Quaternion.identity);
				dead = true;
				Destroy(gameObject);
			}
		}
	}
}
