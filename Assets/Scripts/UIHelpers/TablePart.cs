using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TablePart : MonoBehaviour
{
    public TextMeshProUGUI label;
    public TextMeshProUGUI value;
    public Image image;

    private void Start()
    {
        //Init(info);
    }

    public void Init(UIPartInfo info)
    {
        label.text = info.labelText;
        value.text = info.valueText;
        image.sprite = info.imageSprite;
    }


}
