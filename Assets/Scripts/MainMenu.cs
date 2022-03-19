using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject instruction;
    [SerializeField] GameObject backgroundPanel;
    [SerializeField] AudioManager audioManager;
    

    private void OnMouseDown()
    {
            audioManager.PlaySound("Tap");
            instruction.SetActive(false);
            backgroundPanel.SetActive(true);
    }

    public void OnClickLevel1Button()
    {
        audioManager.PlaySound("Tap");
        SceneManager.LoadScene("Level1");
    }

    public void OnClickLevel2Button()
    {
        audioManager.PlaySound("Tap");
        SceneManager.LoadScene("Level2");
    }

    public void OnClickLevel3Button()
    {
        audioManager.PlaySound("Tap");
        SceneManager.LoadScene("Level3");
    }

    public void OnClickLevel4Button()
    {
        audioManager.PlaySound("Tap");
        SceneManager.LoadScene("Level4");
    }

    public void OnClickCloseButton()
    {
        audioManager.PlaySound("Tap");
        SceneManager.LoadScene("MainMenu");
    }
}
