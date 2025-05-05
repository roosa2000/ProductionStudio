using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    private int counter = 0; // Tracks the number of pickups collected
    [SerializeField] private List<Image> pickupImages; // Assign unique images in order
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = new Color(1, 1, 1, 0.3f); // Faded out

    private void Start()
    {
        // Set all to inactive at start
        foreach (var image in pickupImages)
        {
            image.color = inactiveColor;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (counter < pickupImages.Count)
            {
                pickupImages[counter].color = activeColor;
                counter++;
            }
            Destroy(gameObject); // Destroy the pickup item
            if (counter == 3)
            {
                PathHighlighter.Instance.ShowExitPath();
            }
        }
    }

    private void UpdateSlotUI()
    {
        Debug.Log("Pickups: " + counter);
    }
}
