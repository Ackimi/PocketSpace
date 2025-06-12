using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FruitsManager : MonoBehaviour
{
    [HideInInspector] public MoveCloud moveCloud;

    public int fruitIndex;
	public bool hasBeenDropped = false;

	private GameObject nextFruit;

	public void OnCollisionEnter2D(Collision2D collision)
	{
		hasBeenDropped = true;

		if (collision.gameObject.CompareTag("Fruit"))
		{
			FruitsManager collidedFruit = collision.gameObject.GetComponent<FruitsManager>();

			if(collidedFruit.fruitIndex == fruitIndex) // les mêmes fruits collisionnent
			{
				if (!gameObject.activeSelf || !collision.gameObject.activeSelf)
					return;

				
				collision.gameObject.SetActive(false);
				Destroy(collision.gameObject);

				nextFruit = Instantiate(MoveCloud.instance.fruitsPrefabs[fruitIndex+1].prefab);
				MoveCloud.instance.IncreaseScore(MoveCloud.instance.fruitsPrefabs[fruitIndex + 1].points);
				
				nextFruit.transform.position = transform.position;						

				gameObject.SetActive(false);
				Destroy(gameObject);
			}
		}
	}

}
