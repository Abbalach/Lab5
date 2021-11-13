using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    class UpperTriangularMatrix : Matrix
    {
        public UpperTriangularMatrix(Form1 form) : base(form)
        {

        }
        public UpperTriangularMatrix(Form1 form, ConeVolumeCalculator lastapp) : base(form,lastapp)
        {

        }
        protected override void MatrixCreation(object o, EventArgs e)
        {
            Height = Width;
            base.MatrixCreation(o, e);
            if (Width*52<360)
                Form.Size = new Size(390, 190 + 29 * Width);
            else
                Form.Size = new Size(Width * 52+ 90, 190 + 29 * Width);
            matrixHeightTextBox.Text = matrixWidthTextBox.Text;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (j < i)
                    {
                        matrixElements[j, i].First.ReadOnly = true;
                        matrixElements[j, i].First.Text = "0";
                    }
                }
            }
        }
    }
}
