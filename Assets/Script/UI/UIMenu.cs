using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject tutorialMenu;
    [SerializeField] private static GameObject timeScore;
    private static CanvasGroup win;

    void Start()
    {
        if(tutorialMenu != null) tutorialMenu.SetActive(false);
        /*Get reference to "Time" in the prefab*/
        win = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTutorial()
    {
        if (tutorialMenu != null) tutorialMenu.SetActive(true);
        Debug.Log("Showing Tutorial");
    }
    public static void ShowWinScreen()
    {
        win.alpha = 1;
        Debug.Log("Showing WinScreen");
        if (timeScore != null) timeScore = Chronometer.GetTime();
    }

    public void StartGame(int sceneIndex)
    {
        Debug.Log("DEEZ NUTS");
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
