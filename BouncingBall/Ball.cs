using System;

namespace Igtampe.BouncingBall {

    /// <summary>A basic ball object that can be ticked</summary>
    public class Ball {

        /// <summary>holder for the prop</summary>
        private double x = 0;

        /// <summary>X Position of the upper left corner of the ball</summary>
        public double X { 
            get { return x; }
            set { 
                x = value;
                bool bounce;
                
                do {

                    bounce = false;

                    if(x+width > XLimit) {
                        //B O U N C E
                        x = XLimit - width; //- (((x+width) - XLimit) * Bounciness);
                        XSpeed = XSpeed * -1 * Bounciness;
                        bounce = true;
                    }

                    if(x < 0) {
                        //B O U N C E
                        x = 0 + (x * -1 * Bounciness);
                        XSpeed = (XSpeed * -1 * Bounciness);
                        bounce = true;
                    }
                } while(bounce);
            } 
        }

        /// <summary>holder for the prop</summary>
        private double y=0;

        /// <summary>Y Position of the upper left corner of the ball</summary>
        public double Y { 
            get { return y; } 
            set { 
                y = value;
                bool bounce;
                
                do {

                    bounce = false;

                    if(y+height > YLimit) {
                        //B O U N C E
                        y = YLimit - height;// - (((y+height) - YLimit) * Bounciness);
                        YSpeed = (YSpeed * -1 * Bounciness);
                        bounce = true;
                    }

                    if(y < 0) {
                        //B O U N C E
                        y = 0 + (y * -1 * Bounciness);
                        YSpeed = (YSpeed * -1 * Bounciness);
                        bounce = true;
                    } 
                } while(bounce);
            } 
        }

        /// <summary>holder for the prop</summary>
        private double xlimit;

        /// <summary>Right limit of the ball (assume 0,0 is the upper left limit)</summary>
        public double XLimit { 
            get { return xlimit; } 
            set { xlimit = value;

                //verify the ball fits
                if(XLimit < Width) { throw new ArgumentException("The ball won't fit with this size!"); }

            } 
        }

        /// <summary>holder for the prop</summary>
        private double ylimit;

        /// <summary>Bottom limit of the ball in meters (assume 0,0 is the upper left limit)</summary>
        public double YLimit { 
            get { return ylimit; } 
            set { ylimit = value;

                //verify the ball fits
                if(YLimit  < Height) { throw new ArgumentException("The ball won't fit with this size!"); }

            } 
        }

        /// <summary>Horizontal Speed of the ball in m/s</summary>
        public double XSpeed { get; set; } = 0;

        /// <summary>Vertical speed of the ball in m/s</summary>
        public double YSpeed { get; set; } = 0;

        /// <summary>holder for the prop</summary>
        private double width = 1;

        /// <summary>Width of the ball in meters</summary>
        public double Width { 
            get { return width; } 
            set {
                width = value;
                if(width > XLimit) { throw new ArgumentException("The ball won't fit with this size!"); }
            } 
        }

        /// <summary>holder for the prop</summary>
        private double height = 1;

        /// <summary>Height of the ball in meters</summary>
        public double Height { 
            get { return height; } 
            set {
                height = value;
                if(height > YLimit) { throw new ArgumentException("The ball won't fit with this size!"); }

            } 
        }

        /// <summary>Acceleration of Gravity in m/s^2</summary>
        public double Gravity { get; set; } = 9.8;

        /// <summary>Bounciness of the ball (1 being full bouncy, 0 being not bouncy at all)</summary>
        public double Bounciness { get; set; } = 1;

        /// <summary>Creates a ball</summary>
        /// <param name="XLimit"></param>
        /// <param name="YLimit"></param>
        public Ball(int XLimit,int YLimit) {
            this.XLimit = XLimit;
            this.YLimit = YLimit;
        }

        /// <summary>Ticks the ball exactly one second</summary>
        public void Tick() {

            //increase the yspeed by the gravity
            YSpeed += Gravity;

            //Drag

            //Now move the ball
            X += XSpeed;
            Y += YSpeed;

            //and that's it. The setters handle the bounce.

        }

            
    }
}
