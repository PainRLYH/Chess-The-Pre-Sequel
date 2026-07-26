using UnityEngine;

public class King : Piece
{
    protected override void Start()
    {
        base.Start();
        health = 15;
        attack_pwr = 4;
        defense = 4;
        attack_Type = Attack_Types.Bludgeoning;
        pierce_resist = 1.0f;
        slash_resist = 0.75f;
        bludgeon_resist = 1.25f;
    }

    public override bool legal_Move(Tile target_Tile)
    {
        if (target_Tile.occupied_By != null)
        {
            return false;
        }

        int dx = target_Tile.coordinates.x - current_Coordinates.x;     // Calculate the difference in x and y coordinates between the current position and the target tile
        int dy = target_Tile.coordinates.y - current_Coordinates.y;

        if (!(Mathf.Abs(dx) <=1 && Mathf.Abs(dy) <= 1))
        {
            return false;
        }

        return true;
    }
}
