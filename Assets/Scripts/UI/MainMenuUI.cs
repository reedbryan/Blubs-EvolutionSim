using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("GameSpace");
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("Quit App");
    }
}
