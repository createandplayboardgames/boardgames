using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


/*
 * Menu Object makes reference to each scriptable object option in the create page menu. 
 * It sets up fields, gamepiece and tilebase that can be assigned in Unitys interface.
 * Sets up those objects to be called in EditorUI
 * 
 */
public enum GamePieces
{
    Board,
    // Player Pieces
    // Spinner

}


public class MenuObject : ScriptableObject
{
    [SerializeField] GamePieces gamepiece;
    [SerializeField] TileBase tileBase;


    public TileBase TileBase
    { get { return tileBase; } }

    public GamePieces GamePiece { get { return gamepiece; } }
}
