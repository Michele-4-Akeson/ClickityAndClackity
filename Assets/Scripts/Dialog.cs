using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{  [TextArea(3, 10)]
    public string[] sentences;
    public GameObject Hud;

    public bool active = false;

    public GameObject dialogBox;
    public Text dialogText;
    private int index = 0;

    private void Start()
    {
        Hud = GameObject.Find("PlayerHud");
        dialogBox = Hud.transform.GetChild(4).gameObject;
        
    }



     private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Clickity") || other.gameObject.CompareTag("Clackity")){
            speak();
        }
    }
    public void speak()
    {
        if(!active && !dialogBox.activeSelf){
            active = true;
            dialogBox.SetActive(true);
            dialogText = dialogBox.transform.GetChild(3).GetComponent<Text>();;
            StartCoroutine(typing_routine());

        }
       
    }
       

    IEnumerator typing_routine()
    {
        while (index < sentences.Length)
        {
            foreach (char character in sentences[index].ToCharArray())
            {
                dialogText.text += character.ToString();
                yield return new WaitForSeconds(0.08f);
            }
            yield return new WaitForSeconds(0.7f);
            dialogText.text = "";
            index++;

        }


        yield return new WaitForSeconds(2f);
        dialogText.text = "";
        dialogBox.SetActive(false);

        
    }
}
