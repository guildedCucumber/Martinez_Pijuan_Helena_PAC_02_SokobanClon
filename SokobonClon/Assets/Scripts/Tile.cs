using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offsetColor;
    [SerializeField] private SpriteRenderer renderer;


    public void Init (bool isWall)
    {
        if (isWall)
            renderer.color = offsetColor;
        else
            renderer.color = baseColor;
    }
}
