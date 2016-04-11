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
    public class HitDice
    {
        private int _numSides;
        private int _totalAmount;
        private int _avlAmount;

        public HitDice()
        {
            this.NumberOfSides = 10;
            this.TotalAmount = 1;
            this.AvailableAmount = 1;
        }

        public int NumberOfSides
        {
            get
            {
                return _numSides;
            }

            set
            {
                _numSides = value;
            }
        }

        public int TotalAmount
        {
            get
            {
                return _totalAmount;
            }

            set
            {
                _totalAmount = value;
            }
        }

        public int AvailableAmount
        {
            get
            {
                return _avlAmount;
            }

            set
            {
                _avlAmount = value;
            }
        }

        public override string ToString()
        {
            return this.AvailableAmount.ToString() + "d" + this.NumberOfSides.ToString();
        }
    }
}