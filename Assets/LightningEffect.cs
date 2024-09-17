using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    [SerializeField] private Pool_Lightning _poolLightning;
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private int _effectFrequency = 20;
    private int segmentCount = 20;
    [SerializeField] private float displacement = 0.3f;
    [SerializeField] private float flashInterval = 0.05f;
    [SerializeField] private float lightningDuration = 1f; // ระยะเวลาเอฟเฟคซ็อตไฟฟ้า
    public List<string> allID = new List<string>();
    public LineRenderer lineRenderer;
    private Coroutine lightningCoroutine;
    private float remainingDuration;
    private Transform _startPoint;
    private Transform _endPoint;
    private GameObject _startObj = null;
    private GameObject _endObj = null;
    private bool isStopGame = false;
    public float Damage = 0;

    public float detectRadius = 10f; // รัศมีของ Sphere ที่จะใช้ในการค้นหา
    public string monsterTag = "monster"; // Tag ของมอนเตอร์
    private bool runLight = false;
    private Collider2D[] results = new Collider2D[10]; // buffer สำหรับเก็บผลการค้นหา

    public void StartLightning(float DiffusionAmount, GameObject startPoint, bool isFirst, List<string> myAllID)
    {
        allID = myAllID;
        Debug.Log("DiffusionAmount = " + DiffusionAmount);
        allID.Add(startPoint.GetComponent<monsterController>().uniqueID);
        if (isFirst == true)
        {
            if (startPoint.GetComponent<monsterController>().GetHp() - Damage <= 0)
            {
                startPoint.GetComponent<monsterController>().LightningAttacked(Damage);
                gameObject.SetActive(false);
                return;
            }
            else
            {
                startPoint.GetComponent<monsterController>().LightningAttacked(Damage);
            }
        }
        _startPoint = startPoint.transform;
        _startObj = startPoint;
        GameObject objEnd = DetectNearestMonster();
        if (objEnd == null)
        {
            gameObject.SetActive(false);
            return;
        };
        if (startPoint.GetComponent<monsterController>().uniqueID == objEnd.GetComponent<monsterController>().uniqueID)
        {
            gameObject.SetActive(false);
            return;
        };

        float totalDistance = Vector3.Distance(_startPoint.position, objEnd.transform.position);
        segmentCount = Mathf.FloorToInt(totalDistance * _effectFrequency);
        lineRenderer.positionCount = segmentCount + 1;
        _endObj = objEnd;
        allID.Add(objEnd.GetComponent<monsterController>().uniqueID);
        _endPoint = objEnd.transform;
        if (lightningCoroutine != null)
        {
            StopCoroutine(lightningCoroutine);
        }
        objEnd.GetComponent<monsterController>().LightningAttacked(Damage);
        lightningCoroutine = StartCoroutine(ShowLightning(lightningDuration, _startPoint, _endPoint));
        runLight = true;
        lineRenderer.enabled = true; // เปิดการแสดงผลของ LineRenderer
        if (DiffusionAmount >= 1)
        {
            _poolLightning.GetPool(_endObj, Damage, DiffusionAmount - 1, allID, false);
        }
    }

    public GameObject DetectNearestMonster()
    {
        // หาตำแหน่งปัจจุบันของเรา
        Vector2 center = transform.position;

        // ค้นหา Collider ทั้งหมดที่อยู่ภายในวงกลมที่กำหนด
        int colliderCount = Physics2D.OverlapCircleNonAlloc(center, detectRadius, results, LayerMask.GetMask("monster"));

        GameObject nearestMonster = null;
        float closestDistance = float.MaxValue;

        // ตรวจสอบ Collider ที่ค้นพบ
        for (int i = 0; i < colliderCount; i++)
        {
            Collider2D col = results[i];
            // ตรวจสอบ Tag ของ GameObject ที่ค้นพบ
            if (col.CompareTag(monsterTag) && CheckID(col.gameObject.GetComponent<monsterController>().uniqueID) && col.gameObject.activeSelf)
            {
                // หาตำแหน่งของ GameObject ที่ค้นพบ
                Vector2 monsterPosition = col.transform.position;
                float distance = Vector2.Distance(center, monsterPosition);

                // ตรวจสอบว่ามีมอนเตอร์ใกล้สุดหรือไม่
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestMonster = col.gameObject;
                }
            }
        }

        return nearestMonster;
    }


    public bool CheckID(string ID)
    {
        if (allID.Count == 0)
        {
            return true;
        }
        for (int i = 0; i < allID.Count; i++)
        {
            if (allID[i] == ID || _startObj.GetComponent<monsterController>().uniqueID == ID)
            {
                return false;
            }
        }
        return true;
    }

    private void OnDisable()
    {
        allID = new List<string>();
        _startObj = null;
        _endObj = null;
        runLight = false;
        _startPoint = null;
        _endPoint = null;
        // รีเซ็ต LineRenderer เมื่อ GameObject ถูกปิดใช้งาน
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }

        if (lightningCoroutine != null)
        {
            StopCoroutine(lightningCoroutine);
            lightningCoroutine = null;
        }

        remainingDuration = 0;
    }

    private void OnEnable()
    {
        allID = new List<string>();
        _startObj = null;
        _endObj = null;
        runLight = false;
        _startPoint = null;
        _endPoint = null;
        // รีเซ็ตค่าเมื่อ GameObject ถูกเปิดใช้งานใหม่
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = segmentCount + 1;
            lineRenderer.enabled = false;
        }
        remainingDuration = 0;
    }

    void Update()
    {
        if (_startPoint == null || _endPoint == null || runLight == false)
        {
            return;
        }/*
        if ((_startObj.activeSelf == false || _endObj.activeSelf == false) && runLight == true)
        {
            if (lineRenderer != null)
            {
                lineRenderer.enabled = false;
            }

            if (lightningCoroutine != null)
            {
                StopCoroutine(lightningCoroutine);
                lightningCoroutine = null;
            }
            remainingDuration = 0;
            gameObject.SetActive(false);
            return;
        }*/
        if (_upgradeManager.IsStopGame && isStopGame == false)
        {
            isStopGame = true;
            // หยุดการอัพเดตเวลา แต่ให้สายฟ้ายังคงแสดงผล
            if (lightningCoroutine != null)
            {
                StopCoroutine(lightningCoroutine);
            }
            lightningCoroutine = StartCoroutine(PauseLightning(_startPoint, _endPoint));
        }
        else if (_upgradeManager.IsStopGame == false && isStopGame == true)
        {
            isStopGame = false;
            StopCoroutine(lightningCoroutine);
            lightningCoroutine = StartCoroutine(ShowLightning(remainingDuration, _startPoint, _endPoint));
        }
    }

    private void GenerateLightning(Transform startPoint, Transform endPoint)
    {
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(segmentCount, endPoint.position);

        Vector3[] positions = new Vector3[segmentCount + 1];
        positions[0] = startPoint.position;
        positions[segmentCount] = endPoint.position;

        for (int i = 1; i < segmentCount; i++)
        {
            Vector3 lerpedPosition = Vector3.Lerp(startPoint.position, endPoint.position, (float)i / segmentCount);
            positions[i] = lerpedPosition + (Vector3)Random.insideUnitCircle * displacement;
        }

        lineRenderer.SetPositions(positions);
    }

    private IEnumerator ShowLightning(float duration, Transform startPoint, Transform endPoint)
    {
        lineRenderer.enabled = true; // เปิดการแสดงผลของ LineRenderer

        float timer = 0f;
        remainingDuration = duration;

        while (timer < duration)
        {
            GenerateLightning(startPoint, endPoint); // สร้างเอฟเฟคสายฟ้า
            yield return new WaitForSeconds(flashInterval); // รอเป็นเวลา flashInterval วินาที
            if (!_upgradeManager.IsStopGame)
            {
                timer += flashInterval;
                remainingDuration = duration - timer; // เก็บเวลาที่เหลือ
            }
        }
        transform.gameObject.SetActive(false);
        lineRenderer.enabled = false; // ปิดการแสดงผลของ LineRenderer
        lightningCoroutine = null; // รีเซ็ต coroutine
    }

    private IEnumerator PauseLightning(Transform startPoint, Transform endPoint)
    {
        while (_upgradeManager.IsStopGame)
        {
            GenerateLightning(startPoint, endPoint); // สร้างเอฟเฟคสายฟ้า
            yield return new WaitForSeconds(flashInterval); // รอเป็นเวลา flashInterval วินาที
        }
    }
}
