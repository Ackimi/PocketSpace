using UnityEngine;

public class FruitsManager : MonoBehaviour
{
    [HideInInspector] public MoveCloud moveCloud;

    public int fruitIndex;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Fruit"))
		{
			FruitsManager collidedFruit = collision.gameObject.GetComponent<FruitsManager>();

			if(collidedFruit.fruitIndex == fruitIndex)
			{
				if (!gameObject.activeSelf || !collision.gameObject.activeSelf)
					return;

				collision.gameObject.SetActive(false);
				Destroy(collision.gameObject);

				print("Same Fruit!");

				GameObject nextFruit = Instantiate(MoveCloud.instance.fruitsPrefabs[fruitIndex+1]);
				nextFruit.transform.position = transform.position;

				gameObject.SetActive(false);
				Destroy(gameObject);
			}
		}
	}
}
