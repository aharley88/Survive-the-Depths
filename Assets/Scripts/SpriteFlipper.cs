using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{

    SpriteRenderer spr;
   private void Awake()
    {
        spr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            spr.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            spr.flipX = true;
        }
    }
}
