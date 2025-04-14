using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct UIPartInfo
{
    public string labelText;
    public string valueText;
    public Sprite imageSprite;
}

public class StatusParts : MonoBehaviour
{
    //public List<UIPartInfo> partInfos = new List<UIPartInfo>();

    //public GameObject prefab;
    //private List<TablePart> parts = new List<TablePart>();

    //public List<TablePart> UILabelParts => parts;

    //private void Start()
    //{
    //    for (int i = 0; i < partInfos.Count; i++)
    //    {
    //        var add = Instantiate(prefab, transform);
    //        TablePart part = add.GetComponent<TablePart>();
    //        part.Init(partInfos[i]);

    //        parts.Add(part);
    //    }
    //}

}
