using UnityEngine;

public class UpgradeAutomation : BaseUpgrade
{
    public override void Interact()
    {
		GameManager.Instance.Upgrade.UpgradeAutoProduction();
	}
}
