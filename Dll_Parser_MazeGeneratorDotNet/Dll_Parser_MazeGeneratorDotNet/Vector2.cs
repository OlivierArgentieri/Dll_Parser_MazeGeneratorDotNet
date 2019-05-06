using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll_Parser_MazeGeneratorDotNet
{
    public class Vector2
    {
        public float m_X;
        public float m_Y;

        public Vector2(float _fX, float _fY)
        {
            m_X = _fX;
            m_Y = _fY;
        }

        public override bool Equals(object obj)
        {
            var other = (Vector2)obj;
            return this.m_Y == other.m_Y && m_X == other.m_X;
        }
    }
}
