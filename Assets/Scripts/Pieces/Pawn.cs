    using UnityEngine;

    public class Pawn : Piece
    {
        protected override void Start()
        {
            base.Start();
            m_health = 5;
            m_attackPower = 2;
            m_defense = 1;
            m_attackType = AttackTypes.Slashing;
            m_pierceResist = 1.25f;
            m_slashResist = 1.0f;
            m_bludgeonResist = 0.75f;
        }

       public override bool LegalMove(Tile target_Tile)
       {
            // 1 tile step forward
            if (target_Tile.m_occupiedBy == null)
            {
                if (target_Tile.m_coordinates.y == m_currentCoordinates.y + 1)
                {
                    if (target_Tile.m_coordinates.x == m_currentCoordinates.x)
                    {
                        return true;
                    }
                }
            }

            // 2 tile step forward
            if (target_Tile.m_occupiedBy == null)
            {
                if (target_Tile.m_coordinates.y == m_currentCoordinates.y + 2 && m_currentCoordinates.y == 1)
                {
                    if (m_board.m_tiles[m_currentCoordinates.x, m_currentCoordinates.y + 1].GetComponent<Tile>().m_occupiedBy == null)
                    {
                        if (target_Tile.m_coordinates.x == m_currentCoordinates.x)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
       }

    public override bool LegalAttack(Tile target_Tile)
    {
        if (target_Tile.m_coordinates.y == m_currentCoordinates.y + 1)     // Check if the target tile is one step forward
        {
            if (Mathf.Abs(target_Tile.m_coordinates.x - m_currentCoordinates.x) == 1)      // Check if the target tile is one step diagonally
            {
                return true;
            }
        }
        return false;
    }
}

