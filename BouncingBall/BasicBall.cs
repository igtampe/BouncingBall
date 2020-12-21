using System;
using Igtampe.BasicGraphics;
using Igtampe.BasicRender;

namespace Igtampe.BouncingBall {

    /// <summary>Using the Ball class to make a drawable ball object on the console</summary>
    class BasicBall:Ball {

        public static string[] DefaultBall = { "FF" };

        private Graphic ballgraphic = new BasicGraphic(DefaultBall,"Default Ball");
        public Graphic BallGraphic { 
            get { return ballgraphic; } 
            set { 
                ballgraphic = value;
                Width = value.GetWidth();
                Height = value.GetHeight();
            } 
        }

        public BasicBall():base(Console.WindowWidth,Console.WindowHeight) {
            Width = BallGraphic.GetWidth();
            Height = BallGraphic.GetHeight();
        }

        public void Render() {

            //If where it's *going* to be is the same...
            int[] PreTick = { Convert.ToInt32(X),Convert.ToInt32(Y) };
            Tick();
            int[] PostTick= { Convert.ToInt32(X),Convert.ToInt32(Y) };

            if(PreTick[0].Equals(PostTick[0]) && PreTick[1].Equals(PostTick[1])) { return; }

            //Clear the old ball
            Draw.Box(Console.BackgroundColor,Convert.ToInt32(Width),Convert.ToInt32(Height),PreTick[0],PreTick[1]);

            //Draw the ball
            BallGraphic.Draw(PostTick[0],PostTick[1]);

        }

    }
}
