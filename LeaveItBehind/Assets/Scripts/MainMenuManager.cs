using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls play and quit main menu buttons and scene transition
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] private string m_gameScene;

    /// <summary>
    /// Loads gameplay scene/starts game
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(m_gameScene);
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
