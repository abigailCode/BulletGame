using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class dialogueController : MonoBehaviour
{
    // -------------------------------------------------------------
    // ATRIBUTOS NECESARIOS PARA IMPLEMENTAR EL SISTEMA DE DIÁLOGO
    // -------------------------------------------------------------
    // Velocidad con la que aparecerá el texto
    [SerializeField] float textSpeed = 0.05f;
    [SerializeField] float extraTimeOnScreen = 2f;

    // Las distintas líneas de texto
    [SerializeField] string[] textLines;

    // Índice que indica la línea de texto a mostrar
    [SerializeField] int lineIndex = 0;

    // Referencia al objeto del texto
    [SerializeField] TextMeshProUGUI dialogueText;

    // Método que lanza el sistema de diálogos con la línea
    // que se ha pasado como parámetro
    public void StartDialogue(string item)
    {
        dialogueText.text = ""; // Se borra todo el texto escrito en pantalla

        // Seleccionamos la frase que corresponda según la string de entrada
        switch (item)
        {
            case "sawblade_item": lineIndex = 0; break; // Mostramos la frase de la sierra
            case "spikedball_item": lineIndex = 1; break; // Mostramos la frase de la bola
            default: break;
        }

        StartCoroutine(WriteLine()); // Lanzamos la corrutina
    }

    // Método que desactiva el objeto que lleva el sistema de diálogos
    public void EndDialogue()
    {
        gameObject.SetActive(false); // Desactivamos el objeto
    }

    // -------------------------------------------------
    // CORRUTINA QUE MUESTRA CADA LÍNEA LETRA POR LETRA
    // -------------------------------------------------
    IEnumerator WriteLine()
    {
        // Recorremos cada letra de la línea de texto
        foreach (var letra in textLines[lineIndex].ToCharArray())
        {
            // Concatenamos las letras en el GameObject una a una
            dialogueText.text += letra;
            // Esperamos hasta la siguiente letra
            yield return new WaitForSeconds(textSpeed);
        }
        // Cuando ya se ha mostrado la frase entera, la dejamos un tiempo en pantalla
        yield return new WaitForSeconds(extraTimeOnScreen);
        EndDialogue(); // Desactivamos el sistema de diálogos
    }

    private void Start()
    {
        // Desactivamos los diálogos al arrancar el juego
        // por si estaban activados por defecto
        EndDialogue();
    }


}
