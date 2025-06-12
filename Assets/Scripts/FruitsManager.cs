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

				//Call method to grow and destroy
				new WaitForSeconds(.75f);
				
				nextFruit.transform.position = transform.position;

				StartCoroutine(MergeEffect());
				//gameObject.SetActive(false);
				//
				

			}
			
		}
	}

	IEnumerator MergeEffect()
	{
		Collider2D _collider = GetComponent<PolygonCollider2D>();
		SpriteRenderer _spriteRendr = GetComponent<SpriteRenderer>();
		Color originalColor = _spriteRendr.color;

		Vector3 originalScale = transform.localScale;
		Vector3 targetScale = originalScale * 1.2f; // ou une autre valeur de grossissement

		float duration = 0.1f;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			_collider.enabled = false;

			float newAlpha = Mathf.Lerp(1f, 0f, elapsed / duration);
			_spriteRendr.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

			transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
			elapsed += Time.deltaTime;
			yield return null;
		}
		elapsed = duration;
		Destroy(gameObject);


	}

}
