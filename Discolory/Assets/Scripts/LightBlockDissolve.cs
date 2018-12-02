using UnityEngine;

public class LightBlockDissolve : MonoBehaviour
{
    [Range(0, 1)]
    public float DissolveValue = 0.0f;
    [Range(0, 0.2f)]
    public float DissolveEdge = 0.0f;

    public Material mat;
    public float dissolveSpeed;
    public ParticleSystem spark;

    float toDissolve;

    // Update is called once per frame
    void Update()
    {
        float currentDissolveValue = mat.GetFloat("_Dissolve");
        if (Mathf.Abs(currentDissolveValue - toDissolve) > .01f)
        {
            currentDissolveValue = Mathf.Lerp(currentDissolveValue, toDissolve, dissolveSpeed * Time.deltaTime);
            mat.SetFloat("_Dissolve", currentDissolveValue);
        }
        else if (currentDissolveValue > .5f)
        {
            mat.SetFloat("_Dissolve", 1);
        }
        else
        {
            mat.SetFloat("_Dissolve", 0);
        }
    }

    public void Dissolve(bool on)
    {
        toDissolve = on ? 0 : 1;
    }
}
