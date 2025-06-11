using UnityEngine;

public class ontriggertsyatets : MonoBehaviour
{
    [SerializeField]
    private float _timeOnStay = 3f;
    private float _timer;

    private void OnTriggerStay2D(Collider2D other)
    {
        _timer += Time.deltaTime;

        if (_timer > _timeOnStay) //(timeToCheck == "y"))
        {
            // timer starts
            // game over ui screen
            // scoring screen appears
            //gameManager.GameOver();
            _timer = 0;
            Debug.Log("GameOver");

        }
    }
}
