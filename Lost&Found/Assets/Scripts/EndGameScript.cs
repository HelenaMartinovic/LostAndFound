using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour {

    AudioClip bgAudio;
    AudioSource bgAudioSource;

    Button btnNewGame;
    Button btnExit;

    Text txtEndGame;

    // Use this for initialization
    void Start () {
        txtEndGame = GameObject.Find("Canvas/txtEndGame").GetComponent<Text>();
        
        bgAudioSource = this.GetComponents<AudioSource>()[0];
        bgAudioSource.volume = 0.075f;

        btnNewGame = GameObject.Find("Canvas/btnMainMenu").GetComponent<Button>();
        btnExit = GameObject.Find("Canvas/btnExit").GetComponent<Button>();

        btnNewGame.onClick.AddListener(() => { OpenNewGame(); });
        btnExit.onClick.AddListener(() => { ExitGame(); });

        BackgroundAudio();
    }


    float textColorSpeed = 1;

    void Update()
    {
        txtEndGame.color = Color.Lerp(Color.white, Color.black, Mathf.Sin(Time.time * textColorSpeed));
    }

    private void BackgroundAudio()
    {
        bgAudio = Resources.Load<AudioClip>("Sounds/BG_music/EndScene_Tomorrow");

        bgAudioSource.clip = bgAudio;
        bgAudioSource.Play();

        bgAudioSource.volume = GameManager.bg_music;
    }

    private void OpenNewGame()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
