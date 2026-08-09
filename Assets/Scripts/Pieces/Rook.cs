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
        return CanReach(targetTile, Directions.Orthogonal, 7);  // Rook can move up to 7 tiles in orthogonal directions
    }
}
