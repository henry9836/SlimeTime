using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    public float POWER = 1.0f;
    public float restoreSpeed = 1.0f;

    public float timer = 0;
    private Vector2 initalPos;
    public void ScreenBump(Vector3 vec3)
    {
        Vector2 vec2 = new Vector2(vec3.x, vec3.y);
        StartCoroutine(shakeyboi(vec2 * POWER));
    }

    private void Start()
    {
        initalPos = new Vector2(transform.localPosition.x, transform.localPosition.y);
    }

    void FixedUpdate()
    {
        gameObject.transform.localPosition = new Vector3(Mathf.Lerp(gameObject.transform.localPosition.x, initalPos.x, (timer -1)), Mathf.Lerp(gameObject.transform.localPosition.y, initalPos.y, (timer - 1)), -10.0f);
        timer += Time.deltaTime * restoreSpeed;
        if (timer < 1.0f){
            timer = 1.0f;
        }
    }

    IEnumerator shakeyboi(Vector2 vec2)
    {
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + vec2.x, gameObject.transform.localPosition.y + vec2.y, gameObject.transform.localPosition.z);
        yield return new WaitForSeconds(0.1f);
        //for (float timer = 0.0f; timer < 1.0f; timer += Time.deltaTime)
        timer = 0;
        yield return null;
    }
}
