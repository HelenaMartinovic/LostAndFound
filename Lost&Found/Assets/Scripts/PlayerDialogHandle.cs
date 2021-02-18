using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class PlayerDialogHandle : MonoBehaviour {

	static Text playerDialog;
	Color playerDialogColor;

	bool isFading = false;

	private const float fixDialogTime = 2.5f;
	private static float startTime;
	private static float currentTime;

	private void Awake() {
		playerDialog = GameObject.Find("playerDialog").GetComponent<Text>();
		playerDialogColor = playerDialog.color;
	}
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		playerDialog.text = Dialog.gameStart;
		GameManager.DIALOG_STATE = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.DIALOG_STATE == true && fixDialogTime < currentTime){
			GameManager.DIALOG_STATE = false;
			//playerDialog.text = "";
		} else if (GameManager.DIALOG_STATE == true){
			currentTime += Time.deltaTime;

			isFading = true;
			fadeIn ();
		} else if (isFading == true){
			fadeOut ();
		}
	}

	public static void ShowDialog(Item item){
		int id = item.id;
		string dialog;
		switch (id)
		{
			case (int)Items.Password_0:
				dialog = Dialog.password1;
				break;
			case (int)Items.Password_1:
				dialog = Dialog.password2;
				break;
			case (int)Items.Password_2:
				dialog = Dialog.password3;
				break;
			case (int)Items.Scissors:
				dialog = Dialog.scissors;
				break;
			case (int)Items.Cane:
				dialog = Dialog.cane;
				break;
			case (int)Items.Picture:
				dialog = Dialog.picture;
				break;
			case (int)Items.Key:
				dialog = Dialog.key;
				break;
            case (int)Items.Dust:
                dialog = Dialog.dust;
                break;
            case (int)Items.Bottle_Empty:
                dialog = Dialog.bottle;
                break;
            case (int)Items.Bottle_Full:
                dialog = Dialog.bottleFull;
                break;
            default:
				dialog = nonCombinableInteractions();
				break;
		}

		PlayDialog(dialog);
	}

	private static void PlayDialog(string dialog){
		playerDialog.text = dialog;
		GameManager.DIALOG_STATE = true;
		currentTime = 0f;
	}

	private void fadeOut(){
		playerDialogColor.a -= Time.deltaTime * 3;
		playerDialog.color = playerDialogColor;

		if (playerDialogColor.a < 0)
			isFading = false;
	}

	private void fadeIn(){
		playerDialogColor.a += Time.deltaTime * 3;
		playerDialog.color = playerDialogColor;
	}

	private static string nonCombinableInteractions(){

		string dialogName = "combineItem99_" + (int)Random.Range(0, 4);

		Debug.Log(dialogName);

		return "";
	}
}