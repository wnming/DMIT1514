using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PlatformerGame
{
    internal readonly struct CelAnimationSet
    {
        readonly CelAnimationSequence _idle;
        readonly CelAnimationSequence _run;
        readonly CelAnimationSequence _jump;
        public CelAnimationSequence Idle => _idle;
        public CelAnimationSequence Run => _run;
        public CelAnimationSequence Jump => _jump;

        public CelAnimationSet(Texture2D idle, Texture2D run, Texture2D jump)
        {
            _idle = new CelAnimationSequence(idle, 48, 0.3f);
            _run = new CelAnimationSequence(run, 49, 0.125f);
            _jump = new CelAnimationSequence(jump, 53, 0.25f);
        }
    }
}
