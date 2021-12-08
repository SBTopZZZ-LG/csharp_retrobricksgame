using System.Drawing;
using RetroBricksGame.GameObjects.Abstract;

namespace RetroBricksGame.GameObjects
{
    public enum BlockType
    {
        Air,
        Object,
        Ground
    }

    public class L : Shape
    {
        public override int ComputeWidth()
        {
            return 3;
        }

        public L() : base(new Point(0, 1), new Point(1, 0)) { }
    }
    public class L_Tall : Shape
    {
        public override int ComputeWidth()
        {
            return 5;
        }

        public L_Tall() : base(new Point(0, 1), new Point(0, 2), new Point(1, 0)) { }
    }
    public class L_Tall_Inverted : Shape
    {
        public override int ComputeWidth()
        {
            return 5;
        }

        public L_Tall_Inverted() : base(new Point(0, 1), new Point(0, 2), new Point(-1, 0)) { }
    }
    public class Line : Shape
    {
        public override int ComputeWidth()
        {
            return 3;
        }

        public Line() : base(new Point(-1, 0), new Point(1, 0)) { }
    }
    public class Triangle : Shape
    {
        public override int ComputeWidth()
        {
            return 3;
        }

        public Triangle() : base(new Point(-1, 0), new Point(1, 0), new Point(0, 1)) { }
    }
    public class Plus : Shape
    {
        public override int ComputeWidth()
        {
            return 3;
        }

        public Plus() : base(new Point(-1, 0), new Point(0, 1), new Point(1, 0), new Point(0, -1)) { }
    }

    public class Block
    {
        public char character;
        public BlockType blockType;

        public Block(char character, BlockType blockType)
        {
            this.character = character;
            this.blockType = blockType;
        }
    }
}