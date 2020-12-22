using System;
using System.IO;
using Igtampe.BasicRender;
using Igtampe.BasicGraphics;

namespace Igtampe.BouncingBall {
    class Program {

        /// <summary>A 3x3 square-ish ball</summary>
        public static readonly string[] squareball3x3 = { " F1F1 ",
                                                          "F1F1F1",
                                                          " F1F1 " 
        };

        /// <summary>Starts a bouncy ball</summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {

            BasicBall Ball = new BasicBall {
                XSpeed = 3,
                BallGraphic = new BasicGraphic(squareball3x3,"SquareBall 3x3"),
                Bounciness = 0.9,
                Gravity = 0.5
            }; //default settings

            bool ShowInfo = true;
            int FPS = 20;
            bool StopOnZero = false;

            string ErrorString = "";

            foreach(string arg in args) {
                try {
                    //split
                    string[] SplitArg = arg.Split('=');
                    if(SplitArg.Length < 2) { continue; }

                    //Parse
                    switch(SplitArg[0].ToUpper()) {
                        case "/GRAPHIC":
                            if(SplitArg[1].ToUpper().EndsWith(".DF")) { Ball.BallGraphic = BasicGraphic.LoadFromFile(SplitArg[1]); }
                            if(SplitArg[1].ToUpper().EndsWith(".HC")) { Ball.BallGraphic = HiColorGraphic.LoadFromFile(SplitArg[1]); }
                            break;
                        case "/X":
                            Ball.X = double.Parse(SplitArg[1]);
                            break;
                        case "/Y":
                            Ball.Y = double.Parse(SplitArg[1]);
                            break;
                        case "/XSPEED":
                            Ball.XSpeed = double.Parse(SplitArg[1]);
                            break;
                        case "/YSPEED":
                            Ball.YSpeed = double.Parse(SplitArg[1]);
                            break;
                        case "/SPEED":
                            //ok Speed is going to be formatted like: VEL:ANG (IE: 10:30 is 10 speed units at 30 degrees up
                            string[] splitsplitarg = SplitArg[1].Split(':');

                            double Vel = double.Parse(splitsplitarg[0]);
                            double Ang = double.Parse(splitsplitarg[1])* (Math.PI / 180);

                            Ball.XSpeed = Vel * Math.Cos(Ang);
                            Ball.YSpeed = -1*Vel * Math.Sin(Ang); //negative since positive is *down*

                            break;
                        case "/BOUNCE":
                        case "/BOUNCINESS":
                            Ball.Bounciness = double.Parse(SplitArg[1]);
                            break;
                        case "/GRAV":
                        case "/GRAVITY":
                            Ball.Gravity = double.Parse(SplitArg[1]);
                            break;
                        case "/FPS":
                            FPS = int.Parse(SplitArg[1]);
                            break;
                        case "/SHOWINFO":
                            ShowInfo = bool.Parse(SplitArg[1]);
                            break;
                        case "/STOPONZERO":
                            StopOnZero = bool.Parse(SplitArg[1]);
                            break;
                        default:
                            break;
                    }
                } 
                catch(FormatException) {ErrorString += "\nCould not parse parameter for " + arg;}
                catch(FileNotFoundException) {ErrorString += "\nCould not find file for " + arg;}
                catch(IndexOutOfRangeException) {ErrorString += "\nNo Parameter for " + arg;}
            }

            if(!string.IsNullOrWhiteSpace(ErrorString)) {
                RenderUtils.Echo("\nErrors occurred while parsing parameters:\n" + ErrorString + "\n\nPress CTRL+C to stop execution, or any other key to continue.");
                RenderUtils.Pause();
                Console.Clear();
            }

            BallRenderer(Ball,ShowInfo,FPS,StopOnZero);
        }

        /// <summary>Renders a BasicBall on the console</summary>
        /// <param name="Ball"></param>
        /// <param name="ShowInfo"></param>
        /// <param name="FPS"></param>
        /// <param name="StopOnZero"></param>
        public static void BallRenderer(BasicBall Ball, bool ShowInfo, int FPS, bool StopOnZero) {
            bool Cont = true;

            //Draw for first time
            Ball.BallGraphic.Draw(Convert.ToInt32(Ball.X),Convert.ToInt32(Ball.Y));

            string InfoString;
            string DrawnInfoString = "";

            while(Cont) {

                //Prepare infostring
                InfoString = Ball.BallGraphic.Name + " (" + Ball.Width + "x" + Ball.Height + ")\n\n"
                           + "Position  : " + Ball.X.ToString("0.00") + ", " + Ball.Y.ToString("0.00") + "\n"
                           + "Speed     : " + Ball.XSpeed.ToString("0.00") + ", " + Ball.YSpeed.ToString("0.00") + "\n\n"
                           + "Gravity   : " + Ball.Gravity.ToString("0.00") + "\n"
                           + "Bounciness: " + Ball.Bounciness.ToString("0.00");

                //Redraw the infostring if it is different.
                if(!InfoString.Equals(DrawnInfoString) && ShowInfo) {
                    RenderUtils.SetPos(0,0);
                    RenderUtils.Echo(InfoString);
                    DrawnInfoString = InfoString;
                }

                //Just in case in a try-catch just in case because if the console shrinks while drawing it could cause an error
                try { Ball.Render(); } catch(Exception) { } 

                //Wait
                RenderUtils.Sleep(1000/FPS);

                //If the console has been resized, resize the ball's x and y limit
                if(Ball.XLimit != Console.WindowWidth || Ball.YLimit != Console.WindowHeight) {
                    Ball.XLimit = Console.WindowWidth;
                    Ball.YLimit = Console.WindowHeight;

                    Console.Clear(); //Clear just to clean up any graphical glitches from the resize
                }

                //Read the console key
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
                        case ConsoleKey.I:
                            ShowInfo = !ShowInfo;
                            if(!ShowInfo) { Console.Clear(); }
                            break;
                        default:
                            break;
                    }
                }

                if(StopOnZero && Convert.ToInt32(Ball.YSpeed) == 0 && Convert.ToInt32(Ball.XSpeed) == 0) { return; }
            }
        }
    }
}
