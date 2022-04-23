using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireTrigger : MonoBehaviour
{

    CatMovement player;
    SpriteRenderer noticeE;
    CapsuleCollider2D capsuleCollider2D;

    bool stay = false;
    int[] validStatus = { 1 };
    bool canTransfer = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<CatMovement>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        noticeE = transform.Find("e").GetComponent<SpriteRenderer>();
        noticeE.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
            
            // When cat is solid, fire can through
            if (player.status == 1)
            {
                capsuleCollider2D.isTrigger = true;
            }
            else
            {
                capsuleCollider2D.isTrigger = false;
            }

            if (stay && canTransfer)
            {
                // show interact notice
                noticeE.color = Color.white;
                // change status
                if (Input.GetButtonDown("Interact") && player.isMoving == false)
                {
                    player.transferToLiquid();
                    canTransfer = false;
                    Destroy(gameObject);
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
