using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodTrigger : MonoBehaviour
{
    CatMovement player;
    SpriteRenderer noticeE;
    Rigidbody2D rigidbody2d;
    BoxCollider2D boxCollider2D;

    bool stay = false;
    int[] validStatus = { 0,1,2,3,4,5,6,7 };
    bool canfall = false;
    public bool buttoncontrol = false;
    public bool asStage = false;
    // Start is called before the first frame update
    void Start()
    {
        
        if (transform.Find("e") != null)
        {
            noticeE = transform.Find("e").GetComponent<SpriteRenderer>();
            noticeE.enabled = false;
        }
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>(); 
        player = GameObject.Find("Player").GetComponent<CatMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (buttoncontrol == true)
        {
            rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
        if (stay==true && canfall==true)
        {
            // show interact notice
            noticeE.color = Color.white;
            // change status
            if (Input.GetButtonDown("Interact") && player.isMoving == false)
            {
                rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation|RigidbodyConstraints2D.FreezePositionX;
                canfall = false;
                stay = false;
                boxCollider2D.isTrigger = false;
                noticeE.enabled = false;
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
        if (!stay && other.gameObject.tag == "Player")
        {
            stay = true;
            noticeE.enabled = true;
            CatMovement cat = other.gameObject.GetComponent<CatMovement>();
            print(Array.IndexOf(validStatus, cat.status));
            if (Array.IndexOf(validStatus, cat.status) >= 0)
            {
                canfall = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (stay && other.gameObject.tag == "Player")
        {
            stay = false;
            canfall = false;
            noticeE.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (buttoncontrol==true && collision.gameObject.tag == "Player"&&asStage==false)
        {
            player.transferToShort();
            Destroy(gameObject);
        }
    }
}
