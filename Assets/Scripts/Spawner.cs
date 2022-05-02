using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//spawner code from https://thehardestwork.com/2020/12/10/how-to-build-a-spawner-in-unity/

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject parent;
    public int numberToSpawn;
    public int limit = 20;
    public float rateOfSpawn;
    float spawnTimer;
    public string menuScene;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = rateOfSpawn;
    }

    public void Update()
    {
        if (parent.transform.childCount < limit)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                if (transform.childCount == 0)
                {
                    print("You're finished!");
                    numberToSpawn = 0;
                    rateOfSpawn = 0;
                    enabled = false;
                    SceneManager.LoadScene(menuScene);
                    return;
                }
                else if (transform.childCount > 0)
                {
                    for (int i = 0; i < numberToSpawn; i++)
                    {
                        Instantiate(objectToSpawn, new Vector3(this.transform.position.x + GetModifier(), this.transform.position.y + GetModifier()), Quaternion.identity, parent.transform);
                    }
                }
              spawnTimer = rateOfSpawn;
            }
        }
    }

    float GetModifier()
    {
        float modifier = Random.Range(0f, 1f);
        if (Random.Range(0, 2) > 0)
        {
            return -modifier;
        }
        else
            return modifier;
    }
}
