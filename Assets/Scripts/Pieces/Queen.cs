using UnityEngine;

public class Queen : Piece
{
    protected override void Start()
    {
        base.Start();
        health = 12;
        attack_pwr = 6;
        defense = 2;
        attack_Type = Attack_Types.Slashing;
        pierce_resist = 1.25f;
        slash_resist = 1.0f;
        bludgeon_resist = 0.75f;
    }

    public override bool legal_Move(Tile target_Tile)
    {
        if (target_Tile.occupied_By != null)
        {
            return false;
        }

        // Rook movement check
        if (current_Coordinates.x == target_Tile.coordinates.x || current_Coordinates.y == target_Tile.coordinates.y)
        {
            int min_x = Mathf.Min(current_Coordinates.x, target_Tile.coordinates.x);    // Left
            int max_x = Mathf.Max(current_Coordinates.x, target_Tile.coordinates.x);    // Right
            int min_y = Mathf.Min(current_Coordinates.y, target_Tile.coordinates.y);    // Down
            int max_y = Mathf.Max(current_Coordinates.y, target_Tile.coordinates.y);    // Up

            // Check if there are pieces in the way
            for (int i = min_x + 1; i < max_x; i++)     // Check horizontal movement
            {
                if (board.tiles[i, current_Coordinates.y].GetComponent<Tile>().occupied_By != null)
                {
                    return false;
                }
            }
            for (int j = min_y + 1; j < max_y; j++)     // Check vertical movement
            {
                if (board.tiles[current_Coordinates.x, j].GetComponent<Tile>().occupied_By != null)
                {
                    return false;
                }
            }
            return true;
        }
        
        // Bishop movement check
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
