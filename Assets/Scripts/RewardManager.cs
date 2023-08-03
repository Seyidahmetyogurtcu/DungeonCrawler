using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public GameObject HealthUpgrade;
    public GameObject[] Rewards;
    private GameObject[] enemies;
    private int rewardNum;
    private int maxRewardNum;
    private bool gaveRewardFlag;

    private void Start()
    {
        maxRewardNum = Rewards.Length;
    }
    private void OnEnable()
    {
        ResetManager.OnReset += HandleReset;
    }

    private void OnDisable()
    {
        ResetManager.OnReset -= HandleReset;
    }

    private void HandleReset(int resetCount)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        rewardNum = Mathf.Min(resetCount,maxRewardNum);
        gaveRewardFlag = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (other.CompareTag("Player"))
        {
            if (enemies.Length == 0 && !gaveRewardFlag)
            {
                Instantiate(HealthUpgrade, new Vector3(0, 0, 0), Quaternion.identity);
                Instantiate(Rewards[rewardNum], new Vector3(0, 3, 0), Quaternion.identity);
                gaveRewardFlag = true;
            }
        }
    }
}
