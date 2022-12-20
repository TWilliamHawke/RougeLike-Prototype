using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Map.Actions;

namespace CustomEditors
{
	[CustomEditor(typeof(MapActionTemplate))]
	public class MapActionTemplateEditor : SimpleEditor
	{
		protected override void DrawProperties()
		{
			DrawPropertyField("_displayName");
			DrawPropertyField("_icon");
			DrawPropertyField("_actionType");

			var actionType = (target as MapActionTemplate)?.actionType;

			System.Action action = actionType switch
			{
				MapActionType.loot => DrawLootFields,
				MapActionType.attack => DrawAttackFields,
				_ => DrawNothing,
			};

			action.Invoke();
		}

		private void DrawLootFields()
		{
			DrawPropertyField("_lootTable");
			DrawPropertyField("_lootDescription");
		}

		private void DrawAttackFields()
		{
			DrawPropertyField("_enemyFaction");
		}

		private void DrawNothing()
		{
		}
	}
}


