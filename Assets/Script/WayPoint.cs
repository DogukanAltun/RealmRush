using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] GameObject Defender;
    [SerializeField] private bool isPlaceable = false;
    [SerializeField] private int Cost = 50;
    Bank bank;
    public bool IsPlaceable { get { return isPlaceable; } }
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }
    void OnMouseDown()
    {
        if (isPlaceable)
        {
            if (bank.currentCurrency >= Cost)
            {
                Debug.Log(transform.name);
                Instantiate(Defender, transform.position, Quaternion.identity);
                isPlaceable = false;
                bank.currentCurrency -= Cost;
            }
            else
            {
                Debug.Log("You Dont Have Enough Money");
                return;
            }
        }
    }
}
