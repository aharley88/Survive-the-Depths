using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public string menuScene;
    Text upgrade;

    private void Start()
    {
        upgrade = GetComponent<Text>();
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

        if (curHealth == 0) {
            print("Game over!");
            SceneManager.LoadScene(menuScene);
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
                StartCoroutine(FadeTextToFullAlpha(1f, upgrade));
                upgrade.text = ("Speed upgrade!");
                StartCoroutine(FadeTextToZeroAlpha(1f, upgrade));
                break;
            case 2:
                print("Damage upgrade!");
                weaponSc.damage += 10;
                StartCoroutine(FadeTextToFullAlpha(1f, upgrade));
                upgrade.text = ("Damage upgrade!");
                StartCoroutine(FadeTextToZeroAlpha(1f, upgrade));
                break;
            case 3:
                print("Health upgrade!");
                maxHealth += 10;
                curHealth += 10;
                StartCoroutine(FadeTextToFullAlpha(1f, upgrade));
                upgrade.text = ("Health upgrade!");
                StartCoroutine(FadeTextToZeroAlpha(1f, upgrade));
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

    //text fade code from https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/

    public IEnumerator FadeTextToFullAlpha(float t, Text upgrade)
    {
        upgrade.color = new Color(upgrade.color.r, upgrade.color.g, upgrade.color.b, 0);
        while (upgrade.color.a < 1.0f)
        {
            upgrade.color = new Color(upgrade.color.r, upgrade.color.g, upgrade.color.b, upgrade.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text upgrade)
    {
        upgrade.color = new Color(upgrade.color.r, upgrade.color.g, upgrade.color.b, 1);
        while (upgrade.color.a > 0.0f)
        {
            upgrade.color = new Color(upgrade.color.r, upgrade.color.g, upgrade.color.b, upgrade.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
