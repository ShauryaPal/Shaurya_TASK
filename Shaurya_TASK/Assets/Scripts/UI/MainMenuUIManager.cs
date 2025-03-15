using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void OnClick_Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}
