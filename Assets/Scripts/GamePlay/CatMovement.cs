using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public CharacterController2D controller;
    Animator catAnim;
    Rigidbody2D rigidbody2d;
    Transform playerCeiling;
    Transform playerGround;
    SpriteRenderer spriteRenderer;

    Timer animParameterLive;
    const float resetTime = 0.5f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public float runSpeed = 20f;
    bool jump = false;
    public bool isMoving;
    public int status; // 0 normal, 1 solid, 2 liquid, 3 gas, 4 elec, 5 long, 6 tall, 7 short.
    // Start is called before the first frame update
    int[] unableJump = { 2, 3 };

    #region status

    CapsuleCollider2D capsuleCollider;

    //pramaters of diff status
    Vector2 normalOffset;
    Vector2 normalSize;
    Vector2 scale;
    float normalCillingCheck;
    float normalGroundCheck;
    
    //SolidCat
    Vector2 solidColloderOffset = new Vector2(0f, -0.3f);
    Vector2 solidColloderSize = new Vector2(3.8f, 2.8f);
    float solidCillingCheck = 1f;
    float solidGroundCheck = -1.7f;
    //LiquidCat
    Vector2 liquidColloderOffset = new Vector2(0f, 0f);
    Vector2 liquidColloderSize = new Vector2(3.8f, 2f);
    float liquidCillingCheck = 1.2f;
    float liquidGroundCheck = -1.25f;

    //gasCat
    Vector2 gasColloderOffset = new Vector2(-0.2f, -0.3f);
    Vector2 gasColloderSize = new Vector2(4.2f, 3.9f);
    float gasCillingCheck = 1.7f;
    float gasGroundCheck = -2.4f;

    //electricCat
    Vector2 elecColloderOffset = new Vector2(0.3f, 0f);
    Vector2 elecColloderSize = new Vector2(3.8f, 3f);
    float elecCillingCheck = 1.5f;
    float elecGroundCheck = -1.5f;

    //longCat
    Vector2 longColloderOffset = new Vector2(0.3f, 0f);
    Vector2 longColloderSize = new Vector2(7.9f, 3f);
    float longCillingCheck = 1.5f;
    float longGroundCheck = -1.5f;

    //tallCat
    Vector2 tallColloderOffset = new Vector2(0f, -0.4f);
    Vector2 tallColloderSize = new Vector2(3.5f, 7.2f);
    float tallCillingCheck = 3.45f;
    float tallGroundCheck = -4f;

    //shortCat
    Vector2 shortColloderOffset = new Vector2(0f, -0.2f);
    Vector2 shortColloderSize = new Vector2(3.9f, 1.95f);
    float shortCillingCheck = 1f;
    float shortGroundCheck = -1.2f;

    #endregion

    void Awake()
    {
        status = 0;
        isMoving = false;

        catAnim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animParameterLive = gameObject.AddComponent<Timer>();
        animParameterLive.Duration = resetTime;
        rigidbody2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        // get paramaters of original player
        normalOffset = capsuleCollider.offset;
        normalSize = capsuleCollider.size;
        playerCeiling = transform.Find("CeilingCheck");
        playerGround = transform.Find("GroundCheck");
        normalCillingCheck = playerCeiling.transform.localPosition.y;
        normalGroundCheck = playerGround.transform.localPosition.y;
        scale = transform.localScale;
    }
    
    private void FixedUpdate()
    {
        
        controller.Move(horizontalMove*Time.fixedDeltaTime, false, jump);
        jump = false;

        catAnim.SetBool("isMoving",isMoving);

        if (status == 3)
        {
            
            transform.Translate(new Vector3(0, verticalMove, 0)*0.4f* Time.fixedDeltaTime);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // restrict movable area
        if (rigidbody2d.position.x < ScreenUtils.ScreenLeft)
        {
            rigidbody2d.position = new Vector2(ScreenUtils.ScreenLeft, rigidbody2d.position.y);
        }
        else if(rigidbody2d.position.x > ScreenUtils.ScreenRight)
        {
            rigidbody2d.position = new Vector2(ScreenUtils.ScreenRight, rigidbody2d.position.y);
        }

        // move control
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
        if (Input.GetButtonDown("Jump") && Array.IndexOf(unableJump, status) < 0)
        {
            jump = true;
        }
        
        if (Mathf.Abs(horizontalMove) > 0.1)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (animParameterLive.Finished)
        {
            resetCatAnimParameter();
        }

    }

    void ChangeRunColliderEvent(CapsuleCollider2D capsuleCollider, 
        Vector2 offset, Vector2 size, float ceiling, float ground, bool isVertical)
    {
        // change collider offset and size
        capsuleCollider.offset = offset;
        capsuleCollider.size = size;

        // change position of ceiling and ground
        playerCeiling.transform.localPosition = new Vector2(0, ceiling);
        playerGround.transform.localPosition = new Vector2(0, ground);
        
        if (isVertical)
        {
            capsuleCollider.direction = CapsuleDirection2D.Vertical;
        }
        else
        {
            capsuleCollider.direction = CapsuleDirection2D.Horizontal;
        }
    }

    public void transferToSolid()
    {
        status = 1;
        catAnim.SetBool("toSolid", true);
        ChangeRunColliderEvent(capsuleCollider, solidColloderOffset, solidColloderSize,
            solidCillingCheck, solidGroundCheck, false);
        animParameterLive.Run();
    }

    public void transferToLiquid()
    {
        status = 2;
        catAnim.SetBool("toLiquid", true);
        ChangeRunColliderEvent(capsuleCollider, liquidColloderOffset, liquidColloderSize,
            liquidCillingCheck, liquidGroundCheck, false);
        animParameterLive.Run();
    }
    public void transferToLong()
    {
        status = 5;
        catAnim.SetBool("toLong", true);
        ChangeRunColliderEvent(capsuleCollider, longColloderOffset, longColloderSize,
            longCillingCheck, longGroundCheck, false);
        animParameterLive.Run();
    }
    public void transferToTall()
    {
        status = 6;
        catAnim.SetBool("toTall", true);
        ChangeRunColliderEvent(capsuleCollider, tallColloderOffset, tallColloderSize,
            tallCillingCheck, tallGroundCheck, true);
        animParameterLive.Run();
    }
    public void transferToShort()
    {
        status = 7;
        catAnim.SetBool("toShort", true);
        ChangeRunColliderEvent(capsuleCollider, shortColloderOffset, shortColloderSize,
            shortCillingCheck, shortGroundCheck, false);
        animParameterLive.Run();
    }

    public void transferToGas()
    {
        status = 3;
        catAnim.SetBool("toGas", true);
        ChangeRunColliderEvent(capsuleCollider, gasColloderOffset, gasColloderSize,
            gasCillingCheck, gasGroundCheck, false);
        transform.localScale = new Vector2(scale.x * 1.5f,scale.y*1.5f);
        rigidbody2d.gravityScale = 0;
        animParameterLive.Run();
    }
    public void transferToElec()
    {
        status = 4;
        catAnim.SetBool("toElec", true);
        ChangeRunColliderEvent(capsuleCollider, elecColloderOffset, elecColloderSize,
            elecCillingCheck, elecGroundCheck, false);
        animParameterLive.Run();
    }
    public void transferToNormal()
    {
        status = 0;
        catAnim.SetBool("toNormal", true);
        ChangeRunColliderEvent(capsuleCollider, normalOffset, normalSize,
            normalCillingCheck, normalGroundCheck, false);
        transform.localScale = scale;
        rigidbody2d.gravityScale = 4;
        if (!controller.m_FacingRight)
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        animParameterLive.Run();
    }

    void resetCatAnimParameter()
    {
        catAnim.SetBool("toLiquid", false);
        catAnim.SetBool("toSolid", false);
        catAnim.SetBool("toLong", false);
        catAnim.SetBool("toTall", false);
        catAnim.SetBool("toShort", false);
        catAnim.SetBool("toGas", false);
        catAnim.SetBool("toElec", false);
        catAnim.SetBool("toNormal", false);
    }

}
