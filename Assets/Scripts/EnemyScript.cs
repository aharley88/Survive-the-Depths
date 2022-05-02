using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float velocity = 2f;
    public Vector2 movementDirec;
    private Vector2 movementPerSec;
    public int maxHealth = 20;
    public int curHealth = 20;
    public Spawner sp;
    public WaveScript waveScript;
    public GameObject Player;
    public WeaponScript weaponSc;
    public GameObject Enemy ;
    //public EnemiesDefeated defeat;
    public int enemiesDefeated;
    public int score = 1;
    public int damage = 5;

    // Start is called before the first frame update

    //enemy movement from https://answers.unity.com/questions/1359733/moving-an-enemy-randomly.html
    void Start()
    {
        //enabled = true;
        latestDirectionChangeTime = 0f;
        calculateNewMovementVector();
        this.gameObject.SetActive(true);
    }

    public void followPlayer() { 
        
    }

    void calculateNewMovementVector() {
        movementDirec = new Vector2(Random.Range(-1.0f, 1.0f), 
        Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSec = movementDirec * velocity;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - latestDirectionChangeTime > directionChangeTime) {
            latestDirectionChangeTime = Time.time;
            calculateNewMovementVector();
        }

        transform.position = new Vector2(transform.position.x + (movementPerSec.x * Time.deltaTime),
            transform.position.y + (movementPerSec.y * Time.deltaTime));

        //boundary script from https://answers.unity.com/questions/600429/stop-movement-at-edge-of-screen-2d-shooter.html
        if (transform.position.x <= -20.5f)
        {
            transform.position = new Vector2(-20.5f, transform.position.y);
        }

        else if (transform.position.x >= 20.5f)
        {
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

        //health script from https://forum.unity.com/threads/how-to-reduce-enemy-health-when-hit-with-weapon.451464/
        if (curHealth <= 0) {
            destroyEnemy();
        }

        if (Vector2.Distance(Enemy.transform.position, Player.transform.position) < 5f) {
            movementDirec = Enemy.transform.position - Player.transform.position;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            movementDirec = movementDirec - movementDirec;
        }
    }
    public void destroyEnemy() {      
        this.gameObject.SetActive(false);
    }
}
