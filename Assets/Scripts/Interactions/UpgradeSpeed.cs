using UnityEngine;

public class UpgradeSpeed : BaseUpgrade
{
    public override void Interact()
    {
        Debug.Log("Upgrade Speed Interact Called");        
        //UpgradeManager.Instance.UpgradeSpeed();
    }
}
