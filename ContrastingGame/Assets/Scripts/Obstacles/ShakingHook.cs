using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class ShakingHook : MonoBehaviour
{
    public float shakeSpeed = 60f;
    public float shakeAmount = 0.02f;
    private Vector2 startPos;
    private Transform playerTransform;
    private float range;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerTransform)
        {
            range = playerTransform.GetComponent<PlayerMovement>().grappleRange;
        }
        else
        {
            range = 6f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform && Vector2.Distance(playerTransform.position, startPos) < range)
        {
            
            transform.position = new Vector2(startPos.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount,
                                            startPos.y + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount
                                            );
        }
    }
}
