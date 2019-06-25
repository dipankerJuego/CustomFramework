using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace XRCustomFramework
{
    public partial class KeyBindingWindow : EditorWindow
    {
        #region [Fields]

        [SerializeField] private Texture2D m_highlightTexture, m_hierarchyLabelTexture;
        [SerializeField] private float m_hierarchyPanelWidth = MENU_WIDTH * 2;
        [SerializeField] private Vector2 m_hierarchyScrollPos = Vector2.zero;
        [SerializeField] private Vector2 m_mainPanelScrollPos = Vector2.zero;

        private KeyBindingData m_keyBindingData;

        private GUIStyle m_whiteLabel;
        private GUIStyle m_whiteFoldout;
        private GUIStyle m_warningLabel;
        private GUIStyle m_toggleButtonStyleNormal = null;
        private GUIStyle m_toggleButtonStyleToggled = null;

        private HierarchyTabState m_currentHierarchyTabState;
        private bool m_isDisposed = false;
        private bool m_isResizingHierarchy = false;

        #endregion

        #region [Startup]

        private void OnEnable()
        {
            IsOpen = true;

            EditorApplication.playModeStateChanged += HandlePlayModeChanged;
            m_isDisposed = false;
            if (m_keyBindingData == null)
            {
                m_keyBindingData = (KeyBindingData)Resources.Load(ConstantVar.ResourcesPath.XR_CONTROLLER_DATA);
            }
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {
            if (!m_isDisposed)
            {
                IsOpen = false;
                Texture2D.DestroyImmediate(m_highlightTexture);
                m_highlightTexture = null;
                Texture2D.DestroyImmediate(m_hierarchyLabelTexture);
                m_hierarchyLabelTexture = null;
                EditorApplication.playModeStateChanged -= HandlePlayModeChanged;
                m_isDisposed = true;
            }
        }

        #endregion

        #region [OnGUI]

        private void OnGUI()
        {
            EnsureGUIStyles();

            if (m_keyBindingData == null)
            {
                m_keyBindingData = (KeyBindingData)Resources.Load(ConstantVar.ResourcesPath.XR_CONTROLLER_DATA);
                return;
            }

            Undo.RecordObject(m_keyBindingData, "KeyBindingData");

            if (m_currentHierarchyTabState != HierarchyTabState.None)
                DrawMainPanel();

            UpdateHierarchyPanelWidth();
            DrawHierarchyPanel();
            DrawTopBar();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(m_keyBindingData);
            }
        }

        private void UpdateHierarchyPanelWidth()
        {
            float cursorRectWidth = m_isResizingHierarchy ? MAX_CURSOR_RECT_WIDTH : MIN_CURSOR_RECT_WIDTH;
            Rect cursorRect = new Rect(m_hierarchyPanelWidth - cursorRectWidth / 2, TOOLBAR_HEIGHT, cursorRectWidth,
                                        position.height - TOOLBAR_HEIGHT);
            Rect resizeRect = new Rect(m_hierarchyPanelWidth - MIN_CURSOR_RECT_WIDTH / 2, 0.0f,
                                        MIN_CURSOR_RECT_WIDTH, position.height);

            EditorGUIUtility.AddCursorRect(cursorRect, MouseCursor.ResizeHorizontal);
            switch (Event.current.type)
            {
                case EventType.MouseDown:
                    if (Event.current.button == 0 && resizeRect.Contains(Event.current.mousePosition))
                    {
                        m_isResizingHierarchy = true;
                        Event.current.Use();
                    }
                    break;
                case EventType.MouseUp:
                    if (Event.current.button == 0 && m_isResizingHierarchy)
                    {
                        m_isResizingHierarchy = false;
                        Event.current.Use();
                    }
                    break;
                case EventType.MouseDrag:
                    if (m_isResizingHierarchy)
                    {
                        m_hierarchyPanelWidth = Mathf.Clamp(m_hierarchyPanelWidth + Event.current.delta.x,
                                                         MIN_HIERARCHY_PANEL_WIDTH, position.width / 2);
                        Event.current.Use();
                        Repaint();
                    }
                    break;
                default:
                    break;
            }
        }

        private void DrawTopBar()
        {
            Rect screenRect = new Rect(0.0f, 0.0f, position.width, TOOLBAR_HEIGHT);
            //GUI.BeginGroup(screenRect);
            EditorGUI.LabelField(screenRect, "KEY MAPPING AND KEY BINDING", EditorStyles.toolbarButton);
            //GUI.EndGroup();
        }

        private void DrawHierarchyPanel()
        {
            Rect screenRect = new Rect(0.0f, TOOLBAR_HEIGHT - 5.0f, m_hierarchyPanelWidth, position.height - TOOLBAR_HEIGHT + 10.0f);
            Rect scrollView = new Rect(screenRect.x, screenRect.y + 5.0f, screenRect.width, position.height - screenRect.y);
            Rect viewRect = new Rect(0.0f, 0.0f, scrollView.width, HIERARCHY_ITEM_HEIGHT);

            GUI.Box(screenRect, "");
            m_hierarchyScrollPos = GUI.BeginScrollView(scrollView, m_hierarchyScrollPos, viewRect);

            if (m_toggleButtonStyleNormal == null)
            {
                m_toggleButtonStyleNormal = "Button";
                m_toggleButtonStyleToggled = new GUIStyle(m_toggleButtonStyleNormal);
                m_toggleButtonStyleToggled.normal.background = m_toggleButtonStyleToggled.active.background;
            }

            Rect csRect1 = new Rect(0.0f, TOOLBAR_HEIGHT, screenRect.width, screenRect.height);

            GUI.BeginGroup(csRect1);

            if (GUILayout.Button("Controllers", 
                m_currentHierarchyTabState == HierarchyTabState.ControllerState ? m_toggleButtonStyleToggled : m_toggleButtonStyleNormal, 
                GUILayout.MinHeight(TOGGLE_HEIGHT), GUILayout.MaxWidth(m_hierarchyPanelWidth - 5)))
            {
                m_currentHierarchyTabState = HierarchyTabState.ControllerState;
            }

            if (GUILayout.Button("Key Binding", 
                m_currentHierarchyTabState == HierarchyTabState.KeyBindingState ? m_toggleButtonStyleToggled : m_toggleButtonStyleNormal, 
                GUILayout.MinHeight(TOGGLE_HEIGHT), GUILayout.MaxWidth(m_hierarchyPanelWidth - 5)))
            {
                m_currentHierarchyTabState = HierarchyTabState.KeyBindingState;
            }
            GUI.EndGroup();
            GUI.EndScrollView();
        }

        private void DrawMainPanel()
        {
            Rect position = new Rect(m_hierarchyPanelWidth + 5, TOOLBAR_HEIGHT + 5,
                                        this.position.width - m_hierarchyPanelWidth,
                                        this.position.height - TOOLBAR_HEIGHT);

            Rect viewRect = new Rect(-5.0f, -5.0f, position.width - 10.0f, position.height - 10.0f);
            float contentWidth = position.width - 10.0f;

            if (viewRect.width < MIN_MAIN_PANEL_WIDTH)
            {
                viewRect.width = MIN_MAIN_PANEL_WIDTH;
                contentWidth = viewRect.width - 10.0f;
            }

            if ((position.height - 10.0f) > position.height)
            {
                viewRect.width -= SCROLL_BAR_WIDTH;
                contentWidth -= SCROLL_BAR_WIDTH;
            }

            //BeginScrollView
            m_mainPanelScrollPos = GUI.BeginScrollView(position, m_mainPanelScrollPos, viewRect);

            GUILayout.BeginArea(viewRect);

            switch(m_currentHierarchyTabState)
            {
                case HierarchyTabState.ControllerState:
                    XRController();
                    break;
                case HierarchyTabState.KeyBindingState:
                    KeyBinding();
                    break;
            }

            GUILayout.EndArea();

            GUI.EndScrollView();
        }

        #endregion

        #region [Controllers]

        void XRController()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(250));
            if (GUILayout.Button("Add XR Default Controller", GUILayout.MaxWidth(150), GUILayout.MaxHeight(20)))
            {
                m_keyBindingData.AddNewController();
            }
            EditorGUILayout.EndHorizontal();

            if (m_keyBindingData.controllerList.Count > 0)
            {
                EditorGUILayout.Space();

                if (m_keyBindingData.foldControllerList == false)
                    if (GUILayout.Button("Show", EditorStyles.miniButtonRight, GUILayout.Width(50)))
                        m_keyBindingData.foldControllerList = true;

                if (m_keyBindingData.foldControllerList)
                    if (GUILayout.Button("Hide", EditorStyles.miniButtonRight, GUILayout.Width(50)))
                        m_keyBindingData.foldControllerList = false;

                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
                EditorGUILayout.LabelField(" ", GUILayout.Width(20));
                EditorGUILayout.LabelField("ID", GUILayout.Width(20));
                EditorGUILayout.LabelField("Controller", GUILayout.Width(150));
                EditorGUILayout.LabelField("Total Button", GUILayout.Width(80));
                EditorGUILayout.LabelField("Total Axis1D", GUILayout.Width(80));
                EditorGUILayout.LabelField("Total Axis2D", GUILayout.Width(80));
                EditorGUILayout.EndHorizontal();

                if (m_keyBindingData.foldControllerList)
                {
                    for (int i = 0; i < m_keyBindingData.controllerList.Count; i++)
                    {
                        ControllerStruct controller = m_keyBindingData.controllerList[i];

                        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(280));
                        EditorGUILayout.LabelField((i+1).ToString(), GUILayout.Width(20));
                        EditorGUILayout.LabelField(controller.id.ToString(), GUILayout.Width(20));

                        controller.deviceType = EditorGUILayout.Popup(controller.deviceType, XR_Utilities.GetDefaultDevice(), GUILayout.Width(150));

                        int maxButton = 0, maxAxis = 0, max2DAxis = 0;
                        if (controller.deviceType == 0)
                        {
                            controller.UpdateData("None");
                        }
                        else
                        {
                            if(XR_Utilities.GetDeviceControllerKeysName((XR_Enum.DefaultController)controller.deviceType, XR_Enum.InputType.Bool) != null)
                            {
                                maxButton = XR_Utilities.GetDeviceControllerKeysName((XR_Enum.DefaultController)controller.deviceType, XR_Enum.InputType.Bool).Length - 1;
                                if (controller.TotalButton == 0 || controller.TotalButton > maxButton)
                                    controller.TotalButton = maxButton;
                            }
                            if (XR_Utilities.GetDeviceControllerKeysName((XR_Enum.DefaultController)controller.deviceType, XR_Enum.InputType.Axis1D) != null)
                            {
                                maxAxis = XR_Utilities.GetDeviceControllerKeysName((XR_Enum.DefaultController)controller.deviceType, XR_Enum.InputType.Axis1D).Length - 1;
                                if (controller.TotalAxis1DButton == 0 || controller.TotalAxis1DButton > maxAxis)
                                    controller.TotalAxis1DButton = maxAxis;
                            }
                            if (XR_Utilities.GetDeviceControllerKeysName((XR_Enum.DefaultController)controller.deviceType, XR_Enum.InputType.Axis2D) != null)
                            {
                                max2DAxis = XR_Utilities.GetDeviceControllerKeysName((XR_Enum.DefaultController)controller.deviceType, XR_Enum.InputType.Axis2D).Length - 1;
                                if (controller.TotalAxis2DButton == 0 || controller.TotalAxis2DButton > max2DAxis)
                                    controller.TotalAxis2DButton = max2DAxis;
                            }

                            string deviceName = ((XR_Enum.DefaultController)controller.deviceType).ToString();
                            controller.UpdateData(deviceName, controller.TotalButton, controller.TotalAxis1DButton, controller.TotalAxis2DButton);
                        }


                        EditorGUILayout.LabelField(m_keyBindingData.controllerList[i].TotalButton.ToString(), EditorStyles.boldLabel, GUILayout.Width(30));
                        if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.Width(20)))
                        {
                            if(controller.TotalButton < maxButton)
                                controller.TotalButton++;
                        }
                        if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(20)))
                        {
                            if (controller.TotalButton > 0)
                                controller.TotalButton--;
                        }

                        EditorGUILayout.LabelField(m_keyBindingData.controllerList[i].TotalAxis1DButton.ToString(), EditorStyles.boldLabel, GUILayout.Width(30));
                        if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.Width(20)))
                        {
                            if (controller.TotalButton < maxAxis)
                                controller.TotalAxis1DButton++;
                        }
                        if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(20)))
                        {
                            if (controller.TotalAxis1DButton > 0)
                                controller.TotalAxis1DButton--;
                        }

                        EditorGUILayout.LabelField(m_keyBindingData.controllerList[i].TotalAxis2DButton.ToString(), EditorStyles.boldLabel, GUILayout.Width(30));
                        if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.Width(20)))
                        {
                            if (controller.TotalButton < max2DAxis)
                                controller.TotalAxis2DButton++;
                        }
                        if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(20)))
                        {
                            if (controller.TotalAxis2DButton > 0)
                                controller.TotalAxis2DButton--;
                        }

                        controller.hand = (XR_Enum.Hand)EditorGUILayout.EnumPopup(controller.hand, GUILayout.Width(50));


                        if (GUILayout.Button(" Delete ", EditorStyles.miniButtonMid, GUILayout.Width(40)))
                        {
                            m_keyBindingData.controllerList.RemoveAt(i);
                            break;
                        }

                        if (m_keyBindingData.controllerList.Count > 0)
                            m_keyBindingData.controllerList[i] = controller;

                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }

        #endregion

        #region [Key Binding]

        void KeyBinding()
        {
            EditorGUILayout.Space();

            if (m_keyBindingData.controllerList.Count > 0)
            {
                if (m_keyBindingData.controllerList.Count > 0)
                {
                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));

                    if (GUILayout.Button("<==", GUILayout.Width(50)))
                    {
                        if (m_keyBindingData.currentControllerIndex > 0)
                            m_keyBindingData.currentControllerIndex--;
                        else
                            m_keyBindingData.currentControllerIndex = m_keyBindingData.controllerList.Count - 1;
                    }

                    GUILayout.FlexibleSpace();

                    List<string> deviceName = new List<string>();
                    List<int> deviceIndex = new List<int>();
                    for (int i = 0; i < m_keyBindingData.controllerList.Count; i++)
                    {
                        deviceName.Add(m_keyBindingData.controllerList[i].Name + " " + m_keyBindingData.controllerList[i].hand);
                        deviceIndex.Add(i);
                    }

                    if(deviceName.Count > 0)
                    {
                        m_keyBindingData.currentControllerIndex = 
                            EditorGUILayout.IntPopup(m_keyBindingData.currentControllerIndex, deviceName.ToArray(), deviceIndex.ToArray(), GUILayout.MinWidth(10));
                    }
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("==>", GUILayout.Width(50)))
                    {
                        if (m_keyBindingData.currentControllerIndex < m_keyBindingData.controllerList.Count - 1)
                            m_keyBindingData.currentControllerIndex++;
                        else
                            m_keyBindingData.currentControllerIndex = 0;
                    }

                    EditorGUILayout.EndHorizontal();

                    ControllerStruct controller = m_keyBindingData.GetControllerByIndex(m_keyBindingData.currentControllerIndex);

                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
                    EditorGUILayout.LabelField("Name: " + controller.Name.ToString(), EditorStyles.boldLabel, GUILayout.Width(200));
                    EditorGUILayout.LabelField("Hand: " + controller.hand.ToString(), EditorStyles.boldLabel, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    if (controller.TotalButton > 0)
                    {
                        //EditorGUILayout.LabelField("Button Press/Touch", EditorStyles.boldLabel, GUILayout.Width(250));
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
                        EditorGUILayout.LabelField("Button Name", EditorStyles.boldLabel, GUILayout.Width(150));
                        EditorGUILayout.LabelField("Key Mapping", EditorStyles.boldLabel, GUILayout.Width(150));
                        EditorGUILayout.EndHorizontal();

                        XR_Enum.InputType inputType = XR_Enum.InputType.Bool;
                        string[] enumNames = XR_Utilities.GetDeviceControllerKeysName((XR_Enum.DefaultController)controller.deviceType, inputType);
                        int[] enumValues = XR_Utilities.GetDeviceControllerKeysValues((XR_Enum.DefaultController)controller.deviceType, inputType);
                        if (enumValues != null)
                        {
                            for (int i = 1; i <= controller.TotalButton; i++)
                            {
                                KeyMap keyMap = controller.GetKeyMap(enumValues[i], inputType);
                                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
                                string featureUsageName = ((XR_Enum.FeatureUsageButton)enumValues[i]).ToString();
                                EditorGUILayout.LabelField(featureUsageName, GUILayout.Width(150));
                                //keyMap.enumType = EditorGUILayout.Popup(keyMap.enumType, GetButtonsEnum, GUILayout.Width(100));
                                keyMap.mapedInput = EditorGUILayout.IntPopup(keyMap.mapedInput, enumNames, enumValues, GUILayout.Width(100));
                                controller.UpdateKeyMap(keyMap);
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                    }

                    if (controller.TotalAxis1DButton > 0)
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
                        EditorGUILayout.LabelField("Axis1D Name", EditorStyles.boldLabel, GUILayout.Width(150));
                        EditorGUILayout.LabelField("Key Mapping", EditorStyles.boldLabel, GUILayout.Width(150));
                        EditorGUILayout.EndHorizontal();

                        XR_Enum.InputType inputType = XR_Enum.InputType.Axis1D;
                        string[] enumNames = XR_Utilities.GetDeviceControllerKeysName((XR_Enum.DefaultController)controller.deviceType, inputType);
                        int[] enumValues = XR_Utilities.GetDeviceControllerKeysValues((XR_Enum.DefaultController)controller.deviceType, inputType);
                        if (enumValues != null)
                        {
                            for (int i = 1; i <= controller.TotalAxis1DButton; i++)
                            {
                                KeyMap keyMap = controller.GetKeyMap(enumValues[i], inputType);
                                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
                                string featureUsageName = ((XR_Enum.FeatureUsageAxis)enumValues[i]).ToString();
                                EditorGUILayout.LabelField(featureUsageName, GUILayout.Width(150));
                                keyMap.mapedInput = EditorGUILayout.IntPopup(keyMap.mapedInput, enumNames, enumValues, GUILayout.Width(100));
                                controller.UpdateKeyMap(keyMap);
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                    }

                    if (controller.TotalAxis2DButton > 0)
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
                        EditorGUILayout.LabelField("Axis2D Name", EditorStyles.boldLabel, GUILayout.Width(150));
                        EditorGUILayout.LabelField("Key Mapping", EditorStyles.boldLabel, GUILayout.Width(150));
                        EditorGUILayout.EndHorizontal();

                        XR_Enum.InputType inputType = XR_Enum.InputType.Axis2D;
                        string[] enumNames = XR_Utilities.GetDeviceControllerKeysName((XR_Enum.DefaultController)controller.deviceType, inputType);
                        int[] enumValues = XR_Utilities.GetDeviceControllerKeysValues((XR_Enum.DefaultController)controller.deviceType, inputType);
                        if (enumValues != null)
                        {
                            for (int i = 1; i <= controller.TotalAxis2DButton; i++)
                            {
                                KeyMap keyMap = controller.GetKeyMap(enumValues[i], inputType);
                                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
                                string featureUsageName = ((XR_Enum.FeatureUsage2DAxis)enumValues[i]).ToString();
                                EditorGUILayout.LabelField(featureUsageName, GUILayout.Width(150));
                                keyMap.mapedInput = EditorGUILayout.IntPopup(keyMap.mapedInput, enumNames, enumValues, GUILayout.Width(100));
                                controller.UpdateKeyMap(keyMap);
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                    }
                }
            }

            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(350));
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region [Utility]

        private void CreateHighlightTexture()
        {
            m_highlightTexture = new Texture2D(1, 1);
            m_highlightTexture.SetPixel(0, 0, HIGHLIGHT_COLOR);
            m_highlightTexture.Apply();
        }

        private void CreateHierarchyLabelTexture()
        {
            m_hierarchyLabelTexture = new Texture2D(1, 1);
            m_hierarchyLabelTexture.SetPixel(0, 0, HIERARCHY_LABEL_COLOR);
            m_hierarchyLabelTexture.Apply();
        }

        private void EnsureGUIStyles()
        {
            if (m_highlightTexture == null)
            {
                CreateHighlightTexture();
            }

            if (m_hierarchyLabelTexture == null)
            {
                CreateHierarchyLabelTexture();
            }

            if (m_whiteLabel == null)
            {
                m_whiteLabel = new GUIStyle(EditorStyles.label);
                m_whiteLabel.normal.textColor = Color.white;
            }
            if (m_whiteFoldout == null)
            {
                m_whiteFoldout = new GUIStyle(EditorStyles.foldout);
                m_whiteFoldout.normal.textColor = Color.white;
                m_whiteFoldout.onNormal.textColor = Color.white;
                m_whiteFoldout.active.textColor = Color.white;
                m_whiteFoldout.onActive.textColor = Color.white;
                m_whiteFoldout.focused.textColor = Color.white;
                m_whiteFoldout.onFocused.textColor = Color.white;
            }
            if (m_warningLabel == null)
            {
                m_warningLabel = new GUIStyle(EditorStyles.largeLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Bold,
                    fontSize = 14
                };
            }
        }

        private void HandlePlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode || state == PlayModeStateChange.EnteredPlayMode)
            {

            }
        }

        #endregion

        #region [Static Interface]
        public static bool IsOpen { get; private set; }

        [MenuItem("XR Framework/Key Bindings", false, 0)]
        public static void OpenWindow()
        {
            if (!IsOpen)
            {
                var window = EditorWindow.GetWindow<KeyBindingWindow>("XR KEY BINDING EDITOR");
                window.minSize = new Vector2(MIN_WINDOW_WIDTH, MIN_WINDOW_HEIGHT);
            }
        }

        public static void CloseWindow()
        {
            if (IsOpen)
            {
                var window = EditorWindow.GetWindow<KeyBindingWindow>("XR KEY BINDING EDITOR");
                window.Close();
            }
        }

        #endregion

    }
}
