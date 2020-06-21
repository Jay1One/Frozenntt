using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObjects : MonoBehaviour
{
    [SerializeField] private GameObject m_baseCube;
    [SerializeField] private GameObject[] m_cubes;
    void Start()
    {
        foreach(var cube in m_cubes)
        {
            GameObject newCube = Instantiate(m_baseCube);
            Vector3 pos = cube.transform.position;
            pos.y += 1;
            newCube.transform.position = pos;
            newCube.name = cube.name + "_up";
        }
    }


    void Update()
    {
        m_baseCube.transform.Translate(Vector3.up * Time.deltaTime);
    }
}
