using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float minXPosition = -1.8f;
    [SerializeField] private float maxXPosition = 1.8f;

    public GameObject[] fruitsPrefabs;

    [SerializeField] private GameObject nextFruit;
    [SerializeField] private int nextFruitIndex;

    public static MoveCloud instance;

	private void Awake()
	{
        instance = this;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {        
        nextFruitIndex = Random.Range(0, 3);
        nextFruit = fruitsPrefabs[nextFruitIndex];
    }

	
	// Update is called once per frame
	void Update()
    {
        if(Input.GetKey(KeyCode.Q) && transform.position.x > minXPosition)
		{
            transform.position -= new Vector3(5 * Time.deltaTime, 0, 0);
		}
        else if (Input.GetKey(KeyCode.D) && transform.position.x < maxXPosition)
		{
            transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
        }

		if (Input.GetKeyDown(KeyCode.Space))
		{
            
            GameObject newFruit = Instantiate(nextFruit);
            newFruit.transform.position = transform.position;

            FruitsManager newFruitScript = newFruit.GetComponent<FruitsManager>();            
            newFruitScript.fruitIndex = nextFruitIndex;

            LoadNextFruit();
            
		}
    }

    void LoadNextFruit()
    {
        nextFruitIndex = Random.Range(0, 3);
        nextFruit = fruitsPrefabs[nextFruitIndex];

        foreach(Transform child in transform)
		{
            Destroy(child.gameObject);
		}

        GameObject nextFruitPreview = Instantiate(nextFruit, transform);
        nextFruitPreview.GetComponent<Collider2D>().isTrigger = true;
        nextFruitPreview.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        nextFruitPreview.transform.localPosition = Vector3.zero;
    }
}
