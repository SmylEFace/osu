// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Game.Beatmaps;

namespace osu.Game.Rulesets.Mods
{
    /// <summary>
    /// Interface for a <see cref="Mod"/> that applies changes to the <see cref="IBeatmap"/> generated by the <see cref="BeatmapConverter{TObject}"/>.
    /// </summary>
    public interface IApplicableAfterBeatmapConversion : IApplicableMod
    {
        /// <summary>
        /// Applies this <see cref="Mod"/> to the <see cref="IBeatmap"/> after conversion has taken place.
        /// </summary>
        /// <param name="beatmap">The converted <see cref="IBeatmap"/>.</param>
        void ApplyToBeatmap(IBeatmap beatmap);
    }
}
