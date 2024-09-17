using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Pool_SpawnMonster> monsterPool;
    public List<Pool_SpawnMonster> MiniBossMonsterPool;
    public List<Pool_SpawnMonster> BossMonsterPool;
    public List<Pool_SpawnMonster> BigBossmonsterPool;
    //อัตราเกิดมอนสเตอร์
    public float spawnInterval;
    public Transform player;
    public float minDistance = 1f;
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;
    private Coroutine spawnMonsterCoroutine;
    [SerializeField] private UpgradeManager UpgradeManager;

    private void Start()
    {
        mainCamera = Camera.main;
        SetDifficultyLevel(0);

        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        spawnMonsterCoroutine = StartCoroutine(SpawnMonster());
    }
    IEnumerator SpawnMonster()
    {
        if(UpgradeManager.IsStopGame == false){
            if(player != null){

            Vector3 randomPosition;

            do
            {
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);

                randomPosition = new Vector3(randomX, randomY, 0);
            } while (Vector3.Distance(randomPosition, player.position) < minDistance);

            // สุ่ม monsterPrefab จาก monsterPrefabs array
            Pool_SpawnMonster selectedMonsterPrefab = monsterPool[Random.Range(0, monsterPool.Count)];

            // ตรวจสอบว่า selectedMonsterPrefab ไม่เป็น null ก่อนที่จะสร้าง
            if (selectedMonsterPrefab != null)
            {
                // สร้างมอนสเตอร์ที่ตำแหน่งที่สุ่มและไม่ใกล้เกินระยะห่างขั้นต่ำ
                selectedMonsterPrefab.GetPool(randomPosition, new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            }
            else
            {
                Debug.LogWarning("selectedMonsterPrefab is null. Make sure to assign valid prefabs to the monsterPrefabs array.");
            }}
        }
        yield return new WaitForSeconds(spawnInterval);
        spawnMonsterCoroutine = StartCoroutine(SpawnMonster());
    }
    public void SpawnMonsterMiniBoss()
    {
            if(player != null){

            Vector3 randomPosition;

            do
            {
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);

                randomPosition = new Vector3(randomX, randomY, 0);
            } while (Vector3.Distance(randomPosition, player.position) < minDistance);

            // สุ่ม monsterPrefab จาก monsterPrefabs array
            Pool_SpawnMonster selectedMonsterPrefab = MiniBossMonsterPool[Random.Range(0, monsterPool.Count)];

            // ตรวจสอบว่า selectedMonsterPrefab ไม่เป็น null ก่อนที่จะสร้าง
            if (selectedMonsterPrefab != null)
            {
                // สร้างมอนสเตอร์ที่ตำแหน่งที่สุ่มและไม่ใกล้เกินระยะห่างขั้นต่ำ
                selectedMonsterPrefab.GetPool(randomPosition, new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            }
            else
            {
                Debug.LogWarning("selectedMonsterPrefab is null. Make sure to assign valid prefabs to the monsterPrefabs array.");
            }}
    }
    public void SpawnMonsterBoss(int index)
    {
            if(player != null){

            Vector3 randomPosition;

            do
            {
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);

                randomPosition = new Vector3(randomX, randomY, 0);
            } while (Vector3.Distance(randomPosition, player.position) < minDistance);

            // สุ่ม monsterPrefab จาก monsterPrefabs array
            Pool_SpawnMonster selectedMonsterPrefab = BossMonsterPool[index];

            // ตรวจสอบว่า selectedMonsterPrefab ไม่เป็น null ก่อนที่จะสร้าง
            if (selectedMonsterPrefab != null)
            {
                // สร้างมอนสเตอร์ที่ตำแหน่งที่สุ่มและไม่ใกล้เกินระยะห่างขั้นต่ำ
                selectedMonsterPrefab.GetPool(randomPosition, new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            }
            else
            {
                Debug.LogWarning("selectedMonsterPrefab is null. Make sure to assign valid prefabs to the monsterPrefabs array.");
            }}
    }
    public void SpawnMonsterBigBoss()
    {
            if(player != null){

            Vector3 randomPosition;

            do
            {
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);

                randomPosition = new Vector3(randomX, randomY, 0);
            } while (Vector3.Distance(randomPosition, player.position) < minDistance);

            // สุ่ม monsterPrefab จาก monsterPrefabs array
            Pool_SpawnMonster selectedMonsterPrefab = BigBossmonsterPool[Random.Range(0, monsterPool.Count)];

            // ตรวจสอบว่า selectedMonsterPrefab ไม่เป็น null ก่อนที่จะสร้าง
            if (selectedMonsterPrefab != null)
            {
                // สร้างมอนสเตอร์ที่ตำแหน่งที่สุ่มและไม่ใกล้เกินระยะห่างขั้นต่ำ
                selectedMonsterPrefab.GetPool(randomPosition, new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            }
            else
            {
                Debug.LogWarning("selectedMonsterPrefab is null. Make sure to assign valid prefabs to the monsterPrefabs array.");
            }}
    }

    public void SetDifficultyLevel(int level)
    {
        float levelMultiplier = 2.9f;
        if(level == 0){
            levelMultiplier = 2.9f;
        }else{
            for (int i = 0; i < level; i++)
            {
                levelMultiplier *= 0.9f;
            }
        }

        spawnInterval = 0.1f + levelMultiplier;
    }
    public void StopSpawning()
    {
        if (spawnMonsterCoroutine != null)
        {
            StopCoroutine(spawnMonsterCoroutine);
        }
    }
}