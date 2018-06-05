using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuActions : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene("Arena");
    }
    public void QuitGame()
    {
        Debug.Log("The Game will now Exit.");
        Application.Quit();
    }
}
