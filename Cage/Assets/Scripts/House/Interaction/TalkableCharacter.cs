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

            //jak bêdzie enum i bêdziemy wybieraæ po ang to na podstawie tego mo¿e zrobienie strength itp., podawaæ w argumencie jak¹
            //wielkosc statystyk8i chce dany bohater, albo ile razy wiêksza od swojwj 
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
                case "si³a":
                    if (prefabToFindStats.strength > prefabWhichWantToCompareStats.strength)
                    {
                        dialogUI.ShowDialogue(dialogue1);
                    }
                    else
                    {
                        dialogUI.ShowDialogue(dialogue2);
                    }
                    break;
                case "zrêcznoœæ":
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
                        Debug.Log("powy¿ej 4 ludzi? Mam nadziejê ¿e nie bêdê musia³ czêsto wlaczyc");
                        dialogUI.ShowDialogue(dialogue1);
                    }
                    else
                    {
                        Debug.Log("Za ma³o ludzi");
                        dialogUI.ShowDialogue(dialogue2);
                    }
                    break;
                case "wantToStandout":
                    if (countCharactersInInventory <= 4)
                    {
                        Debug.Log("4 ludzi, chcê ci¹gle walczyæ");
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
