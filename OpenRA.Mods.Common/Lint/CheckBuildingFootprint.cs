#region Copyright & License Information
/*
 * Copyright 2007-2017 The OpenRA Developers (see AUTHORS)
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System;
using System.Linq;
using OpenRA.Mods.Common.Traits;
using OpenRA.Traits;

namespace OpenRA.Mods.Common.Lint
{
	class CheckBuildingFootprint : ILintRulesPass
	{
		public void Run(Action<string> emitError, Action<string> emitWarning, Ruleset rules)
		{
			foreach (var actorInfo in rules.Actors)
			{
				var building = actorInfo.Value.TraitInfoOrDefault<BuildingInfo>();
				if (building == null)
					continue;

				var footprint = building.Footprint.Where(x => !char.IsWhiteSpace(x)).ToArray();
				var dimension = building.Dimensions;
				if (footprint.Length != dimension.X * dimension.Y)
					emitError("Invalid building footprint/dimension for " + actorInfo.Key);
			}
		}
	}
}
