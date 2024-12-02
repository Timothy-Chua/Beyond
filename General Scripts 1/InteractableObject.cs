using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(InteractableInfo))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Collider colliderThis;
    private Outline outline;
    private InteractableInfo interactableInfo;
    [SerializeField] private bool isEnemy;
    [SerializeField] private bool isKey;

    public bool isSelected;

    private void Awake()
    {
        colliderThis = GetComponent<Collider>();
        outline = GetComponent<Outline>();
        interactableInfo = GetComponent<InteractableInfo>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isSelected = false;
        outline.enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (GameManager.instance.state == GameState.Analysis)
        {
            if (isKey && outline.enabled == false)
                outline.enabled = true;

            if (!colliderThis.enabled && !isEnemy)
                colliderThis.enabled = true;
        }
        else
        {
            if (colliderThis.enabled && !isEnemy)
                colliderThis.enabled = false;

            if (outline.enabled == true)
                outline.enabled = false;

            if (isSelected)
                isSelected = false;
        }
    }

    protected virtual void OnMouseEnter()
    {
        if (GameManager.instance.state == GameState.Analysis)
        {
            SetInteractInfo();
        }
        else
        {
            ClearInteractInfo();
        }

        isSelected = true;
    }

    protected virtual void OnMouseOver()
    {
        if (GameManager.instance.state == GameState.Analysis)
        {
            if (Input.GetKeyDown(SettingsManager.instance.keySelect))
            {
                Interact();
            }
        }
        else
        {
            ClearInteractInfo();
        }
    }

    protected virtual void OnMouseExit()
    {
        if (GameManager.instance.state == GameState.Analysis)
        {
            ClearInteractInfo();
        }

        isSelected = false;
    }

    protected virtual void Interact()
    {
        
    }

    protected virtual void SetInteractInfo()
    {
        if (!isKey)
            outline.enabled = true;

        UIManager.instance.txtItemName.text = interactableInfo.itemName;
        UIManager.instance.txtItemDescription.text = interactableInfo.itemDescription;
    }

    protected virtual void ClearInteractInfo()
    {
        if (!isKey)
            outline.enabled = false;
        
        UIManager.instance.txtItemName.text = null;
        UIManager.instance.txtItemDescription.text = null;
    }
}
