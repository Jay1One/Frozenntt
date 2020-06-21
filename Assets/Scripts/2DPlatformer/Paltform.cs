using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paltform : MonoBehaviour
{
    [SerializeField] private bool moved;
    [SerializeField] private float distance=6f;
    [SerializeField] private float speed = 0.4f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private IEnumerator MovementProcess()
    {
        var k = 0f;
        var dir = 1f;
        while (true)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, k);
            k += Time.deltaTime*dir*speed;
            if (k > 1f )
            {
                yield return new WaitForSeconds(1f);
                dir = -1;
                k = 1f;
            }

            if (k < 0)
            {
                dir = 1;
                k = 0;
            }
            yield return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (moved)
        {
            startPosition = transform.position;
            targetPosition = transform.position;
            targetPosition.x += distance;
            StartCoroutine(MovementProcess());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
