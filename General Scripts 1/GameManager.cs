using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;

    [Header("Game Info")]
    public int levelAt;
    public int currentRoom;

    [Header("Analysis Settings")]
    public float maxAnalysisTime = 15f;
    public float currentAnalysisTime;
    public float analysisRegenRate = .5f;
    public float analysisDecayRate = 1f;

    [Header("Delay")]
    public float delayTime = 1f;
    private bool isDelay;

    [Header("Player")]
    public GameObject[] playerObjs;
    private int playerObjIndex;

    public bool isPlayerCrouched;
    public bool isPlayerMoving;
    public int currentPepperSprays;

    public bool isTutorialPresent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        playerObjIndex = 0;

        if (!isTutorialPresent)
            state = GameState.Gameplay;
        else
            state = GameState.Tutorial;

        currentAnalysisTime = maxAnalysisTime;
        isDelay = false;
        isPlayerCrouched = true;
        isPlayerMoving = false;

        Cursor.visible = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPepperSprays = PlayerPrefs.GetInt("_pepperSpray", currentPepperSprays);

        PlayerPrefs.SetInt("_lastLevel", SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case GameState.Gameplay:

                AnalysisRegen();

                if (Input.GetKey(SettingsManager.instance.keyAnalysis) && currentAnalysisTime > 0f)
                {
                    if (!isDelay)
                    {
                        UIManager.instance.SwitchAnalysis();
                        StartCoroutine(Delay());
                    }
                }

                /*if (!isDelay)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        DEVTOOL_NextFloor();
                        StartCoroutine(Delay());
                    }


                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        DEVTOOL_PrevFloor();
                        StartCoroutine(Delay());
                    }

                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        DEVTOOL_NextScene();
                        StartCoroutine(Delay());
                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        DEVTOOL_PrevScene();
                        StartCoroutine(Delay());
                    }
                }*/

                break;

            case GameState.Analysis:

                AnalysisDecay();

                if (Input.GetKey(SettingsManager.instance.keyAnalysis))
                {
                    if (!isDelay)
                    {
                        UIManager.instance.SwitchAnalysis();
                        StartCoroutine(Delay());
                    }
                }

                break;
                
        }
    }

    void AnalysisDecay()
    {
        if (currentAnalysisTime <= 0f)
        {
            currentAnalysisTime = 0f;
            UIManager.instance.SwitchAnalysis();
        }
        else
            currentAnalysisTime -= analysisDecayRate * Time.deltaTime;
    }

    void AnalysisRegen()
    {
        if (currentAnalysisTime < maxAnalysisTime)
            currentAnalysisTime += analysisRegenRate * Time.deltaTime;
        else
            currentAnalysisTime = maxAnalysisTime;
    }

    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }

    public void ConsumePepperSpray()
    {
        currentPepperSprays--;
    }

    public void NextPlayer()
    {
        PlayerController controller = playerObjs[playerObjIndex].GetComponent<PlayerController>();
        controller.Stop();

        playerObjs[playerObjIndex].SetActive(false);

        if (playerObjIndex >= playerObjs.Length)
            playerObjIndex = 0;
        else
            playerObjIndex++;

        playerObjs[playerObjIndex].SetActive(true);
    }
    
    public void PrevPlayer()
    {
        PlayerController controller = playerObjs[playerObjIndex].GetComponent<PlayerController>();
        controller.Stop();

        playerObjs[playerObjIndex].SetActive(false);

        if (playerObjIndex < 0)
            playerObjIndex = playerObjs.Length - 1;
        else
            playerObjIndex--;

        playerObjs[playerObjIndex].SetActive(true);
    }

    public void SetPlayerActive(int index)
    {
        PlayerController controller = playerObjs[playerObjIndex].GetComponent<PlayerController>();
        controller.Stop();

        playerObjs[playerObjIndex].SetActive(false);
        playerObjIndex = index;
        playerObjs[playerObjIndex].SetActive(true);
    }

    public GameObject FindActivePlayer()
    {
        foreach (GameObject player in playerObjs)
        {
            if (player.activeInHierarchy)
                return player;
        }

        Debug.Log("No active player");
        return null;
    }

    public void DEVTOOL_NextFloor()
    {
        GameManager.instance.NextPlayer(); // Set next floor player active
        CameraManager.instance.NextCameraGroup(); // Set Camera group to next
    }

    public void DEVTOOL_PrevFloor()
    {
        GameManager.instance.PrevPlayer();
        CameraManager.instance.PrevCameraGroup();
    }

    public void DEVTOOL_NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DEVTOOL_PrevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void SavePepperSpray()
    {
        PlayerPrefs.SetInt("_pepperSpray", currentPepperSprays);
    }
}

public enum GameState
{
    Gameplay,
    Analysis,
    Paused,
    Tutorial,
    GameOver
}
