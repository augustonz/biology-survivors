using UnityEngine;

public class ExpPoint : PickUp
{
    
    [SerializeField]
    int expValue;

    public int value { set => expValue= value; }

    override public void onPick(Player player) {
        Destroy(gameObject);
        player.GetExp(expValue);
    }
}