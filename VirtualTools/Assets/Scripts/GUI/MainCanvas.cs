using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
	private Text m_connectText;
	private GameObject m_connectPanel;
    private GameObject m_dogPanel;
    private GameObject m_controlPanel;
    private GameObject m_keyboardHints;
    private GameObject m_gamepadHints;
    private GameObject m_dog;
    private GameObject m_escapeMenu;
    private GameObject m_sceneSelector;
    private GameObject m_assessmentModePanel;
    private GameObject m_retryButton;
    private GameObject m_exitButton;
    private GameObject m_nextSessionButton;
    private GameObject m_resumeButton;
    private GameObject m_showTutorialsPanel;
    private GameObject m_tutorialToggle;
    private GameObject m_pauseIcon;

	
    private GameObject m_results;
    private Text m_nameText;
    private Text m_titleText;
    private Text m_studentNumberText;
    private Text m_dateText;
    private Text m_startText;
    private Text m_linkText;
    private TextMeshProUGUI m_logsText;

    public void Init()
    {
		// Get all objects references
		m_connectPanel = GameObject.Find("ConnectPanel");
        m_dogPanel = GameObject.Find("DogPanel");
        m_controlPanel = GameObject.Find("ControlsHintPanel"); 
        m_dog = GameObject.Find("Dog");
        m_keyboardHints = m_controlPanel.transform.Find("KeyboardHints").gameObject;
        m_gamepadHints = m_controlPanel.transform.Find("GamepadHints").gameObject;
        m_escapeMenu = GameObject.Find("EscapeMenu");
        m_sceneSelector = GameObject.Find("SceneSelector");
        m_assessmentModePanel = GameObject.Find("AssessmentModePanel");
        m_showTutorialsPanel = GameObject.Find("ShowTutorialsPanel");
        m_tutorialToggle = GameObject.Find("TutorialToggle");
        m_pauseIcon = GameObject.Find("PauseIcon");
        m_tutorialToggle.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => { GameManager.Instance.SetTurorialFlags(value); });

        m_results = GameObject.Find("ResultsPanel");
		
        m_titleText = m_results.transform.Find("Title").GetComponent<Text>();
        m_nameText = m_results.transform.Find("StudentInfo").transform.Find("Name").GetComponent<Text>();
        m_studentNumberText = m_results.transform.Find("StudentInfo").transform.Find("StudentNumber").GetComponent<Text>();
        m_dateText = m_results.transform.Find("StudentInfo").transform.Find("Date").GetComponent<Text>();
        m_startText = m_results.transform.Find("StudentInfo").transform.Find("StartTime").GetComponent<Text>();

        m_exitButton = m_results.transform.Find("Quit Button").gameObject;
        m_retryButton = m_results.transform.Find("Retry Button").gameObject;
        m_nextSessionButton = m_results.transform.Find("Next Button").gameObject;
        m_resumeButton = m_results.transform.Find("Resume Button").gameObject;

        m_logsText = GameObject.Find("LogsText").GetComponent<TextMeshProUGUI>();
        m_linkText = m_results.transform.Find("Link").GetComponent<Text>();
		m_connectText = m_connectPanel.transform.Find("ConnectText").GetComponent<Text>();

		m_connectPanel.SetActive(false);
        m_dogPanel.SetActive(false);
        m_controlPanel.SetActive(false);
        m_results.SetActive(false);
        m_dog.SetActive(false);
        m_keyboardHints.SetActive(false);
        m_gamepadHints.SetActive(false);
        m_assessmentModePanel.SetActive(false);
        m_nextSessionButton.SetActive(false);
        m_resumeButton.SetActive(false);
        m_showTutorialsPanel.SetActive(false);
        m_pauseIcon.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //Check connection
        StartCoroutine(CheckConnect());        
    }


    public void SetPauseIconOn(bool on)
    {
        m_pauseIcon.SetActive(on);
    }
    public void SetShowTutorialsPanelOn(bool on)
    {
        m_showTutorialsPanel.SetActive(on);
        GameObject yesBtn = m_showTutorialsPanel.transform.Find("Yes").gameObject;
        // Add selected object, so GamePad can navigate buttond
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(yesBtn);
    }

    public void SetAssessmentModePanel(bool on)
    {
        m_assessmentModePanel.SetActive(on);
    }

    // Enable/disable control hints, depending on last input device used
    public void SetHintActive(bool active)
    {
        m_controlPanel.SetActive(active);

        if (active)
        {
            if (GameManager.Instance.IsUsingKeyboard())
            {
                m_gamepadHints.SetActive(false);
                m_keyboardHints.SetActive(true);
            }
            else
            {
                m_keyboardHints.SetActive(false);
                m_gamepadHints.SetActive(true);
            }
        }
        else
        {
            m_gamepadHints.SetActive(false);
            m_keyboardHints.SetActive(false);
        }        
    }

    public void SetResultsPanelTitle(string msg)
    {
        m_titleText.text = msg;
    }

    public void EnableNextSessionBtn(bool enable)
    {
        m_nextSessionButton.SetActive(enable);
    }

    public void EnableResumeBtn(bool enable)
    {
        m_resumeButton.SetActive(enable);
    }

    public void EnableRetryBtn(bool enable)
    {
        m_retryButton.SetActive(enable);
    }
    public void DogSpeak(string text)
    {
        SetDogEnabled(true);
        m_dog.transform.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void SetDogEnabled(bool enabled)
    {
        m_dog.SetActive(enabled);
        m_dogPanel.SetActive(enabled);
    }

    private IEnumerator PopUpMessage(float seconds, string text)
    {
        
        DogSpeak(text);
        yield return new WaitForSeconds(seconds);
        SetDogEnabled(false);
    }
    public void DogPopUp(float seconds, string text)
    {
        StartCoroutine(PopUpMessage(seconds, text));
    }

    public void SetSceneSelectionGUIOn(bool on)
    {
        m_sceneSelector.SetActive(on);
        m_escapeMenu.SetActive(on);
        GameObject selectByName = m_sceneSelector.transform.Find("SelectByNameButton").gameObject;
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(selectByName);
    }
	private IEnumerator CheckConnect()
	{
	    bool loopBool = true;
		while (loopBool)
		{
			yield return new WaitForSeconds(3.0f);
			if(Application.internetReachability == NetworkReachability.NotReachable)
			{
				string t;
				t = "Sorry you do not have connection, please check your internet.";    
				m_connectPanel.transform.GetComponentInChildren<TextMeshProUGUI>().text = t;
				m_connectPanel.SetActive(true);				

			}
			else 
			{
				string t;
				t = "You are connected and online.";
				m_connectPanel.transform.GetComponentInChildren<TextMeshProUGUI>().text = t;
				//m_connectPanel.SetActive(true);
				
				yield return new WaitForSeconds(5.0f);
				m_connectPanel.SetActive(false);
			}
			
		}
    }
    /// <summary>
    /// Displays a sequence of insturctions which can be dismissed by mouse click
    /// </summary>
    /// <param name="instructions">Instructions to be displayed</param>
    /// <param name="action">Action to be taken at the end of the seuence</param>
    /// <returns></returns>
    private IEnumerator InstructionSequence(string[] instructions, Action action)
    {
        yield return new WaitForSeconds(0.05f);

        int index = 0;
        while(index < instructions.Length)
        {
            if (index >= instructions.Length) break;
            DogSpeak(instructions[index]);
            if (Input.GetButtonDown("Fire1") && !SessionManager.Instance.IsSessionPaused())
            {
                index++;
                if (index >= instructions.Length) break;

                DogSpeak(instructions[index]);
                yield return new WaitForSeconds(0.5f);
            }
            else
                yield return null;
        }
        SetDogEnabled(false);
        StartCoroutine(StartAction(action));
        yield return null;

    }

    IEnumerator StartAction(Action action)
    {
        action();
        yield return null;
    }

    /// <summary>
    /// Start instruction sequence
    /// </summary>
    /// <param name="instructions">Instructions to be displayed</param>
    /// <param name="action">Action to be taken at the end of the seuence</param>
    public void DogInstructionSequence(string[] instructions, Action action)
    {
        StopAllCoroutines();
        m_dogPanel.GetComponent<Animator>().SetTrigger("Start");
        StartCoroutine(InstructionSequence(instructions, action));
    }

    public void SetEscapeMenu(bool enabled)
    {
        m_escapeMenu.SetActive(enabled);
    }

    public void EnableExitButton(bool enable)
    {
        m_exitButton.SetActive(enable);
    }

    public void SetSceneSelector(bool enabled)
    {
        m_sceneSelector.SetActive(enabled);
    }

    public void ToggleEscapeMenu(bool includeSceneSelector = false)
    {
        m_escapeMenu.SetActive(!m_escapeMenu.activeSelf);
        SetSceneSelector(m_escapeMenu.activeSelf); //Hide or show the scene selector depending on whether the escape menu is hidden or shown, respectively
    }

    /// <summary>
    /// Display result menu
    /// </summary>
    /// <param name="name">Student name</param>
    /// <param name="studentNumber">Student number</param>
    /// <param name="results">Session results</param>
    /// <param name="isPause">Whether this menu is displayed for pause</param>
    public void DisplayResults(string name, string studentNumber, SessionResults results, bool isPause = false)
    {
        m_gamepadHints.SetActive(false);
        m_keyboardHints.SetActive(false);
        m_assessmentModePanel.SetActive(false);
        m_tutorialToggle.GetComponent<Toggle>().isOn = GameManager.Instance.WillShowTutorials;
        m_results.SetActive(true);

        m_nameText.text = name;
        m_studentNumberText.text = studentNumber;
        m_dateText.text = "Date: " + results.date.ToShortDateString();
        m_startText.text = "Start Time: " + results.startTime.ToShortTimeString();

        m_logsText.text = "";
        m_logsText.richText = true;
        foreach (string log in results.logs)
        {
            if (log.Contains("Failed"))
                m_logsText.text += "<color=red>" + log + "</color>\n";
        }

        if (m_logsText.text.Equals(""))
            m_logsText.text = "No errors made.";

        if(isPause && GameManager.Instance.IsAssessmentMode())
            m_logsText.text = "Not available while the session is running.";

        m_linkText.text = "See the full report at: http://vegas.murdoch.edu.au/vinst/";

        // Add seleceted object for GamePad navigation
        if (m_resumeButton.activeInHierarchy)
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(m_resumeButton);
        else if(m_exitButton.activeInHierarchy)
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(m_exitButton);
        else if (m_nextSessionButton.activeInHierarchy)
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(m_nextSessionButton);
    }

    public void HideResultPanel()
    {
        m_results.SetActive(false);
    }

    public bool GetResultsPanelEnabled()
    {
        return m_results.activeSelf;
    }
}
