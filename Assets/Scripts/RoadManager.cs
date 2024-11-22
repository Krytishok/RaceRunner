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

    private float[] lanePositions = new float[] { -10f, -3f, 4f, 11f };  // ������� �����

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

        // ����� �����������
        for (int i = 0; i < obstaclesPerSection; i++)
        {
            // �������� ��������� ������ �����������
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

            Collider obstacleCollider = obstaclePrefab.GetComponent<Collider>();
            float obstacleSize = obstacleCollider ? obstacleCollider.bounds.size.z : 5f;

            float xPosition = lanePositions[Random.Range(0, lanePositions.Length)] + laneOffsetX;
            float currentMinDistance = Mathf.Max(obstacleSize * 2f, minDistanceBetweenObstacles - difficultyLevel * 0.5f);

            float zPosition;
            bool positionIsValid;

            // �������� ������� ��� �����������
            int attemptCount = 0; // ����������� ������� ������ �������� �������
            do
            {
                zPosition = Random.Range(-sectionLength / 2 + obstacleSize, sectionLength / 2 - obstacleSize);
                positionIsValid = true;

                foreach (Vector3 pos in obstaclePositions)
                {
                    // ��������� ���������� ������ �� Z
                    if (Mathf.Abs(zPosition - pos.z) < currentMinDistance && Mathf.Abs(xPosition - pos.x) < 0.1f)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                attemptCount++;
            } while (!positionIsValid && attemptCount < 20); // ������������� ����������� ����

            if (!positionIsValid) continue; // ���� �� ������ ����� �����, ����������

            Vector3 rayStart = section.transform.position + new Vector3(xPosition, spawnHeight, zPosition);
            Debug.DrawRay(rayStart, Vector3.down * 100f, Color.red, 2f);

            RaycastHit hit;
            if (Physics.Raycast(rayStart, Vector3.down, out hit, 100f))
            {
                Vector3 obstaclePosition = hit.point;

                GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
                obstacle.transform.SetParent(section.transform);
                obstaclePositions.Add(new Vector3(xPosition, 0, zPosition)); // ��������� ������ X � Z
            }
        }

        // ����� �����
        for (int j = 0; j < coinsPerSection; j++)
        {
            GameObject coinPrefab = coinPrefabs[Random.Range(0, coinPrefabs.Length)];

            Collider coinCollider = coinPrefab.GetComponent<Collider>();
            float coinSize = coinCollider ? coinCollider.bounds.size.z : 2f;

            float xPosition = lanePositions[Random.Range(0, lanePositions.Length)] + laneOffsetX;

            float zPosition;
            bool positionIsValid;

            int attemptCount = 0;
            do
            {
                zPosition = Random.Range(-sectionLength / 2 + coinSize, sectionLength / 2 - coinSize);
                positionIsValid = true;

                Vector3 potentialPosition = section.transform.position + new Vector3(xPosition, 0, zPosition);

                // �������� ���������� ����� Physics.OverlapSphere
                Collider[] colliders = Physics.OverlapSphere(potentialPosition, Mathf.Max(coinSize, 1f));
                if (colliders.Length > 0)
                {
                    positionIsValid = false;
                }

                // �������� �� ���������� � �������������
                foreach (Vector3 pos in obstaclePositions)
                {
                    if (Mathf.Abs(xPosition - pos.x) < coinSize && Mathf.Abs(zPosition - pos.z) < coinSize * 2f)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                // �������� �� ���������� � ������� ��������
                foreach (Vector3 pos in coinPositions)
                {
                    if (Mathf.Abs(xPosition - pos.x) < coinSize && Mathf.Abs(zPosition - pos.z) < coinSize * 2f)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                attemptCount++;
            } while (!positionIsValid && attemptCount < 20);

            if (!positionIsValid) continue;

            // ����� ������
            Vector3 coinRayStart = section.transform.position + new Vector3(xPosition, spawnHeight, zPosition);
            Debug.DrawRay(coinRayStart, Vector3.down * 100f, Color.yellow, 2f);

            RaycastHit hit;
            if (Physics.Raycast(coinRayStart, Vector3.down, out hit, 100f))
            {
                Vector3 coinPosition = hit.point + Vector3.up * spawnHeightOffset;

                GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity);
                coin.transform.SetParent(section.transform);

                coinPositions.Add(new Vector3(xPosition, 0, zPosition));
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
