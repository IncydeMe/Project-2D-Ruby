using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customeButton;
    public GameObject optionPanel;
    public bool isTalking = false;

    static Story story;
    TextMeshProUGUI nameTag;
    TextMeshProUGUI message;
    List<string> tags;
    static Choice choiceSelected;

    //Start is call before the first frame update
    void Start()
    {
        story = new Story(inkFile.text);
        nameTag = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        message = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        tags = new List<string>();
        choiceSelected = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Is there more to the story?
            if (story.canContinue)
            {
                nameTag.text = "test";
                AdvanceDialogue();

                //Are there any choices?
                if (story.currentChoices.Count != 0)
                {
                    StartCoroutine(ShowChoices());
                }
            }
            else
            {
                FinishDialogue();
            }
        }
    }

    //Create then show the choices on the screen until one got selected
    IEnumerator ShowChoices()
    {
        Debug.Log("There are choices need to make here!");
        List<Choice> choices = story.currentChoices;

        for (int i = 0; i < choices.Count; i++)
        {
            GameObject temp = Instantiate(customeButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() =>
            {
                temp.GetComponent<Selectable>().Decide();
            });

            optionPanel.SetActive(true);

            yield return new WaitUntil(() =>
            {
                if (choiceSelected != null)
                {
                    AdvanceFromDecision();
                }
                return choiceSelected != null;
            });

            AdvanceFromDecision();
        }
    }

    // Tells the story which branch to go to
    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    // After a choice was made, turn off the panel and advance from that choice
    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        AdvanceDialogue();
    }

    //Finish the Story (Dialogue)
    private void FinishDialogue()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        Debug.Log("End dialogue");
    }

    //Advance through the story
    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        //ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    //type out sentence letter by letter and make character idle if they were talking
    IEnumerator TypeSentence(string sentence)
    {
        //Show message 
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }

        //Idle character
    }


    //====== Tag Parser ======
    //Description: 
    //In inky, you can use tags which can be used to cue stuff in a game.
    //This is just one way of doing it. Not the only method on how to trigger eventss.
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(" ")[1];

            switch (prefix.ToLower())
            {
                case "anim":
                    //Set animation
                    break;
                case "color":
                    //Set color of text
                    break;
            }
        }
    }
}
