using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;
    [SerializeField] private CanvasGroup deathUI;
    [SerializeField] private CanvasGroup gameOverUI;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        instance = this;
    }

    public void ShowHideDeathUI(bool show)
    {
        if (show)
        {
            deathUI.alpha = 0;
            deathUI.gameObject.SetActive(true);
        }
        
        deathUI.DOFade(show ? 1 : 0, fadeDuration).OnComplete(() =>
        {
            if(!show)
                deathUI.gameObject.SetActive(false);
        });
    }
    
    public void ShowHideGameOverUI(bool show)
    {
        if (show)
        {
            gameOverUI.alpha = 0;
            gameOverUI.gameObject.SetActive(true);
        }
        
        gameOverUI.DOFade(show ? 1 : 0, fadeDuration).OnComplete(() =>
        {
            if(!show)
                gameOverUI.gameObject.SetActive(false);
        });
    }

    public void OnClick_RestartGame()
    {
        SaveLoadManager_Game.instance.LoadGame();
        ShowHideDeathUI(false);
    }

    public void OnClick_ResetGame()
    {
        SaveLoadManager_Game.instance.DeleteSaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
