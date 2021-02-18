using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {
	//Game properties:
	//byte playerSprite; //TODO male female, mozda enum?
	bool isMusic;//TODO rename
	bool isSoundEffects;//TODO rename

	//Game progress:
	object playerPosition;
	object playerInventory;
	object gameState;//tu bi se trebalo napraviti da se nekako zna koji se sprajt treba staviti da igrica
	//bude u istom stanju kako ju je igrac ostavio prilikom savea
}
