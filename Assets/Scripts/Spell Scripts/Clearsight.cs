using UnityEngine;
using System.Linq;

public class Clearsight : Spell, ISelfSpell
{

    public override string Name
    {
        get { return "Clearsight"; }
    }

    public override SpellAffectType AffectType
    {
        get { return SpellAffectType.Allies; }
    }

    protected override void InitializeSpellValues()
    {
        manaCost = 25;
        castTime = 2.0f;
    }

    public void Execute(GameObject caster)
    {
        caster.AddComponent<ClearsightBuff>();
    }
}

public class ClearsightBuff : Buff
{

    protected override float duration
    {
        get { return 15.0f; }
    }

    protected override void AddBuffEffects()
    {
        gameObject.AddComponent<ClearsightEffect>();
    }

    protected override void RemoveBuffEffects()
    {
        Destroy(GetComponent<ClearsightEffect>());
    }
}

public class ClearsightEffect : MonoBehaviour
{
    float range = 10f;

    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(gameObject.transform.position + (Vector3.up * 2f), gameObject.transform.forward, range);
        foreach (RaycastHit hit in hits)
        {
            GameObject go = hit.collider.gameObject;
            foreach (Renderer R in go.GetComponentsInChildren<Renderer>())
            {
                AutoTransparent AT = R.GetComponent<AutoTransparent>();
                if (AT == null) // if no script is attached, attach one
                {
                    AT = R.gameObject.AddComponent<AutoTransparent>();
                }
                AT.BeTransparent(); // get called every frame to reset the falloff        
            }
        }

        float cameraDistanceToPlayer = Vector3.Distance(Camera.main.transform.position, gameObject.transform.position);
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(mouseRay.origin, mouseRay.direction, range *2f);
        foreach (RaycastHit hit in hits)
        {
            if (Vector3.Distance(hit.point, gameObject.transform.position) <= (range + cameraDistanceToPlayer * 1.5f) || Vector3.Distance(hit.collider.bounds.center, gameObject.transform.position) <= (range + cameraDistanceToPlayer * 1.5f))
            {
                GameObject go = hit.collider.gameObject;
                foreach (Renderer R in go.GetComponentsInChildren<Renderer>())
                {
                    AutoTransparent AT = R.GetComponent<AutoTransparent>();
                    if (AT == null) // if no script is attached, attach one
                    {
                        AT = R.gameObject.AddComponent<AutoTransparent>();
                    }
                    AT.BeTransparent(); // get called every frame to reset the falloff        
                }
            }
        }
    }
}

public class AutoTransparent : MonoBehaviour
{
    private Shader m_OldShader = null;
    private Color m_OldColor = Color.black;
    private float m_Transparency = 0.3f;
    private const float m_TargetTransparancy = 0.3f;
    private const float m_FallOff = 0.1f; // returns to 100% in 0.1 sec

    public void BeTransparent()
    {
        // reset the transparency;
        m_Transparency = m_TargetTransparancy;
        if (m_OldShader == null)
        {
            // Save the current shader
            if (renderer.material.HasProperty("_Color"))
                m_OldColor = renderer.material.color;

            m_OldShader = renderer.material.shader;
            renderer.material.shader = Shader.Find("Transparent/Diffuse");
        }
    }
    void Update()
    {
        if (m_Transparency < 1.0f)
        {
            Color C = renderer.material.color;
            C.a = m_Transparency;
            renderer.material.color = C;
        }
        else
        {
            // Reset the shader
            renderer.material.shader = m_OldShader;
            renderer.material.color = m_OldColor;
            // And remove this script
            Destroy(this);
        }
        m_Transparency += ((1.0f - m_TargetTransparancy) * Time.deltaTime) / m_FallOff;
    }
}