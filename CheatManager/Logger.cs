﻿using System.Collections.Generic;
using UnityEngine;
using Common.MyGUI;

namespace CheatManager
{
    public class Logger : MonoBehaviour
    {
        private static Logger _Instance = null;

        private static Rect windowRect = new Rect(Screen.width - 500, 764, 500, Screen.height-764);
        
        private static Rect scrollRect = new Rect(windowRect.x, windowRect.y + 26, windowRect.width - 5, windowRect.height - 58);        

        private static Vector2 scrollPos = Vector2.zero;

        private static float contentHeight = 0;
        private static float drawingPos;        

        private static List<LOG> logMessage = new List<LOG>();

        private static int messageCount = 0;
       
        private readonly KeyCode toggleKey = KeyCode.Delete;       

        public bool show = false;
        public static string inputField = "";
        private static readonly int MAXLOG = 100; 
        
        private static List<string> history = new List<string>();

        private static int historyIndex = 0;
        
        private struct LOG
        {
            public string message;
            public string stackTrace;
            public LogType type;
        }
        
        private static readonly Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>()
        {   { LogType.Error, Color.magenta },
            { LogType.Assert, Color.blue },
            { LogType.Warning, Color.yellow },            
            { LogType.Log, Color.green },
            { LogType.Exception, Color.red },            

        };        
         
        private static Logger GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType(typeof(Logger)) as Logger;
                if (_Instance == null)
                {
                    GameObject logger = new GameObject();
                    logger.AddComponent<Logger>();
                    logger.name = "CheatManager.Logger";
                    _Instance = logger.GetComponent<Logger>();
                    _Instance.Awake();
                }
            }

            return _Instance;
        }

        public static void Load()
        {
            GetInstance();            
        }

        public void Awake()
        {
            _Instance = this;
            DontDestroyOnLoad(this);            
            useGUILayout = false;
            
#if DEBUG
            show = true;
#endif
        }        

        public void OnDestroy()
        {
            logMessage.Clear();
        }

        public void OnEnable()
        {
            Application.logMessageReceived += HandleLog;            
        }

        public void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;            
        }             

        private static float CalcTextHeight(string s)
        {
            int count = 1;            
            
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\n')
                {
                    count++;
                }
            }

            if (count == 1)
            {
                int x = GUI_Tools.DivideRoundUP(s.Length, 76);

                if (count == x)
                    return count * 21f;
                else
                    return x * 17f;
            }            

            return (count * 17f);            
        }


        void OnGUI()
        {
            
            if (!show)
            {
                return;
            }            

            GUI_Tools.CreatePopupWindow(windowRect, "CheatManager Console (Press DEL to toggle)");

            scrollPos = GUI.BeginScrollView(scrollRect, scrollPos, new Rect(scrollRect.x, scrollRect.y, scrollRect.width - 40, drawingPos - scrollRect.y));
            
            for (int i = 0; i < logMessage.Count; i++)
            {
                if (i == 0)
                {
                    drawingPos = scrollRect.y;
                }

                GUI.contentColor = logTypeColors[logMessage[i].type];

                contentHeight = CalcTextHeight(logMessage[i].message);
                
                GUI.Label(new Rect(scrollRect.x + 5, drawingPos, 10, 21), "> ");

                GUI.Label(new Rect(scrollRect.x + 20, drawingPos, scrollRect.width - 40, contentHeight), logMessage[i].message);

                drawingPos += contentHeight;

                if (logMessage[i].stackTrace != "")
                {
                    contentHeight = CalcTextHeight(logMessage[i].stackTrace);                   
                    
                    GUI.Label(new Rect(scrollRect.x + 20, drawingPos, scrollRect.width - 40, contentHeight), logMessage[i].stackTrace);

                    drawingPos += contentHeight;
                }              
            }

            if (messageCount != logMessage.Count)
            {
                scrollPos.y += Mathf.Infinity;
                messageCount = logMessage.Count;
            }

            GUI.EndScrollView();

            GUI.contentColor = Color.white;

            
            if (Event.current.Equals(Event.KeyboardEvent("return")) && inputField != "")
            {
                history.Add(inputField);
                historyIndex = history.Count;
                Log(inputField);
                DevConsole.SendConsoleCommand(inputField);
                inputField = "";
            }
            
            if (Event.current.Equals(Event.KeyboardEvent("up")))
            {
                if (history.Count > 0 && historyIndex >= 1)
                {
                    historyIndex--;
                    inputField = history[historyIndex];
                }
            }

            if (Event.current.Equals(Event.KeyboardEvent("down")))
            {
                if (history.Count > 0 && historyIndex < history.Count - 1)
                {
                    historyIndex++;
                    inputField = history[historyIndex];
                }
            }
            
            inputField = GUI.TextField(new Rect(scrollRect.x + 5, scrollRect.y + scrollRect.height + 5, 300, 22), inputField);

            if (GUI.Button(new Rect(scrollRect.x + 310, scrollRect.y + scrollRect.height + 5, scrollRect.width - 310, 22),"Clear Window"))
            {
                logMessage.Clear();
                drawingPos = scrollRect.y;
            }
        }            
            
        public void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                show = !show;                
            }

            if (Player.main != null)
            {
                InfoBar.InitInfoBar(show);
            }            
        }        

        private void Write(string message)
        {
            logMessage.Add(new LOG()
            {
                message = message,
                stackTrace = "",
                type = LogType.Log,
            });
            
            if (logMessage.Count == MAXLOG)
            {
                logMessage.RemoveAt(0);
                messageCount--;
            }
        }

        private void Write(string message, LogType type)
        {
            logMessage.Add(new LOG()
            {
                message = message,
                stackTrace = "",
                type = type,
            });

            if (logMessage.Count == MAXLOG)
            {
                logMessage.RemoveAt(0);
                messageCount--;
            }
        }        

        private void Write(string message, LogType type, params object[] arg)
        {
            logMessage.Add(new LOG()
            {
                message = string.Format(message, arg),
                stackTrace = "",
                type = type,
            });

            if (logMessage.Count == MAXLOG)
            {
                logMessage.RemoveAt(0);
                messageCount--;
            }
        }

        private void Write(string message, string stacktrace, LogType type)
        {
            string temp;

            if (stacktrace != "")
            {
                temp = "<<STACKTRACE>>\n" + stacktrace;
                stacktrace = temp;
            }

            logMessage.Add(new LOG()
            {
                message = message,
                stackTrace = stacktrace,
                type = type,
            });

            if (logMessage.Count == MAXLOG)
            {
                logMessage.RemoveAt(0);
                messageCount--;
            }

        }

        public static void Log(string message) => GetInstance().Write(message);

        public static void Log(string message, LogType type) => GetInstance().Write(message, type);

        public static void Log(string message, LogType type, params object[] arg) => GetInstance().Write(message, type, arg);

        public static void HandleLog(string message, string stacktrace, LogType type) => GetInstance().Write(message, stacktrace, type);
    }
}