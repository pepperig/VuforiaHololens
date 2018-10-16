using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour {

    // KeywordRecognizer object.
    KeywordRecognizer keywordRecognizer;
    public GameObject message;
    GameObject  boxHandler;

    // Defines which function to call when a keyword is recognized.
    delegate void KeywordAction(PhraseRecognizedEventArgs args);
    Dictionary<string, KeywordAction> keywordCollection;
    // Use this for initialization
    void Start () {

        keywordCollection = new Dictionary<string, KeywordAction>();

        // Add keyword to start manipulation.
        keywordCollection.Add("Skip package", SkipCommand);

        boxHandler = GameObject.FindGameObjectsWithTag("BoxHandler")[0];

        // Initialize KeywordRecognizer with the previously added keywords.
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void OnDestroy()
    {
        if(keywordRecognizer != null)
            keywordRecognizer.Dispose();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywordAction;

        if (keywordCollection.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(args);
        }
    }

    private void SkipCommand(PhraseRecognizedEventArgs args)
    {
        message.GetComponent<Text>().text = "Skip";
        boxHandler.GetComponent<BoxHandler>().timerStart = true;
        boxHandler.GetComponent<BoxHandler>().skip = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
