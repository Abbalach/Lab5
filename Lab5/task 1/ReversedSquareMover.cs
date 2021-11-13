using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    class ReversedSquareMover : SquareMover
    {       
        public ReversedSquareMover(Form1 form, ConeVolumeCalculator nextapp) : base(form, nextapp)
        {

        }
        public ReversedSquareMover(Form1 form) : base(form)
        {

        }

        override protected void SquareMove(object o, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    {
                        squarePosition[0] -= 10;
                        Form.Invalidate();
                        break;
                    }
                case Keys.A:
                    {
                        squarePosition[0] += 10;
                        Form.Invalidate();
                        break;
                    }
            }
        }
        
    }
}
