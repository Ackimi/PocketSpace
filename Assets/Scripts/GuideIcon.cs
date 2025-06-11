using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuideIcon : MonoBehaviour
{
    public Transform[] fruitOBJ;
    static public string spawnedYet = "n"; //spawns 1 fruit at the time.
    static public Vector2 guidexPos; // X and Y pos of the guide

    static public Vector2 spawnPos; //position of new fruit spawning
    static public string newFruit = "n"; //when there is a spawn, we change to "y" -> spawn fruit
    bool isSpawning = false;
    
    static public int whichFruit = 0;

    static public int totalColl = 0; //keep from having 3 fruits merging to make 2 bigger ones.

    public float _speed = .25f; //speed of fruit tp to another

    void Start()
    {
        
    }


	void Update()
    {
        SpawnFruit();
        ReplaceFruit();

        // keyboard inputs
        if (Input.GetKey("a"))
		{
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-3, 0);
		}
        
        if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(3, 0);
        }

        if ((!Input.GetKey("a")) && (!Input.GetKey("d")))
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        }

        // x position limit
        if (GetComponent<Rigidbody2D>().transform.position.x > 1.8)
        {
            transform.position = new Vector2(1.8f, transform.position.y);
        }

        if (GetComponent<Rigidbody2D>().transform.position.x < -1.8)
		{
            transform.position = new Vector2(-1.8f, transform.position.y);
		}

        guidexPos = transform.position;

        if ((Input.GetKeyDown("space")) && (spawnedYet == "y"))
		{
            spawnedYet = "n";
            //canSpawnIcon = "n";
        }
    }

    public void SpawnFruit()
	{
		if (spawnedYet == "n")
		{
            isSpawning = true;
            spawnedYet = "w"; //w = "wait"
            StartCoroutine(spawntimer());            
            
		}
	}

    public void ReplaceFruit() // replace fruit inside the glass
	{                

        if (newFruit == "y")

        {
            newFruit = "n"; // to avoid having multiple fruits spawn when snapped                                   
            Instantiate(fruitOBJ[whichFruit+1], spawnPos, fruitOBJ[0].rotation);
		}
        
	}

    /*IEnumerator FuseFruit()
    {
        yield return new WaitForSeconds(0.25f); // maintenant c'est actif
        Instantiate(fruitOBJ[whichFruit + 1], spawnPos, fruitOBJ[0].rotation);
    }*/

    IEnumerator spawntimer()
	{
        yield return new WaitForSeconds(1.75f);
        Instantiate(fruitOBJ[Random.Range(0, 3)], guidexPos, fruitOBJ[0].rotation); //spawn in the guide icon position
        spawnedYet = "y";
        //isSpawning = false;

        
    }

    


}
