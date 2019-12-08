using System;
using System.Collections;
using System.IO;
using TarotReadingPlayer.Detection;
using UnityEditor;
using UnityEngine;

public class TarotCardInfoJEditor : EditorWindow
{

    private enum State
    {
        BLANK,
        EDIT,
        ADD
    }

    private State state;
    private int selectedTarot;


    private string newTarotCardName;
    private int newTarotCardNumber;
    private string newTarotKeyword;

    //explanation
    public string curSituation_up;
    public string curSituation_re;
    public string feelings_up;
    public string feelings_re;
    public string cause_up;
    public string cause_re;
    public string future_up;
    public string future_re;
    public string advice_up;
    public string advice_re;

    //practical use
    public string love_up;
    public string love_re;
    public string work_up;
    public string work_re;
    public string interpersonal_up;
    public string interpersonal_re;
    public string other_up;
    public string other_re;

    private static string currentSituation = "現在の状況 ";
    private static string humanFeelings = "人の気持ち ";
    private static string problemCause = "問題の原因 ";
    private static string future = "未来の行く末 ";
    private static string advice = "アドバイス ";

    private static string love = "恋愛 ";
    private static string work = "仕事 ";
    private static string interpersonal = "対人 ";
    private static string other = "その他 ";
    private static string upright = " (正位)";
    private static string reverse = " (逆位)";
    private static string semicolon = " : ";

    private static string cardName = "カード名: ";
    private static string cardNumber = "カード番号: ";
    private static string keyword = "キーワード: ";

    private static string PROJECT_PATH = @"Assets/Database";
    private static string Database_PATH = @"Assets/Database/TarotDB.asset";

    private TarotCardDatabase tarots;
    private Vector2 scrollPos;

    [MenuItem("Tarot Reading Player/Open TarotCardInfo Editor", false, 1)]
    public static void Initialize()
    {
        TarotCardInfoJEditor window = EditorWindow.GetWindow<TarotCardInfoJEditor>();
        window.minSize = new Vector2(800,400);
        window.Show();
    }

    void OnEnable()
    {
        if (tarots == null)
        {
            LoadDatabase();
        }
        state = State.BLANK;
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        DisplayListArea();
        DisplayMainArea();
        EditorGUILayout.EndHorizontal();
    }

    void LoadDatabase()
    {
        tarots = (TarotCardDatabase) AssetDatabase.LoadAssetAtPath(Database_PATH, typeof(TarotCardDatabase));

        if (tarots == null)
        {
            CreateDatabase();
        }
    }

    void CreateDatabase()
    {
        Debug.Log(PROJECT_PATH);
        if (!Directory.Exists(PROJECT_PATH))
        {
            Directory.CreateDirectory(PROJECT_PATH);
        }

        tarots = ScriptableObject.CreateInstance<TarotCardDatabase>();
        AssetDatabase.CreateAsset(tarots, Database_PATH);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    void DisplayListArea()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(250));
        EditorGUILayout.Space();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, "box", GUILayout.ExpandWidth(true));

        for (int cnt = 0; cnt < tarots.count; cnt++)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("-", GUILayout.Width(25)))
            {
                tarots.RemoveAt(cnt);
                tarots.SortTarotNumber();
                EditorUtility.SetDirty(tarots);
                state = State.BLANK;
                return;
            }

            if (GUILayout.Button(tarots.TarotCard(cnt).number+" "+tarots.TarotCard(cnt).cardName, "box", GUILayout.ExpandWidth(true)))
            {
                selectedTarot = cnt;
                state = State.EDIT;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Tarot Cards : " + tarots.count, GUILayout.Width(100));

        if (GUILayout.Button("New Card"))
        {
            state = State.ADD;
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
    }

    void DisplayMainArea()
    {
        EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        switch (state)
        {
           default:
                DisplayBlankMainArea();
                break;
            case State.EDIT:
                DisplayEditMainArea();
                break;
            case State.ADD:
                DisplayAddMainArea();
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
    }

    void DisplayBlankMainArea()
    {
        EditorGUILayout.LabelField("TEST", GUILayout.ExpandHeight(true));
    }

    void DisplayEditMainArea()
    {
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).cardName = EditorGUILayout.TextField(new GUIContent(cardName), tarots.TarotCard(selectedTarot).cardName);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).number = int.Parse(EditorGUILayout.TextField(new GUIContent(cardNumber), tarots.TarotCard(selectedTarot).number.ToString()));
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).keyword = EditorGUILayout.TextField(new GUIContent(keyword), tarots.TarotCard(selectedTarot).keyword);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).curSituation_up = EditorGUILayout.TextField(new GUIContent(currentSituation + upright + semicolon), tarots.TarotCard(selectedTarot).curSituation_up);
        tarots.TarotCard(selectedTarot).curSituation_re = EditorGUILayout.TextField(new GUIContent(currentSituation + reverse + semicolon), tarots.TarotCard(selectedTarot).curSituation_re);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).feelings_up = EditorGUILayout.TextField(new GUIContent(humanFeelings + upright + semicolon), tarots.TarotCard(selectedTarot).feelings_up);
        tarots.TarotCard(selectedTarot).feelings_re = EditorGUILayout.TextField(new GUIContent(humanFeelings + reverse + semicolon), tarots.TarotCard(selectedTarot).feelings_re);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).cause_up = EditorGUILayout.TextField(new GUIContent(problemCause + upright + semicolon), tarots.TarotCard(selectedTarot).cause_up);
        tarots.TarotCard(selectedTarot).cause_re = EditorGUILayout.TextField(new GUIContent(problemCause + reverse + semicolon), tarots.TarotCard(selectedTarot).cause_re);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).future_up = EditorGUILayout.TextField(new GUIContent(future + upright + semicolon), tarots.TarotCard(selectedTarot).future_up);
        tarots.TarotCard(selectedTarot).future_re = EditorGUILayout.TextField(new GUIContent(future + reverse + semicolon), tarots.TarotCard(selectedTarot).future_re);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).advice_up = EditorGUILayout.TextField(new GUIContent(advice + upright + semicolon), tarots.TarotCard(selectedTarot).advice_up);
        tarots.TarotCard(selectedTarot).advice_re = EditorGUILayout.TextField(new GUIContent(advice + reverse + semicolon), tarots.TarotCard(selectedTarot).advice_re);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).love_up = EditorGUILayout.TextField(new GUIContent(love + upright + semicolon), tarots.TarotCard(selectedTarot).love_up);
        tarots.TarotCard(selectedTarot).love_re = EditorGUILayout.TextField(new GUIContent(love + reverse + semicolon), tarots.TarotCard(selectedTarot).love_re);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).work_up = EditorGUILayout.TextField(new GUIContent(work + upright + semicolon), tarots.TarotCard(selectedTarot).work_up);
        tarots.TarotCard(selectedTarot).work_re = EditorGUILayout.TextField(new GUIContent(work + reverse + semicolon), tarots.TarotCard(selectedTarot).work_re);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).interpersonal_up = EditorGUILayout.TextField(new GUIContent(interpersonal + upright + semicolon), tarots.TarotCard(selectedTarot).interpersonal_up);
        tarots.TarotCard(selectedTarot).interpersonal_re = EditorGUILayout.TextField(new GUIContent(interpersonal + reverse + semicolon), tarots.TarotCard(selectedTarot).interpersonal_re);
        EditorGUILayout.Space();
        tarots.TarotCard(selectedTarot).other_up = EditorGUILayout.TextField(new GUIContent(other + upright + semicolon), tarots.TarotCard(selectedTarot).other_up);
        tarots.TarotCard(selectedTarot).other_re = EditorGUILayout.TextField(new GUIContent(other + reverse + semicolon), tarots.TarotCard(selectedTarot).other_re);



        EditorGUILayout.Space();

        if(GUILayout.Button("設定", GUILayout.Width(100)))
        {
            tarots.SortTarotNumber();
            EditorUtility.SetDirty(tarots);
            state = State.BLANK;
        }
    }

    void DisplayAddMainArea()
    {
        newTarotCardName = EditorGUILayout.TextField(new GUIContent(cardName), newTarotCardName);
        newTarotCardNumber = Convert.ToInt32(EditorGUILayout.TextField(new GUIContent(cardNumber), newTarotCardNumber.ToString()));
        newTarotKeyword = EditorGUILayout.TextField(new GUIContent(keyword), newTarotKeyword);
        EditorGUILayout.Space();
        curSituation_up = EditorGUILayout.TextField(new GUIContent(currentSituation + upright + semicolon), curSituation_up);
        curSituation_re = EditorGUILayout.TextField(new GUIContent(currentSituation + reverse + semicolon), curSituation_re);
        EditorGUILayout.Space();
        feelings_up = EditorGUILayout.TextField(new GUIContent(humanFeelings + upright + semicolon), feelings_up);
        feelings_re = EditorGUILayout.TextField(new GUIContent(humanFeelings + reverse + semicolon), feelings_re);
        EditorGUILayout.Space();
        cause_up = EditorGUILayout.TextField(new GUIContent(problemCause + upright + semicolon), cause_up);
        cause_re = EditorGUILayout.TextField(new GUIContent(problemCause + reverse + semicolon), cause_re);
        EditorGUILayout.Space();
        future_up = EditorGUILayout.TextField(new GUIContent(future + upright + semicolon), future_up);
        future_re = EditorGUILayout.TextField(new GUIContent(future + reverse + semicolon), future_re);
        EditorGUILayout.Space();
        advice_up = EditorGUILayout.TextField(new GUIContent(advice + upright + semicolon), advice_up);
        advice_re = EditorGUILayout.TextField(new GUIContent(advice + reverse + semicolon), advice_re);
        EditorGUILayout.Space();
        love_up = EditorGUILayout.TextField(new GUIContent(love + upright + semicolon), love_up);
        love_re = EditorGUILayout.TextField(new GUIContent(love + reverse + semicolon), love_re);
        EditorGUILayout.Space();
        work_up = EditorGUILayout.TextField(new GUIContent(work + upright + semicolon), work_up);
        work_re = EditorGUILayout.TextField(new GUIContent(work + reverse + semicolon), work_re);
        EditorGUILayout.Space();
        interpersonal_up = EditorGUILayout.TextField(new GUIContent(interpersonal + upright + semicolon), interpersonal_up);
        interpersonal_re = EditorGUILayout.TextField(new GUIContent(interpersonal + reverse + semicolon), interpersonal_re);
        EditorGUILayout.Space();
        other_up = EditorGUILayout.TextField(new GUIContent(other + upright + semicolon), other_up);
        other_re = EditorGUILayout.TextField(new GUIContent(other + reverse + semicolon), other_re);

        EditorGUILayout.Space();

        if(GUILayout.Button("Done", GUILayout.Width(100)))
        {
            var tarot = new TarotCardInformation(newTarotCardName, newTarotCardNumber, newTarotKeyword,
                curSituation_up, curSituation_re, feelings_up, feelings_re,
                cause_up, cause_re, future_up, future_re, advice_up, advice_re, love_up, love_re,
                work_up, work_re, interpersonal_up, interpersonal_re, other_up, other_re);
            tarots.Add(tarot);
            tarots.SortTarotNumber();

            newTarotCardName = string.Empty;
            newTarotCardNumber = 0;
            EditorUtility.SetDirty(tarots);
            state = State.BLANK;
        }
    }
}
