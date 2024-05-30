using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI speakerNameText;

    private int sentenceIndex = -1;
    private StoryScene currentScene;
   
    private State state = State.COMLETED;
    private enum State
    {
        PLAYING, COMLETED
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }
    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text, currentScene.sentences[sentenceIndex].type));
        speakerNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        speakerNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;
    }
    public bool IsCompleted()
    {
        return state==State.COMLETED;
    }
    public bool IsLastSentence()
    {
        return sentenceIndex+1==currentScene.sentences.Count;
    }
    private IEnumerator TypeText(string text, int type)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;
        if (type == 1) barText.fontStyle = FontStyles.Italic;
        else barText.fontStyle = FontStyles.Normal;
        while (state != State.COMLETED) {
            barText.text += text[wordIndex];
            
            yield return new WaitForSeconds(0.01f);
            if(++wordIndex==text.Length)
            {
                state = State.COMLETED;
                break;
            }
        
        }
    }
}
