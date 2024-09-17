using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoot : MonoBehaviour
{
    Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        newPosition = transform.position;
        newPosition.z = 2-10; // กำหนดค่า z ใหม่
        transform.position = newPosition;
        SpriteRenderer slashRenderer = transform.GetComponent<SpriteRenderer>();
        Color color = slashRenderer.color;
        color.a -= Time.deltaTime*2; // ลด alpha ตามเวลา
        slashRenderer.color = color;    

        // ถ้า alpha เป็นค่าน้อยกว่าหรือเท่ากับ 0 ให้ทำลาย GameObject และ reset currentSlash
        if (color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
