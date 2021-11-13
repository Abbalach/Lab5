using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    class TruncatedConeVolumeCalculator : ConeVolumeCalculator
    {
        Label smallRadiusLabel;
        TextBox smallRadiusTextBox;
        string savedsmallRadius { get; set; }
        int smallRadius { get; set; }
        public TruncatedConeVolumeCalculator(Form1 form, SquareMover lastapp, Matrix nextapp) : base(form, lastapp,nextapp)
        {
            smallRadius = 0;
        }
        public TruncatedConeVolumeCalculator(Form1 form, SquareMover lastapp) : base(form,lastapp)
        {
            smallRadius = 0;
        }
        public TruncatedConeVolumeCalculator(Form1 form) : base(form)
        {
            smallRadius = 0;
        }
        override public void Initialize()
        {
            if (helper)
            {
                Form.Size = new Size(170, 290);
                helper = false;

                radiusTextBox = new TextBox
                {
                    Location = new Point(75, 35),
                    Size = new Size(45, 23)
                };
                smallRadiusTextBox = new TextBox
                {
                    Location = new Point(75, 65),
                    Size = new Size(45, 23)
                };
                heightTextBox = new TextBox
                {
                    Location = new Point(75, 95),
                    Size = new Size(45, 23)
                };
                answerVolTextBox = new TextBox
                {
                    Location = new Point(35, 168),
                    Size = new Size(85, 23),
                    ReadOnly = true
                };

                Form.Controls.Add(radiusTextBox);
                Form.Controls.Add(heightTextBox);
                Form.Controls.Add(smallRadiusTextBox);
                Form.Controls.Add(answerVolTextBox);

                VolumeformulaLabel = new Label
                {
                    Location = new Point(2, 9),
                    Size = new Size(150, 15),
                    Text = "V=π*h*(R^2+R*r2+r2^2)/3"
                };
                smallRadiusLabel = new Label
                {
                    Location = new Point(45, 68),
                    Size = new Size(25, 15),
                    Text = "r2="
                };
                radiusLabel = new Label
                {
                    Location = new Point(47, 38),
                    Size = new Size(22, 15),
                    Text = "R="
                };
                heightLabel = new Label
                {
                    Location = new Point(45, 98),
                    Size = new Size(24, 15),
                    Text = "H="
                };
                volumeLabel = new Label
                {
                    Location = new Point(10, 172),
                    Size = new Size(24, 15),
                    Text = "V="
                };

                Form.Controls.Add(VolumeformulaLabel);
                Form.Controls.Add(radiusLabel);
                Form.Controls.Add(smallRadiusLabel);
                Form.Controls.Add(heightLabel);
                Form.Controls.Add(volumeLabel);

                calculateButton = new Button
                {
                    Location = new Point(45, 129),
                    Size = new Size(75, 23),
                    Text = "calculate"
                };
                lastAppButton = new Button
                {
                    Location = new Point(12, 217),
                    Size = new Size(48, 23),
                    Text = "<<"
                };
                nextAppButton = new Button
                {
                    Location = new Point(75, 217),
                    Size = new Size(48, 23),
                    Text = ">>"
                };

                Form.Controls.Add(calculateButton);
                Form.Controls.Add(nextAppButton);
                Form.Controls.Add(lastAppButton);
            }
        }
        public override void EventSetter()
        {
            base.EventSetter(); 
            heightTextBox.TextChanged += (o, e) =>
            {
                if (ErrorHelper(smallRadiusTextBox,false))
                {
                    if (smallRadiusTextBox.Text == "")
                    {
                        smallRadius = 0;
                    }
                    else
                    {
                        smallRadius = int.Parse(smallRadiusTextBox.Text);
                        savedsmallRadius = smallRadiusTextBox.Text;
                    }
                }
                else
                {
                    smallRadiusTextBox.Text = savedsmallRadius;
                }
            };
        }
        override public bool Terminate()
        {
            if (base.Terminate())
            {
                Form.Controls.Remove(smallRadiusLabel);
                Form.Controls.Remove(smallRadiusTextBox);
                return true;
            }
            else
                return false;
        }
        override public void Calculation(object o, EventArgs e)
        {
            if (Height == 0)
                heightTextBox.Text = "0";
            if (Radius == 0)
                radiusTextBox.Text = "0";
            if (smallRadius == 0)
                smallRadiusTextBox.Text = "0";
            answerVolTextBox.Text = (Math.Round(
                Height * Math.PI *(Radius*Radius+Radius*smallRadius+smallRadius*smallRadius)/ 3, 2)).ToString();
            Form.Invalidate();
        }
    }
}
