////////////////////////////////////////////////////////////////////////////////
//  
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VibrationExample : MonoBehaviour
{
  

    
    public void TapVibrate ()
    {
        Vibration.Vibrate ();
    }

  

    public void TapCancelVibrate ()
    {

        Vibration.Cancel ();
    }

    public void TapPopVibrate ()
    {
        Vibration.VibratePop ();
    }

    public void TapPeekVibrate ()
    {
        Vibration.VibratePeek ();
    }

    public void TapNopeVibrate ()
    {
        Vibration.VibrateNope ();
    }
}
