using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterKeuze : MonoBehaviour
{
    private GameObject[] characterList;
    private int index = 0;
    
    private void Start()
    {
        characterList = new GameObject[4];
        //Fill the array with all playermodels
        for (int i = 0; i < transform.childCount; i++)
            characterList[i] = transform.GetChild(i).gameObject;

        foreach (GameObject go in characterList)
            go.SetActive(false);


        if (characterList[0])
            characterList[0].SetActive(true);
    }

    public void ToggleLeft()
    {
        //Toggle off the current model
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
            index = characterList.Length - 1;
        
        //Toggle on the new model
        characterList[index].SetActive(true);

    }

    public void ToggleRight()
    {
        //Toggle off the current model
        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
            index = 0;
        
        //Toggle on the new model
        characterList[index].SetActive(true);

    }

    public void ConfirmButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}