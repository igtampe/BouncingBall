using System;
using Igtampe.BasicGraphics;
using Igtampe.BasicRender;

namespace Igtampe.BouncingBall {

    /// <summary>Using the Ball class to make a drawable ball object on the console</summary>
    class BasicBall:Ball {

        /// <summary>Default ball which is just a white square</summary>
        public static string[] DefaultBall = { "FF" };

        /// <summary>Holder for the prop</summary>
        private Graphic ballgraphic = new BasicGraphic(DefaultBall,"Default Ball");

        /// <summary>Graphic of this BasicBall</summary>
        public Graphic BallGraphic { 
            get { return ballgraphic; } 
            set { 
                ballgraphic = value;
                Width = value.GetWidth();
                Height = value.GetHeight();
            } 
        }

        /// <summary>Creates a BasicBall with the default ball (see DefaultBall)</summary>
        public BasicBall():base(Console.WindowWidth,Console.WindowHeight) {
            Width = BallGraphic.GetWidth();
            Height = BallGraphic.GetHeight();
        }

        /// <summary>Renders the ball, and ticks it one frame</summary>
        public void Render() {
            //If where it's *going* to be is the same place...
            int[] PreTick = { Convert.ToInt32(X),Convert.ToInt32(Y) };
            Tick();
            int[] PostTick= { Convert.ToInt32(X),Convert.ToInt32(Y) };

            //...Don't draw it
            if(PreTick[0].Equals(PostTick[0]) && PreTick[1].Equals(PostTick[1])) { return; }

            //Clear the old ball
            Draw.Box(Console.BackgroundColor,Convert.ToInt32(Width),Convert.ToInt32(Height),PreTick[0],PreTick[1]);

            //Draw the ball in its current location
            BallGraphic.Draw(PostTick[0],PostTick[1]);

        }

    }
}
