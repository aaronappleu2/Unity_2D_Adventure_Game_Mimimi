using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterAreaTrigger : MonoBehaviour
{
    CatMovement player;
    CompositeCollider2D compositeCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        compositeCollider2D = GetComponent<CompositeCollider2D>();
        player = GameObject.Find("Player").GetComponent<CatMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.status == 2)
        {
            compositeCollider2D.isTrigger = true;
        }
        else
        {
            compositeCollider2D.isTrigger = false;
        }
    }
}
