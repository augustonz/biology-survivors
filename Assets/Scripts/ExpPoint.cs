using UnityEngine;

public class ExpPoint : PickUp
{
    
    [SerializeField]
    int expValue;

    override public void onPick(Player player) {
        player.GetExp(expValue);
        Destroy(gameObject);
    }
}