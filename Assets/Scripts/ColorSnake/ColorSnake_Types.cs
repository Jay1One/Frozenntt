using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorSnake_Types : MonoBehaviour
{
    //цвета объектов
    [Serializable]
    public class ColorType
    {
        public string Name;
        public int Id;
        public Color Color;
    }
    [Serializable]
    public class ObjectType
    {
        public string Name;
        public int Id;
        public GameObject Object;
    }
    
    [Serializable]
    public class TemplateType
    {
        public string Name;
        public int Id;
        public Transform[] Points;
    }

    [SerializeField] private ColorType[] m_Colors;
    [SerializeField] private ObjectType[] m_Objects;
    [SerializeField] private TemplateType[] m_Templates;

    public ColorType GetRandomColorType()
    {
        int rand = Random.Range(0, m_Colors.Length);
        return m_Colors[rand];
    }

    public ObjectType GetRandomObjectType()
    {
        int rand = Random.Range(0, m_Objects.Length);
        return m_Objects[rand];
    }

    public TemplateType GetRandomTemplate()
    {
        int rand = Random.Range(0, m_Templates.Length);
        return m_Templates[rand];
    }

    public ColorType GetColorType(int id)
    {
        return m_Colors.FirstOrDefault(c => c.Id == id);
    }

    public ObjectType GetObjectType(int id)
    {
        return m_Objects.FirstOrDefault(c => c.Id == id);
    }
    public TemplateType GetTemplateType(int id)
    {
        return m_Templates.FirstOrDefault(c => c.Id == id);
    }
}
