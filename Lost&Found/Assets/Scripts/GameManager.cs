using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class GameManager : MonoBehaviour {
    public static GameManager instance;

    public delegate void playerDialogEventHandler(Item item);
    public event playerDialogEventHandler PlayerDialog;
    public void PlayerInventoryEvent(Item item) { PlayerDialog?.Invoke(item); }

	//panels
    private GameObject escapePanel;
    private GameObject popupPanel;
    private GameObject inventoryPanel;
	private GameObject topPanel;
	
	//top panel items
	private Text txtItemCount;

	//escape menu items
	private Button btnResume;
    private Button btnSaveGame;
	private Button btnMainMenu;
	private Button btnExit;
	
	//confirm panel items
	private Button btnNo;
	private Button btnYes;//TODO

	//Mouse Texture
	private static Texture2D cursorOver;
	private static Texture2D cursorDefault;


	//todo vidjeti da li postoji bolji nacin, zasada samo dohvacam player position
    [HideInInspector]
	public GameObject player;

	//items
	//private SpriteRenderer previewItem;
	Text txtPreviewItem;
	private List<Item> playerInventory;
	Dictionary<string, Item> lvlItems;

	public static int itemCount = 0;
	public static int mouse_button;
    public static int bg_music = 1;
    public static int audio = 1;

	public const byte PLAYER_IDLE = 0;
    public const byte PLAYER_MOVING = 1;
    //Player animation:
    public const byte AN_PLAYER_IDLE = 0;
    public const byte AN_PLAYER_FRONT = 1;
    public const byte AN_PLAYER_BACK = 2;
    //Panel state:
	public static bool EXIT_MENU_STATE = false;
    public static bool DIALOG_STATE = false;
    public bool INVENTORY_STATE = false;

	//
	public static bool HIT_PLAYER_STOP = false;
	public const int INVENTORY_MAX = 6;



	private enum DialogAction
    {
        resume,
        mainMenu,
        exit
    }

    private DialogAction dialogDesition; 

    public virtual void Awake() {
        itemCount = 0;

        escapePanel = GameObject.Find("Canvas/EscapeMenuPanel");
        popupPanel = GameObject.Find("Canvas/EscapeMenuPanel/PopupPanel");
        inventoryPanel = GameObject.Find("Canvas/InventoryPanel");
		topPanel = GameObject.Find ("Canvas/TopPanel");

        btnResume = GameObject.Find("Canvas/EscapeMenuPanel/btnResume").GetComponent<Button>();
        btnMainMenu = GameObject.Find("Canvas/EscapeMenuPanel/btnMainMenu").GetComponent<Button>();
        btnExit = GameObject.Find("Canvas/EscapeMenuPanel/btnExit").GetComponent<Button>();
		btnNo = GameObject.Find("Canvas/EscapeMenuPanel/PopupPanel/btnNo").GetComponent<Button>();

		txtItemCount = GameObject.Find ("Canvas/TopPanel/txtItemCount").GetComponent<Text>();
		txtPreviewItem = GameObject.Find("Canvas/SelectedItem/ItemName").GetComponent<Text>();
		//previewItem = GameObject.Find("Canvas/SelectedItem/ItemPreview").GetComponent<SpriteRenderer>();

		player = GameObject.Find("player");

		btnResume.onClick.AddListener(()=> {EscapePanelAction();});
        btnMainMenu.onClick.AddListener(()=> {MainMenu();});
        btnExit.onClick.AddListener(()=> {ExitGame();});

		btnNo.onClick.AddListener(() => { ResumeEscapePanel(); });
	}

    // Use this for initialization
    private void Start () {
        PlayerDialog += PlayerDialogHandle.ShowDialog;
		PlayerDialog += PrintInventory;
		PlayerDialog += AddItemToInventory;
		
        escapePanel.SetActive(EXIT_MENU_STATE);
		popupPanel.SetActive(EXIT_MENU_STATE);
        inventoryPanel.SetActive(INVENTORY_STATE);

        SetCount();

		playerInventory = new List<Item> ();

		cursorOver = Resources.Load<Texture2D>("Sprites/cursor/clickCursor");
		cursorDefault = Resources.Load<Texture2D>("Sprites/cursor/defaultcursor");
		
		BackgroundMusic();
		CursorDefault();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			EscapePanelAction();
		}
		else if(Input.GetKeyDown(KeyCode.I))
		{
			ShowInventroy();
		}
		else if(Input.GetMouseButtonDown(mouse_button))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000);

			if(hit.transform != null && !hit.collider.isTrigger)
			{
				UpdateHit(hit.transform);
				HIT_PLAYER_STOP = true;
			}
		}
        UpdateGame();
    }

	public static void CursorOver()
	{
		Cursor.SetCursor(cursorOver, Vector2.zero, CursorMode.Auto);
	}

	public static void CursorDefault()
	{
		Cursor.SetCursor(cursorDefault, Vector2.zero, CursorMode.Auto);
	}

	private void EscapePanelAction()
    {
		EXIT_MENU_STATE = !EXIT_MENU_STATE;
        escapePanel.SetActive(EXIT_MENU_STATE);
    }

    private void MainMenu(){
		EXIT_MENU_STATE = !EXIT_MENU_STATE;
		SceneManager.LoadScene("MainMenu");
    }

    private void ExitGame(){
		Application.Quit();
    }
    
	private void ResumeEscapePanel(){
		popupPanel.SetActive(false);
	}

	public Item selectedItem;
	private void ItemClick(Item item){
		HIT_PLAYER_STOP = true;

		if(selectedItem == null)
			selectedItem = item;
		else if(selectedItem.id == item.id)
			selectedItem = null;
		else
			SelectedSecondItem(item);

		PreviewSelectedItem();
	}

	private void AddItemToInventory(Item item){
        itemCount += 1;
        SetCount();//pokrenuti animaciju
        playerInventory.Add(item);

		foreach(Item i in playerInventory)
		{
			Debug.Log(i.name);
		}

		if(INVENTORY_STATE)
			RefreshInventory();
	}

	public void RemoveItemFromInventory(Item item){
		playerInventory.Remove(item);

		RefreshInventory();
	}

	private void ShowInventroy(){

        INVENTORY_STATE = !INVENTORY_STATE;
        inventoryPanel.SetActive(INVENTORY_STATE);

        if(INVENTORY_STATE)
        {
            for (int i = 0; i< INVENTORY_MAX; i++){
                string objName = "New Sprite (" + i + ")";
				GameObject gameObjectInv = GameObject.Find(objName);
				gameObjectInv.GetComponent<Image>().sprite = null;
				gameObjectInv.GetComponent<Button>().onClick.RemoveAllListeners();

				Debug.Log(playerInventory.Count);
				if(playerInventory.Count > i)
				{
					Item item = playerInventory[i];
					Sprite itemSprite = Resources.Load<Sprite>("Sprites/items/" + item.name);
					
					#region debug,delete
					Debug.Log("sprite:"); Debug.Log(itemSprite);

					Debug.Log("obj:"); Debug.Log(gameObjectInv);
					#endregion

					gameObjectInv.GetComponent<Image>().sprite = itemSprite;
					gameObjectInv.GetComponent<Button>().onClick.AddListener(() => { ItemClick(item); });
				}
            }
        }
    }

	public void RefreshInventory(){
		INVENTORY_STATE = false;

		ShowInventroy();
	}

    public void PrintInventory(Item item){//debug only
            Debug.Log("Item:");
            Debug.Log(item.name);
    }

    private void SetCount()
    {
        txtItemCount.text = itemCount.ToString();
    }

	private void PreviewSelectedItem()
	{
		Debug.Log($"PreviewSelectedItem: {selectedItem?.name}");
		txtPreviewItem.text = selectedItem?.description;
	}

	//Abstranct:
	public abstract void BackgroundMusic();
    public abstract void WalkingAudioClip(bool isWalking);
	public abstract void UpdateHit(Transform transformObj);
	public abstract void SelectedSecondItem(Item item);
    public abstract void UpdateGame();
}