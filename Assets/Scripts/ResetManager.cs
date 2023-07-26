using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class ResetManager : MonoBehaviour
{
    private static ResetManager instance;

    // reset count
    public int resetCount = 0;

    // # times 'q' is hit
    private int resetStage;

    private Vector3 spawnPos;

    // Vignette Effect
    private float startTime;
    private float blurAmount;
    [SerializeField] private float timeBeforeTimeout;
    private float elapsedTime;
    private float initialWeight;

    // Outside Objects
    public GameObject volumeObject;
    private Volume globalVolume;
    public GameObject player;

    public static ResetManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ResetManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("ResetManager");
                    instance = singletonObject.AddComponent<ResetManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        resetStage = 0;

        globalVolume = volumeObject.GetComponent<Volume>();

        spawnPos = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (resetStage == 0)
            {
                Time.timeScale = 0f;
                startTime = Time.realtimeSinceStartup;
                globalVolume.weight = .5f;
                initialWeight = globalVolume.weight;
                resetStage = 1;
            }
            else if (resetStage == 1)
            {
                startTime = Time.realtimeSinceStartup;
                globalVolume.weight = 1f;
                initialWeight = globalVolume.weight;
                resetStage = 2;
            }
            else
            {
                ResetScene();
                resetCount++;
                resetStage = 0;
                globalVolume.weight = 0f;
                Time.timeScale = 1f;
            }
        }

        // Gradually undarken the screen if 'q' key was pressed.
        if (resetStage > 0)
        {
            elapsedTime = Time.realtimeSinceStartup - startTime;
            float t = elapsedTime / timeBeforeTimeout;

            // Gradually revert globalVolume.weight to 0 over 2 seconds.
            float targetWeight = Mathf.Lerp(initialWeight, 0f, t);
            globalVolume.weight = targetWeight;


            if (elapsedTime >= timeBeforeTimeout)
            {
                if (resetStage == 3)
                {
                    ResetScene();
                    resetCount++;
                    Time.timeScale = 1f;
                }
                Debug.Log("Reset Timed Out");
                elapsedTime = 0;
                resetStage = 0;
                Time.timeScale = 1f;
            }
        }
    }

    private void ResetScene()
    {
        /* Reset Code we can write later
        ...
        */

        player.transform.position = spawnPos;
        Debug.Log("Scene Reset");
    }
}
