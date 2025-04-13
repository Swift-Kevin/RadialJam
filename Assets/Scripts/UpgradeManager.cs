using UnityEngine;
using TMPro;
using System.Data;

public class UpgradeManager : MonoBehaviour
{
	public TMP_Text statusText;

	[SerializeField]private SaveData curData;
	private const int clickProductionLimit = 100;
	private const int clickDelayLimit = 10;
	private const int autoProductionLimit = 10;
	private const int autoDelayLimit = 10;
	public Upgrade<long> ClickProduce
	{
		get
		{
			byte tier = curData.upgradeData.productionTier;

			switch(tier)
			{
				case < 5:
					return new Upgrade<long>
					{
						cost = tier * 10,
						value = tier
					};
				case < 10:
					return new Upgrade<long>
					{
						cost = tier * (long)System.Math.Log(tier, 2),
						value = tier * 2
					};
				case < 25:
					return new Upgrade<long>
					{
						cost = tier * tier * tier,
						value = tier * (long)System.Math.Log(tier, 2)
					};
				case < 100:
					return new Upgrade<long>
					{
						cost = (long)System.Math.Pow(tier, 10),
						value = (long)System.Math.Pow(tier, 2)
					};
				default:
					return new Upgrade<long>
					{
						cost = -1,
						value = (long)System.Math.Pow(100, 2)
					};
			}
		}
	}

	public Upgrade<float> ClickDelay
	{
		get
		{
			byte tier = curData.upgradeData.speedTier;

			switch(tier)
			{
				case < 10:
					return new Upgrade<float>()
					{
						cost = tier * (long)System.Math.Pow(10, tier),
						value = 1.0f / tier
					};

				default:
					return new Upgrade<float>()
					{
						cost = -1,
						value = .1f
					};
			}
		}
	}

	public Upgrade<long> AutoProduce
	{
		get
		{
			byte tier = curData.upgradeData.automationTier;

			switch(tier)
			{
				case < 5:
					return new Upgrade<long>
					{
						cost = tier * 10,
						value = tier
					};
				case < 10:
					return new Upgrade<long>
					{
						cost = tier * (long)System.Math.Log(tier, 2),
						value = tier * 2
					};
				case < 25:
					return new Upgrade<long>
					{
						cost = tier * tier * tier,
						value = tier * (long)System.Math.Log(tier, 2)
					};
				case < 100:
					return new Upgrade<long>
					{
						cost = (long)System.Math.Pow(tier, 10),
						value = (long)System.Math.Pow(tier, 2)
					};
				default:
					return new Upgrade<long>
					{
						cost = -1,
						value = (long)System.Math.Pow(100, 2)
					};
			}
		}
	}

	public Upgrade<float> AutoDelay
	{
		get
		{
			byte tier = curData.upgradeData.autoSpeedTier;

			switch(tier)
			{
				case < 10:
					return new Upgrade<float>()
					{
						cost = tier * (long)System.Math.Pow(10, tier),
						value = 1.0f / tier
					};

				default:
					return new Upgrade<float>()
					{
						cost = -1,
						value = .1f
					};
			}
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		UpdateStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void UpdateStatus()
	{
		string newText = $"Produce Speed\n{curData.upgradeData.productionTier} {curData.upgradeData.speedTier}\n{ClickProduce.value} {ClickDelay.value}\n{ClickProduce.cost} {ClickDelay.cost}";

		statusText.text = newText;
	}

	public void UpgradeClickProduction()
	{
		if(curData.clickerData.score < ClickProduce.cost) return;
		curData.clickerData.score -= ClickProduce.cost;
		GameManager.Instance.Clicker.UpdateScore();

		if(++curData.upgradeData.productionTier > clickProductionLimit)
		{
			curData.upgradeData.productionTier = clickProductionLimit;
		}

		UpdateStatus();
	}
	public void UpgradeClickDelay()
	{
		if(curData.clickerData.score < ClickDelay.cost) return;
		curData.clickerData.score -= ClickDelay.cost;
		GameManager.Instance.Clicker.UpdateScore();

		if(++curData.upgradeData.speedTier > clickDelayLimit)
		{
			curData.upgradeData.speedTier = clickDelayLimit;
		}

		UpdateStatus();
	}
	public void UpgradeAutoProduction()
	{
		if(curData.clickerData.score < AutoProduce.cost) return;
		curData.clickerData.score -= AutoProduce.cost;
		GameManager.Instance.Clicker.UpdateScore();

		if(++curData.upgradeData.automationTier > autoProductionLimit)
		{
			curData.upgradeData.automationTier = autoProductionLimit;
		}

		UpdateStatus();
	}
	public void UpgradeAutoDelay()
	{
		if(curData.clickerData.score < AutoDelay.cost) return;
		curData.clickerData.score -= AutoDelay.cost;
		GameManager.Instance.Clicker.UpdateScore();

		if(++curData.upgradeData.autoSpeedTier > autoDelayLimit)
		{
			curData.upgradeData.autoSpeedTier = autoDelayLimit;
		}

		UpdateStatus();
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

public struct Upgrade<T>
{
	public long cost;//the cost to upgrade to the following tier
	public T value;//current tiers value
}