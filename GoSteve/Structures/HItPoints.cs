using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GoSteve.Structures
{
    [Serializable]
    public class HitPoints
    {
        private int _max;
        private int _current;
        private int _temp;

        public HitPoints()
        {
            this.Max = 0;
            this.Current = 0;
            this.Temp = 0;
        }

        public int Max
        {
            get
            {
                return _max;
            }

            set
            {
                _max = value;
            }
        }

        public int Current
        {
            get
            {
                return _current;
            }

            set
            {
                if (value <= this.Max)
                    _current = value;
                else
                    _current = this.Max;

                if (value < 0)
                    _current = 0;
            }
        }

        public int Temp
        {
            get
            {
                return _temp;
            }

            set
            {
                if (value > 0)
                    _temp = value;
            }
        } 
    }
}