using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraphicPanel
{
    public string panelName;
    public GameObject rootPanel;
    private List<GraphicLayer> layers = new List<GraphicLayer> ();

    public GraphicLayer GetLayer(int layerDepth)
    {
        for (int i = 0; i < layers.Count; i++)
        {
            if (layers[i].layerDepth == layerDepth)
                return layers[i];
        }

        return null;
    }

    private GraphicLayer CreateLayer(int layerDepth)
    {
        GraphicLayer layer = new GraphicLayer();
        GameObject panel = new GameObject(string.Format(GraphicLayer.Layer_Object_Name_Format, layerDepth));
        RectTransform rect = panel.AddComponent<RectTransform>();
        panel.AddComponent<CanvasGroup>();
        panel.transform.SetParent(rootPanel.transform, false);

        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.one;

        // e 14 p 1  14:16
    }
}
