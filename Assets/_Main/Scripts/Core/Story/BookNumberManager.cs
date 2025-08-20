using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookNumberManager : MonoBehaviour
{
    [Header("MainBooks")]
    public TextMeshPro Book1Main;
    public TextMeshPro Book2Main;
    public TextMeshPro Book3Main;
    
    [Header("Books")] 
    public TextMeshPro Book1;
    public TextMeshPro Book2;
    public TextMeshPro Book3;
    public TextMeshPro Book4;
    public TextMeshPro Book5;
    public TextMeshPro Book6;
    public TextMeshPro Book7;
    public TextMeshPro Book8;
    public TextMeshPro Book9;
    public TextMeshPro Book10;
    public TextMeshPro Book11;
    public TextMeshPro Book12;
    public TextMeshPro Book13;
    public TextMeshPro Book14;
    public TextMeshPro Book15;
    public TextMeshPro Book16;
    public TextMeshPro Book17;
    public TextMeshPro Book18;
    public TextMeshPro Book19;
    public TextMeshPro Book20;
    public TextMeshPro Book21;
    public TextMeshPro Book22;
    public TextMeshPro Book23;

    private TextMeshPro[] Books;
    private void Awake()
    {
        Books = new[] { Book1, Book2, Book3, Book4, Book5, Book6, Book7, Book8, Book9, Book10, Book11, Book12, Book13, Book14, Book15, Book16, Book17, Book18, Book19, Book20, Book21, Book22, Book23 };
        SetBookNumbers();
    }

    private void SetBookNumbers()
    {
        foreach (TextMeshPro book in Books)
        {
            book.text = RandomeNumber.GenerateNumber(10).ToString();
        }
    }
    
    
}
