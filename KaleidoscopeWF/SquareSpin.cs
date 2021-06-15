using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KaleidoscopeWF
{
    /// <summary>
    /// Manage a specific number that increase and decrease inside a range, with a AutoLoop flag
    /// </summary>
    class SquareSpin
    {
        public enum Axis { X, Y };
        public enum Direction { clockwise, anticlockwise, forward, backward };

        // OLD

        public int Start { get; set; }
        public int End { get; set; }
        public int Actual { get; set; }
        public int Step { get; set; }

        // NEW

        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }
        public Point BottomLeft { get; set; }
        public Point BottomRight { get; set; }
        public Point c1 { get; set; }
        public Point c2 { get; set; }
        public Direction spinDirection { get; set; }
        public List<Point> lstPath;

        public SquareSpin( Point newTopLeft, 
                           Point newTopRight, 
                           Point newBottomRight, 
                           Point newBottomLeft, 
                           int newStep, 
                           Point newC1, 
                           Point newC2, 
                           Direction newDirection )
        {
            TopLeft = newTopLeft;
            TopRight = newTopRight;
            BottomLeft = newBottomLeft;
            BottomRight = newBottomRight;
            Step = newStep;
            c1 = newC1;
            c2 = newC2;
            spinDirection = newDirection;

            // CHECK the consistency of received data in order to proceed
            if (TopLeft.X > TopRight.X)
                throw new System.Exception("X axis error: TopLeft need to be lower than TopRight, in order to have a X axis to run.");

            if (BottomLeft.X > BottomRight.X)
                throw new System.Exception("X axis error: BottomLeft need to be lower than BottomRight, in order to have a X axis to run.");

            if (TopLeft.Y > BottomLeft.Y)
                throw new System.Exception("Y axis error: TopLeft need to be lower than BottomLeft, in order to have a Y axis to run.");

            if (TopRight.Y > BottomRight.Y)
                throw new System.Exception("Y axis error: TopRight need to be lower than BottomRight, in order to have a Y axis to run.");

            // Initialize the path list to receive the path
            lstPath = new List<Point>();

            // calculate top horizontal line FROM TopLeft TO TopRight path
            for ( int i=TopLeft.X; i <= TopRight.X; i+=Step)
            {
                Point newNode = new Point(i, TopLeft.Y);
                lstPath.Add(newNode);
            }

            // calculate TopRight to BottomRight path
            for( int i=TopRight.Y; i <= BottomRight.Y; i += Step)
            {
                Point newNode = new Point(TopRight.X, i);
                lstPath.Add(newNode);
            }

            // calculate BottomRight to BottomLeft path
            for (int i = BottomRight.X; i >= BottomLeft.X; i -= Step)
            {
                Point newNode = new Point(i, BottomRight.Y);
                lstPath.Add(newNode);
            }

            // calculate BottomLeft to TopLeft path
            for (int i = BottomLeft.Y; i >= TopLeft.Y; i -= Step)
            {
                Point newNode = new Point(TopRight.X, i);
                lstPath.Add(newNode);
            }

            var aList4 = BuildListOfPoint(BottomLeft, TopLeft, Step, Axis.Y, Direction.backward  );

        }

        // Produce a sequence of points using the parameters passed
        // Axis means with axis of the point will be forwarded or backwarded
        // Direction.backward means from End to Start, and forward means from Start to End
        private List<Point> BuildListOfPoint( Point Start, Point End, int Step, Axis axis, Direction direction)
        {
            // First, crate an empty list to return
            List<Point> lst = new List<Point>();
            int _start;
            int _end;

            switch(direction)
            {
                case Direction.forward:

                    //TODO: Revisar a lógica, tem erro de entendimento aí

                    _start = (axis == Axis.X) ? Start.X : Start.Y;
                    _end = (axis == Axis.Y) ? Start.Y : End.Y;

                    break;

                case Direction.backward:

                    _start = (axis == Axis.X) ? End.X : Start.Y;
                    _end = (axis == Axis.Y) ? End.Y : Start.Y;

                    break;
            }

            return lst;
        }

        public SquareSpin( int newStart, int newEnd, int newStep)
        {
            Start = newStart;
            End = newEnd;
            Step = newStep;

            Actual = Start;
        }

        public void StepForward()
        {
            Actual = Actual < End ? 
                Actual + Step : 
                End;
        }

        public Boolean NoSpaceToForward
        {
            get
            {
                return Actual == End;
            }
        }
    }
}
