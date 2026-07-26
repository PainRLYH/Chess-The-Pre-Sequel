using UnityEngine;

public class Piece : MonoBehaviour
{
    // Variables
    public bool isWhite;
    private SpriteRenderer sprite;
    public Material material;       // Original material of the piece
    public Material chosen_Material;    // Material to indicate the piece is selected
    public Material highlight_Material;     // Material to indicate a legal move
    public Material attack_Highlight_Material;     // Material to indicate a legal attack move
    public static Piece selected_Piece;    // Static variable to keep track of the currently selected piece
    protected Board board;     // Reference to the Board instance
    protected Combat_Manager combat_Manager;     // Reference to the Combat_Manager instance

    protected Vector2Int current_Coordinates;       // Current coordinates of the piece on the board

    // Stats
    public int health;
    public int attack_pwr;
    public int defense;
    public float pierce_resist;
    public float slash_resist;
    public float bludgeon_resist;
    public Attack_Types attack_Type;

    public enum Attack_Types        // Enum to represent different types of attacks
    {
        Piercing, Slashing, Bludgeoning
    }

    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        board = FindAnyObjectByType<Board>();    // Find the Board instance in the scene
        combat_Manager = FindAnyObjectByType<Combat_Manager>();     // Find the Combat_Manager instance in the scene
        material = sprite.material;     // Store the original material of the piece
    }

    private void OnMouseDown()
    {
        if (isWhite == false)
        {
            Tile my_Tile = GetComponentInParent<Tile>();
            if (selected_Piece != null && my_Tile.is_Attack)    // If there is a selected piece and the clicked tile is a legal attack move
            {
                selected_Piece.Capture(my_Tile);    // Call the Capture method of the selected piece to initiate combat with the piece on the clicked tile
                return;     // Return after initiating combat
            }
            return;     // If the piece is not white, do nothing and return
        }

        Debug.Log("Clicked on " + gameObject.name);    // Log the name of the clicked piece

        if (selected_Piece != null)     // If there is a previously selected piece, reset its material
        {
            selected_Piece.sprite.material = selected_Piece.material;   
        }
        
        selected_Piece = this;      // Set the currently selected piece to this piece
        selected_Piece.sprite.material = chosen_Material;    // Change the material to indicate selection

        Tile current_Tile = GetComponentInParent<Tile>(); 
        current_Coordinates = current_Tile.coordinates;    // Get the coordinates of the tile the piece is currently on

        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                Tile target_Tile = board.tiles[i, j].GetComponent<Tile>();      // Loop through all tiles on the board
                target_Tile.GetComponent<SpriteRenderer>().color = Color.white;     // Reset the color of each tile to white
                target_Tile.is_Lightened = false;   // Reset the is_Lightened property of each tile to false
                target_Tile.is_Attack = false;   // Reset the is_Attack property of each tile to false
            }
        }

        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                Tile target_Tile = board.tiles[i, j].GetComponent<Tile>();  
                if (legal_Move(target_Tile))
                {
                    target_Tile.is_Lightened = true;
                    target_Tile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 0.5f);     // Change the color of legal move tiles to a semi-transparent yellow
                }
                else if (target_Tile.occupied_By != null && target_Tile.occupied_By.isWhite != isWhite)
                {
                    Piece temp = target_Tile.occupied_By;       // Temporarily store the piece occupying the target tile
                    target_Tile.occupied_By = null;     // Temporarily set the occupied_By property of the target tile to null to check if the move is legal without considering the piece on the target tile
                    if (legal_Attack(target_Tile))
                    {
                        target_Tile.is_Lightened = true;
                        target_Tile.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);     // Change the color of legal attack move tiles to a semi-transparent red
                        target_Tile.is_Attack = true;     // Set the is_Attack property of the target tile to true to indicate that it is a legal attack move
                    }
                    target_Tile.occupied_By = temp;     // Restore the occupied_By property of the target tile to its original value after checking legality
                }
            }
        }

        Debug.Log(current_Coordinates);
    }

    public void Move(Tile target)
    {
        GetComponentInParent<Tile>().occupied_By = null;     // Set the occupied_By property of the current tile to null
        
        selected_Piece.transform.SetParent(target.transform);       // Set the parent of the selected piece to the target tile
        selected_Piece.transform.localPosition = Vector3.zero;      // Move the piece to the center of the target tile
        target.occupied_By = this;     // Set the occupied_By property of the target tile to this piece

        for (int i = 0; i <= 7; i++)    
        {
            for (int j = 0; j <= 7; j++)
            {
                Tile target_Tile = board.tiles[i, j].GetComponent<Tile>();      // Loop through all tiles on the board
                target_Tile.GetComponent<SpriteRenderer>().color = Color.white;     // Reset the color of each tile to white
                target_Tile.is_Lightened = false;   // Reset the is_Lightened property of each tile to false
                target_Tile.is_Attack = false;   // Reset the is_Attack property of each tile to false
            }
        }

        selected_Piece.sprite.material = selected_Piece.material;   // Reset the material of the selected piece to its original material
        selected_Piece = null;   // Reset the selected piece to null after moving
    }

    public virtual bool legal_Attack(Tile target)
    {
        return legal_Move(target);     
    }

    public void Capture(Tile target)
    {
        combat_Manager.StartCombat(this, target.occupied_By);      // Start combat between this piece and the piece occupying the target tile

    }

    public virtual bool legal_Move(Tile info)
    {
        return true;
    }

    public virtual bool illegal_Move(Tile info)
    {
        return false;
    }

}
