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
using System;
using SabberStoneCore.Enums;

namespace SabberStoneCore.Model
{
	/// <summary>
	/// Instance which monitors the provided <see cref="Game"/> and invokes the correct
	/// methods when a state change is detected.
	/// </summary>
	/// <autogeneratedoc />
	public class GameEventManager
	{
		private Game _game;

		/// <summary>Initializes a new instance of the <see cref="GameEventManager"/> class.</summary>
		/// <param name="game">The game.</param>
		/// <autogeneratedoc />
		public GameEventManager(Game game)
		{
			_game = game;
			game.EntityChangedEvent += EntityChangedEvent;
		}

		private void EntityChangedEvent(object sender, GameTag t, int oldValue, int newValue)
		{
			//var entity = (Entity)sender;
			//_game.Dump("EntityChangedEvent", $"{sender} - {t}, old: {oldValue}, new: {newValue}");


			if (t == GameTag.NEXT_STEP)
			{
				NextStepEvent(sender as Game, (Step)newValue);
			}
		}

		/// <summary>Invokes the method which corresponds to the next simulation step.</summary>
		/// <param name="game">The game subject.</param>
		/// <param name="step">The (next) step value.</param>
		/// <exception cref="ArgumentOutOfRangeException">step - when the provided step is unknown</exception>
		public void NextStepEvent(Game game, Step step)
		{
			_game.Log(LogLevel.DEBUG, BlockType.TRIGGER, "Event", !_game.Game.Logging? "":$"NextStepEvent - {step}");
			switch (step)
			{
				case Step.BEGIN_FIRST:
					game.Step = step;
					game.BeginFirst();
					break;

				case Step.BEGIN_SHUFFLE:
					game.Step = step;
					game.BeginShuffle();
					break;

				case Step.BEGIN_DRAW:
					game.Step = step;
					game.BeginDraw();
					break;

				case Step.BEGIN_MULLIGAN:
					game.Step = step;
					game.BeginMulligan();
					break;

				case Step.MAIN_BEGIN:
					game.Step = step;
					game.MainBegin();
					break;

				case Step.MAIN_DRAW:
					game.Step = step;
					game.MainDraw();
					break;

				case Step.MAIN_READY:
					game.Step = step;
					game.MainReady();
					break;

				case Step.MAIN_RESOURCE:
					game.Step = step;
					game.MainRessources();
					break;

				case Step.MAIN_START:
					game.Step = step;
					game.MainStart();
					break;

				case Step.MAIN_START_TRIGGERS:
					game.Step = step;
					game.MainStartTriggers();
					break;

				case Step.MAIN_ACTION:
					game.Step = step;
					break;

				case Step.MAIN_COMBAT:
					break;

				case Step.MAIN_CLEANUP:
					game.Step = step;
					game.MainCleanUp();
					break;

				case Step.MAIN_END:
					game.Step = step;
					game.MainEnd();
					break;

				case Step.MAIN_NEXT:
					game.Step = step;
					game.MainNext();
					break;

				case Step.FINAL_WRAPUP:
					game.FinalWrapUp();
					break;

				case Step.FINAL_GAMEOVER:
					game.FinalGameOver();
					break;

				case Step.INVALID:
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(step), step, null);
			}
		}
	}
}
