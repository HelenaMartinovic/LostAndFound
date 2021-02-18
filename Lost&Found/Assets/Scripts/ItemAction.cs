using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAction : MonoBehaviour {
	private void OnMouseOver(){
		GameManager.CursorOver();
	}

	private void OnMouseExit(){
		GameManager.CursorDefault();
	}

	private void OnDestroy() {
		GameManager.CursorDefault();
	}

}
