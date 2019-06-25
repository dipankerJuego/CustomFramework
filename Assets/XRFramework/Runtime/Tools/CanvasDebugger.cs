using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDebugger : MonoBehaviour
{
    struct Log
    {
        public string message;
        public string stackTrace;
        public LogType type;
    }

    [Tooltip("The size of the font the log text is displayed in.")]
    public int fontSize = 14;
    [Tooltip("The colour of the text for an info log message.")]
    public Color infoMessage = Color.black;
    [Tooltip("The colour of the text for an assertion log message.")]
    public Color assertMessage = Color.black;
    [Tooltip("The colour of the text for a warning log message.")]
    public Color warningMessage = Color.yellow;
    [Tooltip("The colour of the text for an error log message.")]
    public Color errorMessage = Color.red;
    [Tooltip("The colour of the text for an exception log message.")]
    public Color exceptionMessage = Color.red;

    private Dictionary<LogType, Color> logTypeColors;
    private ScrollRect scrollWindow;
    private RectTransform consoleRect;
    private Text consoleOutput;
    private const string NEW_LINE = "\n";
    private int lineBuffer = 50;
    private int currentBuffer;
    private string lastMessage;
    private bool collapseLog = false;

    [SerializeField] private UnityEngine.EventSystems.EventSystem m_eventSystem;

    private void Awake()
    {
        logTypeColors = new Dictionary<LogType, Color>()
            {
                { LogType.Assert, assertMessage },
                { LogType.Error, errorMessage },
                { LogType.Exception, exceptionMessage },
                { LogType.Log, infoMessage },
                { LogType.Warning, warningMessage }
            };

        scrollWindow = transform.Find("Panel/Scroll View").GetComponent<ScrollRect>();
        consoleRect = transform.Find("Panel/Scroll View/Viewport/Content").GetComponent<RectTransform>();
        consoleOutput = transform.Find("Panel/Scroll View/Viewport/Content/ConsoleOutput").GetComponent<Text>();

        consoleOutput.fontSize = fontSize;
        ClearLog();

        if (UnityEngine.EventSystems.EventSystem.current == null)
            m_eventSystem.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
        consoleRect.sizeDelta = Vector2.zero;
    }

    private string GetMessage(string message, LogType type)
    {
        string color = ColorUtility.ToHtmlStringRGBA(logTypeColors[type]);
        return "<color=#" + color + ">" + message + "</color>" + NEW_LINE;
    }

    /// <summary>
    /// Records a log from the log callback.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="stackTrace">Trace of where the message came from.</param>
    /// <param name="type">Type of message (error, exception, warning, assert).</param>
    void HandleLog(string message, string stackTrace, LogType type)
    {
        string logOutput = GetMessage(message, type);

        if (!collapseLog || lastMessage != logOutput)
        {
            consoleOutput.text += logOutput;
            lastMessage = logOutput;
        }

        consoleRect.sizeDelta = new Vector2(consoleOutput.preferredWidth, consoleOutput.preferredHeight);
        scrollWindow.verticalNormalizedPosition = 0;
        currentBuffer++;
        if (currentBuffer >= lineBuffer)
        {
            IEnumerable<string> lines = Regex.Split(consoleOutput.text, NEW_LINE).Skip(lineBuffer / 2);
            consoleOutput.text = string.Join(NEW_LINE, lines.ToArray());
            currentBuffer = lineBuffer / 2;
        }
    }

    public void SetCollapse(bool state)
    {
        collapseLog = state;
    }

    public void ClearLog()
    {
        consoleOutput.text = "";
        currentBuffer = 0;
        lastMessage = "";
    }
}
