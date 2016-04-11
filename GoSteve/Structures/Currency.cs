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
    public class Currency
    {
        public enum CurrencyType
        {
            cp,
            sp,
            gp,
            pp
        }

        private int _cp;
        private int _sp;
        private int _gp;
        private int _pp;

        public Currency()
        {
            this.Cp = 0;
            this.Sp = 0;
            this.Gp = 0;
            this.Pp = 0;
        }

        public bool Buy(int amount, CurrencyType type)
        {
            var isSuccess = false;
            var costInCP = 0;
            var totalCP = this.Cp + this.Sp * 10 + this.Gp * 100 + this.Pp * 1000;

            if (type == CurrencyType.cp)
            {
                costInCP = amount;
            }
            else if (type == CurrencyType.sp)
            {
                costInCP = amount * 10;
            }
            else if (type == CurrencyType.gp)
            {
                costInCP = amount * 100;
            }
            else if (type == CurrencyType.pp)
            {
                costInCP = amount * 1000;
            }

            if (costInCP <= totalCP)
            {
                isSuccess = true;
                totalCP -= costInCP;
            }
            else
            {
                isSuccess = false;
            }

            this.Pp = (int)Math.Floor(totalCP / 1000f);
            totalCP -= this.Pp * 1000;

            this.Gp = (int)Math.Floor(totalCP / 100f);
            totalCP -= this.Gp * 100;

            this.Sp = (int)Math.Floor(totalCP / 10f);
            totalCP -= this.Sp * 10;

            this.Cp = totalCP;

            return isSuccess;
        }

        public int Cp
        {
            get
            {
                return _cp;
            }

            set
            {
                _cp = value;
            }
        }

        public int Sp
        {
            get
            {
                return _sp;
            }

            set
            {
                _sp = value;
            }
        }

        public int Gp
        {
            get
            {
                return _gp;
            }

            set
            {
                _gp = value;
            }
        }

        public int Pp
        {
            get
            {
                return _pp;
            }

            set
            {
                _pp = value;
            }
        }
    }
}