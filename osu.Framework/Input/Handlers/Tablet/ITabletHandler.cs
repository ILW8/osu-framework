﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osuTK;

namespace osu.Framework.Input.Handlers.Tablet
{
    /// <summary>
    /// An interface to access OpenTabletDriverHandler.
    /// Can be considered for removal now that we're solely targeting .NET 6
    /// </summary>
    public interface ITabletHandler
    {
        /// <summary>
        /// The offset of the area which should be mapped to the game window.
        /// </summary>
        Bindable<Vector2> AreaOffset { get; }

        /// <summary>
        /// The size of the area which should be mapped to the game window.
        /// </summary>
        Bindable<Vector2> AreaSize { get; }

        /// <summary>
        /// Information on the currently connected tablet device. May be null if no tablet is detected.
        /// </summary>
        IBindable<TabletInfo?> Tablet { get; }

        /// <summary>
        /// The rotation of the tablet area in degrees.
        /// </summary>
        Bindable<float> Rotation { get; }

        /// <summary>
        /// The minimum pressure percentage required to click.
        /// </summary>
        BindableFloat PressureThreshold { get; }

        BindableBool Enabled { get; }
    }
}
