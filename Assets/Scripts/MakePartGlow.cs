using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePartGlow : MonoBehaviour
{
    MaterialPropertyBlock propBlock;
    Color defaultEmissionColor;
    private void Start()
    {
        propBlock = new MaterialPropertyBlock();
        defaultEmissionColor = new Color(0, 0, 0);
    }

    private void OnMouseEnter()
    {
        propBlock.SetColor("_EmissionColor",Color.white * 0.2f);
        GetComponent<Renderer>().SetPropertyBlock(propBlock);
    }

    private void OnMouseExit()
    {
        propBlock.SetColor("_EmissionColor", defaultEmissionColor);
        GetComponent<Renderer>().SetPropertyBlock(propBlock);
    }

}
