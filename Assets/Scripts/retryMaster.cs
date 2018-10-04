using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class retryMaster : MonoBehaviour {

	// Use this for initialization
	public Transform pzero,pone,ptwo,pthree,pfour,pfive,psix,pseven,peight,pnine,pcursor;
	Transform[] pnumbers;
	public float timer = 9.0f, dirX,lastDirX;
	int option;
	void Start () {
		pnumbers = new Transform[]{pzero,pone,ptwo,pthree,pfour,pfive,psix,pseven,peight,pnine};
		pcursor.gameObject.GetComponent<SpriteRenderer>().flipX = true;
		option = 0;
		lastDirX = dirX = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//screenTransition.GetComponent<ScreenTransition>().start = true;
		GameObject[] Numbers = GameObject.FindGameObjectsWithTag("UINumbers");
		for(int i = 0; i < Numbers.Length; i++)
			Destroy(Numbers[i]);
		
		timer -= Time.deltaTime;
		
		Instantiate(pnumbers[Mathf.Max((int)timer,0)],new Vector2(0.34f,0.45f),Quaternion.identity);
		
		dirX = (int)Input.GetAxisRaw("Horizontal");
		if(dirX != lastDirX){
			option += (int)dirX;
			if(option < 0) option = 1;
			else if(option > 1) option = 0;
				
			lastDirX = dirX;
		}
		
		Instantiate(pcursor,new Vector2(-0.74f+option*4.18f, 1.77f),Quaternion.identity);
		
		if(Input.GetKeyDown("x"))
		{
			if(option == 0) {gameObject.GetComponents<AudioSource>()[2].Play();;SceneManager.LoadScene("Stage1");}
			else {gameObject.GetComponents<AudioSource>()[1].Play();;SceneManager.LoadScene("Title");}
		}
		
		if(timer <= 0.0f)
			SceneManager.LoadScene("Title");
		
	}
}
