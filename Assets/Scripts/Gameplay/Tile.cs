using System.Xml.Serialization;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Piece occupied_By;
    public Vector2Int coordinates;
    public bool is_Lightened;
    public bool is_Attack;
    private SpriteRenderer sprite;
    public Material original_Material;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        original_Material = sprite.material;
    }

    private void OnMouseDown()
    {
        if (is_Attack)
        {
            Piece.selected_Piece.Capture(this);
        }
        else if (is_Lightened)
        {
            Piece.selected_Piece.Move(this);
        }
    }
}
