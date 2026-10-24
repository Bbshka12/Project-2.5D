using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereStopStart : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Получаем компонент Rigidbody сферы
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Устанавливаем скорость в ноль
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; // если хотите обнулить вращение
        }
    }
}
