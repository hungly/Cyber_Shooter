﻿using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameController : MonoBehaviour
{
	
	private float timePlayed = 0;
	private float timeStartGame = 0;
	private int bullets = 50;
	private int itemShotConsecutivelyWithoutBeingHit;
	private int missedShotConsecutively = 0;
	private int accurateShotConsecutively = 0;
	private int level;
	private bool isGamePaused = false;
	private int itemAchieved = 0;

	void Start ()
	{
		PlayGamesPlatform.Activate ();

		GameObject.FindWithTag ("BulletCount").guiText.text = "" + bullets;
		GameObject.FindWithTag ("BulletHitIndicator").guiTexture.texture = (Texture2D)Resources.Load ("bullet-status-segment-" + itemShotConsecutivelyWithoutBeingHit);
	}

	void Update ()
	{
	
	}

	public void setTimeStartGame ()
	{
		timeStartGame = Time.time;
	}

	public void calculateActualGamePlayed ()
	{
		timePlayed = Time.time - timeStartGame;
	}

	public int getBullets ()
	{
		return bullets;
	}

	public void bulletShot ()
	{
		bullets--;

		GameObject.FindWithTag ("BulletCount").guiText.text = "" + bullets;

		if (bullets == 0) {
			calculateActualGamePlayed ();
			Social.ReportScore ((long)timePlayed, "CgkI68ebh5kcEAIQEA", (bool successs) => {});
			Social.ReportProgress ("CgkI68ebh5kcEAIQEQ", 100.0f, (bool success) => {});
		}
	}

	public void increaseBullet (string shotObject)
	{
		if (shotObject == "Pyramid") {
			bullets += 3;
		} else if (shotObject == "Diamond") {
			bullets += 5;
		} else if (shotObject == "Star") {
			bullets += 10;
		}
		Debug.Log (shotObject + " is shot, The number of bullet " + bullets);
		GameObject.FindWithTag ("BulletCount").guiText.text = "" + bullets;
	}

	public void increaseItemShotConsecutivelyWithoutBeingHit ()
	{
		itemShotConsecutivelyWithoutBeingHit++;
		
		GameObject.FindWithTag ("BulletHitIndicator").guiTexture.texture = (Texture2D)Resources.Load ("bullet-status-segment-" + itemShotConsecutivelyWithoutBeingHit);
		if (itemShotConsecutivelyWithoutBeingHit == 10 && itemAchieved != 4) {
			itemAchieved++;
			if (itemAchieved == 1){
				Social.ReportProgress("CgkI68ebh5kcEAIQCw",100.0f,(bool success) => {});
			} else if (itemAchieved == 2){
				Social.ReportProgress("CgkI68ebh5kcEAIQCQ",100.0f,(bool success) => {});
			} else if (itemAchieved == 3){
				Social.ReportProgress("CgkI68ebh5kcEAIQCg",100.0f,(bool success) => {});
			}
			resetItemShotConsecutivelyWithoutBeingHit();
		}
	}

	public void resetItemAchieved (){
		itemAchieved = 0;

	public void resetItemShotConsecutivelyWithoutBeingHit ()
	{
		itemShotConsecutivelyWithoutBeingHit = 0;

		GameObject.FindWithTag ("BulletHitIndicator").guiTexture.texture = (Texture2D)Resources.Load ("bullet-status-segment-" + itemShotConsecutivelyWithoutBeingHit);
	}

	public void increaseMissedShot ()
	{
		missedShotConsecutively++;
		accurateShotConsecutively = 0;
		if (missedShotConsecutively == 10){
			Social.ReportProgress("CgkI68ebh5kcEAIQDQ",100.0f, (bool success) =>{});
		}
	}

	public void increaseAccurateShot ()
	{
		accurateShotConsecutively++;
		missedShotConsecutively = 0;
		if (accurateShotConsecutively == 10) {
			Social.ReportProgress("CgkI68ebh5kcEAIQCA",100.0f, (bool success) =>{});
		}
	}

	public void increaseLevel ()
	{
		level++;
		if (level == 2) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQAg", 100.0f, (bool success) => {});
		} else if (level == 2) {
			Social.ReportProgress("CgkI68ebh5kcEAIQAw", 100.0f, (bool success) => {});
		} else if (level == 4) {
			Social.ReportProgress("CgkI68ebh5kcEAIQBA", 100.0f, (bool success) => {});
		} else if (level == 5) {
			Social.ReportProgress("CgkI68ebh5kcEAIQBQ", 100.0f, (bool success) => {});
		}
	}

	public bool IsGamePaused ()
	{
		return isGamePaused;
	}

	public void changeGamePausedStatus ()
	{
		isGamePaused = !isGamePaused;
	}
}