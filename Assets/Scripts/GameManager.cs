
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



}