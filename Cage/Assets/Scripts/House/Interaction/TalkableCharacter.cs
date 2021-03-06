using UnityEngine;

public class TalkableCharacter : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogUI;

    [SerializeField] private DialogueObject dialogue1;
    [SerializeField] private DialogueObject dialogue2;

    [SerializeField] private GameObject objectToDrop;
    [SerializeField] private GameObject objectWanted;

    [SerializeField] private string answerToGiveObject;
    [SerializeField] private string goodAnswer;

    [SerializeField] private bool canBeRecruited;
    [SerializeField] private bool haveToGiveObject;
    [SerializeField] private bool shouldOpenTheDoor;
    [SerializeField] private bool ifHaveImpactOnMind;

    [SerializeField] private int pointsToImpactTheMind;

    [SerializeField] private Hero prefabToFindStats;
    [SerializeField] private Hero prefabWhichWantToCompareStats;
    [SerializeField] private GameObject doorToOpen;


    public void starTalking()
    {
        dialogUI.GetObject(objectToDrop, objectWanted, answerToGiveObject, goodAnswer, canBeRecruited, haveToGiveObject, shouldOpenTheDoor, doorToOpen, gameObject, ifHaveImpactOnMind, pointsToImpactTheMind, prefabToFindStats);

        if (prefabToFindStats != null)
        {           
            Character character = TeamDatabase.control.GetCharacter(prefabWhichWantToCompareStats.name.ToLower());
            var countCharactersInInventory = 0;

            foreach(var hero in Inventory.control.GetAllCharacters())
            {
                if(hero.name != "blank")
                {
                    Debug.Log(hero.name);
                    countCharactersInInventory++;
                }
            }
            Debug.Log(character.whatWantInTeammates);

            //jak b?dzie enum i b?dziemy wybiera? po ang to na podstawie tego mo?e zrobienie strength itp., podawa? w argumencie jak?
            //wielkosc statystyk8i chce dany bohater, albo ile razy wi?ksza od swojwj 
            switch (character.whatWantInTeammates)
            {
             

                case "psychika":

                    if (prefabToFindStats.health > prefabWhichWantToCompareStats.health)
                    {
                        dialogUI.ShowDialogue(dialogue1);
                    }
                    else
                    {
                        dialogUI.ShowDialogue(dialogue2);
                    }

                    break;
                case "si?a":
                    if (prefabToFindStats.strength > prefabWhichWantToCompareStats.strength)
                    {
                        dialogUI.ShowDialogue(dialogue1);
                    }
                    else
                    {
                        dialogUI.ShowDialogue(dialogue2);
                    }
                    break;
                case "zr?czno??":
                    if (prefabToFindStats.dodge > prefabWhichWantToCompareStats.dodge)
                    {
                        dialogUI.ShowDialogue(dialogue1);
                    }
                    else
                    {
                        dialogUI.ShowDialogue(dialogue2);
                    }
                    break;
                case "wantToDisapear":
                    if(countCharactersInInventory > 4){
                        Debug.Log("powy?ej 4 ludzi? Mam nadziej? ?e nie b?d? musia? cz?sto wlaczyc");
                        dialogUI.ShowDialogue(dialogue1);
                    }
                    else
                    {
                        Debug.Log("Za ma?o ludzi");
                        dialogUI.ShowDialogue(dialogue2);
                    }
                    break;
                case "wantToStandout":
                    if (countCharactersInInventory <= 4)
                    {
                        Debug.Log("4 ludzi, chc? ci?gle walczy?");
                        dialogUI.ShowDialogue(dialogue1);
                    }
                    else
                    {
                        Debug.Log("Za duzo ludzi");
                        dialogUI.ShowDialogue(dialogue2);
                    }
                    break;
            }




        }
        else
        {
            if (objectWanted != null && Inventory.control.FindItem(objectWanted.name) !=null)
            {
                dialogUI.ShowDialogue(dialogue2);              

            }
            else
            {
                dialogUI.ShowDialogue(dialogue1);
            }
           
        }
        

    }
}
