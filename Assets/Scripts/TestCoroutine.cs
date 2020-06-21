using System;
using  System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestCoroutine : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("start");
            StartCoroutine(WaitAndPrint("Coroutine"));
        }
        
        private IEnumerator WaitAndPrint(string txt)
        {
            yield return new WaitForSeconds(2f);
            Debug.Log(txt);
        }
    } 
   
    
}