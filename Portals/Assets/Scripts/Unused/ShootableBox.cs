using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBox : MonoBehaviour
{
    public int currentHealth = 3;

    public void damage(int demageAmount)
    {
        currentHealth -= demageAmount;
        if (currentHealth<=0)
        {
            gameObject.SetActive(false);
        }
    }
}
