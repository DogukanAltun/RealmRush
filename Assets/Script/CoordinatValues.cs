using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinatValues : MonoBehaviour
{
    private Color defaultColor = Color.white;
    private Color placeableColor = Color.black;
    private TextMeshPro label;
    WayPoint wayPoint;
    private Vector2Int coordinates = new Vector2Int();

     void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        wayPoint = GetComponentInParent<WayPoint>();
        DisplayCoordinates();
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        } 
        PlaceColors();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / 10);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / 10);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void PlaceColors()
    {
        if (wayPoint.IsPlaceable)
        {
            label.color = placeableColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
