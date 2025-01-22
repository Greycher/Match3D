using MatchHotel.Configuration;
using MatchHotel.Model;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ItemAddress))]
    public class ItemAddressDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var config = GameContext.Instance.itemConfiguration;
            if (!config)
            {
                EditorGUI.LabelField(position, $"{nameof(ItemConfiguration)} is missing at {nameof(GameContext)}!");
                return;
            }
            
            if (config.items.Count == 0)
            {
                EditorGUI.LabelField(position, $"There is no item at{nameof(ItemConfiguration)}!");
                return;
            }
            
            property = property.FindPropertyRelative(nameof(ItemAddress.itemID));

            var dic = config.items;
            var options = new GUIContent[dic.Count];
            var ids = new int[dic.Count];
            var e = dic.GetEnumerator();
            var i = 0;
            var selectedIndex = 0;
            while (e.MoveNext())
            {
                options[i] = new GUIContent(e.Current.Value.itemName);
                ids[i] = e.Current.Key;
                if (ids[i] == property.intValue)
                {
                    selectedIndex = i;
                }

                i++;
            }
            e.Dispose();
            
            EditorGUI.BeginChangeCheck();
            selectedIndex = EditorGUI.Popup(position, label, selectedIndex, options);
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = ids[selectedIndex];
            }
        }
    }
}