using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothTerrainGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Prefab")]
    public GameObject planePrefab;

    [Header("Configuración")]
    public int flatPieces = 3;
    public int uphillPieces = 3;
    public int downhillPieces = 3;

    [Header("Tamaño")]
    public float pieceLength = 10f;

    [Header("Pendiente")]
    public float slopeAngle = 15f;

    private Vector3 currentPosition = Vector3.zero;

    void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        // =========================
        // PLANICIE
        // =========================
        for (int i = 0; i < flatPieces; i++)
        {
            CreatePiece(currentPosition, 0f);

            currentPosition += Vector3.right * pieceLength;
        }

        // =========================
        // SUBIDA
        // =========================
        for (int i = 0; i < uphillPieces; i++)
        {
            float radians = slopeAngle * Mathf.Deg2Rad;

            currentPosition += new Vector3(
                Mathf.Cos(radians) * pieceLength,
                Mathf.Sin(radians) * pieceLength,
                0
            );

            CreatePiece(currentPosition, -slopeAngle);
        }

        // =========================
        // BAJADA
        // =========================
        for (int i = 0; i < downhillPieces; i++)
        {
            float radians = slopeAngle * Mathf.Deg2Rad;

            currentPosition += new Vector3(
                Mathf.Cos(radians) * pieceLength,
                -Mathf.Sin(radians) * pieceLength,
                0
            );

            CreatePiece(currentPosition, slopeAngle);
        }
    }

    void CreatePiece(Vector3 position, float zRotation)
    {
        GameObject piece = Instantiate(
            planePrefab,
            position,
            Quaternion.Euler(0, 0, zRotation),
            transform
        );

        piece.name = "Terrain Piece";
    }
}
