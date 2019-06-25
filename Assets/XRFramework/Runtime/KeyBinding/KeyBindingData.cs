using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRCustomFramework
{
    [CreateAssetMenu(fileName = "Device_Name_Controller", menuName = "XR Framework/Add Controller")]
    public class KeyBindingData : ScriptableObject
    {
        public const string BUTTON_ID_NAME = "OnButton_";
        public const string AXIS1D_ID_NAME = "OnAxis1D_";
        public const string AXIS2D_ID_NAME = "OnAxis2D_";

        public enum HeaderState
        {
            ControllersState,
            ControllerButtonsState
        }

        [HideInInspector] public bool foldControllerList;
        [HideInInspector] public HeaderState currentTabState;
        [HideInInspector] public int currentControllerID;
        [HideInInspector] public int currentControllerIndex;

        public List<ControllerStruct> controllerList;

        public void AddNewController()
        {
            ControllerStruct controllerStruct = new ControllerStruct();
            controllerStruct.Name = "Controller Device Name";
            controllerStruct.TotalButton = 0;
            controllerStruct.TotalAxis1DButton = 0;
            controllerStruct.TotalAxis2DButton = 0;
            controllerStruct.hand = XR_Enum.Hand.RIGHT;
            controllerStruct.id = CreateDeviceID();

            controllerList.Add(controllerStruct);
        }

        int CreateDeviceID()
        {
            int new_ID = 1;
            if(controllerList != null)
            {
                while (controllerList.Exists((obj) => obj.id == new_ID))
                    new_ID++;
            }

            return new_ID;
        }

        public ControllerStruct GetController(int id)
        {
            for (int i = 0; i < controllerList.Count; i++)
            {
                if (controllerList[i].id == id)
                    return controllerList[i];
            }

            return new ControllerStruct();
        }

        public ControllerStruct GetControllerByIndex(int index)
        {
            for (int i = 0; i < controllerList.Count; i++)
            {
                if (i == index)
                    return controllerList[i];
            }

            return new ControllerStruct();
        }
    }

    [Serializable]
    public struct ControllerStruct
    {
        public string Name;
        public int TotalButton;
        public int TotalAxis1DButton;
        public int TotalAxis2DButton;
        public XR_Enum.Hand hand;
        public int deviceType;
        [HideInInspector] public int id;

        public List<KeyMap> keyMappingList;

        public void UpdateData(string deviceName, int totalButton = 0, int totalAxis = 0, int total2DAxis = 0)
        {
            this.Name = deviceName;
            this.TotalButton = totalButton;
            this.TotalAxis1DButton = totalAxis;
            this.TotalAxis2DButton = total2DAxis;

            if(deviceName == "None")
            {
                if(keyMappingList != null)
                    keyMappingList.Clear();
            }
            else
            {
                CreateKeyMap(XR_Enum.InputType.Bool, TotalButton);
                CreateKeyMap(XR_Enum.InputType.Axis1D, TotalAxis1DButton);
                CreateKeyMap(XR_Enum.InputType.Axis2D, TotalAxis2DButton);
            }
        }

        void CreateKeyMap(XR_Enum.InputType inputType, int totalInput)
        {
            int[] enumValues = XR_Utilities.GetDeviceControllerKeysValues((XR_Enum.DefaultController)deviceType, inputType);
            for (int i = 0; i < totalInput; i++)
            {
                KeyMap keyMap = GetKeyMap(enumValues[i], inputType);
            }
        }

        public List<KeyMap> GetKeyMapList(XR_Enum.InputType inputType)
        {
            List<KeyMap> mapList = new List<KeyMap>();
            foreach (var item in keyMappingList)
                if(item.inputType == inputType)
                    mapList.Add(item);

            return mapList;
        }

        public KeyMap GetKeyMap(int buttonID, XR_Enum.InputType inputType)
        {
            if (keyMappingList == null)
                keyMappingList = new List<KeyMap>();

            KeyMap keyMap = keyMappingList.Find((obj) => (obj.inputType == inputType && obj.mainInput == buttonID));
            if(keyMap == null || keyMap.KeyID == null)
            {
                keyMap = new KeyMap(buttonID, inputType);
                keyMappingList.Add(keyMap);
            }

            return keyMap;
        }

        public void UpdateKeyMap(KeyMap keyMap)
        {
            for (int i = 0; i < keyMappingList.Count; i++)
            {
                if(keyMappingList[i].Equals(keyMap))
                {
                    keyMappingList[i] = keyMap;
                }
            }
        }
    }

    [Serializable]
    public struct KeyMap : IEquatable<KeyMap>
    {
        public string KeyID;
        public int enumType;
        public XR_Enum.InputType inputType;
        public int mainInput, mapedInput;

        public KeyMap(int buttonID, XR_Enum.InputType inputType)
        {
            this.mainInput = buttonID;
            this.mapedInput = buttonID;
            this.enumType = 0;
            this.inputType = inputType;
            this.KeyID = "";
        }

        public System.Type type
        {
            get
            {
                switch (this.inputType)
                {
                    case XR_Enum.InputType.Bool: return typeof(bool);
                    case XR_Enum.InputType.Axis1D: return typeof(float);
                    case XR_Enum.InputType.Axis2D: return typeof(Vector2);
                    default:
                        throw new InvalidCastException("No valid managed type for None native types.");
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is KeyMap))
                return false;
            return this.Equals((KeyMap)obj);
        }

        public bool Equals(KeyMap other)
        {
            return this.mainInput == other.mainInput && this.type == other.type;
        }

        public static bool operator == (KeyMap a, KeyMap b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(KeyMap a, KeyMap b)
        {
            return !(a == b);
        }
    }
}
