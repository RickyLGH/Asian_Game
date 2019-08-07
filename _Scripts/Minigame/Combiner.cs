using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Combiner : MonoBehaviour
{
    //only one can exist at a time
    public static Combiner instance;

    public static Combiner Instance { get { return instance; } }

    //the actual components
    public Word slot1;
    public Word slot2;

    private Recipe[] recipes;
    //where to spawn the new objects
    public Transform resultParent;
    //the actual object prefab
    public GameObject itemPrefab;

    public bool madeItem = false;

    //singleton so there is only one in each scene 
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        //loads the recipe scriptable object
        recipes = Resources.LoadAll<Recipe>("Recipe");
    }

    //checks the word being added
    public void AddWord(Word word, int slot)
    {
        if (slot == 1)
        {
            slot1 = word;
        }
        if (slot == 2)
        {
            slot2 = word;
        }

        UpdateResult();
    }

    //removes the words
    public void RemoveWord(int slot)
    {
        //Debug.Log("Remove from slot " + slot);

        if (slot == 1)
        {
            slot1 = null;
        }
        if (slot == 2)
        {
            slot2 = null;
        }

        UpdateResult();
    }

    //checks if it matches the recipe
    void UpdateResult()
    {
        ClearPreviousResult();

        Word[] results = GetResults();
        if (results != null && results.Length != 0)
        {
            foreach (Word result in results)
            {

                Debug.Log(result);
                CreateWord(result);
                madeItem = true;
                Debug.Log("bool:" + madeItem);
            }
        }
    }

    //instantiates the actual word object
    void CreateWord(Word word)
    {
        //resultParent.GetComponentInParent<Panel>();
        GameObject itemObj = Instantiate(itemPrefab, resultParent);
        ItemDisplay display = itemObj.GetComponent<ItemDisplay>();
        if (display != null)
            display.Setup(word);
    }


    //gets rid of the previous words
    void ClearPreviousResult()
    {
        foreach (Transform child in resultParent)
        {
            Destroy(child.gameObject);
        }
    }

    //checks the recipe
    Word[] GetResults()
    {
        if (slot1 == null || slot2 == null)
            return null;

        List<Word> words = new List<Word>();

        foreach (Recipe recipe in recipes)
        {
            if ((recipe.input1 == slot1 && recipe.input2 == slot2) ||
                (recipe.input1 == slot2 && recipe.input2 == slot1))
            {

                words.Add(recipe.result);

            }
        }

        return words.ToArray();
    }
}



