using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Category
{
    Board,
    // Player Pieces
    // Spinner

}


[CreateAssetMenu(fileName = "Buildable", menuName = "BuildingObjects/Create Buildable")]
public class BuildingObject : ScriptableObject
{
    [SerializeField] Category category;
    //[SerializeField] UICategory uiCategory;
    [SerializeField] TileBase tileBase;


    public TileBase TileBase
    { get { return tileBase; } }

    public Category Category { get { return category; } }

    //public UICategory UICategory { get { return uiCategory; } }
}
