using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class IdentityMatrix : Matrix
    {
        public IdentityMatrix(Form1 form) : base(form)
        {

        }
        public IdentityMatrix(Form1 form, ConeVolumeCalculator lastapp) : base(form,lastapp)
        {

        }
        public override void EventSetter()
        {
            if (helper)
            {
                return;
            }
            matrixHeightTextBox.TextChanged += (o, e) =>
            {
                if (matrixHeightTextBox.Text == "" || matrixWidthTextBox.Text == "0" || matrixHeightTextBox.Text == "1")
                {
                    if (matrixHeightTextBox.Text == "")
                    {
                        Height = 0;
                    }
                    else
                    {
                        Height = int.Parse(matrixHeightTextBox.Text);
                        savedHeightdisplay = matrixHeightTextBox.Text;
                    }
                }
                else
                {
                    matrixHeightTextBox.Text = savedHeightdisplay;
                }
            };
            matrixWidthTextBox.TextChanged += (o, e) =>
            {
                if (matrixWidthTextBox.Text == "" || matrixWidthTextBox.Text == "0" || matrixWidthTextBox.Text == "1")
                {
                    if (matrixWidthTextBox.Text == "")
                    {
                        Width = 0;
                    }
                    else
                    {
                        Width = int.Parse(matrixWidthTextBox.Text);
                        savedWidthdisplay = matrixWidthTextBox.Text;
                    }
                }
                else
                {
                    matrixWidthTextBox.Text = savedWidthdisplay;
                }
            };
            createButton.Click += MatrixCreation;
        }
    }
}
