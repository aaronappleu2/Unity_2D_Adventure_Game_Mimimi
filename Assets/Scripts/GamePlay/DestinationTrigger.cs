using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestinationTrigger : MonoBehaviour
{
    CatMovement player;
    SpriteRenderer noticeE;
    public int nextScene;
    HUD hud;
    GameObject choice;
    bool stay = false;
    // Start is called before the first frame update
    void Start()
    {
        noticeE = transform.Find("e").GetComponent<SpriteRenderer>();
        noticeE.enabled = false;
        player = GameObject.Find("Player").GetComponent<CatMovement>();
        hud = GameObject.Find("HUD").GetComponent<HUD>();
        choice = GameObject.Find("choice");
    }

    // Update is called once per frame
    void Update()
    {
        if (stay)
        {
            // show interact notice
            noticeE.color = Color.white;
            // change status
            if (Input.GetButtonDown("Interact") && player.isMoving == false)
            {
                if (nextScene != 8)
                {
                    hud.NextStage();
                    SceneManager.LoadScene(nextScene);
                }
                else
                {
                    choice.GetComponent<SpriteRenderer>().sortingLayerName = "show";
                    choice.transform.Find("1").GetComponent<SpriteRenderer>().sortingLayerName = "item";
                    choice.transform.Find("2").GetComponent<SpriteRenderer>().sortingLayerName = "item";
                    choice.transform.Find("3").GetComponent<SpriteRenderer>().sortingLayerName = "item";
                    choice.GetComponent<makeChoice>().startchoice = true;
                }
                
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!stay && other.gameObject.tag == "Player")
        {
            stay = true;
            noticeE.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (stay && other.gameObject.tag == "Player")
        {
            stay = false;
            noticeE.enabled = false;
        }
    }
}
