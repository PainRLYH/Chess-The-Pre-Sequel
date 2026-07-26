using UnityEngine;

public class Bishop : Piece
{
    protected override void Start()
    {
        base.Start();
        health = 6;
        attack_pwr = 3;
        defense = 1;
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

        int dx = target_Tile.coordinates.x - current_Coordinates.x;     // Calculate the difference in x and y coordinates between the current position and the target tile
        int dy = target_Tile.coordinates.y - current_Coordinates.y;     

        if (Mathf.Abs(dx) != Mathf.Abs(dy))     
        {
            return false;
        }

        int x_dir = (int)Mathf.Sign(dx);    // Determine the direction of movement in the x and y axes
        int y_dir = (int)Mathf.Sign(dy);

        for (int i = 1; i < Mathf.Abs(dx); i++)     // Check each tile along the path to ensure it is not occupied by another piece
        {
            int x = current_Coordinates.x + i * x_dir;      // Calculate the coordinates of the tile being checked
            int y = current_Coordinates.y + i * y_dir;      
            if (board.tiles[x, y].GetComponent<Tile>().occupied_By != null)
            {
                return false;
            }
        }
        return true;
    }
}
