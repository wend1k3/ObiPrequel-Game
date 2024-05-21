
using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "NewStoryScene", menuName="Data/New Story Scene")]
[System.Serializable]
public class StoryScene : ScriptableObject
{
    public List<Sentence> sentences;
    public Sprite background;
    public StoryScene nextScene;

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Speaker speaker;
        public int type; // 0 for normal, 1 for italics, 
    }
}