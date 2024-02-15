/*************************************************************
 *
 *         █▀▀ ▄▀▄ █   █   █▀▀ █▄ █ █▀▀ ▀█▀ █▄ █ █▀▀ 
 *         █▀  █▀█ █▄▄ █▄▄ ██▄ █ ▀█ █▄█ ▄█▄ █ ▀█ ██▄
 *
 * Author: Natalie Soltis
 * Date: 10/3/22
 * 
 * What: OpenTerminalHere.cs
 * 
 * Function: Adds a button when right-clicking in the Project directory
 *   that when selected, will open the operating-system specific terminal
 *   to the root folder of the project.
 *
 * TODO: Add functionality for MacOS. Fix functionality for
 *   Windows. Test functionality for Linux. Add ability to open
 *   Current open directory in project browser.
 *
 * Say thank you on the way out!
 * 
 ***********************************************************/

using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace FallEngine.Editor
{
    public class FallengineMenuItems
    {
        [MenuItem("Assets/Open Project In Terminal")]
        private static void OpenTerminalHere()
        {

            var info = new ProcessStartInfo();

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    info.FileName = "powershell";
                    info.Domain = Application.dataPath;
                    break;
                case RuntimePlatform.LinuxEditor:
                    info.FileName = "bash";
                    break;
                default:
                    Debug.LogError("This Operating system is not supported at this time.");
                    break;
            }

            Process.Start(info);
        }
    }
}