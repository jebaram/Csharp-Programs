using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01
{
    public enum Orientation
    {
        North, East, South, West
    }
    public class RobotSimulator
    {
        public Orientation direction;
        public int x;
        public int y;

        public RobotSimulator()
        {
            x = 0;
            y = 0;
            direction = Orientation.North;
        }

        public RobotSimulator(Orientation direction, int x, int y)
        {
            this.direction = direction;
            this.x = x;
            this.y = y;
        }

        public Orientation Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
        public void Movements(char[] control)
        {
            for (int i = 0; i < control.Length; i++)
            {

                if (control[i] == 'A')
                {
                    Move();
                }
                if (control[i] == 'R')
                {
                    Turn(1);
                }
                if (control[i] == 'L')
                {
                    Turn(-1);
                }

            }
        }

        private void Move()
        {
            switch (direction)
            {
                case Orientation.North:
                    Y++;
                    break;
                case Orientation.East:
                    X++;
                    break;
                case Orientation.West:
                    X--;
                    break;
                case Orientation.South:
                    Y--;
                    break;
                default:
                    break;
            }
        }
        private void Turn(int way)
        {
            int orientationLenght = Enum.GetNames(typeof(Orientation)).Length;

            if (way == 1)
            {
                Direction = (Orientation)(((int)Direction + 1 + orientationLenght) % orientationLenght);
            }
            if (way == -1)
            {
                Direction = (Orientation)(((int)Direction - 1 + orientationLenght) % orientationLenght);
            }
        }       
    }
}