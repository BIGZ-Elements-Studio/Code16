using System;
using UnityEngine;
using UnityEditor;


//����һ��GUI��
public class SampleGUIURP : ShaderGUI
{
    public GUIStyle style = new GUIStyle();
    static bool Foldout(bool display, string title)
    {
        var style = new GUIStyle("ShurikenModuleTitle");
        style.font = new GUIStyle(EditorStyles.boldLabel).font;
        style.border = new RectOffset(15, 7, 4, 4);
        style.fixedHeight = 22;
        style.contentOffset = new Vector2(20f, -2f);
        style.fontSize = 11;
        style.normal.textColor = new Color(0.7f, 0.8f, 0.9f);




        var rect = GUILayoutUtility.GetRect(16f, 25f, style);
        GUI.Box(rect, title, style);

        var e = Event.current;

        var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
        if (e.type == EventType.Repaint)
        {
            EditorStyles.foldout.Draw(toggleRect, false, false, display, false);
        }

        if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
        {
            display = !display;
            e.Use();
        }

        return display;
    }

    static bool _Function_Foldout = false;
    static bool _Base_Foldout = false;
    static bool _Common_Foldout = true;
    static bool _Main_Foldout = true;
    static bool _Tips_Foldout = false;
    static bool _Mask_Foldout = true;
    static bool _Distort_Foldout = true;
    static bool _Dissolve_Foldout = true;
    static bool _FNL_Foldout = true;
    static bool _VTO_Foldout = true;

    MaterialEditor m_MaterialEditor;

    MaterialProperty BlendMode = null;
    MaterialProperty CullMode = null;
    MaterialProperty ZTest = null;

    MaterialProperty MainTex = null;
    MaterialProperty MainColor = null;
    MaterialProperty MainTexAR = null;
    MaterialProperty MainTexUSpeed = null;
    MaterialProperty MainTexVSpeed = null;
    MaterialProperty CustomMainTex = null;

    MaterialProperty FMaskTex = null;
    MaterialProperty MaskTex = null;
    MaterialProperty MaskTexAR = null;
    MaterialProperty MaskTexUSpeed = null;
    MaterialProperty MaskTexVSpeed = null;

    MaterialProperty FDistortTex = null;
    MaterialProperty DistortTex = null;
    MaterialProperty DistortTexAR = null;
    MaterialProperty DistortTexUSpeed = null;
    MaterialProperty DistortTexVSpeed = null;
    MaterialProperty DistortFactor = null;
    MaterialProperty DistortMainTex = null;
    MaterialProperty DistortMaskTex = null;
    MaterialProperty DistortDissolveTex = null;

    MaterialProperty FDissolveTex = null;
    MaterialProperty DissolveTex = null;
    MaterialProperty DissolveTexAR = null;
    MaterialProperty DissolveTexUSpeed = null;
    MaterialProperty DissolveTexVSpeed = null;
    MaterialProperty DissolveFactor = null;
    MaterialProperty DissolveColor = null;
    MaterialProperty CustomDissolve = null;
    MaterialProperty DissolveSoft = null;
    MaterialProperty DissolveWide = null;

    MaterialProperty FFnl = null;
    MaterialProperty FnlColor = null;
    MaterialProperty FnlScale = null;
    MaterialProperty FnlPower = null;
    MaterialProperty ReFnl = null;

    MaterialProperty VTOTex = null;
    MaterialProperty VTOTexAR = null;
    MaterialProperty VTOTexUSpeed = null;
    MaterialProperty VTOTexVSpeed = null;
    MaterialProperty VTOScale = null;
    MaterialProperty IfVTO = null;
    MaterialProperty CustomVTO = null;

    MaterialProperty MainAlpha = null;
    MaterialProperty FDepth = null;
    MaterialProperty DepthFade = null;
    public void FindProperties(MaterialProperty[] props)
    {
        BlendMode = FindProperty("_BlendMode", props);
        CullMode = FindProperty("_CullMode", props);
        ZTest = FindProperty("_ZTest", props);

        MainTex = FindProperty("_MainTex", props);
        MainColor = FindProperty("_MainColor", props);
        MainTexAR = FindProperty("_MainTexAR", props);
        MainTexUSpeed = FindProperty("_MainTexUSpeed", props);
        MainTexVSpeed = FindProperty("_MainTexVSpeed", props);
        CustomMainTex = FindProperty("_CustomMainTex", props);

        FMaskTex = FindProperty("_FMaskTex", props);
        MaskTex = FindProperty("_MaskTex", props);
        MaskTexAR = FindProperty("_MaskTexAR", props);
        MaskTexUSpeed = FindProperty("_MaskTexUSpeed", props);
        MaskTexVSpeed = FindProperty("_MaskTexVSpeed", props);

        FDistortTex = FindProperty("_FDistortTex", props);
        DistortTex = FindProperty("_DistortTex", props);
        DistortTexAR = FindProperty("_DistortTexAR", props);
        DistortTexUSpeed = FindProperty("_DistortTexUSpeed", props);
        DistortTexVSpeed = FindProperty("_DistortTexVSpeed", props);
        DistortFactor = FindProperty("_DistortFactor", props);
        DistortMainTex = FindProperty("_DistortMainTex", props);
        DistortMaskTex = FindProperty("_DistortMaskTex", props);
        DistortDissolveTex = FindProperty("_DistortDissolveTex", props);

        FDissolveTex = FindProperty("_FDissolveTex", props);
        DissolveTex = FindProperty("_DissolveTex", props);
        DissolveTexAR = FindProperty("_DissolveTexAR", props);
        DissolveTexUSpeed = FindProperty("_DissolveTexUSpeed", props);
        DissolveTexVSpeed = FindProperty("_DissolveTexVSpeed", props);
        DissolveFactor = FindProperty("_DissolveFactor", props);
        DissolveColor = FindProperty("_DissolveColor", props);
        CustomDissolve = FindProperty("_CustomDissolve", props);
        DissolveSoft = FindProperty("_DissolveSoft", props);
        DissolveWide = FindProperty("_DissolveWide", props);

        FFnl = FindProperty("_FFnl", props);
        FnlColor = FindProperty("_FnlColor", props);
        FnlScale = FindProperty("_FnlScale", props);
        FnlPower = FindProperty("_FnlPower", props);
        ReFnl = FindProperty("_ReFnl", props);

        VTOTex = FindProperty("_VTOTex", props);
        VTOTexAR = FindProperty("_VTOTexAR", props);
        VTOTexUSpeed = FindProperty("_VTOTexUSpeed", props);
        VTOTexVSpeed = FindProperty("_VTOTexVSpeed", props);
        VTOScale = FindProperty("_VTOScale", props);
        CustomVTO = FindProperty("_CustomVTO", props);
        IfVTO = FindProperty("_IfVTO", props);

        MainAlpha = FindProperty("_MainAlpha", props);
        FDepth = FindProperty("_FDepth", props);
        DepthFade = FindProperty("_DepthFade", props);
    }


    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {




        FindProperties(props);

        m_MaterialEditor = materialEditor;

        Material material = materialEditor.target as Material;

        //
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        _Function_Foldout = Foldout(_Function_Foldout, "���ܶ���");

        if (_Function_Foldout)
        {
            EditorGUI.indentLevel++;
            m_MaterialEditor.ShaderProperty(FMaskTex, "����ģ��");


            GUILayout.Space(5);

            m_MaterialEditor.ShaderProperty(FDistortTex, "UVŤ��ģ��");
            GUILayout.Space(5);

            m_MaterialEditor.ShaderProperty(FDissolveTex, "�ܽ�ģ��");
            GUILayout.Space(5);

            m_MaterialEditor.ShaderProperty(FFnl, "������ģ��");
            GUILayout.Space(5);
            m_MaterialEditor.ShaderProperty(IfVTO, "���㶯��ģ��");
            GUILayout.Space(5);

            m_MaterialEditor.ShaderProperty(FDepth, "������ģ��");
            GUILayout.Space(5);

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndVertical();

        //
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        _Base_Foldout = Foldout(_Base_Foldout, "��������");

        if (_Base_Foldout)
        {
            EditorGUI.indentLevel++;

            GUILayout.Space(5);
            m_MaterialEditor.ShaderProperty(BlendMode, "����ģʽ");
            if (material.GetFloat("_BlendMode") == 0)
            {
                material.SetFloat("_Scr", 5);
                material.SetFloat("_Dst", 10);
            }
            else
            {
                material.SetFloat("_Scr", 1);
                material.SetFloat("_Dst", 1);
            }
            GUILayout.Space(5);

            m_MaterialEditor.ShaderProperty(CullMode, "�޳�ģʽ");
            GUILayout.Space(5);

            m_MaterialEditor.ShaderProperty(ZTest, "��Ȳ���");
            GUILayout.Space(10);

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndVertical();

        //
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        _Main_Foldout = Foldout(_Main_Foldout, "����ͼ");

        if (_Main_Foldout)
        {
            EditorGUI.indentLevel++;

     

     
            m_MaterialEditor.TexturePropertySingleLine(new GUIContent("����ͼ"), MainTex, MainColor);

            GUILayout.Space(5);

            if (MainTex.textureValue != null ) { 
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            m_MaterialEditor.TextureScaleOffsetProperty(MainTex);
            EditorGUILayout.EndVertical();

            GUILayout.Space(5);
            m_MaterialEditor.ShaderProperty(MainTexAR, "RΪ͸��ͨ��");

            GUILayout.Space(5);
              m_MaterialEditor.ShaderProperty(CustomMainTex, "�Զ�������");

            GUILayout.Space(5);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            m_MaterialEditor.ShaderProperty(MainTexUSpeed, "U����");
            m_MaterialEditor.ShaderProperty(MainTexVSpeed, "V����");
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
            }



            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndVertical();


        //
        


     
      


        if (material.GetFloat("_FMaskTex") == 1) {

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _Mask_Foldout = Foldout(_Mask_Foldout, "����ͼ");

        if (_Mask_Foldout)
        {
            EditorGUI.indentLevel++;
        
            m_MaterialEditor.TexturePropertySingleLine(new GUIContent("����ͼ"), MaskTex);

            GUILayout.Space(5);

            if (MaskTex.textureValue != null)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                m_MaterialEditor.TextureScaleOffsetProperty(MaskTex);
                EditorGUILayout.EndVertical();
                GUILayout.Space(5);

                m_MaterialEditor.ShaderProperty(MaskTexAR, "RΪ����ͨ��");
                GUILayout.Space(5);
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                m_MaterialEditor.ShaderProperty(MaskTexUSpeed, "U����");
                m_MaterialEditor.ShaderProperty(MaskTexVSpeed, "V����");
                EditorGUILayout.EndVertical();
                GUILayout.Space(5);
            }



            EditorGUI.indentLevel--;
        }
            EditorGUILayout.EndVertical();

        }
    


        //
        


   
       
       
  


        if (material.GetFloat("_FDistortTex") == 1)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            _Distort_Foldout = Foldout(_Distort_Foldout, "UVŤ��ͼ");

            if (_Distort_Foldout)
            {
                EditorGUI.indentLevel++;

                m_MaterialEditor.TexturePropertySingleLine(new GUIContent("UVŤ��ͼ"), DistortTex);

                GUILayout.Space(5);

                if (DistortTex.textureValue != null)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    m_MaterialEditor.TextureScaleOffsetProperty(DistortTex);
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(5);

                    m_MaterialEditor.ShaderProperty(DistortTexAR, "RΪŤ��ͨ��");
                    GUILayout.Space(5);
                    m_MaterialEditor.ShaderProperty(DistortFactor, "Ť��ǿ��");
                    GUILayout.Space(5);

                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    m_MaterialEditor.ShaderProperty(DistortTexUSpeed, "U����");
                    m_MaterialEditor.ShaderProperty(DistortTexVSpeed, "V����");
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(5);

                    m_MaterialEditor.ShaderProperty(DistortMainTex, "Ť������ͼ");
                    GUILayout.Space(5);
                    if (material.GetFloat("_FMaskTex") == 1) {
                        m_MaterialEditor.ShaderProperty(DistortMaskTex, "Ť������ͼ");
                    GUILayout.Space(5);
                    }

                    if (material.GetFloat("_FDissolveTex") == 1) {
                        m_MaterialEditor.ShaderProperty(DistortDissolveTex, "Ť���ܽ�ͼ");
                    GUILayout.Space(5);
                    }
                }



                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndVertical();
        }


        //









        if (material.GetFloat("_FDissolveTex") == 1)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            _Dissolve_Foldout = Foldout(_Dissolve_Foldout, "�ܽ�ͼ");

            if (_Dissolve_Foldout)
            {
                EditorGUI.indentLevel++;

                m_MaterialEditor.TexturePropertySingleLine(new GUIContent("�ܽ�ͼ"), DissolveTex,DissolveColor);

                GUILayout.Space(5);

                if (DissolveTex.textureValue != null)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    m_MaterialEditor.TextureScaleOffsetProperty(DissolveTex);
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(5);

                    m_MaterialEditor.ShaderProperty(DissolveTexAR, "RΪ�ܽ�ͨ��");
                    GUILayout.Space(5);

                    m_MaterialEditor.ShaderProperty(CustomDissolve, "�Զ�������");
                    GUILayout.Space(5);
                    if (material.GetFloat("_CustomDissolve") == 0) {
                        m_MaterialEditor.ShaderProperty(DissolveFactor, "�ܽ�̶�");
                    GUILayout.Space(5);
                    }
                    m_MaterialEditor.ShaderProperty(DissolveSoft, "���̶�");
                    GUILayout.Space(5);
                    m_MaterialEditor.ShaderProperty(DissolveWide, "�ܽ���");
                    GUILayout.Space(5);
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    m_MaterialEditor.ShaderProperty(DissolveTexUSpeed, "U����");
                    m_MaterialEditor.ShaderProperty(DissolveTexVSpeed, "V����");
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(5);

                  
                }



                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndVertical();
        }

        //







        if (material.GetFloat("_FFnl") == 1)
        {

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _FNL_Foldout = Foldout(_FNL_Foldout, "������");

            if (_FNL_Foldout)
            {
                EditorGUI.indentLevel++;

                m_MaterialEditor.ShaderProperty(ReFnl, "���������");
                GUILayout.Space(5);
                m_MaterialEditor.ShaderProperty(FnlScale, "������ǿ��");
                GUILayout.Space(5);
                m_MaterialEditor.ShaderProperty(FnlPower, "��������");
                GUILayout.Space(5);
                if (material.GetFloat("_ReFnl") == 0) {
                    m_MaterialEditor.ShaderProperty(FnlColor, "��������ɫ");
                    GUILayout.Space(5);
                }


                    EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

        }


        //

        if (material.GetFloat("_IfVTO") == 1)
        {

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            _VTO_Foldout = Foldout(_VTO_Foldout, "����ͼ");

            if (_VTO_Foldout)
            {
                EditorGUI.indentLevel++;

                m_MaterialEditor.TexturePropertySingleLine(new GUIContent("����ͼ"), VTOTex);

                GUILayout.Space(5);
                if (VTOTex.textureValue != null)
                {

                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    m_MaterialEditor.TextureScaleOffsetProperty(VTOTex);
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(5);

                    m_MaterialEditor.ShaderProperty(VTOTexAR, "RΪͨ��");
                    GUILayout.Space(5);

                    m_MaterialEditor.ShaderProperty(CustomVTO, "�Զ�������");
                    GUILayout.Space(5);
                    if (material.GetFloat("_CustomVTO") == 0)
                    {
                        m_MaterialEditor.ShaderProperty(VTOScale, "���ͳ̶�");
                        GUILayout.Space(5);
                    }
          
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                    m_MaterialEditor.ShaderProperty(VTOTexUSpeed, "U����");
                    m_MaterialEditor.ShaderProperty(VTOTexVSpeed, "V����");
                    EditorGUILayout.EndVertical();
                    GUILayout.Space(5);


                }


                    EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();

        }
        //

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        _Common_Foldout = Foldout(_Common_Foldout, "�ۺ�����");

        if (_Common_Foldout)
        {
            EditorGUI.indentLevel++;
            m_MaterialEditor.ShaderProperty(MainAlpha, "��͸����");
            GUILayout.Space(5);
            if (material.GetFloat("_FDepth") == 1)
            {
                m_MaterialEditor.ShaderProperty(DepthFade, "������");
                GUILayout.Space(5);

            }
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUI_Common(material);




            EditorGUILayout.EndVertical();

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndVertical();

      

        //
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        _Tips_Foldout = Foldout(_Tips_Foldout, "˵��");

        if (_Tips_Foldout)
        {
            EditorGUI.indentLevel++;

            style.fontSize = 12;
            style.normal.textColor = new Color(0.5f, 0.5f, 0.5f);
            style.wordWrap = true;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label(" 1.�����Զ�������ʱ�������uv2�������custom1.xyzw,�����custom2.xyzw", style);

            GUILayout.Space(5); GUILayout.Label(" 2.custom1.xy��������ͼuvƫ��", style);

            GUILayout.Space(5); GUILayout.Label(" 3.custom1.z�����ܽ�̶�", style);

            GUILayout.Space(5); GUILayout.Label(" 3.custom1.w�������ͳ̶�", style);

            GUILayout.Space(10);
            EditorGUILayout.EndVertical();

            GUILayout.Label(" �������ɻ���è������", style);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndVertical();
    }


    void GUI_Common(Material material)
    {
       
        EditorGUI.BeginChangeCheck();
        {
            MaterialProperty[] props = { };
            base.OnGUI(m_MaterialEditor, props);
        }

    }

}