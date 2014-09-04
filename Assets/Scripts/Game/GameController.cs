using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameController : MonoBehaviour
{
	public AudioSource upgradeSound;
	private float timePlayed = 0;
	private float timeStartGame = 0;
	private int bullets = 50;
	private int itemShotConsecutivelyWithoutBeingHit;
	private int missedShotConsecutively = 0;
	private int accurateShotConsecutively = 0;
	private int level;
	private bool isGamePaused = false;
	private int itemAchieved = 0;
	private int remainTimeForLaser = 10;
	private float currentTime = 0;
	private float defaultVolume;
	private float defaultVolumeMusic;

	public int GetLevel ()
	{
		return level;
	}

	void Start ()
	{
		PlayGamesPlatform.Activate ();
		defaultVolume = upgradeSound.volume;
		defaultVolumeMusic = audio.volume;
		timeStartGame = Time.time;
	}

	void Update ()
	{
		upgradeSound.volume = defaultVolume * (PlayerPrefs.HasKey ("sfxvol") ? PlayerPrefs.GetFloat ("sfxvol") : 0.5f);
		audio.volume = defaultVolumeMusic * (PlayerPrefs.HasKey ("musicvol") ? PlayerPrefs.GetFloat ("musicvol") : 0.5f);

		if (itemAchieved == 3) {
			if (currentTime == 0) {
				currentTime = Time.time;
			} else {
				if (Time.time - currentTime >= remainTimeForLaser / 10) {
					itemShotConsecutivelyWithoutBeingHit--;
					currentTime = Time.time;
				}
				if (itemShotConsecutivelyWithoutBeingHit <= 0) {
					itemShotConsecutivelyWithoutBeingHit = 0;
					itemAchieved = 2;
				}
			}
		}
	}

	public void setTimeStartGame ()
	{
		timeStartGame = Time.time;
	}

	public void calculateActualGamePlayed ()
	{
		timePlayed = Time.time - timeStartGame;
	}

	public float GetPlayedTime ()
	{
		calculateActualGamePlayed ();
		return timePlayed;
	}

	public int getBullets ()
	{
		return bullets;
	}

	public void bulletShot (int numShot)
	{
		bullets -= numShot;

		if (bullets <= 0) {
			bullets = 0;
			calculateActualGamePlayed ();
			Social.ReportScore((long)timePlayed, "CgkI68ebh5kcEAIQAA", (bool successs) => {});
			Social.ReportProgress ("CgkI68ebh5kcEAIQEQ", 100.0f, (bool success) => {});
			if (!isGamePaused) { 	 	
				changeGamePausedStatus (); 	 	
			} 
			GameObject.FindWithTag ("PauseButton").GetComponent<GameMenuController> ().TootgleGameOverScreen ();
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
	}

	public void increaseItemShotConsecutivelyWithoutBeingHit ()
	{
		if (itemAchieved < 3) {
			itemShotConsecutivelyWithoutBeingHit++;

			if (itemShotConsecutivelyWithoutBeingHit == 10 && itemAchieved != 4) {
				itemAchieved++;
				upgradeSound.Play ();
				if (itemAchieved == 1) {
					Social.ReportProgress ("CgkI68ebh5kcEAIQCw", 100.0f, (bool success) => {});
				} else if (itemAchieved == 2) {
					Social.ReportProgress ("CgkI68ebh5kcEAIQCQ", 100.0f, (bool success) => {});
				} else if (itemAchieved == 3) {
					Social.ReportProgress ("CgkI68ebh5kcEAIQCg", 100.0f, (bool success) => {});
				}

				if (itemAchieved != 3) {	
					itemShotConsecutivelyWithoutBeingHit = 0;
				}
			}
		}
	}

	public int GetItemAchieved ()
	{
		return itemAchieved;
	}

	public void resetItemAchieved ()
	{
		itemAchieved = 0;
		upgradeSound.Play ();
	}

	public void resetItemShotConsecutivelyWithoutBeingHit ()
	{
		itemShotConsecutivelyWithoutBeingHit = 0;

		itemAchieved = 0;
	}

	public void increaseMissedShot ()
	{
		missedShotConsecutively++;
		accurateShotConsecutively = 0;
		if (missedShotConsecutively == 10) {
			// This achievement may work incorrectly, so it is temporarily disabled
			//Social.ReportProgress ("CgkI68ebh5kcEAIQDQ", 100.0f, (bool success) => {});
		}
	}

	public void increaseAccurateShot ()
	{
		accurateShotConsecutively++;
		missedShotConsecutively = 0;
		if (accurateShotConsecutively == 10) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQCA", 100.0f, (bool success) => {});
		}
	}

	public void increaseLevel ()
	{
		level++;
		if (level == 2) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQAg", 100.0f, (bool success) => {});
		} else if (level == 3) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQAw", 100.0f, (bool success) => {});
		} else if (level == 4) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQBA", 100.0f, (bool success) => {});
		} else if (level == 5) {
			Social.ReportProgress ("CgkI68ebh5kcEAIQBQ", 100.0f, (bool success) => {});
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

	public int GetItemShotConsecutivelyWithoutBeingHit ()
	{
		return itemShotConsecutivelyWithoutBeingHit;
	}
}