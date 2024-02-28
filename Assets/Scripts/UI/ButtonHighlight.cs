using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ButtonHighlight : StateMachineBehaviour
{   
    public delegate void ButonOverlay();
    public static event ButonOverlay highlightOn;
    public static event ButonOverlay highlightOff;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        highlightOn?.Invoke();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        highlightOff?.Invoke();
    }


}
