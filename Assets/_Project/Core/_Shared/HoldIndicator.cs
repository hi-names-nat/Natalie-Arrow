/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: 2/18/24
 * 
 * What: HoldIndicator.cs
 * 
 * Function: Simple class. Mostly to ensure that HeldIndicator
 * in UICard is actually a HeldIndicator and is not filled with
 * a random gameObject. Also helps with enabling and disabling
 * the gameObject.
 *
 * 
 ***********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Cards
{

 public class HoldIndicator : MonoBehaviour
 {
  /// <summary>
  /// Set if the object is currently held. Just disables this holdindictor object
  /// </summary>
  /// <param name="held">Should the Hold indicator be displayed?</param>
  internal void SetHeld(bool held)
  {
   gameObject.SetActive(held);
  }
 }
}