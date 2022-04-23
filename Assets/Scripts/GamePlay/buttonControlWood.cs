using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonControlWood : MonoBehaviour
{
    CatMovement player;
    SpriteRenderer noticeE;
    SpriteRenderer apple;
    woodTrigger wood;

    bool stay = false;
    int[] validStatus = { 0, 1, 2, 3, 4, 5, 6, 7 };
    bool canTransfer = false;
    bool canControl = false;
    // Start is called before the first frame update
    void Start()
    {
        noticeE = transform.Find("e").GetComponent<SpriteRenderer>();
        noticeE.enabled = false;
        player = GameObject.Find("Player").GetComponent<CatMovement>();
        if (transform.Find("apple") != null)
        {
            apple = transform.Find("apple").GetComponent<SpriteRenderer>();
            apple.enabled = false;
        }
        else
        {
            apple = null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Wood") != null)
        {
            wood = GameObject.Find("Wood").GetComponent<woodTrigger>();
            if (stay && (canTransfer && wood.buttoncontrol == false&&wood.asStage==false))
            {
                // show interact notice
                noticeE.color = Color.white;
                // change status
                if (Input.GetButtonDown("Interact") && player.isMoving == false)
                {
                    wood.buttoncontrol = true;
                    canTransfer = false;
                }
            }
            else if (stay && (canControl && wood.buttoncontrol == false && wood.asStage == true))
            {
                noticeE.color = Color.white;
                // change status
                if (Input.GetButtonDown("Interact") && player.isMoving == false)
                {
                    wood.buttoncontrol = true;
                    canTransfer = false;
                }
            }
            else if (stay)
            {
                // show fade interact notice
                noticeE.color = Color.gray;
            }
        }
        else
        {
            noticeE.color = Color.gray;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!stay && other.gameObject.tag == "Player")
        {
            stay = true;
            noticeE.enabled = true;
            CatMovement cat = other.gameObject.GetComponent<CatMovement>();
            print(Array.IndexOf(validStatus, cat.status));
            if (Array.IndexOf(validStatus, cat.status) >= 0)
            {
                canControl = true;
                if(cat.status==0 || cat.status == 6)
                {
                    canTransfer = true;
                }
            }
        }

        if(other.gameObject.tag== "WoodtoButton")
        {
            if (apple != null)
            {
                apple.enabled = true;
                apple.transform.parent = null;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (stay && other.gameObject.tag == "Player")
        {
            stay = false;
            canTransfer = false;
            noticeE.enabled = false;
        }
    }
}
