using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMaster : MonoBehaviour {

	int option, dirY, maxOption, menu, lastDirY,dirX, lastDirX;
	int[] players;
	int[] multSettings;
	int[] password;
	int index, stage, passIndex;
	float originalYPos, offset;
	public Transform cursor, pcursor;
	public Transform man,off,com,on,zero,one,two,three,four,five,six,seven,eight,nine,ten,inf,zeroZero,uiOff;
	public Transform stage1,stage2;
	public Transform pzero,pone,ptwo,pthree,pfour,pfive,psix,pseven,peight,pnine;
	Transform[] numbers, stages,pnumbers;
	GameObject screenTransition;
	// Use this for initialization
	void Start () {
		option = 2;
		maxOption = 2;
		lastDirY = -1;
		offset = 0.8f;
		originalYPos = cursor.position.y;
		menu = 0;
		players = new int[]{1,1,2,2};
		multSettings = new int[]{5,3,2,12};
		password = new int[]{0,0,0,0};
		numbers = new Transform[]{zero,one,two,three,four,five,six,seven,eight,nine,ten};
		pnumbers = new Transform[]{pzero,pone,ptwo,pthree,pfour,pfive,psix,pseven,peight,pnine};
		stages = new Transform[]{stage1,stage2};
		stage = 1;
		passIndex = 0;
		screenTransition = GameObject.Find("BlackScreen");

	}
	
	void hideTitleScreen(bool hide)
	{
		int a,b,c;
		if(hide) a = b = c = 0;
		else {a = 1; b = 2; c = 3; option = 2; maxOption = 2; offset = 0.8f; originalYPos = -3.15f; menu = 0;}
		GameObject.Find("Bomb_menu (1)").GetComponent<SpriteRenderer>().sortingOrder = a;
		GameObject.Find("Super_Bomberman_2").GetComponent<SpriteRenderer>().sortingOrder = b;
		GameObject.Find("Bomberman2_options").GetComponent<SpriteRenderer>().sortingOrder = c;
		GameObject.Find("Bomb").GetComponent<SpriteRenderer>().sortingOrder = c;
		
		GameObject[] titleScreen = GameObject.FindGameObjectsWithTag("TitleScreen");
		for(int i = 0; i < titleScreen.Length; i++){
			titleScreen[i].GetComponent<SpriteRenderer>().sortingOrder = b;
		}
	}
	
	void hideUIBattle1(bool hide)
	{
		int a,b;
		if(hide) a = b = 0;
		else {a = 1; b = 2;option = 1; maxOption = 1; offset = 1.4f; originalYPos = 0.38f;menu = 2;}
		GameObject.Find("UIBattle1").GetComponent<SpriteRenderer>().sortingOrder = a;
		GameObject.Find("UIBattle1_arrow").GetComponent<SpriteRenderer>().sortingOrder = b;
		
	}
	
	void hideUIBattle2(bool hide)
	{
		int a,b;
		if(hide) a = b = 0;
		else {a = 1; b = 2; option = 3; maxOption = 3; offset = 1.4f; originalYPos = 0.38f - 1*1.4f; menu = 3;}
		GameObject.Find("UIBattle2").GetComponent<SpriteRenderer>().sortingOrder = a;
		GameObject.Find("UIBattle1_arrow").GetComponent<SpriteRenderer>().sortingOrder = b;
	}
	
	void hideUIBattle3(bool hide)
	{
		int a,b;
		if(hide) a = b = 0;
		else {a = 1; b = 2; option = 3; maxOption = 3; offset = 1.4f; originalYPos = 0.38f - 1*1.4f; menu = 4;}
		GameObject.Find("UIBattle3").GetComponent<SpriteRenderer>().sortingOrder = a;
		GameObject.Find("UIBattle1_arrow").GetComponent<SpriteRenderer>().sortingOrder = b;
	}
	
	void hideUIBattle4(bool hide)
	{
		int a,b;
		if(hide) a = b = 0;
		else {a = 1; b = 0; option = 0; maxOption = 0; offset = 0f; originalYPos = 0.0f; menu = 5;}
		GameObject.Find("UIBattle4").GetComponent<SpriteRenderer>().sortingOrder = a;
		GameObject.Find("UIBattle1_arrow").GetComponent<SpriteRenderer>().sortingOrder = b;
	}
	
	void hideUIPassword(bool hide)
	{
		int a,b;
		if(hide) a = b = 0;
		else {a = 1; b = 0; option = 0; maxOption = 0; offset = 0f; originalYPos = 0.0f; menu = 6;}
		GameObject.Find("UIBomberman_password").GetComponent<SpriteRenderer>().sortingOrder = a;
		GameObject.Find("UIBattle1_arrow").GetComponent<SpriteRenderer>().sortingOrder = b;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown("x"))
		{
			
			if(menu == 0)
			{
				gameObject.GetComponents<AudioSource>()[3].Play();
				if(option == 1) //Activate battle mode
				{
					screenTransition.GetComponent<ScreenTransition>().start = true;
					gameObject.GetComponents<AudioSource>()[0].Stop();
					gameObject.GetComponents<AudioSource>()[1].Play();
					lastDirY = -1;
					cursor = GameObject.Find("UIBattle1_arrow").transform;
					hideTitleScreen(true);
					hideUIBattle1(false);
				}
				else if(option == 2) //Activate History mode
				{
					SceneManager.LoadScene("Stage1");
				}
				else if(option == 0) //Activate Password
				{
					screenTransition.GetComponent<ScreenTransition>().start = true;
					gameObject.GetComponents<AudioSource>()[0].Stop();
					gameObject.GetComponents<AudioSource>()[1].Play();
					lastDirY = -1;
					cursor = GameObject.Find("UIBattle1_arrow").transform;
					hideTitleScreen(true);
					hideUIPassword(false);
				}
			}
			else if(menu == 2)
			{
				gameObject.GetComponents<AudioSource>()[3].Play();
				if(option == 1)
				{
					lastDirY = -1;
					hideUIBattle1(true);
					hideUIBattle2(false);
					cursor.position = new Vector2(cursor.position.x, originalYPos+option*offset);
				}
			}
			else if(menu == 3)
			{
				gameObject.GetComponents<AudioSource>()[3].Play();
				GameObject[] AIOp = GameObject.FindGameObjectsWithTag("AI");
				for(int i = 0; i < AIOp.Length; i++)
					Destroy(AIOp[i]);
				hideUIBattle2(true);
				hideUIBattle3(false);
				cursor.position = new Vector2(cursor.position.x, originalYPos+option*offset);
			}
			else if(menu == 4)
			{
				gameObject.GetComponents<AudioSource>()[3].Play();
				GameObject[] UI3 = GameObject.FindGameObjectsWithTag("UI3");
				for(int i = 0; i < UI3.Length; i++)
					Destroy(UI3[i]);
				
				hideUIBattle3(true);
				hideUIBattle4(false);
				
			}
			else if(menu == 6)
			{
				if(password[0] == 4 && password[1] == 3 && password[2] == 6 && password[3] == 1) {
					gameObject.GetComponents<AudioSource>()[3].Play();
					SceneManager.LoadScene("Stage1");
				}
				else{
					gameObject.GetComponents<AudioSource>()[2].Play();
				}
			}
		
		}
		else if(Input.GetKeyDown("c"))
		{
			if(menu == 2)
			{
				screenTransition.GetComponent<ScreenTransition>().start = true;
				gameObject.GetComponents<AudioSource>()[1].Stop();
				gameObject.GetComponents<AudioSource>()[0].Play();
				hideUIBattle1(true);
				hideTitleScreen(false);
				cursor = GameObject.Find("Bomb").transform;
				cursor.position = new Vector2(cursor.position.x, originalYPos + option*offset);
			}
			else if(menu == 3)
			{
				GameObject[] AIOp = GameObject.FindGameObjectsWithTag("AI");
				for(int i = 0; i < AIOp.Length; i++)
					Destroy(AIOp[i]);
				hideUIBattle2(true);
				hideUIBattle1(false);
				cursor.position = new Vector2(cursor.position.x, originalYPos + option*offset);
			}
			else if(menu == 4)
			{
				GameObject[] Numbers = GameObject.FindGameObjectsWithTag("Numbers");
				for(int i = 0; i < Numbers.Length; i++)
					Destroy(Numbers[i]);
				
				GameObject[] UI3 = GameObject.FindGameObjectsWithTag("UI3");
				for(int i = 0; i < UI3.Length; i++)
					Destroy(UI3[i]);
				hideUIBattle3(true);
				hideUIBattle2(false);
				cursor.position = new Vector2(cursor.position.x, originalYPos + option*offset);
			}
			else if(menu == 5)
			{
				GameObject[] Numbers = GameObject.FindGameObjectsWithTag("Numbers");
				for(int i = 0; i < Numbers.Length; i++)
					Destroy(Numbers[i]);
				
				Numbers = GameObject.FindGameObjectsWithTag("Stages");
				for(int i = 0; i < Numbers.Length; i++)
					Destroy(Numbers[i]);
				
				hideUIBattle4(true);
				hideUIBattle3(false);
				cursor.position = new Vector2(cursor.position.x, originalYPos + option*offset);
			}
			else if(menu == 6)
			{
				screenTransition.GetComponent<ScreenTransition>().start = true;
				gameObject.GetComponents<AudioSource>()[1].Stop();
				gameObject.GetComponents<AudioSource>()[0].Play();
				
				GameObject[] Numbers = GameObject.FindGameObjectsWithTag("UINumbers");
				for(int i = 0; i < Numbers.Length; i++)
					Destroy(Numbers[i]);
				
				hideUIPassword(true);
				hideTitleScreen(false);
				cursor = GameObject.Find("Bomb").transform;
				cursor.position = new Vector2(cursor.position.x, originalYPos + option*offset);
			}

		}
		
		dirX = (int)Input.GetAxisRaw("Horizontal");
		
		if(menu == 3)
		{
			
			if(dirX != lastDirX)
			{
				players[3-option] += dirX;
				gameObject.GetComponents<AudioSource>()[2].Play();
				if(players[3-option] < 0) players[3-option] = 2;
				if(players[3-option] > 2) players[3-option] = 0;
			}
			
			GameObject[] AIOp = GameObject.FindGameObjectsWithTag("AI");
			for(int i = 0; i < AIOp.Length; i++)
				Destroy(AIOp[i]);
			for(int i = 0; i < 4; i++)
			{
				if(players[i] == 0) Instantiate(off,new Vector2(3.54f,3.24f-i*1.4f), Quaternion.identity);
				else if(players[i] == 1) Instantiate(man,new Vector2(3.54f,3.24f-i*1.4f), Quaternion.identity);
				else Instantiate(com,new Vector2(3.54f,3.24f-i*1.4f), Quaternion.identity);
			}
			lastDirX = dirX;
		}
		
		if(menu == 4)
		{
			
			if(dirX != lastDirX)
			{
				index = 3 - option;
				gameObject.GetComponents<AudioSource>()[2].Play();
				multSettings[index] += dirX;
				if(index == 0)
				{
					if(multSettings[index] < 1) multSettings[index] = 9;
					else if(multSettings[index] > 9) multSettings[index] = 1;
				}
				else if(index == 1)
				{
					if(multSettings[index] < 1) multSettings[index] = 5;
					else if(multSettings[index] > 5) multSettings[index] = 1;
				}
				else if(index == 2)
				{
					if(multSettings[index] < 1 || multSettings[index] == 6) multSettings[index] = 11;
					else if(multSettings[index] > 11) multSettings[index] = 1;
					else if(multSettings[index] == 4)
					{
						if(dirX > 0) multSettings[index] = 5;
						else multSettings[index] = 3;
					}
					else if(multSettings[index] == 10) multSettings[index] = 5;
				}
				else if(index == 3)
				{
					if(multSettings[index] > 13) multSettings[index] = 12;
					else if(multSettings[index] < 12) multSettings[index] = 13;
				}
				/*if(multSettings[index] < 0) players[index] = 2;
				if(multSettings[index] > 2) players[index] = 0;*/
			}
			
			GameObject[] Numbers = GameObject.FindGameObjectsWithTag("Numbers");
			for(int i = 0; i < Numbers.Length; i++)
				Destroy(Numbers[i]);
			
			GameObject[] UI3 = GameObject.FindGameObjectsWithTag("UI3");
				for(int i = 0; i < UI3.Length; i++)
					Destroy(UI3[i]);
			
			for(int i = 0; i < 4; i++)
			{
				if(multSettings[i] == 11) Instantiate(inf,new Vector2(3.5f,3.3f-i*1.5f), Quaternion.identity);
				else if(multSettings[i] == 12) Instantiate(on,new Vector2(3.5f,3.3f-i*1.5f), Quaternion.identity);
				else if(multSettings[i] == 13) Instantiate(uiOff,new Vector2(3.5f,3.3f-i*1.5f), Quaternion.identity);
				
				else Instantiate(numbers[multSettings[i]],new Vector2(3.5f,3.3f-i*1.5f), Quaternion.identity);
			}
			lastDirX = dirX;
		}
		if(menu == 5)
		{
			
			if(dirX != lastDirX){
				stage += dirX;
				gameObject.GetComponents<AudioSource>()[2].Play();
				if(stage < 1) stage = 2;
				else if(stage > 2) stage = 1;
			}
			
			GameObject[] Numbers = GameObject.FindGameObjectsWithTag("Numbers");
			for(int i = 0; i < Numbers.Length; i++)
				Destroy(Numbers[i]);
			
			Numbers = GameObject.FindGameObjectsWithTag("Stages");
			for(int i = 0; i < Numbers.Length; i++)
				Destroy(Numbers[i]);
			
			
			Instantiate(numbers[stage], new Vector2(2.6f,1.8f), Quaternion.identity);
			Instantiate(stages[stage-1], new Vector2(-2.5f,1.0f), Quaternion.identity);
			
			lastDirX = dirX;
		}
		
		if(menu == 6)
		{
			if(dirX != lastDirX){
				passIndex += dirX;
				gameObject.GetComponents<AudioSource>()[2].Play();
				if(passIndex < 0) passIndex = 0;
				else if(passIndex > 3) passIndex = 3;
				
			}
			
			GameObject[] Numbers = GameObject.FindGameObjectsWithTag("UINumbers");
			for(int i = 0; i < Numbers.Length; i++)
				Destroy(Numbers[i]);
			
			for(int i = 0; i < password.Length; i++){
				Instantiate(pnumbers[password[i]], new Vector2(-4.27f+2.6f*i,-0.59f), Quaternion.identity);
			}
			Instantiate(pcursor,new Vector2(-5.14f + 2.6f*passIndex,-0.57f), Quaternion.identity);
			lastDirX = dirX;
			
		}
		
		dirY = (int)Input.GetAxisRaw("Vertical");
		if(dirY == lastDirY) return;
		if(menu != 5) gameObject.GetComponents<AudioSource>()[2].Play();
		if(menu == 6)
		{
			password[passIndex] += dirY;
			if(password[passIndex] < 0) password[passIndex] = 9;
			else if(password[passIndex] > 9) password[passIndex] = 0;
		}
		option = (option + dirY);
		if(option > maxOption) option = maxOption;
		if(option < 0) option = 0;
		cursor.position = new Vector2(cursor.position.x, originalYPos + option * offset);
		lastDirY = dirY;
		
		
	}
}
