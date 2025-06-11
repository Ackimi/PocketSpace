using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void Play()
	{
		SceneManager.LoadScene("Suika");
	}

	public void Quit()
	{
		Application.Quit();
		Debug.Log("Quit");
	}
}
