using UnityEngine;

public class Knight : Piece
{
    protected override void Start()
    {
        base.Start();
        m_health = 8;
        m_attackPower = 4;
        m_defense = 2;
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

        if (Mathf.Abs(target_Tile.m_coordinates.x - m_currentCoordinates.x) == 2)      // If the target tile is 2 spaces away horizontally
        {
            if (Mathf.Abs(target_Tile.m_coordinates.y - m_currentCoordinates.y) == 1)      // If the target tile is 1 space away vertically
            {
                return true;
            }
        }
        else if (Mathf.Abs(target_Tile.m_coordinates.x - m_currentCoordinates.x) == 1)     // If the target tile is 1 space away horizontally
        {
             if (Mathf.Abs(target_Tile.m_coordinates.y - m_currentCoordinates.y) == 2)      // If the target tile is 2 spaces away vertically
             {
                 return true;
             }
        }
        else
        {
            return false;
        }
        return false;
    }
}
