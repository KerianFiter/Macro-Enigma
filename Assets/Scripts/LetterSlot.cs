using System;
using System.Linq;
using Oculus.Interaction;
using UnityEngine;

public class LetterSlot : MonoBehaviour
{
    [SerializeField] private int position;
    private SnapInteractable _snapInteractable;
    
    private void Awake()
    {
        _snapInteractable = GetComponent<SnapInteractable>();
    }
    
    public char GetLetter()
    {
        foreach (IInteractorView interactorView in _snapInteractable.SelectingInteractorViews)
        {
            if (interactorView.Data.GetType() == typeof(LetterData))
                return ((LetterData)interactorView.Data).letter;
        }
        return ' ';
    }
    
}
