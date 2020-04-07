using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_OngletSelection : MonoBehaviour, IF_clicableAction, IF_Hover
{
    public int index;
    public SC_UI_OngletContainer ongletContainer;

    bool isBreakdown;

    [SerializeField]
    Animator warningNotif;
    [SerializeField]
    Image img_warningNotif;

    [SerializeField]
    GameObject animated;
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        ongletContainer = SC_UI_OngletContainer.Instance;
        if(animated != null)
        animator = animated.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            animator.SetBool("Hover", false);
            animator.SetBool("Clic", false);
        }
    }

    public void Action()
    {
        SC_UI_OngletContainer.Window newWindow = (SC_UI_OngletContainer.Window)index;
        if(index == 0 && SC_GameStates.Instance.CurTutoState == SC_GameStates.TutorialState.Tutorial1_3)
        {
                SC_CheckList.Instance.NetworkPlayerOperator.GetComponent<SC_Net_Player_TutoState>().CmdChangeTutoState(SC_GameStates.TutorialState.Tutorial1_4);
        }
        else if (index == 1 && SC_GameStates.Instance.CurTutoState == SC_GameStates.TutorialState.Tutorial1_3)
        {
                SC_CheckList.Instance.NetworkPlayerOperator.GetComponent<SC_Net_Player_TutoState>().CmdChangeTutoState(SC_GameStates.TutorialState.Tutorial1_5);
        }
        else if (index == 2 && SC_GameStates.Instance.CurTutoState == SC_GameStates.TutorialState.Tutorial1_3)
        {
                SC_CheckList.Instance.NetworkPlayerOperator.GetComponent<SC_Net_Player_TutoState>().CmdChangeTutoState(SC_GameStates.TutorialState.Tutorial1_6);
        }
        if(index == 3 && SC_GameStates.Instance.CurTutoState == SC_GameStates.TutorialState.Tutorial1_7)
        {
            SC_CheckList.Instance.NetworkPlayerOperator.GetComponent<SC_Net_Player_TutoState>().CmdChangeTutoState(SC_GameStates.TutorialState.Tutorial1_8);
        }
        ongletContainer.ChangeWindow(newWindow);
        OnClicAnimation();
    }

    public void HoverAction()
    {
        IsHover();
    }

    public void isBreakdownSystem(bool state)
    {
        if(state)
        {
            this.warningNotif.SetBool("b_OnNotif", true);
            
        }
        else
        {
            this.warningNotif.SetBool("b_OnNotif", false);
        }
    }


    void IsHover()
    {
        if (animator != null)
        {
            animator.SetBool("Hover", true);
        }

    }

    void OnClicAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Clic", true);
        }
    }
}
