using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll_Parser_MazeGeneratorDotNet
{
    enum Direction { Up, Right, Bottom, Left }
    public static class Temp
    {
        public static List<Square> _AllLabyrintheCases = new List<Square>();
    }

    public class Square
    {
        public Square m_right;
        public Square m_left;
        public Square m_bottom;
        public Square m_up;

        public Vector2 m_current_coord;
        public bool _passed;

        Square(Vector2 _v2CurrentCoord)
        {
            m_right = null;
            m_left = null;
            m_bottom = null;
            m_up = null;
            m_current_coord = _v2CurrentCoord;
        }

        Square(float _fX, float _fY) : this(new Vector2(_fX, _fY))
        {
        }


        public Square(List<Polyline> _lpPolylines)
        {
            Square c = PolylinesToLabyrinthe(_lpPolylines);

            this.m_current_coord = c.m_current_coord;
            this._passed = c._passed;
            this.m_bottom = c.m_bottom;
            this.m_left = c.m_left;
            this.m_right = c.m_right;
            this.m_up = c.m_up;
        }
        private Square GetCaseByPos(Vector2 _v2Position)
        {
            return Temp._AllLabyrintheCases.First(p => p.m_current_coord.Equals(_v2Position));
        }

        private Square PolylinesToLabyrinthe(List<Polyline> _lpPolylines)
        {
            Square HeadList = new Square(_lpPolylines[0].m_values.First());
            Temp._AllLabyrintheCases.Add(HeadList);

            Square CurrentCase = HeadList;

            foreach (Polyline polyline in _lpPolylines)
            {
                CurrentCase = GetCaseByPos(polyline.m_values.First());
                PolylineToPath(polyline, ref CurrentCase);
            }
            return HeadList;
        }

        private Square PolylineToPath(Polyline _pPolyline, ref Square _refCaseStart)
        {
            Square HeadList = _refCaseStart;

            Square CurrentCase = HeadList;
            for (int i = 0; i < _pPolyline.m_values.Count - 1; i++)
            {
                CurrentCase = SegmentToPath(ref CurrentCase, _pPolyline.m_values[i + 1].m_X, _pPolyline.m_values[i + 1].m_Y);
            }

            return CurrentCase;
        }

        public Square SegmentToPath(ref Square _refcLeftCase, float _iX2, float _iY2)
        {
            // arrivé +8 y
            // départ +8 y
            // Case ToReturn = new Case(_iX, _iY);
            float dx, dy;
            dy = _iY2 - _refcLeftCase.m_current_coord.m_Y;
            dx = _iX2 - _refcLeftCase.m_current_coord.m_X;

            if (dx == 0 && dy == 0)
                return _refcLeftCase;

            if (dx == 0 && dy > 0) // down
            {
                CreateCase(ref _refcLeftCase, (int)Math.Abs(dy / 16), Direction.Bottom);
                return _refcLeftCase;
            }

            if (dx > 0 && dy == 0) // right
            {
                CreateCase(ref _refcLeftCase, (int)Math.Abs(dx / 16), Direction.Right);
                return _refcLeftCase;
            }

            if (dx == 0 && dy < 0) // up
            {
                CreateCase(ref _refcLeftCase, (int)Math.Abs(dy / 16), Direction.Up);
                return _refcLeftCase;
            }

            if (dx < 0 && dy == 0) // left
            {
                CreateCase(ref _refcLeftCase, (int)Math.Abs(dx / 16), Direction.Left);
                return _refcLeftCase;
            }
            return null;
        }

        private void CreateCase(ref Square _refCase, int _iNumberOfCase, Direction _dDirection)
        {
            //  Case Head = _refCase;
            for (uint i = 0; i < _iNumberOfCase; i++)
            {
                _refCase = CreateCaseAtPosition(ref _refCase, _dDirection);
            }
            // _refCase = Head;
        }

        private Square CreateCaseAtPosition(ref Square _refCase, Direction _dDirection)
        {
            switch (_dDirection)
            {
                case Direction.Up:
                    _refCase.m_up = new Square(_refCase.m_current_coord.m_X, _refCase.m_current_coord.m_Y - 16);
                    _refCase.m_up.m_bottom = _refCase;
                    Temp._AllLabyrintheCases.Add(_refCase.m_up);
                    return _refCase.m_up;

                case Direction.Right:
                    _refCase.m_right = new Square(_refCase.m_current_coord.m_X + 16, _refCase.m_current_coord.m_Y);
                    _refCase.m_right.m_left = _refCase;
                    Temp._AllLabyrintheCases.Add(_refCase.m_right);
                    return _refCase.m_right;

                case Direction.Bottom:
                    _refCase.m_bottom = new Square(_refCase.m_current_coord.m_X, _refCase.m_current_coord.m_Y + 16);
                    _refCase.m_bottom.m_up = _refCase;
                    Temp._AllLabyrintheCases.Add(_refCase.m_bottom);
                    return _refCase.m_bottom;

                case Direction.Left:
                    _refCase.m_left = new Square(_refCase.m_current_coord.m_X - 16, _refCase.m_current_coord.m_Y);
                    _refCase.m_left.m_right = _refCase;
                    Temp._AllLabyrintheCases.Add(_refCase.m_left);
                    return _refCase.m_left;
                default:
                    return null;
            }
        }
    }
}
