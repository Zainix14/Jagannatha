using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Sur l'image du Canvas
/// Appelle les fonctions de changement de scène pour l'opérateur et le Pilote
/// </summary>

public class SC_MainMenu : MonoBehaviour
{
    public void PlayGameOP()
    {
        Debug.Log("MainMenu - PlayGameOP");
        //SceneManager.LoadScene(1);
    }
    public void PlayGameP()
    {
        Debug.Log("MainMenu - PlayGameP");
        //SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
