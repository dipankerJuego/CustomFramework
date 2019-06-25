using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRCustomFramework
{
    public static class ConstantVar
    {
        public static class DeviceName
        {
            //XRSettings.loadedDeviceName:
            //Onplus6T      cardboard
            //GearVR        Oculus    
            //HTC_VIVE      OpenVR
            //Rift          OpenVR

            //Model Name
            //Onplus6T      Google. Inc. Default - Cardboard
            //GearVR        Samsung S8
            //HTC_VIVE      VIVE MV               
            //Rift          Oculus Rift CV1

            //Device Name
            //Oneplus6T     Daydream HMD
            //GearVR        Oculus HMD
            //HTC_VIVE      VIVE MV
            //Rift          Oculus Rift CV1

            //public const string HTC_Vive = "Vive MV";
            //public const string GearVR = "Oculus HMD";
            //public const string Samsung_Mobile = "Samsung";
        }

        public static class ResourcesPath
        {
            public const string ASSETS_REFERENCE = "Assets_Reference";
            public const string BUILD_SETTINGS = "Build_Settings_Data";
            public const string XR_CONTROLLER_DATA = "XR_Controller_Data";
        }
    }
}
