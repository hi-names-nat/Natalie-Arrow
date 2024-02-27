/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: #DATE#
 * 
 * What: ExitGame.cs
 * 
 * Function: ...
 *
 * TODO: ...
 *
 * Say thank you on the way out!
 * 
 ***********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit(0);
    }
}
