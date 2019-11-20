using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public float sec = 14f;
    private Animation _animation;
    void Start()
    {
        _animation = gameObject.GetComponent<Animation>();
        if (gameObject.activeInHierarchy)
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
 
        StartCoroutine(LateCall());
        
    }
 
    IEnumerator LateCall()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log("cr");
        yield return new WaitForSeconds(sec);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
