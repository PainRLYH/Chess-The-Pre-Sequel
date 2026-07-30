using UnityEngine;

public class Queen : Piece
{
    protected override void Start()
    {
        base.Start();
        m_health = 12;
        m_attackPower = 6;
        m_defense = 2;
        m_attackType = AttackTypes.Slashing;
        m_pierceResist = 1.25f;
        m_slashResist = 1.0f;
        m_bludgeonResist = 0.75f;
    }

    public override bool LegalMove(Tile target_Tile)
    {
        if (target_Tile.m_occupiedBy != null)
        {
            return false;
        }

        // Rook movement check
        if (m_currentCoordinates.x == target_Tile.m_coordinates.x || m_currentCoordinates.y == target_Tile.m_coordinates.y)
        {
            int min_x = Mathf.Min(m_currentCoordinates.x, target_Tile.m_coordinates.x);    // Left
            int max_x = Mathf.Max(m_currentCoordinates.x, target_Tile.m_coordinates.x);    // Right
            int min_y = Mathf.Min(m_currentCoordinates.y, target_Tile.m_coordinates.y);    // Down
            int max_y = Mathf.Max(m_currentCoordinates.y, target_Tile.m_coordinates.y);    // Up

            // Check if there are pieces in the way
            for (int i = min_x + 1; i < max_x; i++)     // Check horizontal movement
            {
                if (m_board.m_tiles[i, m_currentCoordinates.y].GetComponent<Tile>().m_occupiedBy != null)
                {
                    return false;
                }
            }
            for (int j = min_y + 1; j < max_y; j++)     // Check vertical movement
            {
                if (m_board.m_tiles[m_currentCoordinates.x, j].GetComponent<Tile>().m_occupiedBy != null)
                {
                    return false;
                }
            }
            return true;
        }
        
        // Bishop movement check
        int dx = target_Tile.m_coordinates.x - m_currentCoordinates.x;     // Calculate the difference in x and y coordinates between the current position and the target tile
        int dy = target_Tile.m_coordinates.y - m_currentCoordinates.y;

        if (Mathf.Abs(dx) != Mathf.Abs(dy))
        {
            return false;
        }

        int x_dir = (int)Mathf.Sign(dx);    // Determine the direction of movement in the x and y axes
        int y_dir = (int)Mathf.Sign(dy);

        for (int i = 1; i < Mathf.Abs(dx); i++)     // Check each tile along the path to ensure it is not occupied by another piece
        {
            int x = m_currentCoordinates.x + i * x_dir;      // Calculate the coordinates of the tile being checked
            int y = m_currentCoordinates.y + i * y_dir;
            if (m_board.m_tiles[x, y].GetComponent<Tile>().m_occupiedBy != null)
            {
                return false;
            }
        }
        return true;
    }
}
