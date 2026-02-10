using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    // its an array ?? of dice faces i belive 
    public Transform[] diceFaces;
    public Rigidbody rb;

    // It depends on the num of dices u have
    private int _diceIndex = -1;

    private bool _hasStoppedRolling;
    private bool _delayFinished;

    public static UnityAction<int, int> OndiceResult;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_delayFinished) return; 

        if (!_hasStoppedRolling && rb.angularVelocity.sqrMagnitude == 0f)
        {
            _delayFinished = true;
            GetNumberOnTopFace();
        }


}
    [ContextMenu(itemName:"Get top face")]
    private int GetNumberOnTopFace()
    {
        if (diceFaces == null) return -1;

        var topFace = 5;
        var lastYPostion = diceFaces[5].position.y;

        for (int i = 0; i < diceFaces.Length; i++)
        {
            if (diceFaces[i].position.y > lastYPostion)
            {
                lastYPostion = diceFaces[i].position.y;
                topFace = i;
            }
        }

        Debug.Log($"Dice result {topFace + 1}");

        OndiceResult?.Invoke(_diceIndex, topFace + 1);

        return topFace + 1;

    }
}
