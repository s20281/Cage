using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Canvas myCanvas;
    void Update()
    {

        foreach (var canv in FindObjectsOfType<Canvas>())
        {
            if (canv.name == "Canvas")
            {
                myCanvas = canv;
            }
        }

        if (myCanvas == null)
        {
            myCanvas = FindObjectOfType<Canvas>();
        }


        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);

    }
}
