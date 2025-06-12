using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveCloud : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float minXPosition = -2f;
    [SerializeField] private float maxXPosition = 2f;

    private float currentMinXPosition = 1.8f;
    private float currentMaxXPosition = 2f;

    public Transform guideIcon;

    public FruitsList[] fruitsPrefabs;

    public GameObject nextFruit;
    private int nextFruitIndex;

    private bool canDropFruit = true;

    public static MoveCloud instance;

    [SerializeField] private TextMeshProUGUI scoreText, scoreTextEND;
    [SerializeField] private int currentScore = 0;

    private float defaultYPosition = 3.88f;
    [SerializeField] private float sideGapForBiggerFruits = 0.07f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadNextFruit();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && transform.position.x > currentMinXPosition) // movements player
        {
            transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < currentMaxXPosition)
        {
            transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDropFruit) // touche pour spawn
        {
            canDropFruit = false;
            StartCoroutine(ResetDropTimer()); // Cooldown

            GameObject newFruit = Instantiate(nextFruit);
            newFruit.transform.position = guideIcon.position;

            

            FruitsManager newFruitScript = newFruit.GetComponent<FruitsManager>();
            newFruitScript.fruitIndex = nextFruitIndex;
            
            LoadNextFruit();            

        }

    }

    IEnumerator ResetDropTimer()
	{
        yield return new WaitForSeconds(1f);
        canDropFruit = true;
	}

    public void LoadNextFruit() // Load un prochain fruit dans le player
    {
        

        nextFruitIndex = Random.Range(0, 3);
        nextFruit = fruitsPrefabs[nextFruitIndex].prefab;
            
        foreach (Transform child in transform)
        {
           Destroy(child.gameObject);
        }

        GameObject nextFruitPreview = Instantiate(nextFruit, transform);
        nextFruitPreview.GetComponent<Collider2D>().isTrigger = true;
        nextFruitPreview.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        nextFruitPreview.transform.localPosition = Vector3.zero;

        CalculateCloudBounds();
               
    }

	private void CalculateCloudBounds()
	{
        currentMinXPosition = minXPosition + (nextFruitIndex * sideGapForBiggerFruits);
        currentMaxXPosition = maxXPosition + (nextFruitIndex * sideGapForBiggerFruits);

        if(transform.position.x < currentMinXPosition)
		{
            transform.position = new Vector3(currentMinXPosition, defaultYPosition, 0);
		}
        else if (transform.position.x > currentMaxXPosition)
		{
            transform.position = new Vector3(currentMaxXPosition, defaultYPosition, 0);
		}
    }

	public void IncreaseScore(int value)
    {
        currentScore += value;
        scoreText.text = currentScore.ToString();
    }
         

}

[System.Serializable]
public class FruitsList
{
    public GameObject prefab;
    public int points;
}
