
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {

	public static GM instance = null;

	public float yMinLive = -10f;

	PlayerCtrl player;
		
	public float TimeToRespawn = 2f;

	public Transform spawnPoint;

	public GameObject playerPrefab;

	public float maxTime = 120f;
	bool timerOn = true;

	float timeLeft;

	public UI ui;

	GameData data = new GameData();

	void Awake(){
		if (instance ==null){
			instance = this;
		}
	
	}

	
	void Start () {
		if (player == null){
			RespawnPlayer();
		}
		timeLeft = maxTime;
	}
	

	void Update () {
		if (player == null){
			GameObject obj = GameObject.FindGameObjectWithTag("Player");
			if( obj != null) {
				player = obj.GetComponent<PlayerCtrl>();
			}
		}
		 UpdateTimer();
		 DisplayHudData();
		
	}

	void UpdateTimer(){
		if (timerOn){
			timeLeft = timeLeft - Time.deltaTime;
			if(timeLeft <= 0f){
				timeLeft = 0;
				ExpirePlayer();
			}
		}
	}

	void DisplayHudData() {
		ui.hud.txtCoinCount.text = "x " + data.coinCount;
		ui.hud.txtTimer.text = "Timer: " + timeLeft.ToString("F1");
	}

	public void IncrementeCoinCount(){
		data.coinCount++;
	}

	
    public void KillPlayer(){
		if (player != null){
			Destroy(player.gameObject);
			Invoke("RespawnPlayer", TimeToRespawn);
		}
	}

	public void ExpirePlayer(){
		if (player != null){
			Destroy(player.gameObject);
		}
		GameOver();
	}

	void GameOver(){
		timerOn = false;
		ui.gameOver.txtCoinCount.text = "Coins: " + data.coinCount;
		ui.gameOver.txtTimer.text = "Timer: " + timeLeft.ToString("F1");
		ui.gameOver.GameOverPanel.SetActive(true);
	}
	
	public void RespawnPlayer(){
		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
}

	
}
