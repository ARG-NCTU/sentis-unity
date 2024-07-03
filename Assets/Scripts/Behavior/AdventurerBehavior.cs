using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

using Unity.Sentis;

using Newtonsoft.Json;

public class AdventurerBehavior : MonoBehaviour
{
    [System.Serializable]
    public struct Actions
    {
        public string inputsentence;
        public string outputsentence;
    }

    [Header("NPC Brain")]
    public SentenceSimilarity AdventurerBrain;

    [Header("Adventurer list of responses")]
    public List<Actions> responsesList;

    [Header("Input UI")]
    public TMPro.TMP_InputField inputField;
    private int responsesIndex;

    [HideInInspector]
    public List<string> inputsentences;
    public string[] sentencesArray;

    [HideInInspector]
    public float maxScore;
    public int maxScoreIndex;

    public Transform playerPosition;
    public Camera cam;
    public TMP_Text dialogueText;

    void Awake()
    {
        foreach (AdventurerBehavior.Actions actions in responsesList)
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
        Tuple<int, float> tuple_ = AdventurerBrain.RankSimilarityScores(prompt, sentencesArray);
        Utility(tuple_.Item2, tuple_.Item1);
    }

    // Update is called once per frame
    void Update()
    {
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
                dialogueText.text = responsesList[3].outputsentence;
                break;
            case 4:
                dialogueText.text = responsesList[4].outputsentence;
                break;
            case 10:
                dialogueText.text = "I have no idea what you are talking about...";
                break;
        }
    }
}
