using System;
using Igtampe.BasicRender;
using Igtampe.BasicGraphics;

namespace Igtampe.BouncingBall {
    class Program {

        /// <summary>A 3x3 square-ish ball</summary>
        public static string[] squareball3x3 = { " F1F1 ",
                                                 "F1F1F1",
                                                 " F1F1 " };

        /// <summary>Starts a bouncy ball</summary>
        /// <param name="args"></param>
        static void Main(string[] args) {

            bool Cont = true;
            BasicBall Ball = new BasicBall {
                XSpeed = 3,
                BallGraphic = new BasicGraphic(squareball3x3,"SquareBall 3x3"),
                Bounciness = 0.9,
                Gravity = 0.5
            };

            if(args.Length > 1) {
                if(args[0].ToUpper().EndsWith(".DF")) { Ball.BallGraphic = BasicGraphic.LoadFromFile(args[0]); }
                if(args[0].ToUpper().EndsWith(".HC")) { Ball.BallGraphic = HiColorGraphic.LoadFromFile(args[0]); }
            }


            Ball.BallGraphic.Draw(Convert.ToInt32(Ball.X),Convert.ToInt32(Ball.Y));

            string InfoString;
            string DrawnInfoString="";

            while(Cont) {

                InfoString = Ball.BallGraphic.Name + " (" + Ball.Width + "x" + Ball.Height + ")\n\n"
                           + "Position  : " + Ball.X.ToString("0.00") + "," + Ball.Y.ToString("0.00") + "\n"
                           + "Speed     : " + Ball.XSpeed.ToString("0.00") + "," + Ball.YSpeed.ToString("0.00") + "\n\n"
                           + "Gravity   : " + Ball.Gravity.ToString("0.00") + "\n"
                           + "Bounciness: " + Ball.Bounciness.ToString("0.00");

                if(!InfoString.Equals(DrawnInfoString)) {
                    RenderUtils.SetPos(0,0);
                    RenderUtils.Echo(InfoString);
                    DrawnInfoString = InfoString;
                }

                try {Ball.Render();} catch(Exception) {}
                
                RenderUtils.Sleep(50);

                if(Ball.XLimit != Console.WindowWidth || Ball.YLimit != Console.WindowHeight) {
                    Ball.XLimit = Console.WindowWidth;
                    Ball.YLimit = Console.WindowHeight;

                    Console.Clear();
                }

                if(Console.KeyAvailable) {
                    switch(Console.ReadKey(true).Key) {
                        case ConsoleKey.UpArrow:
                            //Kick up.
                            Ball.YSpeed -= 5;
                            break;
                        case ConsoleKey.DownArrow:
                            //kick down
                            Ball.YSpeed += 5;
                            break;
                        case ConsoleKey.LeftArrow:
                            //Kick left
                            Ball.XSpeed -= 5;
                            break;
                        case ConsoleKey.RightArrow:
                            //Kick right
                            Ball.XSpeed += 5;
                            break;
                        case ConsoleKey.OemPlus:
                        case ConsoleKey.Add:
                            //More gravity
                            Ball.Gravity += .1;
                            break;
                        case ConsoleKey.OemMinus:
                        case ConsoleKey.Subtract:
                            //less gravity
                            Ball.Gravity -= .1;
                            break;
                        case ConsoleKey.Spacebar:
                            //Stop the ball
                            Ball.XSpeed = 0;
                            Ball.YSpeed = 0;
                            break;
                        case ConsoleKey.OemPeriod:
                            //up bounciness
                            Ball.Bounciness = Math.Min(1,Ball.Bounciness + 0.1);
                            break;
                        case ConsoleKey.OemComma:
                            //down bounciness
                            Ball.Bounciness = Math.Max(0,Ball.Bounciness - 0.1);
                            break;
                        case ConsoleKey.Escape:
                            //close
                            return;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
