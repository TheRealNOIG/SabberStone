﻿#region copyright
// SabberStone, Hearthstone Simulator in C# .NET Core
// Copyright (C) 2017-2019 SabberStone Team, darkfriend77 & rnilva
//
// SabberStone is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License.
// SabberStone is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
#endregion
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCore.Model.Entities;

namespace SabberStoneCore.Tasks.SimpleTasks
{
	public class RandomMinionNumberTask : SimpleTask
	{
		public RandomMinionNumberTask(GameTag tag)
		{
			Tag = tag;
		}

		public GameTag Tag { get; set; }

		public override TaskState Process()
		{
			List<Card> cardsList;
			if (Tag == GameTag.COST)
			{
				Cards.CostMinionCards(Game.FormatType).TryGetValue(Number, out cardsList);
				if (cardsList == null)
					return TaskState.STOP;
			}
			else
			{
				IEnumerable<Card> cards = Game.FormatType == FormatType.FT_STANDARD ? Cards.AllStandard : Cards.AllWild;
				cardsList = cards.Where(p => p.Type == CardType.MINION && p[Tag] == Number).ToList();
				if (!cardsList.Any())
					return TaskState.STOP;
			}

			IPlayable playable = Entity.FromCard(Controller, Util.Choose(cardsList));
			Playables = new List<IPlayable> { playable };

			Game.OnRandomHappened(true);

			return TaskState.COMPLETE;
		}

		public override ISimpleTask Clone()
		{
			var clone = new RandomMinionNumberTask(Tag);
			clone.Copy(this);
			return clone;
		}
	}
}
