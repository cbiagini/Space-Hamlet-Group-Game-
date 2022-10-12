using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    public GameObject audioManagerObject;
    public GameObject globalLightObject;
    public GameObject pauseMenuVisible;
    [SerializeField] private List<TextMeshProUGUI> changeableFontObjects = new List<TextMeshProUGUI>();
    public bool isPaused = false;
    public TMP_FontAsset[] selectableFonts;


    // Start is called before the first frame update
    void Awake()
    {
        pauseMenuVisible.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused == false)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenuVisible.SetActive(true);
        Time.timeScale = 0f;
        //audioManagerObject.GetComponent<AudioManager>().BGM.pitch *= 0.5f;
    }

    public void Resume()
    {
        pauseMenuVisible.SetActive(false);
        Time.timeScale = 1f;
        //audioManagerObject.GetComponent<AudioManager>().BGM.pitch *= 2f;
    }

    public void BackToMain()
    {
        Time.timeScale = 1f; //debug set scene back to speed in case you do use this
        SceneManager.LoadScene("Main Menu");
    }

    public void ChangeAllGameFonts(int fontNumber)
    {
        TMP_FontAsset font = selectableFonts[fontNumber];
        foreach (TextMeshProUGUI changer in changeableFontObjects)
        {
            changer.font = font;
        }
    }

    public void ChangeGameLanguage(int languageNumber)
    {
        switch (languageNumber) {
            case 0:
                //set to english;
                Debug.LogWarning("Trying to set to English, but no language settings yet!");
                break;
            case 1:
                //set to spanish;
                Debug.LogWarning("Intentaste cambiar a Español, ¡aunque no hay opciones de idioma!");
                break;
            case 2:
                //set to portuguese;
                Debug.LogWarning("Tentando colocar em Porutuguês, mas ainda não há opções de idioma!");
                break;
            default:
                //set to english;
                Debug.LogError("Setting off of language barrier!");
                break;
        }
    }
}
