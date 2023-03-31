using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject tutorialMenu;

    void Start()
    {
        if(tutorialMenu != null) tutorialMenu.SetActive(false);
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
