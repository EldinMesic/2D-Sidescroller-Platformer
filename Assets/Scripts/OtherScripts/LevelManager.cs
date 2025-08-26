using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Pixelizable.PixelStatus pixelStatus;
    private int currStage = 1;
    [SerializeField] private int currAct = 1;
    public static LevelManager Instance { get; private set; }


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene());
    }

    public void SetPixelStatus(Pixelizable.PixelStatus pixelStatus)
    {
        Pixelizable[] pixelList = FindObjectsOfType<Pixelizable>();
        foreach (Pixelizable pixel in pixelList)
        {
            pixel.pixelStatus = pixelStatus;
        }
    }

    public void LoadNextScene()
    {
        currStage++;
        if(currStage > 4)
        {
            currAct++;
            if(currAct <= 4)
            {
                pixelStatus++;
                currStage = 1;
                StartCoroutine(LoadScene(LoadSceneMode.Single));
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
            
        }
        else
        {
            StartCoroutine(LoadScene(LoadSceneMode.Additive));
        }
    }

    IEnumerator LoadScene(LoadSceneMode mode)
    {
        LevelTextManager textManager = FindFirstObjectByType<LevelTextManager>().GetComponent<LevelTextManager>();

        string sceneName = $"Level {currAct}-{currStage}";
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);
        textManager.ShowLevelMessage(sceneName);

        FindFirstObjectByType<PlayerInput>().GetComponent<PlayerInput>().enabled = false;


        while (!operation.isDone)
        {
            yield return null;
        }
        SetPixelStatus(pixelStatus);

        yield return new WaitForSeconds(textManager.fixedStayTime);

        textManager.HideTextMessage();

        FindFirstObjectByType<PlayerInput>().GetComponent<PlayerInput>().enabled = true;
        




    }


    IEnumerator StartScene()
    {
        string sceneName = $"Level {currAct}-{currStage}";

        LevelTextManager textManager = FindFirstObjectByType<LevelTextManager>().GetComponent<LevelTextManager>();
        textManager.ShowLevelMessage(sceneName);
        FindFirstObjectByType<PlayerInput>().GetComponent<PlayerInput>().enabled = false;

        SetPixelStatus(pixelStatus);
        yield return new WaitForSeconds(textManager.fixedStayTime);
        

        textManager.HideTextMessage();

        FindFirstObjectByType<PlayerInput>().GetComponent<PlayerInput>().enabled = true;
    }


}
