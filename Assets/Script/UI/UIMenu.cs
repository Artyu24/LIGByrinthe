using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject tutorialMenu;
    [SerializeField] private Text timeScore;
    private static CanvasGroup win;
    private static Text winText;

    void Start()
    {
        if(tutorialMenu != null) 
            tutorialMenu.SetActive(false);
        
        win = GetComponent<CanvasGroup>();
        winText = timeScore;
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
        
        if (winText != null)
            winText.text = Chronometer.GetTime().ToString();
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
