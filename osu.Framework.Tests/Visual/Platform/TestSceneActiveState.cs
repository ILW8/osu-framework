// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Veldrid;
using osu.Framework.Platform;
using osuTK.Graphics;

namespace osu.Framework.Tests.Visual.Platform
{
    public partial class TestSceneActiveState : FrameworkTestScene
    {
        private IBindable<bool> isActive;
        private IBindable<bool> cursorInWindow;
        private double displayLinkThingPeriod = -1.0f;

        private Drawable isActiveBox;
        private Drawable cursorInWindowBox;
        private SpriteText displayedText;

        private IRenderer renderer;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            renderer = host.Renderer;
            isActive = host.IsActive.GetBoundCopy();
            cursorInWindow = host.Window?.CursorInWindow.GetBoundCopy();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Children = new[]
            {
                isActiveBox = new DisplayBox("host.IsActive")
                {
                    Width = 0.5f,
                    RelativeSizeAxes = Axes.Both,
                },
                cursorInWindowBox = new DisplayBox("host.Window.CursorInWindow")
                {
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.5f,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                },
                displayedText = new SpriteText()
            };

            isActive.BindValueChanged(active => isActiveBox.Colour = active.NewValue ? Color4.Green : Color4.Red, true);
            cursorInWindow?.BindValueChanged(active =>
            {
                cursorInWindowBox.Colour = active.NewValue ? Color4.Green : Color4.Red;

                double refreshPeriod = -1.0d;
                double nominalRefreshPeriod = -1.0d;

                if (renderer is VeldridRenderer veldridRenderer)
                {
                    refreshPeriod = veldridRenderer.Device.DisplayLinkGetActualOutputVideoRefreshPeriod();
                    nominalRefreshPeriod = veldridRenderer.Device.DisplayLinkGetNominalOutputVideoRefreshPeriod();
                }

                displayedText.Text = $"CVDisplayLinkGetActualOutputVideoRefreshPeriod: {refreshPeriod}    "
                                     + $"CVDisplayLinkGetNominalOutputVideoRefreshPeriod: {nominalRefreshPeriod}";
            }, true);
        }

        public partial class DisplayBox : CompositeDrawable
        {
            public DisplayBox(string label)
            {
                InternalChildren = new Drawable[]
                {
                    new Box
                    {
                        Colour = Color4.White,
                        RelativeSizeAxes = Axes.Both,
                    },
                    new SpriteText
                    {
                        Text = label,
                        Colour = Color4.Black,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    }
                };
            }
        }
    }
}
