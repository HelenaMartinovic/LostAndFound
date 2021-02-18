using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	//panels
	GameObject mainPanel;
	GameObject optionsPanel;

	//mainPanel
	Button btnNewGame;
	Button btnLoadGame;
	Button btnOptions;
	Button btnExit;

	//optionsPanel
	Button btnMainMenu;
	Toggle tglMouse;
	Toggle tglBgMusic;
	Toggle tglAudio;

	private bool optionsPanelState = false;
	private static bool mouseControll = true;
	private static bool bgMusicControll = true;
	private static bool audioControll = true;

	private void Awake() {
		mainPanel = GameObject.Find("Canvas/MainPanel");
		optionsPanel = GameObject.Find("Canvas/OptionsPanel");

		btnNewGame = GameObject.Find("Canvas/MainPanel/btnNewGame").GetComponent<Button>();
		btnOptions = GameObject.Find("Canvas/MainPanel/btnOptions").GetComponent<Button>();
		btnExit = GameObject.Find("Canvas/MainPanel/btnExit").GetComponent<Button>();

		btnMainMenu = GameObject.Find("Canvas/OptionsPanel/btnMainMenu").GetComponent<Button>();
		tglMouse = GameObject.Find("Canvas/OptionsPanel/tglMouse").GetComponent<Toggle>();
		tglBgMusic = GameObject.Find("Canvas/OptionsPanel/tglBgMusic").GetComponent<Toggle>();
		tglAudio = GameObject.Find("Canvas/OptionsPanel/tglAudio").GetComponent<Toggle>();

		btnNewGame.onClick.AddListener(()=> {OpenNewGame();});
		btnOptions.onClick.AddListener(()=> { Options(); });
		btnExit.onClick.AddListener(()=> {ExitGame();});

		btnMainMenu.onClick.AddListener(() => { Options(); });
		tglMouse.onValueChanged.AddListener((bool isOn) => { MouseControll(isOn); });
		tglBgMusic.onValueChanged.AddListener((bool isOn) => { BgMusicControll(isOn);});
		tglAudio.onValueChanged.AddListener((bool isOn) => { AuidoControll(isOn);});
	}

	private void Start()
	{
		optionsPanel.SetActive(optionsPanelState);
		tglMouse.isOn = mouseControll;
		tglBgMusic.isOn = bgMusicControll;
		tglAudio.isOn = audioControll;
	}
    
	private void OpenNewGame()
    {

        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

	public void Options()
	{
		optionsPanelState = !optionsPanelState;

		mainPanel.SetActive(!optionsPanelState);
		optionsPanel.SetActive(optionsPanelState);
	}

	private void ExitGame()
    {
        Application.Quit();
    }

	private void MouseControll(bool isOn)
	{
		if(isOn)
			GameManager.mouse_button = 0;
		else
			GameManager.mouse_button = 1;

		mouseControll = isOn;
	}

	private void BgMusicControll(bool isOn){
		if(isOn)
			GameManager.bg_music = 1;
		else
			GameManager.bg_music = 0;

		bgMusicControll = isOn;
	}

	private void AuidoControll(bool isOn){
		if(isOn)
			GameManager.audio = 1;
		else
			GameManager.audio = 0;

		audioControll = isOn;
	}
}
