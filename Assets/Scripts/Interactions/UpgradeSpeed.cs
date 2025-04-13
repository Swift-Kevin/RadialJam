using UnityEngine;

public class UpgradeSpeed : BaseUpgrade
{
    public override void Interact()
    {
		GameManager.Instance.Upgrade.UpgradeClickDelay();
	}
}
