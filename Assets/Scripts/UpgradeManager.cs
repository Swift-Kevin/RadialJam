using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

	public SaveData curData;

	public long ClickProduce
	{
		get
		{
			byte tier = curData.upgradeData.productionTier;

			switch(tier)
			{
				case < 5:
					return tier;
				case < 10:
					return tier * 2;
				case < 25:
					return tier * (long)System.Math.Log(tier, 2);
				case < 100:
					return (long)System.Math.Pow(tier, 2);
				default:
					return (long)System.Math.Pow(100, 2);
			}
		}
	}

	public float ClickDelay
	{
		get
		{
			byte tier = curData.upgradeData.speedTier;

			switch(tier)
			{
				case < 10:
					return 1.0f/tier;
				default:
					return 0.1f;
			}
		}
	}

	public long AutoProduce
	{
		get
		{
			byte tier = curData.upgradeData.automationTier;

			switch(tier)
			{
				case < 5:
					return tier;
				case < 10:
					return tier * 2;
				case < 25:
					return (long)System.Math.Pow(tier,2);
				case < 100:
					return (long)System.Math.Pow(2, tier);
				default:
					return (long)System.Math.Pow(2, 100);
			}
		}
	}

	public float AutoDelay
	{
		get
		{
			byte tier = curData.upgradeData.autoSpeedTier;

			switch(tier)
			{
				case < 10:
					return 1.0f / tier;
				default:
					return 0.1f;
			}
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct UpgradeData
{
    public byte productionTier;
    public byte speedTier;
    public byte automationTier;
    public byte autoSpeedTier;
}
