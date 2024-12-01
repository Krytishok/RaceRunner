using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roadSectionPrefabs;       // Массив префабов секций дороги
    public GameObject[] startSectionPrefabs;      // Начальные секции дороги
    public GameObject[] obstaclePrefabs;          // Массив префабов препятствий
    public GameObject[] coinPrefabs;
    public GameObject npcPrefab;// Префаб NPC
    public GameObject WeaponZone;

    public int initialSections = 5;               // Количество начальных секций
    public float sectionLength = 119.5f;          // Длина одной секции
    public int despawnDistance = 2;               // Расстояние для удаления старой секции

    public float minDistanceBetweenObstacles = 15f; // Минимальное расстояние между препятствиями
    public float difficultyIncreaseRate = 0.05f;     // Скорость роста сложности
    public float maxObstaclesPerSection = 7;       // Максимальное количество препятствий на сегмент при высокой сложности
    private float difficultyLevel = 1f;             // Текущий уровень сложности

    private Queue<GameObject> activeSections = new Queue<GameObject>(); // Очередь активных секций
    private Vector3 nextPosition;                 // Позиция для следующего сегмента
    private int sectionCounter = 0;               // Счётчик секций
    private GameObject currentNPC = null;         // Текущий NPC

    [Header("NPC Spawn Settings")]
    public int npcSpawnInterval = 10;            // Интервал спавна NPC (в секциях)
    public int disableObstaclesBeforeNPC = 2;    // Количество секций до NPC без препятствий

    private bool disableObstacles = false;       // Флаг отключения генерации препятствий и монет

    [Header("Spawn Settings")]
    public float laneOffsetX = 1f;           // Смещение полосы вправо/влево
    public float spawnHeight = 20f;           // Высота начала спавна (для Raycast)
    public float spawnHeightOffset = 1f;     // Сдвиг высоты для монет над поверхностью

    private float[] lanePositions = new float[] { -10f, -3f, 4f, 11f };  // Позиции полос

    [Header("Bonuses Settings")]
    public GameObject[] bonusPrefabs;         // Массив префабов бонусов
    public float bonusSpawnChance = 0.05f;    // Шанс спавна бонуса (например, 5%)
    public float minDistanceFromObstacles = 10f; // Минимальное расстояние от препятствий и монет

    private int weaponZoneCounter = 0; // Счётчик для отслеживания секций между WeaponZone


    //Managers
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        nextPosition = new Vector3(-17.27f, -29.843f, (-635.73f - sectionLength * (startSectionPrefabs.Length - 1)));

        foreach (var section in startSectionPrefabs)
        {
            activeSections.Enqueue(section);
        }

        for (int i = 0; i < initialSections - startSectionPrefabs.Length; i++)
        {
            SpawnSection();
        }

        
    }

    private void Update()
    {
        // Постепенно увеличиваем сложность по мере прохождения игроком дороги
        difficultyLevel += difficultyIncreaseRate * Time.deltaTime;

        
    }

    public void SpawnSection()
    {
        GameObject sectionPrefab = roadSectionPrefabs[Random.Range(0, roadSectionPrefabs.Length)];
        GameObject newSection = Instantiate(sectionPrefab, nextPosition, Quaternion.identity);

        activeSections.Enqueue(newSection);

        sectionCounter++;
        weaponZoneCounter++;

        List<Vector3> obstaclePositions = new List<Vector3>();
        List<Vector3> coinPositions = new List<Vector3>();

        // Проверяем состояние генерации препятствий
        if (sectionCounter % npcSpawnInterval == 0)
        {
            disableObstacles = true; // Отключаем генерацию препятствий перед NPC
        }

        if (disableObstacles)
        {
            int sectionsLeft = sectionCounter % npcSpawnInterval;
            if (sectionsLeft <= disableObstaclesBeforeNPC)
            {
                Debug.Log($"Disabling obstacles for the next {disableObstaclesBeforeNPC} sections.");
            }
            else
            {
                SpawnNPC(newSection);
                
                disableObstacles = false;
            }
        }
        else if (!_gameManager._IsEnemyOnRoad)
        {
            // Генерация препятствий и монет только если NPC отсутствует
            SpawnObstacles(newSection, obstaclePositions);
            SpawnCoins(newSection, obstaclePositions, coinPositions);
            SpawnBonuses(newSection, obstaclePositions, coinPositions);
        }

        // Проверяем, нужно ли заспавнить WeaponZone
        while (_gameManager._IsEnemyOnRoad && weaponZoneCounter >= 3)
        {
            WeaponeZone(newSection);
            weaponZoneCounter = 0; // Сбрасываем счётчик
        }

        nextPosition += new Vector3(0, 0, -sectionLength);
    }

    private void SpawnObstacles(GameObject section, List<Vector3> obstaclePositions)
    {
        int obstaclesPerSection = (int)Mathf.Min(difficultyLevel * 2f, maxObstaclesPerSection);

        for (int i = 0; i < obstaclesPerSection; i++)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Collider obstacleCollider = obstaclePrefab.GetComponent<Collider>();
            float obstacleSize = obstacleCollider ? obstacleCollider.bounds.size.z : 8f;

            float xPosition = lanePositions[Random.Range(0, lanePositions.Length)] + laneOffsetX;
            float currentMinDistance = Mathf.Max(obstacleSize * 2f, minDistanceBetweenObstacles - difficultyLevel * 0.15f);

            float zPosition;
            bool positionIsValid;

            int attemptCount = 0;
            do
            {
                zPosition = Random.Range(-sectionLength / 2 + obstacleSize, sectionLength / 2 - obstacleSize);
                positionIsValid = true;

                foreach (Vector3 pos in obstaclePositions)
                {
                    if (Mathf.Abs(zPosition - pos.z) < currentMinDistance && Mathf.Abs(xPosition - pos.x) < 0.15f)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                attemptCount++;
            } while (!positionIsValid && attemptCount < 20);

            if (!positionIsValid) continue;

            Vector3 rayStart = section.transform.position + new Vector3(xPosition, spawnHeight, zPosition);
            Debug.DrawRay(rayStart, Vector3.down * 100f, Color.red, 2f);

            RaycastHit hit;
            if (Physics.Raycast(rayStart, Vector3.down, out hit, 100f))
            {
                Vector3 obstaclePosition = hit.point;

                GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
                obstacle.transform.SetParent(section.transform);
                obstaclePositions.Add(new Vector3(xPosition, 0, zPosition));
            }
        }
    }

    private void SpawnCoins(GameObject section, List<Vector3> obstaclePositions, List<Vector3> coinPositions)
    {
        int coinsPerSection = 8;

        for (int j = 0; j < coinsPerSection; j++)
        {
            GameObject coinPrefab = coinPrefabs[Random.Range(0, coinPrefabs.Length)];
            Collider coinCollider = coinPrefab.GetComponent<Collider>();
            float coinSize = coinCollider ? coinCollider.bounds.size.z : 6f;

            float xPosition = lanePositions[Random.Range(0, lanePositions.Length)] + laneOffsetX;

            float zPosition;
            bool positionIsValid;

            int attemptCount = 0;
            do
            {
                zPosition = Random.Range(-sectionLength / 2 + coinSize, sectionLength / 2 - coinSize);
                positionIsValid = true;

                Vector3 potentialPosition = section.transform.position + new Vector3(xPosition, 0, zPosition);

                Collider[] colliders = Physics.OverlapSphere(potentialPosition, Mathf.Max(coinSize, 6f));
                if (colliders.Length > 0)
                {
                    positionIsValid = false;
                }

                foreach (Vector3 pos in obstaclePositions)
                {
                    if (Mathf.Abs(xPosition - pos.x) < coinSize && Mathf.Abs(zPosition - pos.z) < coinSize * 6f)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                foreach (Vector3 pos in coinPositions)
                {
                    if (Mathf.Abs(xPosition - pos.x) < coinSize && Mathf.Abs(zPosition - pos.z) < coinSize * 6f)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                attemptCount++;
            } while (!positionIsValid && attemptCount < 20);

            if (!positionIsValid) continue;

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

    private void SpawnBonuses(GameObject section, List<Vector3> obstaclePositions, List<Vector3> coinPositions)
    {
        if (Random.value < bonusSpawnChance)
        {
            GameObject bonusPrefab = bonusPrefabs[Random.Range(0, bonusPrefabs.Length)];
            Collider bonusCollider = bonusPrefab.GetComponent<Collider>();
            float bonusSize = bonusCollider ? bonusCollider.bounds.size.z : 8f;

            float xPosition = lanePositions[Random.Range(0, lanePositions.Length)] + laneOffsetX;

            float zPosition;
            bool positionIsValid;

            int attemptCount = 0;
            do
            {
                zPosition = Random.Range(-sectionLength / 2 + bonusSize, sectionLength / 2 - bonusSize);
                positionIsValid = true;

                Vector3 potentialPosition = section.transform.position + new Vector3(xPosition, 0, zPosition);

                Collider[] colliders = Physics.OverlapSphere(potentialPosition, Mathf.Max(bonusSize, 8f));
                if (colliders.Length > 0)
                {
                    positionIsValid = false;
                }

                foreach (Vector3 pos in obstaclePositions)
                {
                    if (Vector3.Distance(potentialPosition, section.transform.position + pos) < minDistanceFromObstacles)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                foreach (Vector3 pos in coinPositions)
                {
                    if (Vector3.Distance(potentialPosition, section.transform.position + pos) < minDistanceFromObstacles)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

                attemptCount++;
            } while (!positionIsValid && attemptCount < 20);

            if (positionIsValid)
            {
                Vector3 bonusRayStart = section.transform.position + new Vector3(xPosition, spawnHeight, zPosition);
                Debug.DrawRay(bonusRayStart, Vector3.down * 100f, Color.green, 2f);

                RaycastHit hit;
                if (Physics.Raycast(bonusRayStart, Vector3.down, out hit, 100f))
                {
                    Vector3 bonusPosition = hit.point + Vector3.up * spawnHeightOffset;

                    GameObject bonus = Instantiate(bonusPrefab, bonusPosition, Quaternion.identity);
                    bonus.transform.SetParent(section.transform);
                }
            }
        }
    }


    private void SpawnNPC(GameObject section)
    {

        if (!_gameManager._IsEnemyOnRoad) // Проверка наличия врага на сцене
        {
            
            Vector3 npcPosition = section.transform.position + new Vector3(0, 5, 0);
            // Смещение NPC над секцией
            currentNPC = Instantiate(npcPrefab, npcPosition, Quaternion.identity);
           
            Debug.Log("NPC Spawned");
        }
    }

    public void WeaponeZone(GameObject section)
    {
        // Проверка наличия врага на сцене
        
            float xPosition = lanePositions[Random.Range(1, (lanePositions.Length)-1)] + laneOffsetX;
            Vector3 weaponPosition = section.transform.position + new Vector3(xPosition, 31.5f, 300);

            GameObject weaponZoneInstance = Instantiate(WeaponZone, weaponPosition, Quaternion.identity);
            weaponZoneInstance.transform.SetParent(section.transform, true);

            Debug.Log("WeaponZone Spawned as child of the section.");
        
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
