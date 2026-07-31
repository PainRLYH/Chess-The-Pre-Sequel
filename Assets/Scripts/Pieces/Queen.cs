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

    public override bool LegalMove(Tile targetTile)
    {
        if (targetTile.m_occupiedBy != null)
        {
            return false;
        }

        // Rook movement check
        if (m_currentCoordinates.x == targetTile.m_coordinates.x || m_currentCoordinates.y == targetTile.m_coordinates.y)
        {
            int minX = Mathf.Min(m_currentCoordinates.x, targetTile.m_coordinates.x);    // Left
            int maxX = Mathf.Max(m_currentCoordinates.x, targetTile.m_coordinates.x);    // Right
            int minY = Mathf.Min(m_currentCoordinates.y, targetTile.m_coordinates.y);    // Down
            int maxY = Mathf.Max(m_currentCoordinates.y, targetTile.m_coordinates.y);    // Up

            // Check if there are pieces in the way
            for (int i = minX + 1; i < maxX; i++)     // Check horizontal movement
            {
                if (m_board.m_tiles[i, m_currentCoordinates.y].GetComponent<Tile>().m_occupiedBy != null)
                {
                    return false;
                }
            }
            for (int j = minY + 1; j < maxY; j++)     // Check vertical movement
            {
                if (m_board.m_tiles[m_currentCoordinates.x, j].GetComponent<Tile>().m_occupiedBy != null)
                {
                    return false;
                }
            }
            return true;
        }
        
        // Bishop movement check
        int dx = targetTile.m_coordinates.x - m_currentCoordinates.x;     // Calculate the difference in x and y coordinates between the current position and the target tile
        int dy = targetTile.m_coordinates.y - m_currentCoordinates.y;

        if (Mathf.Abs(dx) != Mathf.Abs(dy))
        {
            return false;
        }

        int xDir = (int)Mathf.Sign(dx);    // Determine the direction of movement in the x and y axes
        int yDir = (int)Mathf.Sign(dy);

        for (int i = 1; i < Mathf.Abs(dx); i++)     // Check each tile along the path to ensure it is not occupied by another piece
        {
            int x = m_currentCoordinates.x + i * xDir;      // Calculate the coordinates of the tile being checked
            int y = m_currentCoordinates.y + i * yDir;
            if (m_board.m_tiles[x, y].GetComponent<Tile>().m_occupiedBy != null)
            {
                return false;
            }
        }
        return true;
    }
}
