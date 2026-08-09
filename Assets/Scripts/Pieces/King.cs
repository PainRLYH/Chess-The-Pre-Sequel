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
        return CanReach(targetTile, Directions.Universal, 1);  // King can move 1 tile in any direction
    }
}
