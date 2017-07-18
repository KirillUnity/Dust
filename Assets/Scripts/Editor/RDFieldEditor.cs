using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;

public class RDFieldEditor : EditorWindow
{
    #region Подключаемые компоненты и переменные

    private String path = "Resources/Json/level.json";

    //для фильтра филдов дефолтная запись
    private static string DEFAULT_FIELD_NAME = "None";
    private static int CHECK_UNIQUE_CHARS_COUNT = 3;

    //выбранный филд
    private static string _selectedFieldName;
    private static int _selectedFieldIndex;
    //список Name филдов для фильтра
    private static List<string> _knownFields = new List<string>();
    //все наши филды
    private static List<FieldContainer> _conts = new List<FieldContainer>();


    //JsonConvert.DeserializeObject<List<FieldContainer>>("");

    private static FieldContainer? _selectedField;

    Vector2 _scrollPosition;

    bool _showDimensions = true;
    bool _showExitConditions = true;
    bool _showObject = true;

    bool _showMobs = true;

    protected GUIStyle _selectionButtonStyle;
    protected GUIStyle SelectionButtonStyle
    {
        get
        {
            if (_selectionButtonStyle == null)
            {

                _selectionButtonStyle = new GUIStyle(EditorStyles.toolbarButton);
                _selectionButtonStyle.alignment = TextAnchor.MiddleLeft;
            }

            return _selectionButtonStyle;
        }
    }

    #endregion

    #region Editor window init

    private static RDFieldEditor Instance;
    private string WindowHeader = "Редактор полей";

    [MenuItem("Пыль / Редактор уровней / Редактор полей %l")]

    public static void Init()
    {
        Instance = GetWindow<RDFieldEditor>();
        Instance.titleContent = new GUIContent("Field Editor"); ;
        Instance.minSize = new Vector2(800, 600);
    }

    #endregion

    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(1, 1, position.width - 2, position.height - 2));
        {

            #region Header

            GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.Height(20));
            {

                EditorGUILayout.LabelField(WindowHeader, GUILayout.ExpandWidth(true));

            }
            GUILayout.EndVertical();

            #endregion

            #region Left panel 

            GUILayout.BeginArea(new Rect(1, 23, 200, position.height - 26));
            {

                GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                {
                    DrawList();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();

            #endregion

            #region Right panel 

            GUILayout.BeginArea(new Rect(202, 23, position.width - 204, position.height - 26));
            {

                GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
                {

                    DrawSelectedField();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();

            #endregion

        }
        GUILayout.EndArea();
    }

    #region List of fields 

    private void DrawList()
    {
        if (!_conts.Any())
            RecreateContents();

        if (GUILayout.Button("Добавить поле", EditorStyles.toolbarButton))
        {
            AddField();
        }

        if (GUILayout.Button("Сохранить", EditorStyles.toolbarButton))
        {
            SaveFieldButton();
        }

        if (GUILayout.Button("Загрузить", EditorStyles.toolbarButton))
        {
            LoadFieldButton();
        }


        GUILayout.Space(10);
        EditorGUILayout.LabelField("Список полей", EditorStyles.boldLabel);
        var filteredData = _conts;

        for (int i = 0; i < filteredData.Count(); i++)
        {
            var field = filteredData[i].Field;

            var _fieldName = _selectedField != null && filteredData[i].Equals(_selectedField)
                ? String.Format("[{0}] {1}", field.Id, field.Name)
                : String.Format("[{0}] {1}", field.Id, field.Name);

            if (GUILayout.Button(_fieldName, SelectionButtonStyle, GUILayout.ExpandWidth(true)))
            {
                _selectedField = filteredData[i];
            }
        }
    }

    #endregion

    #region Buttons in list

    private void AddField()
    {
        var field = new DataField
        {
            Name = "",
            Width = 5,
            Height = 5,
            RandomEnterPosition = true,
            NeedKillMobs = false
        };

        var cont = new FieldContainer(field)
        {
            Saved = true,
            currentItem = 6,
            currentMob = 3
        };
        _conts.Insert(0, cont);
    }

    private static void RecreateContents()
    {
        _conts.Clear();
        _knownFields.Clear();

        var fields = _conts.Select(c => c.Field.Name).Distinct().ToList();
        fields.Insert(0, DEFAULT_FIELD_NAME);
        _knownFields = fields;
    }

    #endregion

    #region Field properties 

    private void DrawSelectedField()
    {
        if (_selectedField != null)
            DrawField(_selectedField.Value);
    }

    private void DrawField(FieldContainer cont)
    {

        DrawGeneralFieldProperties(_selectedField.Value);

        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
        {

            #region Размерность поля

            GUILayout.Space(4);
            GUILayout.BeginVertical("Box");
            {

                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                {

                    EditorGUILayout.LabelField("Размерность поля", EditorStyles.boldLabel, GUILayout.ExpandWidth(true));

                    if (_showDimensions)
                    {

                        if (GUILayout.Button("Свернуть", EditorStyles.toolbarButton, GUILayout.Width(75)))
                        {
                            _showDimensions = false;
                        }
                    }

                    if (!_showDimensions)
                    {
                        if (GUILayout.Button("Развернуть", EditorStyles.toolbar, GUILayout.Width(75)))
                        {
                            _showDimensions = true;
                        }
                    }
                }
                GUILayout.EndHorizontal();

                if (_showDimensions)
                {

                    GUILayout.BeginVertical("Box");
                    {

                        GUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("Ширина поля", GUILayout.Width(150));
                            cont.Field.Width = EditorGUILayout.IntField(cont.Field.Width, GUILayout.ExpandWidth(true));
                            EditorGUILayout.LabelField("Высота поля", GUILayout.Width(150));
                            cont.Field.Height = EditorGUILayout.IntField(cont.Field.Height, GUILayout.ExpandWidth(true));
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal();
                        {

                            EditorGUILayout.LabelField("Площадь поля", GUILayout.Width(150));
                            EditorGUILayout.LabelField((cont.Field.Width * cont.Field.Height).ToString() + " cells", EditorStyles.boldLabel, GUILayout.ExpandWidth(true));
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();
                }
            }
            GUILayout.EndVertical();

            #endregion

            #region Условия выхода 

            GUILayout.Space(4);
            GUILayout.BeginVertical("Box");
            {

                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                {

                    EditorGUILayout.LabelField("Спаун выхода", EditorStyles.boldLabel, GUILayout.ExpandWidth(true));

                    if (_showExitConditions)
                    {

                        if (GUILayout.Button("Свернуть", EditorStyles.toolbarButton, GUILayout.Width(75)))
                        {
                            _showExitConditions = false;
                        }
                    }

                    if (!_showExitConditions)
                    {
                        if (GUILayout.Button("Развернуть", EditorStyles.toolbarButton, GUILayout.Width(75)))
                        {
                            _showExitConditions = true;
                        }
                    }
                }
                GUILayout.EndHorizontal();

                if (_showExitConditions)
                {

                    GUILayout.BeginVertical("Box");
                    {
                        if (!cont.Field.RandomEnterPosition)
                        {
                            GUILayout.BeginHorizontal();
                            {

                                EditorGUILayout.LabelField("Спаун выхода по х", GUILayout.Width(150));
                                cont.Field._enterPosition.X = EditorGUILayout.IntField(cont.Field._enterPosition.X, GUILayout.ExpandWidth(true));

                                EditorGUILayout.LabelField("Спаун выхода по y", GUILayout.Width(150));
                                cont.Field._enterPosition.Y = EditorGUILayout.IntField(cont.Field._enterPosition.Y, GUILayout.ExpandWidth(true));

                            }
                            GUILayout.EndHorizontal();
                        }

                        GUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("Координаты спауна выхода, выбираются случайно", GUILayout.Width(400));//, GUILayout.ExpandWidth(true));
                            cont.Field.RandomEnterPosition = EditorGUILayout.Toggle(cont.Field.RandomEnterPosition);
                        }
                        GUILayout.EndHorizontal();

                        GUILayout.Space(3);
                    }
                    GUILayout.EndVertical();
                }

            }
            GUILayout.EndVertical();

            #endregion
            #region Объекты

            GUILayout.Space(4);
            GUILayout.BeginVertical("Box");
            {

                GUILayout.BeginHorizontal(EditorStyles.toolbar);
                {

                    EditorGUILayout.LabelField("Колличество объектов", EditorStyles.boldLabel, GUILayout.ExpandWidth(true));

                    if (_showObject)
                    {

                        if (GUILayout.Button("Свернуть", EditorStyles.toolbarButton, GUILayout.Width(75)))
                        {
                            _showObject = false;
                        }
                    }

                    if (!_showObject)
                    {
                        if (GUILayout.Button("Развернуть", EditorStyles.toolbarButton, GUILayout.Width(75)))
                        {
                            _showObject = true;
                        }
                    }
                }
                GUILayout.EndHorizontal();

                if (_showObject)
                {

                    GUILayout.BeginVertical("Box");
                    {
                       
                            GUILayout.BeginHorizontal();
                            {

                                EditorGUILayout.LabelField("Колличество мобов", GUILayout.Width(150));
                                cont.Field._enterPosition.X = EditorGUILayout.IntField(cont.Field._enterPosition.X, GUILayout.ExpandWidth(true));

                                EditorGUILayout.LabelField("Колличество препятсвий", GUILayout.Width(150));
                                cont.Field._enterPosition.Y = EditorGUILayout.IntField(cont.Field._enterPosition.Y, GUILayout.ExpandWidth(true));

                            }
                            GUILayout.EndHorizontal();
                        

                  
                        GUILayout.Space(3);
                    }
                    GUILayout.EndVertical();
                }

            }
            GUILayout.EndVertical();

            #endregion

        }
        GUILayout.EndScrollView();
    }

    private void DrawGeneralFieldProperties(FieldContainer cont)
    {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Space(3);

            EditorGUILayout.LabelField("", GUILayout.ExpandWidth(true));

            if (GUILayout.Button("Удалить", EditorStyles.toolbarButton))
            {

                DeleteFieldButton(cont);
            }

        }
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical("Box");
        {

            EditorGUILayout.LabelField("Общие свойства", EditorStyles.boldLabel, GUILayout.ExpandWidth(true));

            GUILayout.BeginVertical("Box");
            {

                GUILayout.BeginHorizontal();
                {

                    EditorGUILayout.LabelField("Id:", GUILayout.Width(150));
                    cont.Field.Id = EditorGUILayout.LongField(cont.Field.Id, GUILayout.ExpandWidth(true));
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {

                    EditorGUILayout.LabelField("Название:", GUILayout.Width(150));
                    cont.Field.Name = EditorGUILayout.TextField(cont.Field.Name, GUILayout.ExpandWidth(true));
                }
                GUILayout.EndHorizontal();


            }
            GUILayout.EndVertical();
        }
        GUILayout.EndVertical();
    }

    #endregion

    #region Buttons in general properties

    public void SaveFieldButton()
    {
        FileWriter.Write(path, _conts);
    }

    public void LoadFieldButton()
    {
        if(FileWriter.Read(path, Application.dataPath) != null)
           _conts = JsonConvert.DeserializeObject<List<FieldContainer>>(FileWriter.Read(path, Application.dataPath));
        _selectedField = null;
        DrawSelectedField();
    }

    public void DeleteFieldButton(FieldContainer cont)
    {
        _selectedField = null;
        DrawSelectedField();

        if (cont.Saved)
            _conts.Remove(cont);
    }

    #endregion

    #region Utils 

    void FieldProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(400, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
    }

    public static int CompareId(DataField item1, DataField item2)
    {
        if (item1.Id > item2.Id)
            return 1;

        if (item2.Id > item1.Id)
            return -1;

        return 0;
    }

    #endregion
}
