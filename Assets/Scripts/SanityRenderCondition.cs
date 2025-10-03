using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class SanityRenderCondition : MonoBehaviour
{
    public SanityCondition[] conditions;
    public bool renderIfFalse = false;

    private new Renderer renderer;

    void Awake() => renderer = GetComponent<Renderer>();

    void Update()
    {
        var render = true;

        foreach (var condition in conditions)
        {
            if (!condition.ConditionMet())
            { render = false; break; }
        }

        if (renderIfFalse) { render = !render; }

        renderer.forceRenderingOff = !render;
    }
}
