using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField][Range(1, 20)]
    public float speed;

    private byte playerState;
    private Vector2 targetPosition;
    private Animator animator;
    
    // Use this for initialization
    void Start () {
        playerState = GameManager.PLAYER_IDLE;
        animator = GetComponent<Animator>();
        animator.SetInteger("direction", 0);
    }

	void Update () {
		if (!GameManager.EXIT_MENU_STATE) {
			if(Input.GetMouseButtonUp(GameManager.mouse_button))
			{
				if(!GameManager.HIT_PLAYER_STOP)
					startPlayerMovement();
				else
					GameManager.HIT_PLAYER_STOP = false;
			}

			if (playerState.Equals (GameManager.PLAYER_MOVING))
				playerMoving();
		} else {
			playerStop();
		}
    }

	private void playerStop()
	{
		//TODO redudancija koda, refaktor
		playerState = GameManager.PLAYER_IDLE;
		animator.SetInteger ("direction", 0); //Animator param direction, animation idle
		//TODO umjesto animation idle satviti animation none
	}

	private void startPlayerMovement()
	{
		targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		playerState = GameManager.PLAYER_MOVING;

		if (this.transform.position.y > targetPosition.y)
			animator.SetInteger ("direction", 1); //Animator param direction, animation front
		else if (this.transform.position.y < targetPosition.y)
			animator.SetInteger ("direction", 2); //Animator param direction, animation back
		
		GameManager.instance.WalkingAudioClip(true);
	}

	private void playerMoving()
	{
		this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, speed * Time.deltaTime);

		if(Vector2.Distance(this.transform.position, targetPosition)<0.1f)
		{
			playerState = GameManager.PLAYER_IDLE;
			animator.SetInteger("direction", 0); //Animator param direction, animation idle

			GameManager.instance.WalkingAudioClip(false);
		}
	}
}
