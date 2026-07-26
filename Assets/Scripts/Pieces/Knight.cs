using UnityEngine;

public class Knight : Piece
{
    protected override void Start()
    {
        base.Start();
        health = 8;
        attack_pwr = 4;
        defense = 2;
        attack_Type = Attack_Types.Piercing;
        pierce_resist = 0.75f;
        slash_resist = 1.25f;
        bludgeon_resist = 1.0f;
    }

    public override bool legal_Move(Tile target_Tile)
    {
        if (target_Tile.occupied_By != null)
        {
            return false;
        }

        if (Mathf.Abs(target_Tile.coordinates.x - current_Coordinates.x) == 2)      // If the target tile is 2 spaces away horizontally
        {
            if (Mathf.Abs(target_Tile.coordinates.y - current_Coordinates.y) == 1)      // If the target tile is 1 space away vertically
            {
                return true;
            }
        }
        else if (Mathf.Abs(target_Tile.coordinates.x - current_Coordinates.x) == 1)     // If the target tile is 1 space away horizontally
        {
             if (Mathf.Abs(target_Tile.coordinates.y - current_Coordinates.y) == 2)      // If the target tile is 2 spaces away vertically
             {
                 return true;
             }
        }
        else
        {
            return false;
        }
        return false;
    }
}
