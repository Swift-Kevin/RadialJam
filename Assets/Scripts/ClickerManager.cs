using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerManager : MonoBehaviour
{

    public TMP_Text scoreText;
    public Button clickerButton;

	public SaveData curData;

	public RadialMenu radial;
	private RadialSegment clickerSegment;

	private void Awake()
	{
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
		clickerButton.onClick.AddListener(ClickerClick);
	}

	private void OnDisable()
	{
		clickerButton.onClick.RemoveListener(ClickerClick);
	}

	public void ClickerClick()
	{
		clickerButton.interactable = false;

		StartCoroutine(Timer.Countdown(1, ScoreRoutine));
	}

	public void ScoreRoutine(CountdownStatus status)
	{
		clickerSegment.SetFill(status.progress);

		if(status.isDone)
		{
			curData.score++;
			UpdateScore();
			clickerButton.interactable = true;
		}
	}

	public void UpdateScore()
	{
		scoreText.text = $"{curData.score}";
	}
}
