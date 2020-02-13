using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_CanvasManager : MonoBehaviour
{
    [SerializeField]
    Canvas[] canvas; //Tableau des canvas

    [SerializeField]
    Material materialRenderTexture; //Material contenant la texture de l'écran solo

    int numRealDisplay = 4;

    Color32 backgroundColor;
    // Start is called before the first frame update
    void Start()
    {
        backgroundColor = new Color32(21, 13, 25, 255); //Violet actuel
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void allScreenActive()
    {
        for (int i = 0; i < numRealDisplay; i++)
        {
            canvas[i].GetComponent<Image>().color = new Color32(0, 0, 0, 255); //Active l'alpha sans couleur, sinon sprite n'apparait pas 
            canvas[i].GetComponent<Image>().material = null; //Désactiver le material (renderTexture)
            activateChild(i); //Activation des enfants
        }
    }

    void desactivateChild(int indexCanvas)
    {
        int numChildCnv0 = canvas[indexCanvas].gameObject.transform.childCount;
        for (int i = 0; i < numChildCnv0; i++)
        {
            Transform objectChild = canvas[indexCanvas].gameObject.transform.GetChild(i);
            objectChild.gameObject.SetActive(false);
        }
    }

    public void activateChild(int indexCanvas)
    {
        int numChild = canvas[indexCanvas].gameObject.transform.childCount;
        for (int i = 0; i < numChild; i++)
        {
            Transform objectChild = canvas[indexCanvas].gameObject.transform.GetChild(i);
            objectChild.gameObject.SetActive(true);
        }
    }

    public void checkTaskBeforeGo()
    {
        for (int i = 0; i < numRealDisplay; i++)
        {
            canvas[i].GetComponent<Image>().color = backgroundColor;
            canvas[i].GetComponent<Image>().material = null; //Désactiver le material (renderTexture)
            activateChild(i); //Activation des enfants
        }
        //canvas[5].enabled = false;
    }
    public void lockScreenDisplay()
    {
        for (int i = 0; i < numRealDisplay; i++)
        {
            canvas[i].GetComponent<Image>().material = materialRenderTexture;
            desactivateChild(i); //Canvas en World = visible par monoscreenCam
        }
        canvas[1].GetComponent<Image>().material = null;
        canvas[1].GetComponent<Image>().color = new Color32(0, 0, 0, 255);

    }
}
