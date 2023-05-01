using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;

    private void Awake()
    {
        InputMenu(PlayerPrefs.GetInt("CurrentLevel"));
    }

    public void PlayeGame()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
            LoadLevel();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void InputMenu(int value)
    {
        if (value == 1)
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        if (value == 2)
        {
            PlayerPrefs.SetInt("CurrentLevel", 2);
        }
        if (value == 3)
        {
            PlayerPrefs.SetInt("CurrentLevel", 3);
        }
    }

    public void LoadLevel()
    {
        int indexLevel = PlayerPrefs.GetInt("CurrentLevel");
        SceneManager.LoadScene(indexLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
