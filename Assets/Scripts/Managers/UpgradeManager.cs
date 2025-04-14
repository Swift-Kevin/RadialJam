using UnityEngine;
using TMPro;
using System.Data;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI amtValue;
    public TextMeshProUGUI rateValue;
    public TextMeshProUGUI autoAmtValue;
    public TextMeshProUGUI autoRateValue;

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

	public void UpdateStatus(float _amtValue = -1, float _rateValue = -1, float _autoAmt = -1, float _autoRate = -1)
	{
		//string newText = $"Produce Speed\n{curData.upgradeData.productionTier} {curData.upgradeData.speedTier}\n{ClickProduce.value} {ClickDelay.value}\n{ClickProduce.cost} {ClickDelay.cost}";
		//statusText.text = newText;
		if (_amtValue > -1)
		{
			amtValue.text = _amtValue.ToString();
			//UIManager.Instance.upgradeValues["Amount"].text = ClickProduce.cost.ToString();
		}
        if (_rateValue > -1)
        {
            rateValue.text = _rateValue.ToString();
			//UIManager.Instance.upgradeValues["Rate"].text = ClickDelay.cost.ToString();
        }
        if (_autoAmt > -1)
        {
            autoAmtValue.text = _autoAmt.ToString();
			//UIManager.Instance.upgradeValues["AutoAmount"].text = AutoProduce.cost.ToString();
        }
        if (_autoRate > -1)
        {
            autoRateValue.text = _autoRate.ToString();
			//UIManager.Instance.upgradeValues["AutoRate"].text = AutoDelay.cost.ToString();
        }
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

		UpdateStatus(_amtValue: curData.upgradeData.productionTier);
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

		UpdateStatus(_rateValue: curData.upgradeData.speedTier);
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

		UpdateStatus(_autoAmt: curData.upgradeData.automationTier);
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

		UpdateStatus(_autoRate: curData.upgradeData.autoSpeedTier);
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