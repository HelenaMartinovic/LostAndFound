using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMLevel2 : GameManager
{
    private Animator enemy0_animator;
    //audio
    AudioSource bgAudioSource;
    AudioSource playerAudioSource;
    AudioClip bgAudio;
    AudioClip walking;

    bool isFirstBottle;

    GameObject bottle_empty;
    GameObject dust;

    GameObject enemy0;
    GameObject enemy1;
    GameObject teleport0;
    GameObject teleport1;
    GameObject door;
    GameObject mouse;
    Transform answer1;
    Transform answer2;
    Transform answer3;
    SpriteRenderer answer1X;
    SpriteRenderer answer2Key;
    SpriteRenderer answer3X;

    Vector2 platform;
    Vector2 platform1;

    public override void Awake()
    {
        base.Awake();
        GameManager.instance = this;

        bgAudioSource = GameManager.instance.player.GetComponents<AudioSource>()[0];
        bgAudioSource.volume = 0.075f;
        bgAudioSource.pitch = 0.93f;
        playerAudioSource = GameManager.instance.player.GetComponents<AudioSource>()[1];
        playerAudioSource.volume = 1f;

        bottle_empty = GameObject.Find("env/bottle_empty");
        dust = GameObject.Find("env/dust");

        enemy0 = GameObject.Find("enemy/enemy_0");
        enemy1 = GameObject.Find("enemy/enemy_1");

        teleport0 = GameObject.Find("env/teleport/teleport_0");
        teleport1 = GameObject.Find("env/teleport/teleport_1");

        platform = GameObject.Find("env/teleport_places/platform_0").GetComponent<Transform>().position;
        platform1 = GameObject.Find("env/teleport_places/platform_1").GetComponent<Transform>().position;

        door = GameObject.Find("env/door_2");

        answer1 = GameObject.Find("env/answers/answer_0").transform;
        answer2 = GameObject.Find("env/answers/answer_1").transform;
        answer3 = GameObject.Find("env/answers/answer_2").transform;

        mouse = GameObject.Find("env/mouse");

        answer1X = answer1.GetChild(0).GetComponent<SpriteRenderer>();
        answer2Key = answer2.GetChild(0).GetComponent<SpriteRenderer>();
        answer3X = answer3.GetChild(0).GetComponent<SpriteRenderer>();

        var color = answer2Key.color;
        color.a = 0;
        answer2Key.color = color;

        isFirstBottle = true;

        enemy0_animator = enemy0.GetComponent<Animator>();
    }

    public override void BackgroundMusic()
    {
        bgAudio = Resources.Load<AudioClip>("Sounds/BG_music/Lvl1_Always_With_Me");
        walking = Resources.Load<AudioClip>("Sounds/Clips/level2_walking");

        bgAudioSource.clip = bgAudio;
        bgAudioSource.Play();

        bgAudioSource.volume = GameManager.bg_music;
        playerAudioSource.volume = GameManager.audio;
    }

    public override void WalkingAudioClip(bool isWalking)
    {
        if (isWalking)
        {
            playerAudioSource.clip = walking;
            playerAudioSource.Play();
        }
        else
        {
            playerAudioSource.Stop();
        }
    }

    public override void UpdateHit(Transform transformObj)
    {
        if (bottle_empty.transform == transformObj)
        {
            Debug.Log("bottle hit");
            bottle_empty.SetActive(false);
            Item emptyBottleItem = new Item() { id = (int)Items.Bottle_Empty, name = "bottle_empty", description = "Empty bottle" };

            PlayerInventoryEvent(emptyBottleItem);

            selectedItem = null;
        }
        else if (dust.transform == transformObj)
        {
            dust.SetActive(false);

            Item dustItem = new Item() { id = (int)Items.Dust, name = "dust", description = "Dust" };
            PlayerInventoryEvent(dustItem);

            selectedItem = null;
        }
        else if (enemy0.transform == transformObj)
        {
            if (selectedItem?.id == (int)Items.Bottle_Full)
            {

                enemy0.SetActive(false);

                RemoveItemFromInventory(selectedItem);

                selectedItem = null;
            }
        }
        else if (enemy1.transform == transformObj)
        {
            enemy1.SetActive(false);
        }
        else if (teleport0.transform == transformObj)
        {
            if (!enemy0.activeSelf)
            {
                player.transform.localPosition = new Vector2(platform1.x, platform1.y);
            }
        }
        else if (teleport1.transform == transformObj)
        {
            if (!enemy1.activeSelf)
            {
                player.transform.localPosition = new Vector2(platform1.x, platform1.y);
            }
        }
        else if (door.transform == transformObj)
        {
            if (selectedItem?.id == (int)Items.Key)
            {
                selectedItem = null;
                SceneManager.LoadScene("Level2Boss", LoadSceneMode.Single);
            }
        }
        if (answer2Key.transform == transformObj)
        {
            Debug.Log("key hit");
            answer2Key.gameObject.SetActive(false);
            Item keyItem = new Item() { id = (int)Items.Key, name = "key", description = "Key" };

            PlayerInventoryEvent(keyItem);

            selectedItem = null;
        }
    }

    public override void SelectedSecondItem(Item item)
    {
        if (
                (selectedItem.id == (int)Items.Bottle_Empty && item.id == (int)Items.Dust) ||
                (selectedItem.id == (int)Items.Dust && item.id == (int)Items.Bottle_Empty)
            )
        {
            if (isFirstBottle)
                isFirstBottle = false;
            else
                RemoveItemFromInventory(item);

            RemoveItemFromInventory(item.id == (int)Items.Bottle_Empty ? item : selectedItem);

            Item bottleItem = new Item() { id = (int)Items.Bottle_Full, name = "bottle_full", description = "Filled bottle" };
            PlayerInventoryEvent(bottleItem);

            selectedItem = null;
        }
    }

    public override void UpdateGame()
    {
        var playerCollider = player.GetComponent<BoxCollider2D>();
        var size = playerCollider.size;
        size.x *= playerCollider.transform.localScale.x;
        size.y *= playerCollider.transform.localScale.y;
        var hits = Physics2D.BoxCastAll(playerCollider.transform.position, size, 0, Vector2.zero);

        bool hasAnwser1 = false;
        bool hasAnwser3 = false;
        foreach (var hit in hits)
        {
            if(hit.transform == answer1)
            {
                hasAnwser1 = true;
            }

            if (hit.transform == answer2)
            {
                var color = answer2Key.color;
                color.a += Time.deltaTime;
                answer2Key.color = color;
                if(color.a > 0.7f)
                {
                    answer2Key.GetComponent<Collider2D>().enabled = true;
                }
            }

            if (hit.transform == answer3)
            {
                hasAnwser3 = true;
            }
        }

        if (hasAnwser1)
        {
            var color = answer1X.color;
            color.a += Time.deltaTime;
            color.a = Mathf.Clamp01(color.a);
            answer1X.color = color;
        }
        else
        {
            var color = answer1X.color;
            color.a -= Time.deltaTime;
            color.a = Mathf.Clamp01(color.a);
            answer1X.color = color;
        }

        if (hasAnwser3)
        {
            var color = answer3X.color;
            color.a += Time.deltaTime;
            color.a = Mathf.Clamp01(color.a);
            answer3X.color = color;
        }
        else
        {
            var color = answer3X.color;
            color.a -= Time.deltaTime;
            color.a = Mathf.Clamp01(color.a);
            answer3X.color = color;
        }
        Debug.Log(answer3X.color);
    }
}
