using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public bool isPlaying;
    public int count;
    int level;

    [SerializeField] AudioManager audioManager;
    


    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Theme");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        count = 6;
        level = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        StartCoroutine(Instruction());
    }

    IEnumerator Instruction()
    {
        isPlaying = true;
        yield return new WaitForSeconds(2f);
        audioManager.PlaySound("test");
        yield return new WaitForSeconds(2f);
        isPlaying = false;
    }

    public void SetCount(int count)
    {
        this.count = count;
    }
    public void OnClickCloseButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickRetryButton()
    {
        SceneManager.LoadScene(level);
    }
    public void OnClickNextButton()
    {
        if(level==4)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(level + 1);
        }
    }
}
