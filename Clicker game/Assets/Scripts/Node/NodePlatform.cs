using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseDown()
    //{
    //    // prevent clicking through UI
    //    if (MouseOverUILayerObject.IsPointerOverUIObject())
    //    {
    //        return;
    //    }
    //    // DEBUG
    //    if (GameManager.i.platformSelectedInUI == null)
    //    {
    //        return;
    //    }
    //    if (GameManager.i.platformSelectedInUI)
    //    {
    //        AddPlaceHolder();
    //    }
    //}

    //public void AddPlaceHolder()
    //{
    //    // Audio
    //    AudioManager.instance.Play(SoundList.BuildingPlaced);
    //    // Add the node into the list **(GameManager - nodeList)
    //    GameManager.i.nodeList.Add(gameObject);
    //    // **(GameManager - futureBuildingList)
    //    GameManager.i.futureBuildingList.Add(GameManager.i.buildingSelectedInUI);
    //    // Using a index to recongnize the order (from 0)
    //    nodeIndex = GameManager.i.nodeList.IndexOf(gameObject);
    //    // indicator only, make the node recongnize the placeHolder
    //    placeHolder = Instantiate(GameManager.i.placeHolder, transform.position, Quaternion.identity);
    //    // Oppositely, the placeHolder needs to recongnize the node.
    //    PlaceHolder placeHolder_script = placeHolder.GetComponent<PlaceHolder>();
    //    placeHolder_script.attachedNode = gameObject;
    //    // Store what building will be placed in the particular node
    //    placeHolder_building_REF = GameManager.i.buildingSelectedInUI;
    //}
}
