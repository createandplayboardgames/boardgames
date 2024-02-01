using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    private Sprite[] diceSides; // change with gameState

    private SpriteRenderer rend; // change sprites

    //private int maxRoll = 6; // change with gameState

    // private int playerTurn = 1;
    // Start is called before the first frame update
    void Start()
    {
        // assign sprite rend component
        rend = GetComponent<SpriteRenderer>();

        // load dice sprites from folder
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        
    }

    // click mouse to start Spin
    private void OnMouseDown()
    {
        StartCoroutine("Spin");
    }

    private IEnumerator Spin()
    {
        // initialize spinner and end roll
        int spinResult = 0;
        int finalState;

        // roll 
        for (int i = 0; i <= 20; i++)
        {
            // roll 0 - 5 (could be changed in future depending on spinner type)
            spinResult = Random.Range(0, 5);

            // render dice faces while roll is happening
            rend.sprite = diceSides[spinResult];

            yield return new WaitForSeconds(0.05f);
        }

        // end state and save for player movement
        finalState = spinResult + 1;
        
        Debug.Log(finalState);
    }
}
