using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerManager : MonoBehaviour
{

    public TMP_Text scoreText;

	[SerializeField]private SaveData curData;

	public RadialMenu radial;
	private RadialSegment clickerSegment;

	private bool isClicking;

	private void Awake()
	{
		isClicking = false;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		UpdateScore();
		clickerSegment = radial.Segments[0];
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void ClickerClick()
	{
		if(isClicking) return;
		isClicking = true;

		StartCoroutine(Timer.Countdown(GameManager.Instance.Upgrade.ClickDelay, ScoreRoutine));
	}

	private void ScoreRoutine(CountdownStatus status)
	{
		clickerSegment.SetFill(status.progress);

		if(status.isDone)
		{
			curData.clickerData.score += GameManager.Instance.Upgrade.ClickProduce;
			UpdateScore();
			isClicking = false;
		}
	}

	public void UpdateScore()
	{
		scoreText.text = $"{curData.clickerData.score}";
	}
}

[System.Serializable]
public struct ClickerData
{
	public long score;
}

public struct ClickerInt
{

}