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

    public override bool LegalMove(Tile target_Tile)
    {
        // X & Y checks
        if (m_currentCoordinates.x == target_Tile.m_coordinates.x || m_currentCoordinates.y == target_Tile.m_coordinates.y)
        {
            if (target_Tile.m_occupiedBy == null)
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
        }
        return false;
    }
}
