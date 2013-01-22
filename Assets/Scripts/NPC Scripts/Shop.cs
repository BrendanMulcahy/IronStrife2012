using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shop : InteractableObject {

    public LinkedList<Item> itemsForSale;
    public Transform lookTarget = null;
    public bool isShopping = false;
    private Vector2 scrollPosition = new Vector2();

    public ItemAvailability sellingAbility;
    const float maxShopDistance = 5.0f;

    private Rect shopRect = new Rect(Screen.width * .2f, Screen.height * .2f, Screen.width * .6f, Screen.height * .6f);
    GUISkin skin;
    public bool disabledThisFrame = false;
    public int counter = 1;
    private AudioClip coinSound;

	void Awake() 
    {
        coinSound = Resources.Load("Sounds/coins") as AudioClip;
        sellingAbility = ItemAvailability.Regular;

        itemsForSale = new LinkedList<Item>();
        var dict = ItemFactory.GetAllItems();
        foreach (Item i in dict.Values)
        {
            if (i.availability == sellingAbility)
            {
                itemsForSale.AddLast(i);
            }
        }

        animation.Play("Idle01");
        skin = Resources.Load("ISEGUISkin") as GUISkin;
	}
	
	void Update() 
    {
        if (counter-- == 0)
            disabledThisFrame = false;
        if (lookTarget)
        {
            transform.LookAt(lookTarget);
        }
        if (isShopping)
        {
            if ((Vector3.Distance(lookTarget.transform.position, this.transform.position) > maxShopDistance))
            {
                isShopping = false;
                lookTarget = null;
                Util.MyLocalPlayerObject.EnableControls();

            }
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                isShopping = false;
                lookTarget = null;
                disabledThisFrame = true; counter = 1;
                Util.MyLocalPlayerObject.EnableControls();

            }
        }
	}

    void OnGUI()
    {
        GUI.skin = skin;
        if (isShopping)
        {
            shopRect = GUI.Window("shop".GetHashCode(), shopRect, ShowShop, GUIContent.none, GUI.skin.GetStyle("smallWindow"));
        }
    }

    public override void InteractWith(GameObject player)
    {
        if (isShopping)
        {
            isShopping = false;
            lookTarget = null;
            Util.MyLocalPlayerObject.EnableControls();
        }
        else
        {
            if (!disabledThisFrame)
            {
                lookTarget = player.transform;
                isShopping = true;
                Util.MyLocalPlayerObject.DisableControls();
            }
        }
        
    }

    void ShowShop(int id)
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GUILayout.BeginVertical();
        foreach (Item item in itemsForSale)
        {
            if (GUILayout.Button(item.name + "\t\t\t\t"+item.goldCost, GUI.skin.GetStyle("smallButton")))
            {
                ButtonPressed(item);
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();

        GUI.DragWindow();
    }

    private void ButtonPressed(Item item)
    {
        Debug.Log("You are trying to buy " + item.name + " for "+item.goldCost + " gold.");
        if (Util.MyLocalPlayerObject.GetInventory().Gold > item.goldCost)
        {
            if (Network.isServer)
                Util.MyLocalPlayerObject.GetInventory().TryPurchaseItem(item.name, this.networkView.viewID);
            Util.MyLocalPlayerObject.networkView.RPC("TryPurchaseItem", RPCMode.Server, item.name, this.networkView.viewID);
        }
        else
        {
            PopupMessage.LocalDisplay("You don't have enough gold to buy that!");
        }
    }

    [RPC]
    void ItemPurchasedSound()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(coinSound);
    }
}
