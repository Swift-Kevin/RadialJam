using UnityEngine;

public class UpgradeAutomationSpeed: BaseUpgrade
{
    public override void Interact()
    {
		GameManager.Instance.Upgrade.UpgradeAutoDelay();
	}
}
