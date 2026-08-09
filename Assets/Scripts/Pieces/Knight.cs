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

    public override bool LegalMove(Tile targetTile)
    {
        return CanReach(targetTile, Directions.Knight, 1);  // Knight can move in an L-shape (2 in one direction and 1 in the perpendicular direction)
    }
}
