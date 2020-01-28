using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Sur l'image du Canvas
/// Appelle les fonctions de changement de scène pour l'opérateur et le Pilote
/// </summary>

public class MainMenu : MonoBehaviour
{
    public void PlayGameOP()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayGameP()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
