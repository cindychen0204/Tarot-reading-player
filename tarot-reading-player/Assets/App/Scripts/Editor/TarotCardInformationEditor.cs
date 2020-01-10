using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace TarotReadingPlayer.Information.Editor
{
    public class TarotCardInformationDatabaseEditor : EditorWindow
    {
        private enum State
        {
            BLANK,
            EDIT,
            ADD
        }

        private State dataState;
        private int selectedCard;
        private static string DatabasePath = @"Assets/Database";
        private static string DatabaseAssetPath = @"Assets/Database/TarotDB.asset";

        private TarotCardDatabaseObject databaseObject;
        private Vector2 scrollPosition;

        #region タロットカードのプロパティ
        private string newTarotCardName;
        private string newTarotCardEnglishName;
        private int newTarotCardNumber = -1;
        private string newTarotKeyword;

        //explanation
        public string currentSituation_up;
        public string currentSituation_re;
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
        
        //保存のファイル名になる
        private static string cardEnglishName = "英語名称（スペースを\"_\"に）: ";
        private static string cardName = "カード名: ";
        private static string cardNumber = "カード番号: ";
        private static string keyword = "キーワード: ";

        #endregion

        [MenuItem("TarotCard Reading Player/Open TarotCardInfo Editor", false, 1)]
        public static void Initialize()
        {
            var window = GetWindow<TarotCardInformationDatabaseEditor>();
            window.minSize = new Vector2(800,600);
            window.Show();
        }

        #region Databaseの前処理
        void OnEnable()
        {
            if (databaseObject == null)
            {
                LoadDatabase();
            }
            dataState = State.BLANK;
        }

        void LoadDatabase()
        {
            databaseObject = (TarotCardDatabaseObject)AssetDatabase.LoadAssetAtPath(DatabaseAssetPath, typeof(TarotCardDatabaseObject));

            if (databaseObject == null)
            {
                CreateDatabase();
            }
        }

        void CreateDatabase()
        {
            if (!Directory.Exists(DatabasePath))
            {
                Directory.CreateDirectory(DatabasePath);
            }

            databaseObject = CreateInstance<TarotCardDatabaseObject>();
            AssetDatabase.CreateAsset(databaseObject, DatabaseAssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        #endregion

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            DisplayListArea();
            DisplayMainArea();
            EditorGUILayout.EndHorizontal();
        }

        void DisplayListArea()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(250));
            EditorGUILayout.Space();

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, "box", GUILayout.ExpandWidth(true));

            for (int i = 0; i < databaseObject.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button(databaseObject.TarotCard(i).number + " " + databaseObject.TarotCard(i).cardName, "box", GUILayout.ExpandWidth(true)))
                {
                    selectedCard = i;
                    dataState = State.EDIT;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField("カードの数 : " + databaseObject.Count, GUILayout.Width(100));

            if (GUILayout.Button("新しいカードを登録"))
            {
                dataState = State.ADD;
            }

            if (GUILayout.Button("一括ファイル生成"))
            {
                for (int cnt = 0; cnt < databaseObject.Count; cnt++)
                {
                    var progress = (float) cnt / databaseObject.Count;
                    EditorUtility.DisplayProgressBar("ファイル作成中", databaseObject.TarotCard(cnt).cardEngName + ".txt",  progress);

                    var jsonString = CreateTarotInformationJson(databaseObject.TarotCard(cnt));
                    var filename = databaseObject.TarotCard(cnt).number + "_" + databaseObject.TarotCard(cnt).cardEngName + ".json";
                    SaveFile(jsonString, filename);
                }
                EditorUtility.ClearProgressBar();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }

        void DisplayMainArea()
        {
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            EditorGUILayout.Space();

            switch (dataState)
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
            var notice = "\n ここは以下の情報を登録できます："
                               + "\n\n [1]タロットカードの名前"
                               + "\n\n [2]タロットカードの番号"
                               + "\n\n [3]タロットカードのキーワード"
                               + "\n\n [4]タロットカードの解釈（正逆位全部で10項目）"
                               + "\n\n [4]タロットカードの応用（正逆位全部で8項目）";

            EditorGUILayout.LabelField(notice, GUILayout.ExpandHeight(true));
            if (databaseObject.Count == 0 || GUILayout.Button("カード情報をJsonから読み込む"))
            {
                RetrieveFromDatabase();
            }
        }

        void DisplayEditMainArea()
        {
            var selectCard = databaseObject.TarotCard(selectedCard);

            #region レイアウト

            EditorGUILayout.Space();
            selectCard.cardName = EditorGUILayout.TextField(new GUIContent(cardName), selectCard.cardName);
            EditorGUILayout.Space();
            selectCard.cardEngName = EditorGUILayout.TextField(new GUIContent(cardEnglishName), selectCard.cardEngName);
            EditorGUILayout.Space();
            selectCard.number =
                int.Parse(EditorGUILayout.TextField(new GUIContent(cardNumber), selectCard.number.ToString()));
            EditorGUILayout.Space();
            selectCard.keyword = EditorGUILayout.TextField(new GUIContent(keyword), selectCard.keyword);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            selectCard.curSituation_up =
                EditorGUILayout.TextField(new GUIContent(currentSituation + upright + semicolon),
                    selectCard.curSituation_up);
            selectCard.curSituation_re =
                EditorGUILayout.TextField(new GUIContent(currentSituation + reverse + semicolon),
                    selectCard.curSituation_re);
            EditorGUILayout.Space();
            selectCard.feelings_up = EditorGUILayout.TextField(new GUIContent(humanFeelings + upright + semicolon),
                selectCard.feelings_up);
            selectCard.feelings_re = EditorGUILayout.TextField(new GUIContent(humanFeelings + reverse + semicolon),
                selectCard.feelings_re);
            EditorGUILayout.Space();
            selectCard.cause_up =
                EditorGUILayout.TextField(new GUIContent(problemCause + upright + semicolon), selectCard.cause_up);
            selectCard.cause_re =
                EditorGUILayout.TextField(new GUIContent(problemCause + reverse + semicolon), selectCard.cause_re);
            EditorGUILayout.Space();
            selectCard.future_up =
                EditorGUILayout.TextField(new GUIContent(future + upright + semicolon), selectCard.future_up);
            selectCard.future_re =
                EditorGUILayout.TextField(new GUIContent(future + reverse + semicolon), selectCard.future_re);
            EditorGUILayout.Space();
            selectCard.advice_up =
                EditorGUILayout.TextField(new GUIContent(advice + upright + semicolon), selectCard.advice_up);
            selectCard.advice_re =
                EditorGUILayout.TextField(new GUIContent(advice + reverse + semicolon), selectCard.advice_re);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            selectCard.love_up =
                EditorGUILayout.TextField(new GUIContent(love + upright + semicolon), selectCard.love_up);
            selectCard.love_re =
                EditorGUILayout.TextField(new GUIContent(love + reverse + semicolon), selectCard.love_re);
            EditorGUILayout.Space();
            selectCard.work_up =
                EditorGUILayout.TextField(new GUIContent(work + upright + semicolon), selectCard.work_up);
            selectCard.work_re =
                EditorGUILayout.TextField(new GUIContent(work + reverse + semicolon), selectCard.work_re);
            EditorGUILayout.Space();
            selectCard.interpersonal_up = EditorGUILayout.TextField(new GUIContent(interpersonal + upright + semicolon),
                selectCard.interpersonal_up);
            selectCard.interpersonal_re = EditorGUILayout.TextField(new GUIContent(interpersonal + reverse + semicolon),
                selectCard.interpersonal_re);
            EditorGUILayout.Space();
            selectCard.other_up =
                EditorGUILayout.TextField(new GUIContent(other + upright + semicolon), selectCard.other_up);
            selectCard.other_re =
                EditorGUILayout.TextField(new GUIContent(other + reverse + semicolon), selectCard.other_re);
            EditorGUILayout.Space();

            #endregion

            if (GUILayout.Button("Jsonファイルを作成して保存", GUILayout.Width(200), GUILayout.Height(100)))
            {
                databaseObject.SortTarotNumber();
                EditorUtility.SetDirty(databaseObject);

                //Cloud use
                var cardInfo = selectCard.cardEngName;
                var filename = cardInfo + ".txt";

                var jsonString =  CreateTarotInformationJson(selectCard);
                filename = selectCard.number + "_" + selectCard.cardEngName + ".json";
                SaveFile(jsonString, filename);

                dataState = State.BLANK;
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("このカード削除", GUILayout.Width(100)))
            {
                Debug.Log("Deleted data from database:"+ selectCard.cardEngName);
                databaseObject.Remove(selectCard);
                databaseObject.SortTarotNumber();
                EditorUtility.SetDirty(databaseObject);
                dataState = State.BLANK;
            }
        }

        void RetrieveFromDatabase()
        {
            databaseObject.Clear();
            foreach (string file in Directory.EnumerateFiles(DatabasePath, "*.json"))
            {
                var jsonString = File.ReadAllText(file);
                var tarots = JsonUtility.FromJson<TarotDatabaseInformations>(jsonString);
                databaseObject.Add(tarots.TarotCard[0]);
            }
            databaseObject.SortTarotNumber();
            Debug.Log("情報を回復しました");
        }

        string CreateTarotInformationJson(TarotCardInformation tarotInfo)
        {
            var list = new List<TarotCardInformation> {tarotInfo};
            TarotDatabaseInformations database = new TarotDatabaseInformations();
            database.TarotCard = list.ToArray();
            return JsonUtility.ToJson(database, true);
        }

        void SaveFile(string content, string filename)
        {
            var filepath = Path.Combine(DatabasePath, filename);

            using (var fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                using (var r = new StreamWriter(fs))
                {
                    r.Write(content);
                }
            }

            AssetDatabase.ImportAsset(filepath);
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(filepath);
            EditorGUIUtility.PingObject(Selection.activeObject);
        }

        void DisplayAddMainArea()
        {
            newTarotCardName = EditorGUILayout.TextField(new GUIContent(cardName), newTarotCardName);
            newTarotCardEnglishName = EditorGUILayout.TextField(new GUIContent(cardEnglishName), newTarotCardEnglishName);
            newTarotCardNumber = Convert.ToInt32(EditorGUILayout.TextField(new GUIContent(cardNumber), newTarotCardNumber.ToString()));
            newTarotKeyword = EditorGUILayout.TextField(new GUIContent(keyword), newTarotKeyword);
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            currentSituation_up = EditorGUILayout.TextField(new GUIContent(currentSituation + upright + semicolon), currentSituation_up);
            currentSituation_re = EditorGUILayout.TextField(new GUIContent(currentSituation + reverse + semicolon), currentSituation_re);
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
            EditorGUILayout.Space();
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

            if (GUILayout.Button("Jsonファイルを作成して保存", GUILayout.Width(200), GUILayout.Height(100)))
            {
                var tarot = new TarotCardInformation(newTarotCardName,
                    newTarotCardEnglishName,
                    newTarotCardNumber, 
                    newTarotKeyword,
                    currentSituation_up, 
                    currentSituation_re, 
                    feelings_up, 
                    feelings_re,
                    cause_up, 
                    cause_re, 
                    future_up, 
                    future_re, 
                    advice_up, 
                    advice_re, 
                    love_up, 
                    love_re,
                    work_up, 
                    work_re, 
                    interpersonal_up, 
                    interpersonal_re, 
                    other_up, 
                    other_re);

                var jsonString = CreateTarotInformationJson(tarot);
                var filename = tarot.number + "_" + tarot.cardEngName + ".json";
                SaveFile(jsonString, filename);

                databaseObject.Add(tarot);
                databaseObject.SortTarotNumber();
                EditorUtility.SetDirty(databaseObject);
                dataState = State.BLANK;
                ResetParameters();
            }
        }

        void ResetParameters()
        {
            newTarotCardName = string.Empty;
            newTarotCardEnglishName = string.Empty;
            newTarotCardNumber = -1;
            newTarotKeyword = string.Empty;
            currentSituation_up = string.Empty;
            currentSituation_re = string.Empty;
            feelings_up = string.Empty;
            feelings_re = string.Empty;
            cause_up = string.Empty;
            cause_re = string.Empty;
            future_up = string.Empty;
            future_re = string.Empty;
            advice_up = string.Empty;
            advice_re = string.Empty;
            love_up = string.Empty;
            love_re = string.Empty;
            work_up = string.Empty;
            work_re = string.Empty;
            interpersonal_up = string.Empty;
            interpersonal_re = string.Empty;
            other_up = string.Empty;
            other_re = string.Empty;
        }
    }
}
