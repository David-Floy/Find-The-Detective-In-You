using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;



public class SelectHighlight : MonoBehaviour, INotifyPropertyChanged
{
    private static readonly SelectHighlight instance = new SelectHighlight();
    

    public SelectHighlight Instance
    {
        get { return instance; }
    }
    
    
    
    private static Transform highlight;
    private static Transform oldHighlight;
    private static RaycastHit _raycastHit;

    private  bool hoverOver;

    private bool HoverOver
    {
        get { return hoverOver; }
        set
        {
            if (hoverOver != value)
            {
                hoverOver = value;
                OnPropertyChanged(nameof(HoverOver));

                if (hoverOver)
                {
                    ActivateOutline();
                }
                else
                {
                    DeactivateOutline();
                }
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private  void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static void HoverHighlight(Ray ray)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out _raycastHit, PlayerInteractable.interactRange))
        {
            highlight = _raycastHit.transform;
            if (highlight.CompareTag("Selectable"))
            {
                if (oldHighlight != highlight)
                {
                    instance.HoverOver = false;
                }
                instance.HoverOver = true;
                
            }
            else
            {
                highlight = null;
                instance.HoverOver = false;
                
            }
        }
        else
        {
            if (oldHighlight != null)
            {
                instance.HoverOver = false;
            }
        }
         

    }

    private static void ActivateOutline()
    {
        oldHighlight = highlight;
        if (highlight.gameObject.GetComponent<Outline>() != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = true;
            HintContainer.HintBox.ShowHint("Press E");
        }
        else
        {
            HintContainer.HintBox.ShowHint("Press E");
            Outline outline = highlight.gameObject.AddComponent<Outline>();
            outline.enabled = true;
            highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
            highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
        }
    }

    private static void DeactivateOutline()
    {
        //highlight.gameObject.GetComponent<Outline>().enabled = false;
        oldHighlight.gameObject.GetComponent<Outline>().enabled = false;
        oldHighlight = null;
        HintContainer.HintBox.Hide();
    }

    
}
