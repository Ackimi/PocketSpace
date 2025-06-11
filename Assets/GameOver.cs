using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    
    public float _timeOnStay = 1f;
    public float _timer;
    //private bool fruitInTrigger = false;
    public UnityEvent gameOverUI;
    private Collider2D limitCollider = null;

	private void Update()
	{
		if (limitCollider != null)
		{
            _timer += Time.deltaTime;

            if (_timer >= _timeOnStay)
            {
                gameOverUI.Invoke();
                limitCollider = null;
                Debug.Log("GameOver");
            }
        }
	}
	private void OnTriggerEnter2D(Collider2D other)
    {
		if (!other.CompareTag("Border"))
		{
            limitCollider = other;
            _timer = 0f;
            Debug.Log("inside");
        }            
        
    }

	private void OnTriggerExit2D(Collider2D other)
	{
        if(other == limitCollider)
		{
            limitCollider = null;
            _timer = 0f;
        }
        
    }
}
