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

    public override bool LegalMove(Tile targetTile)
    {
        return CanReach(targetTile, Directions.Diagonal, 7);     // Check if the target tile is reachable in a diagonal direction within 7 spaces
    }
}
