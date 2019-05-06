using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll_Parser_MazeGeneratorDotNet
{
    public class Polyline
    {
        public List<Vector2> m_values;

        public Polyline(string[] _saFormattedStringArray, bool _bFirstPolyline = false)
        {
            m_values = new List<Vector2>();
            StringArrayPolylinesToSegments(_saFormattedStringArray, _bFirstPolyline);
        }


        private void StringArrayPolylinesToSegments(string[] _saFormattedStringArray, bool _bFirstPolyline)
        {
            foreach (var s in _saFormattedStringArray)
            {
                this.m_values.Add(new Vector2(int.Parse(s.Split(',')[0]), int.Parse(s.Split(',')[1])));
            }

            if (_bFirstPolyline)
            {
                AdaptStartAndEndCase(m_values);
            }
        }

        private void AdaptStartAndEndCase(List<Vector2> _polylines)
        {
            _polylines.First().m_Y += 8;
            _polylines.Last().m_Y -= 8;
        }
    }
}
