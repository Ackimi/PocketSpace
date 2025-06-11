using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText, scoreTextEnd;
	

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			//DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
				
	}

	private void Start()
	{
		UpdateScoreText();
	}

	public void AddScore(int amount)
	{
		score += amount;
		UpdateScoreText();
	}

	public void ResetScore()
	{
		score = 0;
		UpdateScoreText();
	}

	public void UpdateScoreText()
	{
		if(scoreText != null && scoreTextEnd !=null)
		{
			scoreText.text = " " + score.ToString();
			scoreTextEnd.text = " " + score.ToString();
		}
	}
}
