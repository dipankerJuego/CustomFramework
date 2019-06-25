namespace XRCustomFramework
{
    public static class XR_Enum
    {
        public enum Hand
        {
            LEFT,
            RIGHT
        }

        public enum InputType
        {
            None = 0,
            Bool = 1,
            Axis1D = 2,
            Axis2D = 3
        }

        public enum FeatureUsageButton
        {
            None,
            PrimaryButton, //[2/0]
            PrimaryTouch,
            SecondaryButton,
            SecondaryTouch,
            GripButton,
            TriggerButton,
            MenuButton,
            Primary2DAxisClick,
            Primary2DAxisTouch,
            Thumbrest
        }

        public enum FeatureUsageAxis
        {
            None,
            Trigger,
            Grip,
            IndexTouch,
            ThumbTouch,
            IndexFinger,
            MiddleFinger,
            RingFinger,
            PinkyFinger,
            CombinedTrigger
        }

        public enum FeatureUsage2DAxis
        {
            None,
            Primary2DAxis,
            Secondary2DAxis
        }

        public enum Device
        {
            None = 0,
            Vive = DefaultController.Vive,
            Oculus = DefaultController.Oculus,
            GearVR = DefaultController.GearVR,
            WMR = DefaultController.WMR,
            OpenVR_Full = DefaultController.OpenVR_Full,
            OpenVR_Oculus = DefaultController.OpenVR_Oculus,
            OpenVR_MWR = DefaultController.OpenVR_MWR,
            Daydream = DefaultController.Daydream
        }

        #region Editor

        public enum SDKType
        {
            None = 0,
            XR,
            WaveVR
        }

        #endregion

        #region Device Controller

        public enum DefaultController
        {
            None = 0,
            Vive = 1,
            Oculus = 2,
            GearVR = 3,
            WMR = 4,
            OpenVR_Full = 5,
            OpenVR_Oculus = 6,
            OpenVR_MWR = 7,
            Daydream = 8
        }

        #region HTC

        public enum HTC_Vive_Button
        {
            None = 0,
            Primary = FeatureUsageButton.PrimaryButton,
            GripPress = FeatureUsageButton.GripButton,
            TriggerPress = FeatureUsageButton.TriggerButton,
            StickOrPadPress = FeatureUsageButton.Primary2DAxisClick,
            StickOrPadTouch = FeatureUsageButton.Primary2DAxisTouch
        }

        public enum HTC_Vive_Axis
        {
            None = 0,
            Trigger = FeatureUsageAxis.Trigger,
            Grip = FeatureUsageAxis.Grip,
            CombinedTrigger = FeatureUsageAxis.CombinedTrigger
        }

        public enum HTC_Vive_2DAxis
        {
            None = 0,
            Trackpad = FeatureUsage2DAxis.Primary2DAxis
        }

        #endregion

        #region Oculus

        public enum Oculus_Button
        {
            None = 0,
            XA_Press = FeatureUsageButton.PrimaryButton,
            XA_Touch = FeatureUsageButton.PrimaryTouch,
            YB_Press = FeatureUsageButton.SecondaryButton,
            YB_Touch = FeatureUsageButton.SecondaryTouch,
            GripPress = FeatureUsageButton.GripButton,
            IndexTouch = FeatureUsageButton.TriggerButton,
            Start = FeatureUsageButton.MenuButton,
            ThumbstickClick = FeatureUsageButton.Primary2DAxisClick,
            ThumbstickTouch = FeatureUsageButton.Primary2DAxisTouch,
            ThumbRestTouch = FeatureUsageButton.Thumbrest
        }

        public enum Oculus_Axis
        {
            None = 0,
            Trigger = FeatureUsageAxis.Trigger,
            Grip = FeatureUsageAxis.Grip,
            IndexNearTouch = FeatureUsageAxis.IndexTouch,
            ThumbNearTouch = FeatureUsageAxis.ThumbTouch,
            CombinedTrigger = FeatureUsageAxis.CombinedTrigger
        }

        public enum Oculus_2DAxis
        {
            None = 0,
            Joystick = FeatureUsage2DAxis.Primary2DAxis
        }

        #endregion

        #region Gear_VR

        public enum GearVR_Button
        {
            None = 0,
            TriggerPress = FeatureUsageButton.TriggerButton,
            TouchpadClick = FeatureUsageButton.Primary2DAxisClick,
            TouchpadTouch = FeatureUsageButton.Primary2DAxisTouch
        }

        public enum GearVR_Axis
        {
            None = 0,
            Trigger = FeatureUsageAxis.Trigger,
            CombinedTrigger = FeatureUsageAxis.CombinedTrigger
        }

        public enum GearVR_2DAxis
        {
            None = 0,
            Joystick = FeatureUsage2DAxis.Primary2DAxis
        }

        #endregion

        #region Daydream

        public enum Daydream_Button
        {
            None = 0,
            App = FeatureUsageButton.PrimaryButton,
            GripPress = FeatureUsageButton.GripButton,
            TriggerPress = FeatureUsageButton.TriggerButton,
            TouchpadClick = FeatureUsageButton.Primary2DAxisClick,
            TouchpadTouch = FeatureUsageButton.Primary2DAxisTouch
        }

        public enum Daydream_Axis
        {
            None = 0,
            Trigger = FeatureUsageAxis.Trigger,
            Grip = FeatureUsageAxis.Grip,
        }

        public enum Daydream_2DAxis
        {
            None = 0,
            Touchpad = FeatureUsage2DAxis.Primary2DAxis
        }

        #endregion

        #region WMR

        public enum WMR_Button
        {
            None = 0,
            GripPress = FeatureUsageButton.GripButton,
            TriggerPress = FeatureUsageButton.TriggerButton,
            Menu = FeatureUsageButton.MenuButton,
            TouchpadClick = FeatureUsageButton.Primary2DAxisClick,
            TouchpadTouch = FeatureUsageButton.Primary2DAxisTouch,
            JoystickClick = FeatureUsageButton.Thumbrest
        }

        public enum WMR_Axis
        {
            None = 0,
            Trigger = FeatureUsageAxis.Trigger,
            Grip = FeatureUsageAxis.Grip,
            CombinedTrigger = FeatureUsageAxis.CombinedTrigger
        }

        public enum WMR_2DAxis
        {
            None = 0,
            Joystick = FeatureUsage2DAxis.Primary2DAxis,
            Touchpad = FeatureUsage2DAxis.Secondary2DAxis
        }

        #endregion

        #region OpenVRFull

        public enum OpenVRFull_Button
        {
            None = 0,
            Primary = FeatureUsageButton.PrimaryButton,
            Alternate = FeatureUsageButton.SecondaryButton,
            GripPress = FeatureUsageButton.GripButton,
            TriggerPress = FeatureUsageButton.TriggerButton,
            StickOrPadPress = FeatureUsageButton.Primary2DAxisClick,
            StickOrPadTouch = FeatureUsageButton.Primary2DAxisTouch
        }

        public enum OpenVRFull_Axis
        {
            None = 0,
            Trigger = FeatureUsageAxis.Trigger,
            Grip = FeatureUsageAxis.Grip,
            Index = FeatureUsageAxis.IndexFinger,
            Middle = FeatureUsageAxis.MiddleFinger,
            Ring = FeatureUsageAxis.RingFinger,
            Pinky = FeatureUsageAxis.PinkyFinger,
            CombinedTrigger = FeatureUsageAxis.CombinedTrigger
        }

        public enum OpenVRFull_2DAxis
        {
            None = 0,
            Trackpad = FeatureUsage2DAxis.Primary2DAxis
        }

        #endregion

        #region OpenVR_Oculus

        public enum OpenVR_Oculus_Button
        {
            None = 0,
            PrimaryYB = FeatureUsageButton.PrimaryButton,
            AlternateBA = FeatureUsageButton.SecondaryButton,
            GripPress = FeatureUsageButton.GripButton,
            TriggerPress = FeatureUsageButton.TriggerButton,
            StickOrPadPress = FeatureUsageButton.Primary2DAxisClick,
            StickOrPadTouch = FeatureUsageButton.Primary2DAxisTouch
        }

        public enum OpenVR_Oculus_Axis
        {
            None = 0,
            Trigger = FeatureUsageAxis.Trigger,
            Grip = FeatureUsageAxis.Grip,
            CombinedTrigger = FeatureUsageAxis.CombinedTrigger
        }

        public enum OpenVR_Oculus_2DAxis
        {
            None = 0,
            Joystick = FeatureUsage2DAxis.Primary2DAxis
        }

        #endregion

        #region OpenVR_WMR

        public enum OpenVR_WMR_Button
        {
            None = 0,
            Menu = FeatureUsageButton.PrimaryButton,
            Grip = FeatureUsageButton.GripButton,
            TriggerPress = FeatureUsageButton.TriggerButton,
            TouchpadClick = FeatureUsageButton.Primary2DAxisClick,
            TouchpadTouch = FeatureUsageButton.Primary2DAxisTouch
        }

        public enum OpenVR_WMR_Axis
        {
            None = 0,
            Trigger = FeatureUsageAxis.Trigger,
            Grip = FeatureUsageAxis.Grip
        }

        public enum OpenVR_WMR_2DAxis
        {
            None = 0,
            Joystick = FeatureUsage2DAxis.Primary2DAxis,
            Touchpad = FeatureUsage2DAxis.Secondary2DAxis
        }

        #endregion

        #endregion
    }
}
