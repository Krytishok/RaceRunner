using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roadSectionPrefabs;       // ������ �������� ������ ������
    public GameObject[] startSectionPrefabs;      // ��������� ������ ������
    public GameObject[] obstaclePrefabs;          // ������ �������� �����������
    public GameObject[] coinPrefabs;

    public int initialSections = 5;               // ���������� ��������� ������
    public float sectionLength = 119.5f;          // ����� ����� ������
    public int despawnDistance = 2;               // ���������� ��� �������� ������ ������

    public float minDistanceBetweenObstacles = 15f; // ����������� ���������� ����� �������������
    public float difficultyIncreaseRate = 0.05f;     // �������� ����� ���������
    public float maxObstaclesPerSection = 7;       // ������������ ���������� ����������� �� ������� ��� ������� ���������
    private float difficultyLevel = 1f;             // ������� ������� ���������

    private Queue<GameObject> activeSections = new Queue<GameObject>(); // ������� �������� ������
    private Vector3 nextPosition;                 // ������� ��� ���������� ��������


    [Header("Spawn Settings")]
    public float laneOffsetX = 1f;           // �������� ������ ������/�����
    public float spawnHeight = 20f;           // ������ ������ ������ (��� Raycast)
    public float spawnHeightOffset = 1f;     // ����� ������ ��� ����� ��� ������������

    private float[] lanePositions = new float[] { -10f, -3f, 3f, 10f };  // ������� �����

    private void Start()
    {
        nextPosition = new Vector3(-17.27f, -29.843f, (-635.73f - sectionLength * (startSectionPrefabs.Length - 1)));

        foreach (var section in startSectionPrefabs)
        {
            activeSections.Enqueue(section);
        }

        for (int i = 0; i < initialSections - startSectionPrefabs.Length; i++)
        {
            SpawnSection();
        }

        Debug.Log("START");
    }

    private void Update()
    {
        // ���������� ����������� ��������� �� ���� ����������� ������� ������
        difficultyLevel += difficultyIncreaseRate * Time.deltaTime;
    }

    public void SpawnSection()
    {
        GameObject sectionPrefab = roadSectionPrefabs[Random.Range(0, roadSectionPrefabs.Length)];
        GameObject newSection = Instantiate(sectionPrefab, nextPosition, Quaternion.identity);

        activeSections.Enqueue(newSection);

        // ��������� ����������� ��� ������ �������� � ������ ������ ���������
        SpawnObstaclesAndCoins(newSection);

        nextPosition += new Vector3(0, 0, -sectionLength);
    }

    private void SpawnObstaclesAndCoins(GameObject section)
    {
        int obstaclesPerSection = (int)Mathf.Min(difficultyLevel * 2f, maxObstaclesPerSection);
        int coinsPerSection = 10;

        List<Vector3> obstaclePositions = new List<Vector3>();
        List<Vector3> coinPositions = new List<Vector3>();

        for (int i = 0; i < obstaclesPerSection; i++)
        {
            GameObject obstaclePrefab = difficultyLevel > 3 && obstaclePrefabs.Length > 1
                ? obstaclePrefabs[Random.Range(1, obstaclePrefabs.Length)]
                : obstaclePrefabs[0];

            Collider obstacleCollider = obstaclePrefab.GetComponent<Collider>();
            float obstacleSize = obstacleCollider ? obstacleCollider.bounds.size.z : 5f;

            float xPosition = lanePositions[Random.Range(0, lanePositions.Length)] + laneOffsetX;
            float currentMinDistance = Mathf.Max(obstacleSize * 1.5f, minDistanceBetweenObstacles - difficultyLevel * 0.5f);

            float zPosition;
            bool positionIsValid;

            do
            {
                zPosition = Random.Range(currentMinDistance, sectionLength / 2 - currentMinDistance);
                positionIsValid = true;

                foreach (Vector3 pos in obstaclePositions)
                {
                    if (Vector3.Distance(new Vector3(xPosition, 0, zPosition), pos) < currentMinDistance)
                    {
                        positionIsValid = false;
                        break;
                    }
                }
            } while (!positionIsValid);

            Vector3 rayStart = section.transform.position + new Vector3(xPosition, spawnHeight, zPosition);
            Debug.DrawRay(rayStart, Vector3.down * 100f, Color.red, 2f);

            RaycastHit hit;
            if (Physics.Raycast(rayStart, Vector3.down, out hit, 100f))
            {
                Vector3 obstaclePosition = hit.point;

                GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
                obstacle.transform.SetParent(section.transform);
                obstaclePositions.Add(obstaclePosition);
            }
        }

        for (int j = 0; j < coinsPerSection; j++)
        {
            GameObject coinPrefab = coinPrefabs[Random.Range(0, coinPrefabs.Length)];

            float xPosition = lanePositions[Random.Range(0, lanePositions.Length)] + laneOffsetX;

            float zPosition;
            bool positionIsValid;

            do
            {
                zPosition = Random.Range(-sectionLength / 2 + 2f, sectionLength / 2 - 2f);
                positionIsValid = true;

                foreach (Vector3 pos in coinPositions)
                {
                    if (Vector3.Distance(new Vector3(xPosition, 0, zPosition), pos) < 2f)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                foreach (Vector3 pos in obstaclePositions)
                {
                    if (Vector3.Distance(new Vector3(xPosition, 0, zPosition), pos) < 2f)
                    {
                        positionIsValid = false;
                        break;
                    }
                }
            } while (!positionIsValid);

            Vector3 coinRayStart = section.transform.position + new Vector3(xPosition, spawnHeight, zPosition);
            Debug.DrawRay(coinRayStart, Vector3.down * 100f, Color.yellow, 2f);

            RaycastHit hit;
            if (Physics.Raycast(coinRayStart, Vector3.down, out hit, 100f))
            {
                Vector3 coinPosition = hit.point + Vector3.up * spawnHeightOffset;

                GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity);
                coin.transform.SetParent(section.transform);
                coinPositions.Add(coinPosition);
            }
        }
    }

    public void DespawnOldSection()
    {
        if (activeSections.Count > initialSections + despawnDistance)
        {
            GameObject oldSection = activeSections.Dequeue();
            Destroy(oldSection);
        }
    }
}
