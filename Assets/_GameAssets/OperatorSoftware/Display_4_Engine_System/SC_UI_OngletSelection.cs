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
    GameObject animated;
    Animator animator;

    [SerializeField]
    Image[] img_ToBreakDown;
   
    Material[] wireSafe;

    [SerializeField]
    Material wireBreakdown;

    // Start is called before the first frame update
    void Start()
    {
        ongletContainer = SC_UI_OngletContainer.Instance;
        if(animated != null)
        animator = animated.GetComponent<Animator>();

        wireSafe = new Material[img_ToBreakDown.Length];

        for(int i = 0; i<wireSafe.Length; i++)
        {
            wireSafe[i] = img_ToBreakDown[i].material;
        }

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
            //this.warningNotif.SetBool("b_OnNotif", true);
            StartCoroutine(RedWireCoro());
        }
        else
        {
            //this.warningNotif.SetBool("b_OnNotif", false);
            
            EndCoroutine();
        }
    }

    void EndCoroutine()
    {
        StopAllCoroutines();
        for (int i = 0; i<img_ToBreakDown.Length; i++)
        {
            img_ToBreakDown[i].material = wireSafe[i];
            img_ToBreakDown[i].color = Color.white;
        }

    }
    IEnumerator RedWireCoro()
    {
        float animTime = 0.5f;
        float maxOpacity = 1;
        float minOpacity = 0f;
        float ratePerSec = maxOpacity - minOpacity / animTime;
        float curOpacity;
        bool Add = true;
        float t = 0;

        for (int i = 0; i < img_ToBreakDown.Length; i++)
        {
            img_ToBreakDown[i].material = wireBreakdown;
        }

        Vector4 ColorTampon = Color.white;
        curOpacity = minOpacity;

        while (true)
        {
            if(t < animTime)
            {
                t += Time.deltaTime;
                if (Add)
                {

                    if (curOpacity < maxOpacity)
                         curOpacity = Mathf.Lerp(curOpacity,maxOpacity, ratePerSec * Time.deltaTime);
                }
                else
                {

                    if (curOpacity > minOpacity)
                        curOpacity = Mathf.Lerp(curOpacity, minOpacity, ratePerSec * Time.deltaTime);

                }

                for (int i = 0; i < img_ToBreakDown.Length; i++)
                {
                    img_ToBreakDown[i].color = new Vector4(ColorTampon.x, ColorTampon.y, ColorTampon.z, curOpacity);
                }
                
            }
            else
            {
                Add = !Add;
                t = 0;
            }
            yield return 0;
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
