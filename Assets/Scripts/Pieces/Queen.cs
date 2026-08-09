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
        return CanReach(targetTile, Directions.Universal, 7);  // Queen can move up to 7 tiles in any direction
    }
}
