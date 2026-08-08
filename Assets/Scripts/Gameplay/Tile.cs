using UnityEngine;

public class Tile : MonoBehaviour
{
    public Piece m_occupiedBy;
    public Vector2Int m_coordinates;
    public bool m_isLightened;
    public bool m_isAttack;
    private SpriteRenderer m_sprite;
    private SelectionManager m_selectionManager;

    private void Awake()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_selectionManager = FindAnyObjectByType<SelectionManager>();
    }

    public void OnMouseDown()
    {
        m_selectionManager.OnTileClicked(this);
    }

    public void ClearHighlights()
    {
        m_isLightened = false;
        m_isAttack = false;
        m_sprite.color = Color.white;
    }
}
