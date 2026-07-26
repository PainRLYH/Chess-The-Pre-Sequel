using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject tile_White_Prefab, tile_Black_Prefab;
    public GameObject W_Pawn_Prefab, W_Rook_Prefab, W_Knight_Prefab, W_Bishop_Prefab, W_Queen_Prefab, W_King_Prefab;
    public GameObject B_Pawn_Prefab, B_Rook_Prefab, B_Knight_Prefab, B_Bishop_Prefab, B_Queen_Prefab, B_King_Prefab;

    [Header("Settings")]
    public bool fisher = false;

    [HideInInspector]
    public static string[] alphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };

    //Generate the chess board
    public GameObject[,] tiles = new GameObject[8, 8];

    // Array to hold the white pieces
    GameObject[] w_Piece_Arangement;

    // Array to hold the black pieces
    GameObject[] b_Piece_Arangement;

    public void CreateBoard()
    {
        for(int i = 0; i <= 7; i++)
        {
            for(int j = 0; j <= 7; j++)
            {
               if((i + j) % 2 == 0)
               {
                    tiles[i, j] = Instantiate(tile_White_Prefab, new Vector3(i, j, 0), Quaternion.identity);
                    tiles[i, j].transform.SetParent(gameObject.transform);
                    tiles[i, j].name = alphabet[i] + (j + 1);
                    tiles[i, j].GetComponent<Tile>().coordinates = new Vector2Int(i, j);
               }
               else
               {
                    tiles[i, j] = Instantiate(tile_Black_Prefab, new Vector3(i, j, 0), Quaternion.identity);
                    tiles[i, j].transform.SetParent(gameObject.transform);
                    tiles[i, j].name = alphabet[i] + (j + 1);
                    tiles[i, j].GetComponent<Tile>().coordinates = new Vector2Int(i, j);
                }
            }
        }
    }

    public void ArrangePieces()
    {
        w_Piece_Arangement = new GameObject[] { 
            W_Rook_Prefab,                          // R0
            W_Knight_Prefab,                        // N1
            W_Bishop_Prefab,                        // B2
            W_Queen_Prefab,                         // Q3
            W_King_Prefab,                          // K4
            W_Bishop_Prefab,                        // B5
            W_Knight_Prefab,                        // N6
            W_Rook_Prefab                           // R7
        };

        b_Piece_Arangement = new GameObject[] {
            B_Rook_Prefab,                          // R0
            B_Knight_Prefab,                        // N1
            B_Bishop_Prefab,                        // B2
            B_Queen_Prefab,                         // Q3
            B_King_Prefab,                          // K4
            B_Bishop_Prefab,                        // B5
            B_Knight_Prefab,                        // N6
            B_Rook_Prefab                           // R7
        };

        if(fisher)
        {
            RandomizedArrangement(w_Piece_Arangement);
            RandomizedArrangement(b_Piece_Arangement);
        }

        for (int i = 0; i <= 7; i++)
        {
            // Instantiate the Pieces
            GameObject new_White_Piece = Instantiate(w_Piece_Arangement[i], tiles[i, 0].transform);
            new_White_Piece.GetComponent<Piece>().isWhite = true;
            tiles[i, 0].GetComponent<Tile>().occupied_By = new_White_Piece.GetComponent<Piece>();
            GameObject new_Black_Piece = Instantiate(b_Piece_Arangement[i], tiles[i, 7].transform);
            new_Black_Piece.GetComponent<Piece>().isWhite = false;
            tiles[i, 7].GetComponent<Tile>().occupied_By = new_Black_Piece.GetComponent<Piece>();

            // Instatiate the Pawns
            GameObject new_White_Pawn = Instantiate(W_Pawn_Prefab, tiles[i, 1].transform);
            new_White_Pawn.GetComponent<Piece>().isWhite = true;
            tiles[i, 1].GetComponent<Tile>().occupied_By = new_White_Pawn.GetComponent<Piece>();
            GameObject new_Black_Pawn = Instantiate(B_Pawn_Prefab, tiles[i, 6].transform);
            new_Black_Pawn.GetComponent<Piece>().isWhite = false;
            tiles[i, 6].GetComponent<Tile>().occupied_By = new_Black_Pawn.GetComponent<Piece>();
        }
    }

    public void RandomizedArrangement(GameObject[] pieces)
    {
        for (int i = pieces.Length - 1; i > 0; i--)
        {
            int random_Index = UnityEngine.Random.Range(0, i);
            GameObject temp_Holder = pieces[i];
            pieces[i] = pieces[random_Index];
            pieces[random_Index] = temp_Holder;
        }
    }
}
