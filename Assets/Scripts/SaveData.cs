using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/SaveData")]
public class SaveData : ScriptableObject
{
	public ClickerData clickerData;
	public UpgradeData upgradeData;
}
