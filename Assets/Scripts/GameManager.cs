
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Camera GameCamera;

    public static GameManager Instance { get; private set; }

    [SerializeField] private ClickerManager clickerMan;
	public ClickerManager Clicker
    {
        get
        {
            return clickerMan;
        }
    }

    [SerializeField] private UpgradeManager upgradeMan;
	public UpgradeManager Upgrade
	{
		get
		{
			return upgradeMan;
		}
	}

	private void Awake()
    {
        Instance = this;
        GameCamera = Camera.main;
    }
}