using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GMLevel1 : GameManager {

	//audio
	AudioSource bgAudioSource;
	AudioSource playerAudioSource;
	AudioClip bgAudio;
	AudioClip walking;

	//interactables
	GameObject safe_0;
	GameObject safe_1;
	GameObject safe_2;
	GameObject mapKey;

	GameObject cabinet_0;
	GameObject cabinet_1;
	GameObject cabinet_2;

	GameObject picture;
	GameObject password_0;
	GameObject scissors;
	GameObject door;

	public override void Awake() {
		base.Awake();
		GameManager.instance = this;

		bgAudioSource = GameManager.instance.player.GetComponents<AudioSource>()[0];
		bgAudioSource.volume = 0.075f;
		playerAudioSource = GameManager.instance.player.GetComponents<AudioSource>()[1];
			playerAudioSource.volume = 1f;

		safe_0 = GameObject.Find("env/safe/safe_0");
		safe_1 = GameObject.Find("env/safe/safe_1");
		safe_2 = GameObject.Find("env/safe/safe_2");
		mapKey = GameObject.Find("env/safe/mapKey");

		cabinet_0 = GameObject.Find("env/cabinet/cabinet_0");
		cabinet_1 = GameObject.Find("env/cabinet/cabinet_1");
		cabinet_2 = GameObject.Find("env/cabinet/cabinet_2");

		picture = GameObject.Find("env/picture");
		password_0 = GameObject.Find("env/password_0");
		scissors = GameObject.Find("env/scissors");
		door = GameObject.Find("env/door");

		safe_0.SetActive(true);
		safe_1.SetActive(false);
		safe_2.SetActive(false);
		mapKey.SetActive(false);

		cabinet_0.SetActive(true);
		cabinet_1.SetActive(false);
		cabinet_2.SetActive(false);
	}

	public override void BackgroundMusic(){
		bgAudio = Resources.Load<AudioClip>("Sounds/BG_music/Lvl1_Always_With_Me");
		walking = Resources.Load<AudioClip>("Sounds/Clips/level1_walking");//TODO naci novi walking sound

		bgAudioSource.clip = bgAudio;
		bgAudioSource.Play();
		
		bgAudioSource.volume = GameManager.bg_music;
		playerAudioSource.volume = GameManager.audio;
	}

	public override void WalkingAudioClip(bool isWalking){
        if (isWalking) {
			playerAudioSource.clip = walking;
			playerAudioSource.Play();
		} else {
			playerAudioSource.Stop();
		}
	}

	public override void UpdateHit(Transform transformObj)
	{
		if (safe_0.transform == transformObj)
		{
			safe_0.SetActive(false);
			safe_1.SetActive(true);
		}
		else if (safe_1.transform == transformObj)
		{
			if(selectedItem?.id == (int)Items.Password_2)
			{
				safe_1.SetActive(false);
				safe_2.SetActive(true);
				mapKey.SetActive(true);

				RemoveItemFromInventory(selectedItem);
				selectedItem = null;
			}
		}
		else if (mapKey.transform == transformObj)
		{
			mapKey.SetActive(false);

			Item keyItem = new Item() { id = (int)Items.Key, description = "Key", name = "Key" };
			PlayerInventoryEvent(keyItem);
		}
		else if (cabinet_0.transform == transformObj)
		{
			cabinet_0.SetActive(false);
			cabinet_1.SetActive(true);
		}
		else if(cabinet_1.transform == transformObj)
		{
			cabinet_1.SetActive(false);
			cabinet_2.SetActive(true);

			Item caneItem = new Item() { id = (int)Items.Cane, description = "Cane part", name = "cane_part" };
			PlayerInventoryEvent(caneItem);
		}
		else if (picture.transform == transformObj)
		{
			if(selectedItem?.id == (int)Items.Cane)
			{
				picture.SetActive(false);

				Item pictureItem = new Item() { id = (int)Items.Picture, description = "Picture", name = "picture" };
				PlayerInventoryEvent(pictureItem);

				RemoveItemFromInventory(selectedItem);
				selectedItem = null;
			}
		}
		else if (password_0.transform == transformObj)
		{
			password_0.SetActive(false);

			Item passwordItem = new Item() { id = (int)Items.Password_0, description = "Left password piece", name= "password_0" };
			PlayerInventoryEvent(passwordItem);
		}
		else if(scissors.transform == transformObj)
		{
			scissors.SetActive(false);

			Item scissorsItem = new Item() { id = (int)Items.Scissors, description = "Scissors", name = "scissors" };
			PlayerInventoryEvent(scissorsItem);
		}
		else if(door.transform == transformObj)
		{
			if(selectedItem?.id == (int)Items.Key)
			{
				selectedItem = null;
				SceneManager.LoadScene("Level2", LoadSceneMode.Single);
			}
		}
	}

	public override void SelectedSecondItem(Item item)
	{
		if (
				(selectedItem.id == (int)Items.Password_0 && item.id == (int)Items.Password_1) ||
				(selectedItem.id == (int)Items.Password_1 && item.id == (int)Items.Password_0)
			)
		{
			RemoveItemFromInventory(selectedItem);
			RemoveItemFromInventory(item);

			Item password_2 = new Item() { id = (int)Items.Password_2, description = "Complete password", name = "password_2" };
			PlayerInventoryEvent(password_2);
		}
		else if (
					(selectedItem.id == (int)Items.Scissors && item.id == (int)Items.Picture) ||
					(selectedItem.id == (int)Items.Picture && item.id == (int)Items.Scissors)
				)
		{
			RemoveItemFromInventory(selectedItem);
			RemoveItemFromInventory(item);

			Item password_1 = new Item() { id = (int)Items.Password_1, description = "Right password piece", name = "password_1" };
			PlayerInventoryEvent(password_1);
		}

		selectedItem = null;
	}

    public override void UpdateGame(){}
}
