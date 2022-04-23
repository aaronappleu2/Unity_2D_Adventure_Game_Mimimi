using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refrigeratorTrigger : MonoBehaviour
{

    CatMovement player;
    SpriteRenderer noticeE;

    bool stay = false;
    int[] validStatus = { 0, 2, 4 };
    bool canTransfer = false;
    // Start is called before the first frame update
    void Start()
    {
        
        noticeE = transform.Find("e").GetComponent<SpriteRenderer>();
        noticeE.enabled = false;
        player = GameObject.Find("Player").GetComponent<CatMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stay && canTransfer)
        {
            // show interact notice
            noticeE.color = Color.white;
            // change status
            if (Input.GetButtonDown("Interact") && player.isMoving == false)
            {
                player.transferToSolid();
                canTransfer = false;
            }
        }
        else if (stay)
        {
            // show fade interact notice
            noticeE.color = Color.gray;
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!stay && other.gameObject.tag=="Player")
        {
            stay = true;
            noticeE.enabled = true;
            CatMovement cat = other.gameObject.GetComponent<CatMovement>();
            print(Array.IndexOf(validStatus, cat.status));
            if (Array.IndexOf(validStatus, cat.status)>=0)
            {
                canTransfer = true;
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
