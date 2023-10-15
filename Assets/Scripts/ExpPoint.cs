using UnityEngine;

public class ExpPoint : PickUp
{
    
    [SerializeField]
    int expValue;

    public int value { set => expValue= value; }

    override public void onPick(Player player) {
        player.GetExp(expValue);
        Destroy(gameObject);
    }
}