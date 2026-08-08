using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Piece m_selectedPiece;    // Reference to the currently selected piece 
    private Board m_board;    // Reference to the board

    private void Start()
    {
        m_board = FindAnyObjectByType<Board>();  
    }

    public void SelectPiece(Piece piece)
    { 
        if (m_selectedPiece != null)
        {
            ClearSelection();      // Call the Deselect method to reset the material of the previously selected piece
        }

        piece.Select();        // Call the Select method to change the material of the newly selected piece

        m_selectedPiece = piece;      // Set the currently selected piece to this piece

        Tile currentTile = piece.GetComponentInParent<Tile>();

        m_board.ClearAllHighlights();     // Clear all highlights on the board

        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                Tile targetTile = m_board.m_tiles[i, j];
                if (piece.LegalMove(targetTile))
                {
                    targetTile.m_isLightened = true;
                    targetTile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 0.5f);     // Change the color of legal move tiles to a semi-transparent yellow
                }
                else if (targetTile.m_occupiedBy != null && targetTile.m_occupiedBy.m_isWhite != piece.m_isWhite)
                {
                    Piece temp = targetTile.m_occupiedBy;       // Temporarily store the piece occupying the target tile
                    targetTile.m_occupiedBy = null;     // Temporarily set the occupied_By property of the target tile to null to check if the move is legal without considering the piece on the target tile
                    if (piece.LegalAttack(targetTile))
                    {
                        targetTile.m_isLightened = true;
                        targetTile.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);     // Change the color of legal attack move tiles to a semi-transparent red
                        targetTile.m_isAttack = true;     // Set the is_Attack property of the target tile to true to indicate that it is a legal attack move
                    }
                    targetTile.m_occupiedBy = temp;     // Restore the occupied_By property of the target tile to its original value after checking legality
                }
            }
        }
    }

    public void OnTileClicked(Tile tile)
    {
        if(tile.m_isAttack)     // Check if the clicked tile is a legal attack move
        {
            m_selectedPiece.Capture(tile);      // Call the Capture method of the selected piece to capture the piece on the clicked tile
            ClearSelection();      // Clear the selection after the capture is made
            m_board.ClearAllHighlights();     // Clear all highlights on the board after the capture is made
            return;
        }
        if (tile.m_isLightened)    // Check if the clicked tile is a legal move
        {
            m_selectedPiece.Move(tile);      // Call the Move method of the selected piece to move it to the clicked tile
            ClearSelection();      // Clear the selection after the capture is made
            m_board.ClearAllHighlights();     // Clear all highlights on the board after the capture is made
            return;
        }
        if(tile.m_occupiedBy != null && tile.m_occupiedBy.m_isWhite)        // Check if the clicked tile is occupied by a white piece
        {
            SelectPiece(tile.m_occupiedBy);      // If the piece on the clicked tile is white, select it
            return;
        }

        ClearSelection();      // Clear the selection after the move or attack is made
        m_board.ClearAllHighlights();     // Clear all highlights on the board after the move or attack is made
    }

    private void ClearSelection()
    {
        if (m_selectedPiece != null)        // Check if there is a currently selected piece
        {
            m_selectedPiece.Deselect();      // Call the Deselect method to reset the material of the currently selected piece
            m_selectedPiece = null;      // Set the currently selected piece to null
        }
    }
}
