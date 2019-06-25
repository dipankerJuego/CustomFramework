using System.Collections;
using UnityEditor;
using UnityEngine;

namespace XRCustomFramework
{
    public partial class KeyBindingWindow : EditorWindow
    {
        private static readonly Color32 HIGHLIGHT_COLOR = new Color32(62, 125, 231, 200);
        private static readonly Color32 HIERARCHY_LABEL_COLOR = new Color32(250, 250, 250, 150);

        private const float MIN_WINDOW_WIDTH = 600.0f;
        private const float MIN_WINDOW_HEIGHT = 200.0f;
        private const float MENU_WIDTH = 100.0f;
        private const float MIN_HIERARCHY_PANEL_WIDTH = 150.0f;
        private const float MIN_CURSOR_RECT_WIDTH = 10.0f;
        private const float MAX_CURSOR_RECT_WIDTH = 50.0f;
        private const float TOOLBAR_HEIGHT = 18.0f;
        private const float HIERARCHY_ITEM_HEIGHT = 18.0f;
        private const float TOGGLE_HEIGHT = 35.0f;
        private const float SCROLL_BAR_WIDTH = 15.0f;
        private const float MIN_MAIN_PANEL_WIDTH = 300.0f;
    
        private enum HierarchyTabState
        {
            None,
            ControllerState,
            KeyBindingState
        }
    }
}
