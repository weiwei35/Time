using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.name);
    }
}
