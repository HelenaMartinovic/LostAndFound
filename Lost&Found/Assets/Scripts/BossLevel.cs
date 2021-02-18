using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossLevel : MonoBehaviour {
	bool? isPlayer;
    
	List<GameObject> fields;

	Sprite x;
	Sprite o;

    float enemyDelay;

	// Use this for initialization
	void Start () {
		
		fields = new List<GameObject>();
		fields.Add(GameObject.Find("TicTacToe/p00"));
		fields.Add(GameObject.Find("TicTacToe/p01"));
		fields.Add(GameObject.Find("TicTacToe/p02"));
		fields.Add(GameObject.Find("TicTacToe/p10"));
		fields.Add(GameObject.Find("TicTacToe/p11"));
		fields.Add(GameObject.Find("TicTacToe/p12"));
		fields.Add(GameObject.Find("TicTacToe/p20"));
		fields.Add(GameObject.Find("TicTacToe/p21"));
		fields.Add(GameObject.Find("TicTacToe/p22"));

		x = Resources.Load<Sprite>("Sprites/ttt/X");
		o = Resources.Load<Sprite>("Sprites/ttt/O");
		isPlayer = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayer == null)
		{
			if(Input.GetMouseButtonUp(GameManager.mouse_button))
			{
				RaycastHit2D raycast = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000);

				Debug.Log("Mouse up " + raycast.transform);

				ScreenHit(raycast.transform);
			}
		}
		else if(isPlayer.Value == true)
		{
			Debug.Log("Bot player move");
            enemyDelay = 1.5f;

            PlayerTurn();
			isPlayer = false;
		}
		else if (isPlayer.Value == false)
		{
			Debug.Log("Ai move");
            enemyDelay -= Time.deltaTime;
            if(enemyDelay <= 0)
            {
                EnemyTurn();
                isPlayer = null;
            }
		}
	}
    
	private void ScreenHit(Transform transformObj)
	{
	    ScreenHitStage(transformObj);
	}

	private void ScreenHitStage(Transform transformObj)
	{
		if(
			fields[0].transform.Equals(transformObj) ||
			fields[1].transform.Equals(transformObj) ||
			fields[3].transform.Equals(transformObj) ||
			fields[4].transform.Equals(transformObj) ||
			fields[6].transform.Equals(transformObj) ||
			fields[7].transform.Equals(transformObj))
		{
			Debug.Log("Bad player input");
			isPlayer = true;
		}
		else if(
			fields[2].transform.Equals(transformObj) ||
			fields[5].transform.Equals(transformObj) ||
			fields[8].transform.Equals(transformObj))
		{
			Debug.Log("Player input okey");
			PlayerInput(transformObj);
			isPlayer = false;
		}

		Debug.Log("fields: " + fields[0] + " player" + transformObj);
	}

	private void PlayerInput(Transform transformObj)
	{
		if(transform.gameObject.GetComponent<SpriteRenderer>() == null)
		{
			transformObj.gameObject.AddComponent<SpriteRenderer>().sprite = x;
		}
	}

	private void PlayerTurn()
	{
			PlayerStage();
	}

	private void EnemyTurn()
	{
		EnemyStage();
	}

	private void PlayerStage()
	{
		if(fields[2].GetComponent<SpriteRenderer>() == null)
			fields[2].AddComponent<SpriteRenderer>().sprite = x;
		else if(fields[5].GetComponent<SpriteRenderer>() == null)
			fields[5].AddComponent<SpriteRenderer>().sprite = x;
		else if(fields[8].GetComponent<SpriteRenderer>() == null)
			fields[8].AddComponent<SpriteRenderer>().sprite = x;
	}

	private void EnemyStage()
	{
		if(fields[0].GetComponent<SpriteRenderer>() == null)
			fields[0].AddComponent<SpriteRenderer>().sprite = o;
		else if(fields[1].GetComponent<SpriteRenderer>() == null)
			fields[1].AddComponent<SpriteRenderer>().sprite = o;
		else
			StageComplete();
	}

	private void StageComplete()
	{
		Debug.Log("End stage");
		SceneManager.LoadScene("EndGame", LoadSceneMode.Single);
	}
}
