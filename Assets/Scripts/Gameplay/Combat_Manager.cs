using UnityEngine;

public class Combat_Manager : MonoBehaviour
{
    public void StartCombat(Piece attacker, Piece defender)
    {
        int damage = 0;
        switch (attacker.attack_Type)
        {
            case Piece.Attack_Types.Piercing:
                damage = (int)((attacker.attack_pwr * defender.pierce_resist) - defender.defense);  // Calculate damage based on attack type and defender's resistances
                break;
            case Piece.Attack_Types.Slashing:
                damage = (int)((attacker.attack_pwr * defender.slash_resist) - defender.defense);
                break;
            case Piece.Attack_Types.Bludgeoning:
                damage = (int)((attacker.attack_pwr * defender.bludgeon_resist) - defender.defense);
                break;
        }
        defender.health -= (int)Mathf.Max(damage, 1);
        Debug.Log($"{attacker.name} attacked {defender.name} for {Mathf.Max(damage, 1)} damage. {defender.name} has {defender.health} health remaining.");

        if (defender.health <= 0)
        {
            Tile defender_Tile = defender.GetComponentInParent<Tile>();     // Get the tile that the defender is on
            GameObject.Destroy(defender.gameObject);    // Destroy the defender piece
            defender_Tile.occupied_By = null;   // Clear the tile's reference to the destroyed piece
            Piece.selected_Piece.Move(defender_Tile);   // Move the attacker to the defender's tile
        }
    }
}
