using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    protected int selectedCharacterIndex;
    private Color desiredColor;


    [Header("Player Info")]
    public int playerNumber = 0;
   
    private bool HasSelectedCharacter = false; 

    [Header("List of Characters")]
    [SerializeField] protected List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] protected TextMeshProUGUI characterName;
    [SerializeField] protected Image characterSplash;
    [SerializeField] protected Image backgroundColor;
    [SerializeField] protected GameObject UIforplayer;


    [Header("Sounds")]
    [SerializeField] protected AudioClip arrowClickSfx;
    [SerializeField] protected AudioClip CharSelectMusic;

    [Header("Options")]
    [SerializeField] protected float backgroundColorTransitionSpeed = 5f;



    public void Start()
    {
        
        UpdateCharacterSelectionUI();
       
    }




    public void Update()
    {
        if (this.gameObject.tag == "CharacterSelection")
        {
            backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
        }

       
    }
    
    public void Fire4(bool value)
    {
        ConfirmSelection();
    }

    public void Fire5(bool value)
    {
        LeftArrow();
    }

    public void Fire6(bool value)
    {
        RightArrow(); 
    }


    public void ConfirmSelection()
    {

       

        


        
    }


    public void LeftArrow()
    {
        if (HasSelectedCharacter)
        {
            return;
        }

        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterList.Count - 1;
        }

        UpdateCharacterSelectionUI();
    }
    public void RightArrow()
    {
        if (HasSelectedCharacter)
        {
            return;
        }

        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterList.Count)
        {
            selectedCharacterIndex = 0;
        }

        UpdateCharacterSelectionUI();
    }



    private void UpdateCharacterSelectionUI()
    {
        characterSplash.sprite = characterList[selectedCharacterIndex].splash;
        characterName.text = characterList[selectedCharacterIndex].characterName;
        desiredColor = characterList[selectedCharacterIndex].characterColor;


    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Color characterColor;
        public GameObject selectedchar;
        public GameObject selectedcharUI;
    }
}
