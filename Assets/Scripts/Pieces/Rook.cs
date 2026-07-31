using UnityEngine;

public class Rook : Piece
{
    protected override void Start()
    {
        base.Start();
        m_health = 10;
        m_attackPower = 5;
        m_defense = 3;
        m_attackType = AttackTypes.Bludgeoning;
        m_pierceResist = 1.0f;
        m_slashResist = 0.75f;
        m_bludgeonResist = 1.25f;
    }

    public override bool LegalMove(Tile targetTile)
    {
        // X & Y checks
        if (m_currentCoordinates.x == targetTile.m_coordinates.x || m_currentCoordinates.y == targetTile.m_coordinates.y)
        {
            if (targetTile.m_occupiedBy == null)
            { 
                int minX = Mathf.Min(m_currentCoordinates.x, targetTile.m_coordinates.x);    // Left
                int maxX = Mathf.Max(m_currentCoordinates.x, targetTile.m_coordinates.x);    // Right
                int minY = Mathf.Min(m_currentCoordinates.y, targetTile.m_coordinates.y);    // Down
                int maxY = Mathf.Max(m_currentCoordinates.y, targetTile.m_coordinates.y);    // Up

                // Check if there are pieces in the way
                for (int i = minX + 1; i < maxX; i++)     // Check horizontal movement
                {
                    if (m_board.m_tiles[i, m_currentCoordinates.y].m_occupiedBy != null)
                    {
                        return false;
                    }
                }
                for (int j = minY + 1; j < maxY; j++)     // Check vertical movement
                {
                    if (m_board.m_tiles[m_currentCoordinates.x, j].m_occupiedBy != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        return false;
    }
}
