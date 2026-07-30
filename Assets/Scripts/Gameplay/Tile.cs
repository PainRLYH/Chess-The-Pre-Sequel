using UnityEngine;

public class Tile : MonoBehaviour
{
    public Piece m_occupiedBy;
    public Vector2Int m_coordinates;
    public bool m_isLightened;
    public bool m_isAttack;
    private SpriteRenderer m_sprite;
    public Material m_originalMaterial;

    private void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_originalMaterial = m_sprite.material;
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
}
