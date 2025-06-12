using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fruits : MonoBehaviour
{
    private string inTheGuide = "y";
    static public string timeToCheck = "n";
    public int scoreValue;

    //private float _speed = .25f;

    //private Vector2 _spriteScale;

    public GameManager gameManager;

    
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        

        if (transform.position.y < 1)
		{
            inTheGuide = "n";
            GetComponent<Rigidbody2D>().gravityScale = 1;
		}
    }

    
    void Update()
    {
        if (inTheGuide == "y")
		{
            GetComponent<Transform>().position = GuideIcon.guidexPos;
		}

		if (Input.GetKeyDown("space"))
		{
            GetComponent<Rigidbody2D>().gravityScale = 1;
            inTheGuide = "n";
            GuideIcon.spawnedYet = "n";
            //StartCoroutine(CheckGameOver());
		}
    }

	public void OnCollisionEnter2D(Collision2D collision)
	{
        Collider2D _collision = GetComponent<PolygonCollider2D>();
        
        /*if(_collision.gameObject.tag == "GuideIcon")
		{
            _collision.enabled = false;
		}*/

        if ((collision.gameObject.tag == gameObject.tag) && (collision.gameObject.tag != "7") && (transform.position.y < 2))
		{

            GuideIcon.totalColl += 1;
            Debug.Log(GuideIcon.totalColl);

            if ((GuideIcon.totalColl < 3))
			{
                new WaitForSeconds(.75f);

                GuideIcon.spawnPos = transform.position;
                GuideIcon.newFruit = "y";
                GuideIcon.whichFruit = int.Parse(gameObject.tag); //turns tag into int.
                _collision.enabled = false;

                Score.Instance.AddScore(scoreValue);
                StartCoroutine(GrowAndDestroy());
                //Destroy(gameObject);
            }

			else
			{
                GuideIcon.totalColl = 0;
                Destroy(gameObject);
            }


        }
        
        
    }


    IEnumerator GrowAndDestroy()
    {
        Collider2D _collider = GetComponent<PolygonCollider2D>();
        SpriteRenderer _spriteRendr = GetComponent<SpriteRenderer>();
        Color originalColor = _spriteRendr.color;


        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 1.2f; // ou une autre valeur de grossissement

        float duration = 0.15f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            GuideIcon.newFruit = "n";            
            _collider.enabled = false;

            float newAlpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            _spriteRendr.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            //transform.position = Vector2.MoveTowards(transform.position, GuideIcon.spawnPos, _speed * Time.deltaTime); // tp target to another
            transform.position = GuideIcon.spawnPos;
            transform.rotation = Quaternion.Euler(0,0,0);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _spriteRendr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        elapsed = duration; // assure que la taille finale est bien atteinte
        //yield return new WaitForSeconds(0.1f); // petite pause si nécessaire

        Destroy(gameObject);
        GuideIcon.newFruit = "y";
    }

    /*private void OnTriggerStay2D(Collider2D other)
	{
        _timer += Time.deltaTime;

		if ((other.gameObject.name == "Limit") && _timer > _timeOnStay) //(timeToCheck == "y"))
		{
            // timer starts
            // game over ui screen
            // scoring screen appears
            //gameManager.GameOver();
            Debug.Log("GameOver");
            
		}
	}*/

    // checking if fruit is staying too long on the collider
    /*IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(.75f);
        timeToCheck = "y";
    }*/
}
