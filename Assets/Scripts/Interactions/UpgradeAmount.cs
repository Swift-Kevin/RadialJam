using UnityEngine;

public class UpgradeAmount : BaseUpgrade
{
    public override void Interact()
    {
        Debug.Log("Upgrade Amount Interact Called");
        //UpgradeManager.Instance.UpgradeAmount();
    }
}
