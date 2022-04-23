using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class makeChoice : MonoBehaviour
{
    HUD hud;
    public bool startchoice = false;
    SpriteRenderer choice2;
    SpriteRenderer choice3;
    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.Find("HUD").GetComponent<HUD>();
        choice2 = transform.Find("2").GetComponent<SpriteRenderer>();
        choice3 = transform.Find("3").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startchoice == true)
        {
            if (hud.gold < 5)
            {
                choice2.color = Color.gray;
                choice3.color = Color.gray;
                if (Input.GetKeyDown("1"))
                {
                    hud.choice = 1;
                    hud.NextStage();
                    SceneManager.LoadScene(8);
                }
            }
            else if (hud.gold>=5 && hud.gold < 20)
            {
                choice2.color = Color.white;
                choice3.color = Color.gray;
                if (Input.GetKeyDown("1"))
                {
                    hud.choice = 1;
                    hud.NextStage();
                    SceneManager.LoadScene(8);
                }
                else if (Input.GetKeyDown("2"))
                {
                    hud.choice = 2;
                    hud.NextStage();
                    SceneManager.LoadScene(8);
                }
            }
            else
            {
                choice3.color = Color.white;
                if (Input.GetKeyDown("1"))
                {
                    hud.choice = 1;
                    hud.NextStage();
                    SceneManager.LoadScene(8);
                }
                else if (Input.GetKeyDown("2"))
                {
                    hud.choice = 2;
                    hud.NextStage();
                    SceneManager.LoadScene(8);
                }
                else if (Input.GetKeyDown("3"))
                {
                    hud.choice = 3;
                    hud.NextStage();
                    SceneManager.LoadScene(8);
                }
            }
        }
    }
}
