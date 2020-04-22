using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_OngletSelection : MonoBehaviour, IF_clicableAction, IF_Hover
{
    public int index;
    public SC_UI_OngletContainer ongletContainer;
    SC_UI_WireBlink wireBlink;


    [SerializeField]
    GameObject animated;
    Animator animator;

    [SerializeField]
    int[] wireIndex;



    // Start is called before the first frame update
    void Start()
    {
        ongletContainer = SC_UI_OngletContainer.Instance;
        if(animated != null)
        animator = animated.GetComponent<Animator>();
        wireBlink = GetComponentInParent<SC_UI_WireBlink>();


    }

    // Update is called once per frame
    void Update()
    {
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
        #region AnimIn&Out
        if(index == 0)
        {
            ongletContainer.DisplayIn();
        }
        if (index == 1)
        {
            ongletContainer.WeaponIn();
        }
        if (index == 2)
        {
            ongletContainer.MoveIn();
        }
        if (index == 3)
        {
            ongletContainer.DisplayOut();
        }
        if (index == 4)
        {
            ongletContainer.WeaponOut();
        }
        if (index == 5)
        {
            ongletContainer.MoveOut();
        }
        #endregion
    }

    public void HoverAction()
    {
        IsHover();
    }

    public void isBreakdownSystem(bool state)
    {
        
        for (int i = 0; i < wireIndex.Length; i++)
        {
            wireBlink.SetBreakDown(wireIndex[i], state);
        }

    }


    void IsHover()
    {
        if (animator != null)
        {
            animator.SetBool("Hover", true);
            StartCoroutine(EndCoroutine("Hover"));

        }

    }

    void OnClicAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Clic", true);
            StartCoroutine(EndCoroutine("Clic"));
        }
    }

    IEnumerator EndCoroutine(string Bool)
    {
        yield return new WaitForEndOfFrame();
        animator.SetBool(Bool, false);
    }
}
