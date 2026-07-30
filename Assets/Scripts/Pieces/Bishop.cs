using UnityEngine;

public class Bishop : Piece
{
    protected override void Start()
    {
        base.Start();
        m_health = 6;
        m_attackPower = 3;
        m_defense = 1;
        m_attackType = AttackTypes.Piercing;
        m_pierceResist = 0.75f;
        m_slashResist = 1.25f;
        m_bludgeonResist = 1.0f;
    }

    public override bool LegalMove(Tile target_Tile)
    {
        if (target_Tile.m_occupiedBy != null)
        {
            return false;
        }

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
            if (m_board.tiles[x, y].GetComponent<Tile>().m_occupiedBy != null)
            {
                return false;
            }
        }
        return true;
    }
}
