using UnityEngine;

public class UpgradeAmount : BaseUpgrade
{
    public override void Interact()
    {
        GameManager.Instance.Upgrade.UpgradeClickProduction();
    }
}
