using UnityEngine;

public class Tile : MonoBehaviour
{
    public Piece m_occupiedBy;
    public Vector2Int m_coordinates;
    public bool m_isLightened;
    public bool m_isAttack;
    private SpriteRenderer m_sprite;

    private void Awake()
    {
        m_sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (m_isAttack)
        {
            Piece.s_selectedPiece.Capture(this);
        }
        else if (m_isLightened)
        {
            Piece.s_selectedPiece.Move(this);
        }
    }

    public void ClearHighlights()
    {
        m_isLightened = false;
        m_isAttack = false;
        m_sprite.color = Color.white;
    }
}
