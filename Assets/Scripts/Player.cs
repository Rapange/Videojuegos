using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	// Use this for initialization
	
	public Animator anim;
	public Transform bomb, bomber_die, bomber_win, pause;
	Transform placed_bomb;

	bool triggered, dead;
	public float m_speed;
	float dirX, dirY;
	int max_bombs,power;
	static int lives = 3;
	public int bombs_placed;
	public float colX,colY;
	public bool canPlay;
	int direction;
	bool isPaused;
	GameObject scoreBoard, master;
	
	void kick(GameObject bomb_to_kick){
		
		if(direction == 1) bomb_to_kick.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,5.0f);
		else if(direction == 2) bomb_to_kick.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f,0.0f);
		else if(direction == 3) bomb_to_kick.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,-5.0f);
		else if(direction == 4) bomb_to_kick.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f,0.0f);
		
	}
	
	void Start () {
		triggered = false;
		max_bombs = 1;
		power = 1;
		bombs_placed = 0;
		direction = 3;
		dead = false;
		scoreBoard = GameObject.Find("Bomberman2_score");
		master = GameObject.Find("Master");
		
		if(lives < 0){
			lives = 3;
			SceneManager.LoadScene("Retry");
		}
		
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("p"))
		{
			isPaused = !isPaused;
			if(isPaused){ Time.timeScale = 0; Instantiate(pause, new Vector2(6.4f,5.0f), Quaternion.identity);}
			else {Time.timeScale = 1; Destroy(GameObject.Find("Pause(Clone)"));}
		}
		
		if(!canPlay) return;
		
		
		
		
		dirX = Input.GetAxisRaw("Horizontal") * m_speed * Time.deltaTime;
		dirY = Input.GetAxisRaw("Vertical") * m_speed * Time.deltaTime;
		
		scoreBoard.GetComponent<Score>().fire = power;
		scoreBoard.GetComponent<Score>().bombs = max_bombs;
		scoreBoard.GetComponent<Score>().lives = lives;
		
		if(dirX > 0)
		{
			anim.SetInteger("Direction",2);
			direction = 2;
			gameObject.GetComponent<BoxCollider2D>().size = new Vector2(colY,colX);
		}
		else if(dirX < 0)
		{
			anim.SetInteger("Direction",4);
			direction = 4;
			gameObject.GetComponent<BoxCollider2D>().size = new Vector2(colY,colX);
		}
		
		
		if(dirY > 0)
		{
			anim.SetInteger("Direction",1);
			direction = 1;
			gameObject.GetComponent<BoxCollider2D>().size = new Vector2(colX,colY);
		}
		else if(dirY < 0)
		{
			anim.SetInteger("Direction",3);
			direction = 3;
			gameObject.GetComponent<BoxCollider2D>().size = new Vector2(colX,colY);
		}
		
		if(dirX == 0 && dirY == 0)
		{
			anim.SetInteger("Direction",0);
			anim.SetTrigger("Stop");
			
		}
		
		transform.position = new Vector2(transform.position.x + dirX, transform.position.y + dirY);	

		if(Input.GetKeyDown("x") && !triggered && bombs_placed < max_bombs)
		{
			placed_bomb = Instantiate(bomb, new Vector2( (int)((transform.position.x+0.4f)/0.8f)*0.8f,(int)((transform.position.y)/0.8)*0.8f), Quaternion.identity); //Bomb
			placed_bomb.gameObject.GetComponent<Bomb_script>().strength = power;
			placed_bomb.gameObject.GetComponent<Bomb_script>().bomberman_owner = gameObject;
			bombs_placed++;
		}		
		

	}
	
	void reStart(){
		SceneManager.LoadScene("Stage1");
	}
	
	public void die(){
		if(!dead){
			gameObject.SetActive(false);
			Instantiate(bomber_die,transform.position,Quaternion.identity);
			lives--;
			Invoke("reStart",1.8f);
			dead = true;
			gameObject.SetActive(false);
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.CompareTag("Bomb")){
			col.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
			
		}
		triggered = false;
	}
	
	void OnTriggerStay2D(Collider2D col){
		triggered = true;

	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.CompareTag("Door")){
			Instantiate(bomber_win,transform.position,Quaternion.identity);
			scoreBoard.GetComponent<Score>().stop = true;
			master.GetComponents<AudioSource>()[0].Stop();
			Destroy(gameObject);
		}
		
		else if(col.gameObject.CompareTag("Explosion")){
			die();
		}
	}
	
	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.CompareTag("Bomb")){
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
		}
	}
	
	void OnCollisionEnter2D(Collision2D col){
		string name;
		if(col.gameObject.CompareTag("Item")){
			gameObject.GetComponents<AudioSource>()[0].Play();
			name = col.gameObject.name.Substring(5,4);
			if(name == "bomb") max_bombs=Mathf.Min(max_bombs+1,9);
			else if(name == "powa") power=Mathf.Min(power+1,9);
			else if(name == "boot") m_speed=Mathf.Min(m_speed+1,9);
			Destroy(col.gameObject);
		}
		else if(col.gameObject.CompareTag("Bomb")){
			//kick(col.gameObject);
			//gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
		}
		else if(col.gameObject.CompareTag("Enemy")){
			die();
		}
	}
}
