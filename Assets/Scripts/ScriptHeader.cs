using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour), true), CanEditMultipleObjects]
public class ScriptHeader : Editor
{
	private const float RECT_HEIGHT = 30f;

	[ColorUsage(true, true)] public Color rectangleColor = new Color(0.03f, 0.01f, 0.57f);
	[ColorUsage(true, true)] public Color textColor_ = Color.white;

	int fontSize_ = 20;
	float textXOffset = 25f;
	Rect headerRect;

	public override void OnInspectorGUI()
	{
		if (target == null)
			return;

		DrawInspectorRect();
		DrawInspectorLabel();


		EditorGUILayout.Space();

		DrawDefaultInspector();
	}

	void DrawInspectorRect()
	{
		headerRect = EditorGUILayout.GetControlRect(false, RECT_HEIGHT);
		headerRect.xMin = 10f;
		headerRect.xMax = headerRect.width + 20f;

		EditorGUI.DrawRect(headerRect, rectangleColor);
	}

	void DrawInspectorLabel()
	{
		Rect labelRect = headerRect;
		labelRect.xMin = textXOffset;
		labelRect.yMin += 0f;

		var textStyle = new GUIStyle(EditorStyles.boldLabel)
		{
			fontSize = fontSize_,
			alignment = TextAnchor.MiddleLeft,
			normal = { textColor = textColor_ }
		};

		string labelText = SplitScriptName();

		GUI.Label(labelRect, labelText, textStyle);
	}

	string SplitScriptName()
	{
		string labelText = target.GetType().Name;
		var words = labelText.ToCharArray().Select((c, i) => char.IsUpper(c) && i > 0 ? " " + c : c.ToString());
		labelText = string.Join("", words);
		return labelText;
	}
}