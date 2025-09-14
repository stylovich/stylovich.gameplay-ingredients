using UnityEngine;
using TriInspector;

namespace GameplayIngredients
{
    [HelpURL(Help.URL + "counters")]
    [AddComponentMenu(ComponentMenu.counterPath + "Counter")]
    [AdvancedHierarchyIcon("Packages/com.stylovich.gameplay-ingredients/Icons/Misc/ic-counter.png")]
    [DeclareBoxGroup("Default Value", Title = "Default Value")]

    public class Counter : GameplayIngredientsBehaviour
    {
        public enum ValueSourceType
        {
            Property,
            GlobalVariable,
            GameSave,
        }
        [Group("Default Value")]
        public ValueSourceType ValueSource = ValueSourceType.Property;
        [Group("Default Value"), ShowIf("isValueProperty")]
        public int Value = 1;

        [Group("Default Value"), ShowIf("isValueGameSave")]
        public string GameSaveVariableName = "Variable";
        [Group("Default Value"), ShowIf("isValueGameSave")]
        public GameSaveManager.Location GameSaveLocation = GameSaveManager.Location.System;

        [Group("Default Value"), ShowIf("isValueGlobal")]
        public string GlobalVariableName = "Variable";
        [Group("Default Value"), ShowIf("isValueGlobal")]
        public Globals.Scope GlobalScope = Globals.Scope.Global;

        public int CurrentValue { get; private set; }

        public Callable[] OnValueChanged;

        bool isValueProperty() { return ValueSource == ValueSourceType.Property; }
        bool isValueGameSave() { return ValueSource == ValueSourceType.GameSave; }
        bool isValueGlobal() { return ValueSource == ValueSourceType.GlobalVariable; }

        void Awake()
        {
            int value;
            switch (ValueSource)
            {
                default:
                case ValueSourceType.Property:
                    value = Value;
                    break;
                case ValueSourceType.GlobalVariable:
                    if (Globals.HasInt(GlobalVariableName, GlobalScope))
                        value = Globals.GetInt(GlobalVariableName, GlobalScope);
                    else
                    {
                        Debug.LogWarning($"CounterLogic ({name}) : Could not find Global integer {GlobalVariableName}({GlobalScope})");
                        value = 0;
                    }
                    break;
                case ValueSourceType.GameSave:
                    var gsm = Manager.Get<GameSaveManager>();

                    if (gsm.HasInt(GameSaveVariableName, GameSaveLocation))
                        value = gsm.GetInt(GameSaveVariableName, GameSaveLocation);
                    else
                    {
                        Debug.LogWarning($"CounterLogic ({name}) : Could not find Game Save integer {GameSaveVariableName}({GameSaveLocation})");
                        value = 0;
                    }
                    break;
            }

            CurrentValue = value;
        }

        public void SetValue(int newValue, GameObject instigator = null)
        {
            if (newValue != CurrentValue)
            {
                CurrentValue = newValue;
                Callable.Call(OnValueChanged, instigator);
            }
        }

    }
}

