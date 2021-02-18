using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO razmisliti o lokalizaciji
public static class Dialog {//TODO provjeriti rijeci! :3
	
	#region UI
	
	//Pop up msg:
	public const string EXIT_GAME = "Are you sure you want to exit the game. Unsaved progress will be lost";
	public const string MAIN_MENU = "Are you sure you want to go to start menu. Unsaved progress will be lost";


	public const string btnResume = "Resume";
	public const string btnMainMenu = "Main Menu";
	public const string btnExit = "Exit";
	
	public const string btnOK = "OK";
	public const string btnNo = "No";
	public const string msg = "Msg";

	#endregion

	#region GAME DIALOG
	public const string gameStart = "Hmm...everything is so big...";

	//Interactable items interactions:
	//
	//

	//Level 1:

	//Safe interactions
	public const string safeCovered = "Ugh... I won't touch that bare handed";
	public const string safe = "I need password to open the safe";
	public const string safeUnlocked = "A key and a map...";

	//Scissors Interaction
	public const string scissors = "This will be usefully later";

	//Cane interaction
	public const string cane = "Gramps cane. He used to chase me with it all over the house";

	//Picture Interaction
	public const string pricture = "This picture always looked fishy to me";

	//Password interactions
	public const string firstPassword = "Looks like it's missing parts";
	public const string secondPasswordInteraction ="One more";
	public const string thirdPasswordInteraction = "";
	
	//Item description:
	public const string descScissors = "Really sharp object";
	public const string descCane = "I could use it as handle";
	public const string descPicture = "Suspicious picture";
	public const string descPaper = "There is some kind of code here";
	public const string descKey = "A key.";

	public const string password1 = "There is some kind of code written in here";//TODO
	public const string password2 = "Second part of a code. I should combine it with first one.";//TODO
	public const string password3 = "Final piece of a paper!";//TODO
	public const string completeCode = "Whole code is here";//TODO
	public const string picture = "I don't remember beeing in this picture";
	public const string key = "I can't wait to see mr. Toto ...";

	//Level 2:
	//dirt/
	public const string dust = "Fairy dust! It won't come off";
	public const string bottle = "It's empty bottle";
	public const string bottleFull = "Weapon of my choice";

	//Non-combinable items interaction:
	public const string combineItem99_0 = "I don't want to combine that...";
	public const string combineItem99_1 = "Don't think so...";
	public const string combineItem99_2 = "You think you are so smart, don't you";
	public const string combineItem99_3 = "...nope...";
	public const string combineItem99_4 = "*muching sound*";


	public const string bossWrong = "No!";
	public const string bossOk = "Yes! I mean NOO!";
	#endregion
}
	