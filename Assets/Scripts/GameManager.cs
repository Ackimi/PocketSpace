using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public GameObject pressEscapeUI;

    static public string retryCheck = "n";

    public MoveCloud guideIcon;

	private void Start()
	{
        //gameOverUI.gameObject.SetActive(false);
        pressEscapeUI.gameObject.SetActive(true);
        

        guideIcon = GameObject.Find("Guide_Icon").GetComponent<MoveCloud>();
            
    }

	/*public void GameOver() _old
	{
        gameOverUI.gameObject.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Game Over");
	}*/

	void Update()
	{

        if (Input.GetKeyDown(KeyCode.Space) && (pressEscapeUI.activeInHierarchy == true))
        {
            pressEscapeUI.gameObject.SetActive(false);
            new WaitForSeconds(.75f);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
		{
            pauseMenuUI.gameObject.SetActive(!pauseMenuUI.gameObject.activeSelf);
            
        }

        if(pauseMenuUI.activeInHierarchy == true)
		{
            Time.timeScale = 0;
        }

        if (pauseMenuUI.activeInHierarchy == false)
        {
            Time.timeScale = 1;
        }

    }

    public void Resume()
	{
        pauseMenuUI.gameObject.SetActive(!pauseMenuUI.gameObject.activeSelf);
        
    }

    public void Retry()
	{
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        //guideIcon.SpawnFruit();
        retryCheck = "y";
        
    }

    IEnumerator CheckRetry()
    {
        if (retryCheck == "y")
		{
            yield return new WaitForSeconds(1f);
            guideIcon.LoadNextFruit();
        }
        
    }

    public void MainMenu()
	{
        SceneManager.LoadScene("MainMenu");
	}
}
