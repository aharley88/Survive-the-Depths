using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startGameScene;
    public string helpScreenScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        SceneManager.LoadScene(startGameScene);
    }

    public void HelpScreen() {
        SceneManager.LoadScene(helpScreenScene);
    }

    public void QuitGame() {
        Application.Quit();    
    }
}
