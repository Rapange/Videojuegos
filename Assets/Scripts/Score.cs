using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	// Use this for initialization
	public int score, lives, bombs, fire;
	public float iniTime;
	public bool stop;
	public Transform zero,one,two,three,four,five,six,seven,eight,nine,bomberman;
	Transform[] numbers;
	void Start () {
		numbers = new Transform[]{zero,one,two,three,four,five,six,seven,eight,nine};
	}
	
	void drawScore()
	{
		int cont = 0;
		if(score == 0)
		{
			for(; cont < 2; cont++)
				Instantiate(numbers[score % 10],new Vector2(3.5f-cont*0.365f,9.5f),Quaternion.identity);
		}
		int cScore = score;
		while(cScore > 0)
		{
			Instantiate(numbers[cScore % 10],new Vector2(3.5f-cont*0.365f,9.5f),Quaternion.identity);
			cScore /= 10;
			cont++;
		}
	}
	
	void drawTimer()
	{
		int minutes = (int)iniTime / 60, seconds = (int)iniTime % 60, i = 0;
		Instantiate(numbers[minutes], new Vector2(5.38f,9.5f), Quaternion.identity);
		if(seconds < 10)
			Instantiate(numbers[0], new Vector2(6.1f,9.5f), Quaternion.identity);
		
		do{
			Instantiate(numbers[seconds%10], new Vector2(6.5f-i*0.4f,9.5f), Quaternion.identity);
			seconds /= 10;
			i++;
		}while(seconds > 0);
	}
	
	void drawLives()
	{
		Instantiate(numbers[lives],new Vector2(8.48f,9.5f),Quaternion.identity);
	}
	
	void drawPowerUps()
	{
		Instantiate(numbers[bombs],new Vector2(10f,9.5f), Quaternion.identity);
		Instantiate(numbers[fire],new Vector2(11.17f,9.5f), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if(iniTime <= 0.0f) bomberman.GetComponent<Player>().die();
		GameObject[] numbers = GameObject.FindGameObjectsWithTag("AdvNumbers");
		for(int i = 0; i < numbers.Length; i++)
			Destroy(numbers[i]);
		
		if(!stop){
			iniTime -= Time.deltaTime;
			iniTime = Mathf.Max(iniTime,0.0f);
		}
		drawLives();
		drawPowerUps();
		drawScore();
		drawTimer();
	}
}
