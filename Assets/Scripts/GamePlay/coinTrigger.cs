using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinTrigger : MonoBehaviour
{
    HUD hud;

    void Start()
    {
        hud = GameObject.Find("HUD").GetComponent<HUD>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hud.AddPoints(1);
            Destroy(gameObject);
        }
    }
}
