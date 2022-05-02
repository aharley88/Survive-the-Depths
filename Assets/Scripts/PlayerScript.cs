using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(25, 25);
    private Vector2 movement;
    private Rigidbody2D rigidBodyComp;
    public Stopwatch time;
    public int x = 1;
    int upgradeNum;
    public WeaponScript weaponSc;
    public int maxHealth = 20;
    public int curHealth = 20;

    private void Start()
    {
        time = gameObject.GetComponent<Stopwatch>();
        weaponSc = gameObject.GetComponentInChildren<WeaponScript>();
    }
    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);

        //boundary script from https://answers.unity.com/questions/600429/stop-movement-at-edge-of-screen-2d-shooter.html
        if (transform.position.x <= -20.5f)
        {
            transform.position = new Vector2(-20.5f, transform.position.y);
        }

        else if (transform.position.x >= 20.5f) {
            transform.position = new Vector2(20.5f, transform.position.y);
        }
        if (transform.position.y <= -12.5f)
        {
            transform.position = new Vector2(transform.position.x, -12.5f);
        }

        else if (transform.position.y >= 9.5f)
        {
            transform.position = new Vector2(transform.position.x, 9.5f);
        }
        
        if (time.GetMinutes() == x) {
            x++;
            print("You survived for " + x + " minutes!");
            upgrades();
        }
        
    }

    void upgrades() {
        upgradeNum = Random.Range(1, 3);
        switch (upgradeNum)
        {
            case 1:
                print("Speed upgrade!");
                speed.x += 10;
                speed.y += 10;
                break;
            case 2:
                print("Damage upgrade!");
                weaponSc.damage += 10;
                break;
            case 3:
                print("Health upgrade!");
                maxHealth += 10;
                curHealth += 10;
                break;
        }
    }

    void FixedUpdate()
    {
        if (rigidBodyComp == null) rigidBodyComp = GetComponent<Rigidbody2D>();

        rigidBodyComp.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            print("Ouch!");
            curHealth -= collision.GetComponent<EnemyScript>().damage;
        }
    }
}
