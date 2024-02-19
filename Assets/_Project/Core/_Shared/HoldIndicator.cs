/*************************************************************
 *
 * Author: Natalie Soltis
 * Date: #DATE#
 * 
 * What: HoldIndicator.cs
 * 
 * Function: Simple class. Mostly to ensure that HeldIndicator
 * in UICard is actually a HeldIndicator and is not filled with
 * a random gameObject. Also helps with enabling and disabling
 * the gameObject.
 *
 * TODO: ...
 *
 * Say thank you on the way out!
 * 
 ***********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Cards
{

 public class HoldIndicator : MonoBehaviour
 {
  internal void SetHeld(bool held)
  {
   gameObject.SetActive(held);
  }
 }
}