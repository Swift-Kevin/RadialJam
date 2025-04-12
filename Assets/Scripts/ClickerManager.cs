using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerManager : MonoBehaviour
{

    public TMP_Text scoreText;
    public Button clickerButton;

	public SaveData curData;

	private void Awake()
	{
		
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		UpdateScore();
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
		curData.score++;

		UpdateScore();
	}

	public void UpdateScore()
	{
		scoreText.text = $"{curData.score}";
	}
}
