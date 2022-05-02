using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponScript : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    //collision trigger code from https://forum.unity.com/threads/how-to-reduce-enemy-health-when-hit-with-weapon.451464/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            print("Hit!");
            collision.GetComponent<EnemyScript>().curHealth -= damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _renderer.flipX = true;

        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _renderer.flipX = false;
        }
    }

}
