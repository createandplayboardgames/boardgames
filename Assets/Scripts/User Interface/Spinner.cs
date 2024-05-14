using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sources: https://www.youtube.com/watch?v=W8ielU8iURI&ab_channel=AlexanderZotov
 */

public class Spinner : MonoBehaviour
{

    private Sprite[] diceSides; // change with gameState

    private SpriteRenderer rend; // change sprites

    public static bool coroutineAllowed = false;

    public int finalState;

    // Start is called before the first frame update
    void Start()
    {
        // assign sprite rend component
        rend = GetComponent<SpriteRenderer>();

        // load dice sprites from folder
        diceSides = Resources.LoadAll<Sprite>("images/DiceSides/");
        
    }

    // click mouse to start Spin
    private void OnMouseDown()
    {
        if(coroutineAllowed) { StartCoroutine("Spin"); }
    }

    private IEnumerator Spin()
    {
        // initialize spinner and end roll
        coroutineAllowed = false;
        int spinResult = 0;

        // roll 
        for (int i = 0; i <= 20; i++)
        {
            // roll 0 - 5 (could be changed in future depending on spinner type)
            spinResult = Random.Range(0, 6);

            // render dice faces while roll is happening
            rend.sprite = diceSides[spinResult];

            yield return new WaitForSeconds(0.05f);
        }

        // end state and save for player movement
        finalState = spinResult + 1;

        Debug.Log(finalState);

    }
}
