using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBank : MonoBehaviour {

    public List<Word> words;
    public List<Word> startingWords;

    public GameObject wordPrefab;
	// Use this for initialization
	void Start () {
		foreach(Word word in startingWords)
        {
            AddWord(word);
        }
	}
	

    void AddWord(Word word)
    {
        words.Add(word);
    }

    void Removeword(Word word)
    {
        words.Remove(word);
    }
}
