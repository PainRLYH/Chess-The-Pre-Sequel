using UnityEngine;

public class Piece : MonoBehaviour
{
    // Variables
    public bool m_isWhite;
    private SpriteRenderer m_sprite;
    public Material m_originalMaterial;       // Original material of the piece
    public Material m_chosenMaterial;    // Material to indicate the piece is selected
    protected Board m_board;     // Reference to the Board instance
    protected CombatManager m_combatManager;     // Reference to the Combat_Manager instance

    protected Vector2Int m_currentCoordinates;       // Current coordinates of the piece on the board

    // Stats
    public int m_health;
    public int m_attackPower;
    public int m_defense;
    public float m_pierceResist;
    public float m_slashResist;
    public float m_bludgeonResist;
    public AttackTypes m_attackType;

    public enum AttackTypes        // Enum to represent different types of attacks
    {
        Piercing, Slashing, Bludgeoning
    }

    protected virtual void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_board = FindAnyObjectByType<Board>();    // Find the Board instance in the scene
        m_combatManager = FindAnyObjectByType<CombatManager>();     // Find the Combat_Manager instance in the scene
        m_originalMaterial = m_sprite.material;     // Store the original material of the piece
    }

    public void Move(Tile target)
    {
        GetComponentInParent<Tile>().m_occupiedBy = null;     // Set the occupied_By property of the current tile to null
        
        transform.SetParent(target.transform);       // Set the parent of the selected piece to the target tile
        transform.localPosition = Vector3.zero;      // Move the piece to the center of the target tile
        target.m_occupiedBy = this;     // Set the occupied_By property of the target tile to this piece
    }

    public void Select()
    {
        m_sprite.material = m_chosenMaterial;      // Change the material of the piece to indicate it is selected
        m_currentCoordinates = GetComponentInParent<Tile>().m_coordinates;      // Store the current coordinates of the piece on the board
    }

    public void Deselect()
    {
        m_sprite.material = m_originalMaterial;      // Revert the material of the piece to its original state
    }

    public virtual bool LegalAttack(Tile target)
    {
        return LegalMove(target);     
    }

    public void Capture(Tile target)
    {
        m_combatManager.StartCombat(this, target.m_occupiedBy);      // Start combat between this piece and the piece occupying the target tile
    }

    public virtual bool LegalMove(Tile info)
    {
        return true;
    }
}
