using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eye_monster : MonoBehaviour
{
    public bool isFilp = false;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<Sprite> eye_sprite;
    [SerializeField] Transform playerReference;
    private bool isWaiting = false;
    private Transform playerTransform;
    public bool islobby = false;
    public float attackRange = 10f;
    public int onlylobbyeyefalse;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdateEyePositionWithDelay());
    }
    IEnumerator UpdateEyePositionWithDelay()
    {
        if((playerReference != null || playerTransform != null) && !isWaiting){
            isWaiting = true;

            yield return new WaitForSeconds(0.1f);
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if(islobby == true){
                if(distanceToPlayer <= attackRange){

                    Vector3 mousePosition = Input.mousePosition;

                    // Set player position as the reference point
                    mousePosition.z = playerReference.position.z;

                    // Convert the mouse position to world coordinates

                    // Subtract player position to get the relative position
                    Vector3 relativeMousePosition = playerTransform.position - playerReference.position;

                    // Debug.Log($"Mouse Position - X: {relativeMousePosition.x}, Y: {relativeMousePosition.y}");
                    if(isFilp == false){
                        if (relativeMousePosition.x < -1 && relativeMousePosition.y > 1)
                        {
                            spriteRenderer.sprite = eye_sprite[0];
                        }else if((relativeMousePosition.x > -1 && relativeMousePosition.x < 1)&& relativeMousePosition.y > 1)
                        {
                            spriteRenderer.sprite = eye_sprite[1];
                        }else if(relativeMousePosition.x > 1 && relativeMousePosition.y > 1)
                        {
                            spriteRenderer.sprite = eye_sprite[2];
                        }else if(relativeMousePosition.x < -1 && (relativeMousePosition.y < 1 && relativeMousePosition.y > -1))
                        {
                            spriteRenderer.sprite = eye_sprite[3];
                        }else if(relativeMousePosition.x > 1 && (relativeMousePosition.y < 1 && relativeMousePosition.y > -1))
                        {
                            spriteRenderer.sprite = eye_sprite[4];
                        }else if(relativeMousePosition.x < -1 && relativeMousePosition.y < -1)
                        {
                            spriteRenderer.sprite = eye_sprite[5];
                        }else if((relativeMousePosition.x > -1 && relativeMousePosition.x < 1) && relativeMousePosition.y < -1)
                        {
                            spriteRenderer.sprite = eye_sprite[6];
                        }else if(relativeMousePosition.x > 1 && relativeMousePosition.y < -1)
                        {
                            spriteRenderer.sprite = eye_sprite[7];
                        }else{
                            spriteRenderer.sprite = eye_sprite[8];
                        }
                    }else {
                        if (relativeMousePosition.x < -1 && relativeMousePosition.y > 1)
                        {
                            spriteRenderer.sprite = eye_sprite[2];
                        }else if((relativeMousePosition.x > -1 && relativeMousePosition.x < 1)&& relativeMousePosition.y > 1)
                        {
                            spriteRenderer.sprite = eye_sprite[1];
                        }else if(relativeMousePosition.x > 1 && relativeMousePosition.y > 1)
                        {
                            spriteRenderer.sprite = eye_sprite[0];
                        }else if(relativeMousePosition.x < -1 && (relativeMousePosition.y < 1 && relativeMousePosition.y > -1))
                        {
                            spriteRenderer.sprite = eye_sprite[4];
                        }else if(relativeMousePosition.x > 1 && (relativeMousePosition.y < 1 && relativeMousePosition.y > -1))
                        {
                            spriteRenderer.sprite = eye_sprite[3];
                        }else if(relativeMousePosition.x < -1 && relativeMousePosition.y < -1)
                        {
                            spriteRenderer.sprite = eye_sprite[7];
                        }else if((relativeMousePosition.x > -1 && relativeMousePosition.x < 1) && relativeMousePosition.y < -1)
                        {
                            spriteRenderer.sprite = eye_sprite[6];
                        }else if(relativeMousePosition.x > 1 && relativeMousePosition.y < -1)
                        {
                            spriteRenderer.sprite = eye_sprite[5];
                        }else{
                            spriteRenderer.sprite = eye_sprite[8];
                        }
                    }
                }else{
                    spriteRenderer.sprite = eye_sprite[onlylobbyeyefalse];
                }
            }else if(islobby == false){

                Vector3 mousePosition = Input.mousePosition;

                // Set player position as the reference point
                mousePosition.z = playerReference.position.z;

                // Convert the mouse position to world coordinates

                // Subtract player position to get the relative position
                Vector3 relativeMousePosition = playerTransform.position - playerReference.position;

                // Debug.Log($"Mouse Position - X: {relativeMousePosition.x}, Y: {relativeMousePosition.y}");
                if(isFilp == false){
                    if (relativeMousePosition.x < -1 && relativeMousePosition.y > 1)
                    {
                        spriteRenderer.sprite = eye_sprite[0];
                    }else if((relativeMousePosition.x > -1 && relativeMousePosition.x < 1)&& relativeMousePosition.y > 1)
                    {
                        spriteRenderer.sprite = eye_sprite[1];
                    }else if(relativeMousePosition.x > 1 && relativeMousePosition.y > 1)
                    {
                        spriteRenderer.sprite = eye_sprite[2];
                    }else if(relativeMousePosition.x < -1 && (relativeMousePosition.y < 1 && relativeMousePosition.y > -1))
                    {
                        spriteRenderer.sprite = eye_sprite[3];
                    }else if(relativeMousePosition.x > 1 && (relativeMousePosition.y < 1 && relativeMousePosition.y > -1))
                    {
                        spriteRenderer.sprite = eye_sprite[4];
                    }else if(relativeMousePosition.x < -1 && relativeMousePosition.y < -1)
                    {
                        spriteRenderer.sprite = eye_sprite[5];
                    }else if((relativeMousePosition.x > -1 && relativeMousePosition.x < 1) && relativeMousePosition.y < -1)
                    {
                        spriteRenderer.sprite = eye_sprite[6];
                    }else if(relativeMousePosition.x > 1 && relativeMousePosition.y < -1)
                    {
                        spriteRenderer.sprite = eye_sprite[7];
                    }else{
                        spriteRenderer.sprite = eye_sprite[8];
                    }
                }else {
                    if (relativeMousePosition.x < -1 && relativeMousePosition.y > 1)
                    {
                        spriteRenderer.sprite = eye_sprite[2];
                    }else if((relativeMousePosition.x > -1 && relativeMousePosition.x < 1)&& relativeMousePosition.y > 1)
                    {
                        spriteRenderer.sprite = eye_sprite[1];
                    }else if(relativeMousePosition.x > 1 && relativeMousePosition.y > 1)
                    {
                        spriteRenderer.sprite = eye_sprite[0];
                    }else if(relativeMousePosition.x < -1 && (relativeMousePosition.y < 1 && relativeMousePosition.y > -1))
                    {
                        spriteRenderer.sprite = eye_sprite[4];
                    }else if(relativeMousePosition.x > 1 && (relativeMousePosition.y < 1 && relativeMousePosition.y > -1))
                    {
                        spriteRenderer.sprite = eye_sprite[3];
                    }else if(relativeMousePosition.x < -1 && relativeMousePosition.y < -1)
                    {
                        spriteRenderer.sprite = eye_sprite[7];
                    }else if((relativeMousePosition.x > -1 && relativeMousePosition.x < 1) && relativeMousePosition.y < -1)
                    {
                        spriteRenderer.sprite = eye_sprite[6];
                    }else if(relativeMousePosition.x > 1 && relativeMousePosition.y < -1)
                    {
                        spriteRenderer.sprite = eye_sprite[5];
                    }else{
                        spriteRenderer.sprite = eye_sprite[8];
                    }
                }
            }
        }
        isWaiting = false;
        StartCoroutine(UpdateEyePositionWithDelay());
    }

    void Print(string message)
    {
        Debug.Log(message);
    }
}