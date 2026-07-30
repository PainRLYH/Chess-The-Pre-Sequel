using UnityEngine;

public class King : Piece
{
    protected override void Start()
    {
        base.Start();
        m_health = 15;
        m_attackPower = 4;
        m_defense = 4;
        m_attackType = AttackTypes.Bludgeoning;
        m_pierceResist = 1.0f;
        m_slashResist = 0.75f;
        m_bludgeonResist = 1.25f;
    }

    public override bool LegalMove(Tile targetTile)
    {
        if (targetTile.m_occupiedBy != null)
        {
            return false;
        }

        int dx = targetTile.m_coordinates.x - m_currentCoordinates.x;     // Calculate the difference in x and y coordinates between the current position and the target tile
        int dy = targetTile.m_coordinates.y - m_currentCoordinates.y;

        if (!(Mathf.Abs(dx) <=1 && Mathf.Abs(dy) <= 1))
        {
            return false;
        }

        return true;
    }
}
