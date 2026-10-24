using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    private Renderer rend;
    private bool isPressed = false;
    public GameObject obj;


    void Start()
    {
        rend = GetComponent<Renderer>();
        obj.SetActive(!obj.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        Color color = rend.material.color;

        if (other.CompareTag("Player") && isPressed == false)
        {
            isPressed = true;
            color.a = 0.3f;
            rend.material.color = color;
            obj.SetActive(!obj.activeSelf);
        }
        else
        {
            isPressed = false;
            color.a = 1f;
            obj.SetActive(!obj.activeSelf);
        }


        rend.material.color= color;
    }
}
