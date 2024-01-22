using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    private Sprite[] diceImage; //change with gameState
    private SpriteRenderer rend;
    private int maxRoll = 6; // change with gameState
    private int playerTurn = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Spin(int maxRoll)
    {
        var spinResult = Random.Range(1, maxRoll + 1);
        // sprite renderer
        return spinResult;
    }
}
