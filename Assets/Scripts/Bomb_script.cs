using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb_script : MonoBehaviour {

	public Transform exp_mid, exp_up, exp_down, exp_left, exp_right, exp_hor, exp_ver;
	GameObject master;
	public GameObject bomberman_owner;
	public int strength;
	public bool exploded;
	public float timer;
	// Use this for initialization
	void Start () {
		timer = 0;
		exploded = false;
		master = GameObject.Find("Master");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer >= 2.0f)
			explode();
	}
	
	public void reposition(){
		transform.position = new Vector2((int)((transform.position.x+0.4f)/0.8f)*0.8f,(int)((transform.position.y+0.0f)/0.8)*0.8f);
	}
	
	GameObject isCollision(float x, float y, ref GameObject[] walls){
		for(int i = 0; i < walls.Length; i++){
			if(Math.Abs(walls[i].transform.position.x - x) < 0.005 && Math.Abs(walls[i].transform.position.y - y) < 0.005f)
				return walls[i];
		}
		return null;
	}
	
	void OnCollisionStay2D(Collision2D col)
	{
		if(col.gameObject != bomberman_owner)
		{
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,0.0f);
			//col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,0.0f);
			//reposition();
		}

	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("Item")){
			Destroy(col.gameObject);
			return;
		}
		if(col.gameObject != bomberman_owner)
		{
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,0.0f);
			//col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,0.0f);
			//reposition();
		}
	}
	
	void explode(){
		
		if((Vector2)gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero) reposition();
		//else reposition();
		
		exploded = true;
		GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
		GameObject[] d_walls = GameObject.FindGameObjectsWithTag("Wall_d");
		GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
		GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
		GameObject[] door = GameObject.FindGameObjectsWithTag("Door");
		GameObject[] switches = GameObject.FindGameObjectsWithTag("Switch");
		GameObject[] magnets = GameObject.FindGameObjectsWithTag("Magnet");
		
		GameObject[] all = new GameObject[walls.Length+d_walls.Length+bombs.Length+items.Length+door.Length+switches.Length+magnets.Length];
		for(int i = 0; i < walls.Length;i++)
			all[i] = walls[i];
		for(int i = 0; i < d_walls.Length;i++)
			all[i+walls.Length] = d_walls[i];
		for(int i = 0; i < bombs.Length; i++)
			all[i+walls.Length+d_walls.Length] = bombs[i];
		for(int i = 0; i < items.Length; i++)
			all[i+walls.Length+d_walls.Length+bombs.Length] = items[i];
		for(int i = 0; i < door.Length; i++)
			all[i+walls.Length+d_walls.Length+bombs.Length+items.Length] = door[i];
		for(int i = 0; i < switches.Length; i++)
			all[i+walls.Length+d_walls.Length+bombs.Length+items.Length+door.Length] = switches[i];
		for(int i = 0; i < magnets.Length; i++)
			all[i+walls.Length+d_walls.Length+bombs.Length+items.Length+door.Length+switches.Length] = magnets[i];
		
		GameObject go;
		bool collided = false;
		
		float x,y;
		x = transform.position.x;
		y = transform.position.y;
		Instantiate(exp_mid,transform.position,Quaternion.identity);
		
		
		for(int i = 1; i < strength; i++){
			go = isCollision(x,y+0.8f*i,ref all);
			if(go != null) {
				if(go.tag == "Bomb" && !go.GetComponent<Bomb_script>().exploded) go.GetComponent<Bomb_script>().timer = 1.9f;//go.GetComponent<Bomb_script>().explode();
				else if(go.tag == "Wall_d"){
					go.GetComponent<Animator>().SetTrigger("destroy");
					go.GetComponent<destroyHandler>().autoDestroy();
				}
				else if(go.tag == "Item") go.GetComponent<item_script>().explode();
				else if(go.tag == "Switch")
				{
					go.GetComponent<Animator>().SetTrigger("activate");
					Instantiate(exp_ver,new Vector2(x,y+0.8f*i),Quaternion.identity);
					master.GetComponent<Master>().switches--;
					continue;
				}
				else if(go.tag == "Magnet"){
					go.transform.Rotate(Vector3.forward*-90);
					go.GetComponent<Magnet>().direction = (go.GetComponent<Magnet>().direction)%4+1;
				}
				else if(go.tag == "Door" && !master.GetComponent<Master>().win){
					master.GetComponent<Master>().SpawnEnemies();
				}
				collided = true;
				break;
			}
			
			Instantiate(exp_ver,new Vector2(x,y+0.8f*i),Quaternion.identity);
		}
		if(!collided){
			go = isCollision(x,y+0.8f*strength,ref all);
			if(go == null){
				Instantiate(exp_up,new Vector2(x,y+0.8f*strength),Quaternion.identity);
			}
			else if(go.tag=="Bomb" && !go.GetComponent<Bomb_script>().exploded) go.GetComponent<Bomb_script>().timer = 1.9f;//go.GetComponent<Bomb_script>().explode();
			else if(go.tag == "Wall_d"){
				go.GetComponent<Animator>().SetTrigger("destroy");
				go.GetComponent<destroyHandler>().autoDestroy();
			}
			else if(go.tag == "Item") go.GetComponent<item_script>().explode();
			else if(go.tag == "Switch")
			{
				go.GetComponent<Animator>().SetTrigger("activate");
				Instantiate(exp_up,new Vector2(x,y+0.8f*strength),Quaternion.identity);
				master.GetComponent<Master>().switches--;
			}
			else if(go.tag == "Magnet"){
				go.transform.Rotate(Vector3.forward*-90);
				go.GetComponent<Magnet>().direction = (go.GetComponent<Magnet>().direction)%4+1;
			}
			else if(go.tag == "Door" && !master.GetComponent<Master>().win){
				master.GetComponent<Master>().SpawnEnemies();
			}
		}
		collided = false;
		
		
		for(int i = 1; i < strength; i++){
			go = isCollision(x,y-0.8f*i,ref all);
			if(go != null) {
				if(go.tag == "Bomb" && !go.GetComponent<Bomb_script>().exploded) go.GetComponent<Bomb_script>().timer = 1.9f;//go.GetComponent<Bomb_script>().explode();
				else if(go.tag == "Wall_d"){
					go.GetComponent<Animator>().SetTrigger("destroy");
					go.GetComponent<destroyHandler>().autoDestroy();
				}
				else if(go.tag == "Item") go.GetComponent<item_script>().explode();
				else if(go.tag == "Switch")
				{
					go.GetComponent<Animator>().SetTrigger("activate");
					Instantiate(exp_ver,new Vector2(x,y-0.8f*i),Quaternion.identity);
					master.GetComponent<Master>().switches--;
					continue;
				}
				else if(go.tag == "Magnet"){
					go.transform.Rotate(Vector3.forward*-90);
					go.GetComponent<Magnet>().direction = (go.GetComponent<Magnet>().direction)%4+1;
				}
				collided = true;
				break;
			}
			Instantiate(exp_ver,new Vector2(x,y-0.8f*i),Quaternion.identity);
		}
		if(!collided){
			go = isCollision(x,y-0.8f*strength,ref all);
			if(go == null){
				Instantiate(exp_down,new Vector2(x,y-0.8f*strength),Quaternion.identity);
			}
			else if(go.tag == "Bomb" && !go.GetComponent<Bomb_script>().exploded) go.GetComponent<Bomb_script>().timer = 1.9f;//go.GetComponent<Bomb_script>().explode();
			else if(go.tag == "Wall_d"){
				go.GetComponent<Animator>().SetTrigger("destroy");
				go.GetComponent<destroyHandler>().autoDestroy();
			}
			else if(go.tag == "Item") go.GetComponent<item_script>().explode();
			else if(go.tag == "Switch")
			{
				go.GetComponent<Animator>().SetTrigger("activate");
				Instantiate(exp_down,new Vector2(x,y-0.8f*strength),Quaternion.identity);
				master.GetComponent<Master>().switches--;
			}
			else if(go.tag == "Magnet"){
				go.transform.Rotate(Vector3.forward*-90);
				go.GetComponent<Magnet>().direction = (go.GetComponent<Magnet>().direction)%4+1;
			}
		}
		collided = false;
		
		for(int i = 1; i < strength; i++){
			go = isCollision(x+i*0.8f,y,ref all);
			if(go != null) { 
				if(go.tag == "Bomb" && !go.GetComponent<Bomb_script>().exploded) go.GetComponent<Bomb_script>().timer = 1.9f;//go.GetComponent<Bomb_script>().explode();
				else if(go.tag == "Wall_d"){
					go.GetComponent<Animator>().SetTrigger("destroy");
					go.GetComponent<destroyHandler>().autoDestroy();
				}
				else if(go.tag == "Item") go.GetComponent<item_script>().explode();
				else if(go.tag == "Switch")
				{
					go.GetComponent<Animator>().SetTrigger("activate");
					Instantiate(exp_hor,new Vector2(x+i*0.8f,y),Quaternion.identity);
					master.GetComponent<Master>().switches--;
					continue;
				}
				else if(go.tag == "Magnet"){
					go.transform.Rotate(Vector3.forward*-90);
					go.GetComponent<Magnet>().direction = (go.GetComponent<Magnet>().direction)%4+1;
				}
				collided = true;
				break;
			}
			Instantiate(exp_hor,new Vector2(x+i*0.8f,y),Quaternion.identity);
		}	
		if(!collided){
			go = isCollision(x+0.8f*strength,y,ref all);
			if(go == null){
				Instantiate(exp_right,new Vector2(x+0.8f*strength,y),Quaternion.identity);
			}
			else if(go.tag == "Bomb" && !go.GetComponent<Bomb_script>().exploded) go.GetComponent<Bomb_script>().timer = 1.9f;//go.GetComponent<Bomb_script>().explode();
			else if(go.tag == "Wall_d"){
				go.GetComponent<Animator>().SetTrigger("destroy");
				go.GetComponent<destroyHandler>().autoDestroy();
			}
			else if(go.tag == "Item") go.GetComponent<item_script>().explode();
			else if(go.tag == "Switch")
			{
				go.GetComponent<Animator>().SetTrigger("activate");
				Instantiate(exp_right,new Vector2(x+0.8f*strength,y),Quaternion.identity);
				master.GetComponent<Master>().switches--;
			}
			else if(go.tag == "Magnet"){
				go.transform.Rotate(Vector3.forward*-90);
				go.GetComponent<Magnet>().direction = (go.GetComponent<Magnet>().direction)%4+1;
			}
			
		}
		collided = false;
		
		
		for(int i = 1; i < strength; i++){
			go = isCollision(x-i*0.8f,y,ref all);
			if(go != null) {
				
				if(go.tag == "Bomb" && !go.GetComponent<Bomb_script>().exploded) go.GetComponent<Bomb_script>().timer = 1.9f;//go.GetComponent<Bomb_script>().explode();
				else if(go.tag == "Wall_d"){
					go.GetComponent<Animator>().SetTrigger("destroy");
					go.GetComponent<destroyHandler>().autoDestroy();
				}
				else if(go.tag == "Item") go.GetComponent<item_script>().explode();
				else if(go.tag == "Switch")
				{
					go.GetComponent<Animator>().SetTrigger("activate");
					Instantiate(exp_hor,new Vector2(x-i*0.8f,y),Quaternion.identity);
					master.GetComponent<Master>().switches--;
					continue;
				}
				else if(go.tag == "Magnet"){
					go.transform.Rotate(Vector3.forward*-90);
					go.GetComponent<Magnet>().direction = (go.GetComponent<Magnet>().direction)%4+1;
				}
				collided = true; 
				break;
			}
			Instantiate(exp_hor,new Vector2(x-i*0.8f,y),Quaternion.identity);
		}
		if(!collided){
			go = isCollision(x-0.8f*strength,y,ref all);
			if(go == null){
				Instantiate(exp_left,new Vector2(x-0.8f*strength,y),Quaternion.identity);
			}
			else if(go.tag == "Bomb" && !go.GetComponent<Bomb_script>().exploded) go.GetComponent<Bomb_script>().timer = 1.9f;//go.GetComponent<Bomb_script>().explode();
			else if(go.tag == "Wall_d"){
				go.GetComponent<Animator>().SetTrigger("destroy");
				go.GetComponent<destroyHandler>().autoDestroy();
			}
			else if(go.tag == "Item") go.GetComponent<item_script>().explode();
			else if(go.tag == "Switch")
			{
				go.GetComponent<Animator>().SetTrigger("activate");
				Instantiate(exp_left,new Vector2(x-0.8f*strength,y),Quaternion.identity);
				master.GetComponent<Master>().switches--;
			}
			else if(go.tag == "Magnet"){
				go.transform.Rotate(Vector3.forward*-90);
				go.GetComponent<Magnet>().direction = (go.GetComponent<Magnet>().direction)%4+1;
			}
		}
		collided = false;
		
		if(bomberman_owner != null) bomberman_owner.GetComponent<Player>().bombs_placed--;
		Destroy(gameObject);
	}
}
