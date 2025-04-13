using UnityEngine;

public class ClickerInteraction: BaseUpgrade
{
    public override void Interact()
    {
        GameManager.Instance.Clicker.ClickerClick();
    }
}
