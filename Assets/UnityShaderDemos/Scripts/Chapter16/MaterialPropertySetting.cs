using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPropertySetting : MonoBehaviour
{
    private Renderer m_Renderer;
    private static MaterialPropertyBlock m_MatProperty;

    public Color TintColor = Color.white;
    public Texture MainTex;


    private MaterialPropertyBlock MatProperty
    {
        get {
            if (m_MatProperty == null)
            {
                m_MatProperty = new MaterialPropertyBlock();
            }
            return m_MatProperty;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        MatProperty.SetColor("_Color", TintColor);
        if (MainTex != null)
        {
            MatProperty.SetTexture("_MainTex", MainTex);
        }
        
        m_Renderer.SetPropertyBlock(MatProperty);
    }
    
}
