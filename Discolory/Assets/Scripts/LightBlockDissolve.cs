using UnityEngine;

public class LightBlockDissolve : MonoBehaviour
{
    [Range(0, 1)]
    public float DissolveValue = 0.0f;
    [Range(0, 0.2f)]
    public float DissolveEdge = 0.0f;

    public float dissolveSpeed;
    public ParticleSystem spark;

    float toDissolve;
    Material mat;
    Color startColor;
    bool psInit;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void SetColor(Color col)
    {
        startColor = col;
        mat.SetColor("_EmissionColor", col);
        toDissolve = 1;
        mat.SetFloat("_Dissolve", toDissolve);
        GetComponent<Collider>().enabled = false;
    }

    public void UpdateIntensity(float i)
    {
        mat.SetColor("_EmissionColor", startColor * i);
    }

    // Update is called once per frame
    void Update()
    {
        if (!psInit && startColor != null)
        {
            ParticleSystem.MainModule main = spark.main;
            main.startColor = startColor;
            psInit = true;
        }

        float currentDissolveValue = mat.GetFloat("_Dissolve");
        if (Mathf.Abs(currentDissolveValue - toDissolve) > .01f)
        {
            currentDissolveValue = Mathf.Lerp(currentDissolveValue, toDissolve, dissolveSpeed * Time.deltaTime);
            mat.SetFloat("_Dissolve", currentDissolveValue);
        }
        else
        {
            mat.SetFloat("_Dissolve", toDissolve);
        }
    }

    public void Dissolve(bool on)
    {
        toDissolve = on ? 0 : 1;
        GetComponent<Collider>().enabled = on;
        if (on)
        {
            spark.Stop();
        }
        else
        {
            spark.Play();
        }
    }

    public bool IsThere()
    {
        return GetComponent<Collider>().enabled;
    }
}
