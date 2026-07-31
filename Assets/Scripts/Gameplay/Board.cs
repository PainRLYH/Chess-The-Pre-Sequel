using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject m_tileWhitePrefab, m_tileBlackPrefab;
    public GameObject m_whitePawnPrefab, m_whiteRookPrefab, m_whiteKnightPrefab, m_whiteBishopPrefab, m_whiteQueenPrefab, m_whiteKingPrefab;
    public GameObject m_blackPawnPrefab, m_blackRookPrefab, m_blackKnightPrefab, m_blackBishopPrefab, m_blackQueenPrefab, m_blackKingPrefab;

    [Header("Settings")]
    public bool m_useFisherRandom = false;

    [HideInInspector]
    public static string[] s_alphabet = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };

    //Generate the chess board
    public Tile[,] m_tiles = new Tile[8, 8];

    // Array to hold the white pieces
    GameObject[] m_whitePieceArrangement;

    // Array to hold the black pieces
    GameObject[] m_blackPieceArrangement;

    public void CreateBoard()
    {
        for(int i = 0; i <= 7; i++)
        {
            for(int j = 0; j <= 7; j++)
            {
               if((i + j) % 2 == 0)
               {
                    GameObject tempTileHold = Instantiate(m_tileWhitePrefab, new Vector3(i, j, 0), Quaternion.identity);
                    m_tiles[i, j] = tempTileHold.GetComponent<Tile>();
                    m_tiles[i, j].transform.SetParent(gameObject.transform);
                    m_tiles[i, j].name = s_alphabet[i] + (j + 1);
                    m_tiles[i, j].m_coordinates = new Vector2Int(i, j);
               }
               else
               {
                    GameObject tempTileHold = Instantiate(m_tileBlackPrefab, new Vector3(i, j, 0), Quaternion.identity);
                    m_tiles[i, j] = tempTileHold.GetComponent<Tile>();
                    m_tiles[i, j].transform.SetParent(gameObject.transform);
                    m_tiles[i, j].name = s_alphabet[i] + (j + 1);
                    m_tiles[i, j].m_coordinates = new Vector2Int(i, j);
                }
            }
        }
    }

    public void ArrangePieces()
    {
        m_whitePieceArrangement = new GameObject[] { 
            m_whiteRookPrefab,                          // R0
            m_whiteKnightPrefab,                        // N1
            m_whiteBishopPrefab,                        // B2
            m_whiteQueenPrefab,                         // Q3
            m_whiteKingPrefab,                          // K4
            m_whiteBishopPrefab,                        // B5
            m_whiteKnightPrefab,                        // N6
            m_whiteRookPrefab                           // R7
        };

        m_blackPieceArrangement = new GameObject[] {
            m_blackRookPrefab,                          // R0
            m_blackKnightPrefab,                        // N1
            m_blackBishopPrefab,                        // B2
            m_blackQueenPrefab,                         // Q3
            m_blackKingPrefab,                          // K4
            m_blackBishopPrefab,                        // B5
            m_blackKnightPrefab,                        // N6
            m_blackRookPrefab                           // R7
        };

        if(m_useFisherRandom)
        {
            RandomizedArrangement(m_whitePieceArrangement);
            RandomizedArrangement(m_blackPieceArrangement);
        }

        for (int i = 0; i <= 7; i++)
        {
            // Instantiate the Pieces
            GameObject newWhitePiece = Instantiate(m_whitePieceArrangement[i], m_tiles[i, 0].transform);
            newWhitePiece.GetComponent<Piece>().m_isWhite = true;
            m_tiles[i, 0].m_occupiedBy = newWhitePiece.GetComponent<Piece>();
            GameObject newBlackPiece = Instantiate(m_blackPieceArrangement[i], m_tiles[i, 7].transform);
            newBlackPiece.GetComponent<Piece>().m_isWhite = false;
            m_tiles[i, 7].m_occupiedBy = newBlackPiece.GetComponent<Piece>();

            // Instatiate the Pawns
            GameObject newWhitePawn = Instantiate(m_whitePawnPrefab, m_tiles[i, 1].transform);
            newWhitePawn.GetComponent<Piece>().m_isWhite = true;
            m_tiles[i, 1].m_occupiedBy = newWhitePawn.GetComponent<Piece>();
            GameObject newBlackPawn = Instantiate(m_blackPawnPrefab, m_tiles[i, 6].transform);
            newBlackPawn.GetComponent<Piece>().m_isWhite = false;
            m_tiles[i, 6].m_occupiedBy = newBlackPawn.GetComponent<Piece>();
        }
    }

    public void RandomizedArrangement(GameObject[] pieces)
    {
        for (int i = pieces.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            GameObject tempHolder = pieces[i];
            pieces[i] = pieces[randomIndex];
            pieces[randomIndex] = tempHolder;
        }
    }
}
