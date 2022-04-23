using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// The HUD
/// </summary>
public class HUD : MonoBehaviour
{
    // scoring support
    [SerializeField]
    Text GoldText;
    public int gold;
    int stage=0;
    int currentStageCoin = 0;
    public int choice=0;
    SpriteRenderer end2;
    SpriteRenderer end3;


    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
	{
        GoldText.text = gold.ToString();
        GameObject.DontDestroyOnLoad(gameObject);

    }
    private void Update()
    {
        if(stage ==0 || stage == 8)
        {
            GoldText.enabled = false;
            if(stage==0 && Input.GetKeyDown("return"))
            {
                GoldText.enabled = true;
                stage = 1;
                SceneManager.LoadScene(1);
            }
        }
        if (stage < 4)
        {
            GoldText.color = Color.black;
        }
        else
        {
            GoldText.color = Color.white;
        }

        if (Input.GetKeyDown("r"))
        {
            if(stage == 8)
            {
                gold = 0;
                GoldText.text = gold.ToString();
                GoldText.enabled = true;
                stage = 1;
                SceneManager.LoadScene(1);
            }
            else if(stage>0)
            {
                ResetCoin();
                SceneManager.LoadScene(stage);
            }
        }
        if (Input.GetKeyDown("q"))
        {
            Application.Quit();
        }
        if(stage == 8)
        {
            if(choice == 2)
            {
                end2 = GameObject.Find("end2").GetComponent<SpriteRenderer>();
                end2.sortingLayerName = "item";
            }
            else if (choice == 3)
            {
                end3 = GameObject.Find("end3").GetComponent<SpriteRenderer>();
                end3.sortingLayerName = "item";
            }
        }
    }

    /// <summary>
    /// Adds the given points to the score
    /// </summary>
    /// <param name="points">points</param>
    public void AddPoints(int points)
    {
        gold += points;
        currentStageCoin += points;
        GoldText.text = gold.ToString();
        AudioManager.Play(AudioClipName.coin);
    }
    public void NextStage()
    {
        stage += 1;
        currentStageCoin = 0;
        AudioManager.Play(AudioClipName.cat_meow);
    }
    void ResetCoin()
    {
        gold = gold - currentStageCoin;
        GoldText.text = gold.ToString();
        currentStageCoin = 0;
    }
}
