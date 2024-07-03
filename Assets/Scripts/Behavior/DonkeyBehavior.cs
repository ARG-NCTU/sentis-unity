using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

using Unity.Sentis;

using Newtonsoft.Json;

public class DonkeyBehavior : MonoBehaviour
{
    [System.Serializable]
    public struct Actions
    {
        public string inputsentence;
        public string outputsentence;
    }

    [Header("NPC Brain")]
    public SentenceSimilarity DonkeyBrain;

    [Header("Donkey list of responses")]
    public List<Actions> responsesList;

    [Header("Input UI")]
    public TMPro.TMP_InputField inputField;
    private int responsesIndex;
    private bool iscarrot = false;

    [HideInInspector]
    public List<string> inputsentences;
    public string[] sentencesArray;

    [HideInInspector]
    public float maxScore;
    public int maxScoreIndex;

    public Transform playerPosition;
    public Camera cam;
    public TMP_Text dialogueText;
    public GameObject carrot;

    void Awake()
    {

        foreach (DonkeyBehavior.Actions actions in responsesList)
        {
            inputsentences.Add(actions.inputsentence);
        }
        sentencesArray = inputsentences.ToArray();
    }


    public void Utility(float maxScore, int maxScoreIndex)
    {
        // First we check that the score is > of 0.2, otherwise we let our agent perplexed;
        // This way we can handle strange input text (for instance if we write "Go see the dog!" the agent will be puzzled).
        if (maxScore < 0.40f)
        {
            responsesIndex = 10;
        }
        else
        {
            responsesIndex = maxScoreIndex;
        }
    }

    public void OnOrderGiven(string prompt)
    {
        Tuple<int, float> tuple_ = DonkeyBrain.RankSimilarityScores(prompt, sentencesArray);
        Utility(tuple_.Item2, tuple_.Item1);
    }

    // Update is called once per frame
    void Update()
    {
        Collider collider = carrot.GetComponent<Collider>();
        iscarrot = collider.isTrigger;

        switch (responsesIndex)
        {
            case 0:
                dialogueText.text = responsesList[0].outputsentence;
                break;
            case 1:
                dialogueText.text = responsesList[1].outputsentence;
                break;
            case 2:
                dialogueText.text = responsesList[2].outputsentence;
                break;
            case 3:
                if (iscarrot == true)
                {
                    dialogueText.text = responsesList[3].outputsentence;
                    break;
                }
                else
                {
                    dialogueText.text = "I have no idea what you are talking about...";
                    break;
                }
            case 4:
                if (iscarrot == true)
                {
                    dialogueText.text = responsesList[4].outputsentence;
                    break;
                }
                else
                {
                    dialogueText.text = "I have no idea what you are talking about...";
                    break;
                }

            case 5:
                if (iscarrot == true)
                {
                    dialogueText.text = responsesList[5].outputsentence;
                    break;
                }
                else
                {
                    dialogueText.text = "I have no idea what you are talking about...";
                    break;
                }
            case 10:
                dialogueText.text = "I have no idea what you are talking about...";
                break;
        }
    }
}
