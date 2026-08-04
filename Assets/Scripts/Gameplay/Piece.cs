using UnityEngine;

public class Piece : MonoBehaviour
{
    // Variables
    public bool m_isWhite;
    private SpriteRenderer m_sprite;
    public Material m_originalMaterial;       // Original material of the piece
    public Material m_chosenMaterial;    // Material to indicate the piece is selected
    public static Piece s_selectedPiece;    // Static variable to keep track of the currently selected piece
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

    private void OnMouseDown()
    {
        if (m_isWhite == false)
        {
            Tile myTile = GetComponentInParent<Tile>();
            if (s_selectedPiece != null && myTile.m_isAttack)    // If there is a selected piece and the clicked tile is a legal attack move
            {
                s_selectedPiece.Capture(myTile);    // Call the Capture method of the selected piece to initiate combat with the piece on the clicked tile
                return;     // Return after initiating combat
            }
            return;     // If the piece is not white, do nothing and return
        }

        Debug.Log("Clicked on " + gameObject.name);    // Log the name of the clicked piece

        Deselect();      // Call the Deselect method to reset the material of the previously selected piece
        
        s_selectedPiece = this;      // Set the currently selected piece to this piece
        s_selectedPiece.m_sprite.material = m_chosenMaterial;    // Change the material to indicate selection

        Tile currentTile = GetComponentInParent<Tile>(); 
        m_currentCoordinates = currentTile.m_coordinates;    // Get the coordinates of the tile the piece is currently on

        m_board.ClearAllHighlights();     // Clear all highlights on the board

        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                Tile targetTile = m_board.m_tiles[i, j];  
                if (LegalMove(targetTile))
                {
                    targetTile.m_isLightened = true;
                    targetTile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 0.5f);     // Change the color of legal move tiles to a semi-transparent yellow
                }
                else if (targetTile.m_occupiedBy != null && targetTile.m_occupiedBy.m_isWhite != m_isWhite)
                {
                    Piece temp = targetTile.m_occupiedBy;       // Temporarily store the piece occupying the target tile
                    targetTile.m_occupiedBy = null;     // Temporarily set the occupied_By property of the target tile to null to check if the move is legal without considering the piece on the target tile
                    if (LegalAttack(targetTile))
                    {
                        targetTile.m_isLightened = true;
                        targetTile.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);     // Change the color of legal attack move tiles to a semi-transparent red
                        targetTile.m_isAttack = true;     // Set the is_Attack property of the target tile to true to indicate that it is a legal attack move
                    }
                    targetTile.m_occupiedBy = temp;     // Restore the occupied_By property of the target tile to its original value after checking legality
                }
            }
        }

        Debug.Log(m_currentCoordinates);
    }

    public void Move(Tile target)
    {
        GetComponentInParent<Tile>().m_occupiedBy = null;     // Set the occupied_By property of the current tile to null
        
        transform.SetParent(target.transform);       // Set the parent of the selected piece to the target tile
        transform.localPosition = Vector3.zero;      // Move the piece to the center of the target tile
        target.m_occupiedBy = this;     // Set the occupied_By property of the target tile to this piece

        m_board.ClearAllHighlights();     // Clear all highlights on the board

        Deselect();      // Deselect the piece after moving
    }

    public static void Deselect()
    {
        if (s_selectedPiece != null)     // If there is a selected piece, reset its material and clear the selection
        {
            s_selectedPiece.m_sprite.material = s_selectedPiece.m_originalMaterial;   
            s_selectedPiece = null;      // Clear the selected piece
        }
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
