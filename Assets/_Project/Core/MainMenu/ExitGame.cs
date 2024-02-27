/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: #DATE#
 * 
 * What: ExitGame.cs
 * 
 * Function: Quit the game
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
