using UnityEngine;
using System.Collections;
using System.IO;

namespace XRCustomFramework
{
    public static class XR_Utilities
    {
        public static void ResetTransform(this Transform transform, bool local = true)
        {
            if (local)
            {
                transform.localPosition = new Vector3(0, 0, 0);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.position = new Vector3(0, 0, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
                transform.localScale = new Vector3(1f, 1f, 1f);
        }

        #region IO

        public static bool IsPathDirectory(string path)
        {
            if (path == null) throw new System.ArgumentNullException("path");
            path = path.Trim();

            if (Directory.Exists(path))
                return true;

            if (File.Exists(path))
                return false;

            return string.IsNullOrWhiteSpace(Path.GetExtension(path));
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Debug.Log(string.Format(@"Copying {0}\{1}", target.FullName, fi.Name));
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        #endregion

        #region XR_ENUM Utilities

        public static T ToEnum<T>(this string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        public static string[] GetDefaultDevice()
        {
            return System.Enum.GetNames(typeof(XR_Enum.DefaultController));
        }

        public static string GetKeyName(int index, XR_Enum.DefaultController defaultController, XR_Enum.InputType inputType)
        {
            switch (defaultController)
            {
                case XR_Enum.DefaultController.Vive:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return ((XR_Enum.HTC_Vive_Button)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return ((XR_Enum.HTC_Vive_Axis)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return ((XR_Enum.HTC_Vive_2DAxis)index).ToString();
                        else
                            return "None";
                    }

                case XR_Enum.DefaultController.GearVR:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return ((XR_Enum.GearVR_Button)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return ((XR_Enum.GearVR_Axis)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return ((XR_Enum.GearVR_2DAxis)index).ToString();
                        else
                            return "None";
                    }

                case XR_Enum.DefaultController.Oculus:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return ((XR_Enum.Oculus_Button)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return ((XR_Enum.Oculus_Axis)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return ((XR_Enum.Oculus_2DAxis)index).ToString();
                        else
                            return "None";
                    }

                case XR_Enum.DefaultController.WMR:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return ((XR_Enum.WMR_Button)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return ((XR_Enum.WMR_Axis)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return ((XR_Enum.WMR_2DAxis)index).ToString();
                        else
                            return "None";
                    }

                case XR_Enum.DefaultController.OpenVR_Full:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return ((XR_Enum.OpenVRFull_Button)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return ((XR_Enum.OpenVRFull_Axis)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return ((XR_Enum.OpenVRFull_2DAxis)index).ToString();
                        else
                            return "None";
                    }

                case XR_Enum.DefaultController.OpenVR_Oculus:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return ((XR_Enum.OpenVR_Oculus_Button)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return ((XR_Enum.OpenVR_Oculus_Axis)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return ((XR_Enum.OpenVR_Oculus_2DAxis)index).ToString();
                        else
                            return "None";
                    }

                case XR_Enum.DefaultController.OpenVR_MWR:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return ((XR_Enum.OpenVR_WMR_Button)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return ((XR_Enum.OpenVR_WMR_Axis)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return ((XR_Enum.OpenVR_WMR_2DAxis)index).ToString();
                        else
                            return "None";
                    }

                case XR_Enum.DefaultController.Daydream:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return ((XR_Enum.Daydream_Button)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return ((XR_Enum.Daydream_Axis)index).ToString();
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return ((XR_Enum.Daydream_2DAxis)index).ToString();
                        else
                            return "None";
                    }

                default: return "None";
            }
        }

        public static string[] GetDeviceControllerKeysName(XR_Enum.DefaultController defaultController, XR_Enum.InputType inputType)
        {
            switch (defaultController)
            {
                case XR_Enum.DefaultController.Vive:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetNames(typeof(XR_Enum.HTC_Vive_Button));
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetNames(typeof(XR_Enum.HTC_Vive_Axis));
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetNames(typeof(XR_Enum.HTC_Vive_2DAxis));
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.GearVR:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetNames(typeof(XR_Enum.GearVR_Button));
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetNames(typeof(XR_Enum.GearVR_Axis));
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetNames(typeof(XR_Enum.GearVR_2DAxis));
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.Oculus:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetNames(typeof(XR_Enum.Oculus_Button));
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetNames(typeof(XR_Enum.Oculus_Axis));
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetNames(typeof(XR_Enum.Oculus_2DAxis));
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.WMR:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetNames(typeof(XR_Enum.WMR_Button));
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetNames(typeof(XR_Enum.WMR_Axis));
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetNames(typeof(XR_Enum.WMR_2DAxis));
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.OpenVR_Full:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetNames(typeof(XR_Enum.OpenVRFull_Button));
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetNames(typeof(XR_Enum.OpenVRFull_Axis));
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetNames(typeof(XR_Enum.OpenVRFull_2DAxis));
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.OpenVR_Oculus:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetNames(typeof(XR_Enum.OpenVR_Oculus_Button));
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetNames(typeof(XR_Enum.OpenVR_Oculus_Axis));
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetNames(typeof(XR_Enum.OpenVR_Oculus_2DAxis));
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.OpenVR_MWR:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetNames(typeof(XR_Enum.OpenVR_WMR_Button));
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetNames(typeof(XR_Enum.OpenVR_WMR_Axis));
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetNames(typeof(XR_Enum.OpenVR_WMR_2DAxis));
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.Daydream:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetNames(typeof(XR_Enum.Daydream_Button));
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetNames(typeof(XR_Enum.Daydream_Axis));
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetNames(typeof(XR_Enum.Daydream_2DAxis));
                        else
                            return null;
                    }

                default: return null;
            }
        }

        public static int[] GetDeviceControllerKeysValues(XR_Enum.DefaultController defaultController, XR_Enum.InputType inputType)
        {
            switch (defaultController)
            {
                case XR_Enum.DefaultController.Vive:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetValues(typeof(XR_Enum.HTC_Vive_Button)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetValues(typeof(XR_Enum.HTC_Vive_Axis)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetValues(typeof(XR_Enum.HTC_Vive_2DAxis)) as int[];
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.GearVR:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetValues(typeof(XR_Enum.GearVR_Button)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetValues(typeof(XR_Enum.GearVR_Axis)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetValues(typeof(XR_Enum.GearVR_2DAxis)) as int[];
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.Oculus:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetValues(typeof(XR_Enum.Oculus_Button)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetValues(typeof(XR_Enum.Oculus_Axis)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetValues(typeof(XR_Enum.Oculus_2DAxis)) as int[];
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.WMR:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetValues(typeof(XR_Enum.WMR_Button)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetValues(typeof(XR_Enum.WMR_Axis)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetValues(typeof(XR_Enum.WMR_2DAxis)) as int[];
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.OpenVR_Full:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetValues(typeof(XR_Enum.OpenVRFull_Button)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetValues(typeof(XR_Enum.OpenVRFull_Axis)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetValues(typeof(XR_Enum.OpenVRFull_2DAxis)) as int[];
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.OpenVR_Oculus:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetValues(typeof(XR_Enum.OpenVR_Oculus_Button)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetValues(typeof(XR_Enum.OpenVR_Oculus_Axis)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetValues(typeof(XR_Enum.OpenVR_Oculus_2DAxis)) as int[];
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.OpenVR_MWR:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetValues(typeof(XR_Enum.OpenVR_WMR_Button)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetValues(typeof(XR_Enum.OpenVR_WMR_Axis)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetValues(typeof(XR_Enum.OpenVR_WMR_2DAxis)) as int[];
                        else
                            return null;
                    }

                case XR_Enum.DefaultController.Daydream:
                    {
                        if (inputType == XR_Enum.InputType.Bool)
                            return System.Enum.GetValues(typeof(XR_Enum.Daydream_Button)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis1D)
                            return System.Enum.GetValues(typeof(XR_Enum.Daydream_Axis)) as int[];
                        else if (inputType == XR_Enum.InputType.Axis2D)
                            return System.Enum.GetValues(typeof(XR_Enum.Daydream_2DAxis)) as int[];
                        else
                            return null;
                    }

                default: return null;
            }
        }

        #endregion
    }
}
