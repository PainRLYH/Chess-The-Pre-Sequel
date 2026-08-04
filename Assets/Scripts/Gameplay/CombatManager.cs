using UnityEngine;

public class CombatManager : MonoBehaviour
{
    // References
    Board m_board;

    private void Start()
    {
        m_board = FindAnyObjectByType<Board>();    
    }

    public void StartCombat(Piece attacker, Piece defender)
    {
        int damage = 0;
        switch (attacker.m_attackType)
        {
            case Piece.AttackTypes.Piercing:
                damage = (int)((attacker.m_attackPower * defender.m_pierceResist) - defender.m_defense);  // Calculate damage based on attack type and defender's resistances
                break;
            case Piece.AttackTypes.Slashing:
                damage = (int)((attacker.m_attackPower * defender.m_slashResist) - defender.m_defense);
                break;
            case Piece.AttackTypes.Bludgeoning:
                damage = (int)((attacker.m_attackPower * defender.m_bludgeonResist) - defender.m_defense);
                break;
        }
        defender.m_health -= (int)Mathf.Max(damage, 1);
        Debug.Log($"{attacker.name} attacked {defender.name} for {Mathf.Max(damage, 1)} damage. {defender.name} has {defender.m_health} health remaining.");

        if (defender.m_health <= 0)
        {
            Tile defenderTile = defender.GetComponentInParent<Tile>();     // Get the tile that the defender is on
            GameObject.Destroy(defender.gameObject);    // Destroy the defender piece
            defenderTile.m_occupiedBy = null;   // Clear the tile's reference to the destroyed piece
            Piece.s_selectedPiece.Move(defenderTile);   // Move the attacker to the defender's tile
        }

        m_board.ClearAllHighlights();     // Clear all highlights on the board

        Piece.Deselect();    // Deselect the attacking piece
    }
}
