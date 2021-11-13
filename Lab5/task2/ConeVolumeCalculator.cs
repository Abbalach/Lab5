using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    class ConeVolumeCalculator : AppBase
    {
        protected int Radius { get; set; }
        protected int Height { get; set; }
        protected override bool helper { get; set; }

        protected string savedRadius { get; set; }
        protected string savedHeight { get; set; }

        protected TextBox radiusTextBox;
        protected TextBox heightTextBox;
        protected TextBox answerVolTextBox;
        private TextBox answerAreaTextBox;

        protected Label VolumeformulaLabel;
        private Label AreaformulaLabel;
        protected Label radiusLabel;
        protected Label heightLabel;
        protected Label volumeLabel;
        private Label areaLabel;

        protected Button calculateButton;
        protected Button nextAppButton;
        protected Button lastAppButton;

        public SquareMover LastApp { get; set; }
        public Matrix NextApp { get; set; }

        public ConeVolumeCalculator(Form1 form, SquareMover lastapp, Matrix nextapp)
        {
            Form = form;
            helper = true;
            LastApp = lastapp;
            NextApp = nextapp;
            Radius = 0;
            Height = 0;
        }
        public ConeVolumeCalculator(Form1 form, SquareMover lastapp)
        {
            Form = form;
            helper = true;
            LastApp = lastapp;
            Radius = 0;
            Height = 0;
        }
        public ConeVolumeCalculator(Form1 form)
        {
            Form = form;
            helper = true;
            Radius = 0;
            Height = 0;
        }
        override public void Initialize()
        {
            if (helper)
            {
                Form.Size = new Size(158, 290);
                helper = false;

                radiusTextBox = new TextBox
                {
                    Location = new Point(75,61),
                    Size = new Size(45,23)
                };
                heightTextBox = new TextBox
                {
                    Location = new Point(75, 90),
                    Size = new Size(45, 23)
                };
                answerVolTextBox = new TextBox
                {
                    Location = new Point(35, 148),
                    Size = new Size(85, 23),
                    ReadOnly = true
                };
                answerAreaTextBox = new TextBox
                {
                    Location = new Point(35, 178),
                    Size = new Size(85, 23),
                    ReadOnly = true
                };

                Form.Controls.Add(radiusTextBox);
                Form.Controls.Add(heightTextBox);
                Form.Controls.Add(answerVolTextBox);
                Form.Controls.Add(answerAreaTextBox);

                VolumeformulaLabel = new Label
                {
                    Location = new Point(29, 9),
                    Size = new Size(91, 15),
                    Text = "V=(pi*H*R^2)/3"
                };
                AreaformulaLabel = new Label
                {
                    Location = new Point(61, 35),
                    Size = new Size(91, 15),
                    Text = "S=pi*R^2"
                };
                radiusLabel = new Label
                {
                    Location = new Point(47, 64),
                    Size = new Size(22, 15),
                    Text = "R="
                };
                heightLabel = new Label
                {
                    Location = new Point(45, 93),
                    Size = new Size(24, 15),
                    Text = "H="
                };
                volumeLabel = new Label
                {
                    Location = new Point(10, 152),
                    Size = new Size(24, 15),
                    Text = "V="
                };
                areaLabel = new Label
                {
                    Location = new Point(10, 182),
                    Size = new Size(24, 15),
                    Text = "S="
                };

                Form.Controls.Add(VolumeformulaLabel);
                Form.Controls.Add(AreaformulaLabel);
                Form.Controls.Add(radiusLabel);
                Form.Controls.Add(heightLabel);
                Form.Controls.Add(volumeLabel);
                Form.Controls.Add(areaLabel);

                calculateButton = new Button 
                {
                    Location = new Point(45, 119),
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
        override public void EventSetter()
        {
            if (helper)
            {
                return;
            }
            radiusTextBox.TextChanged += (o, e) =>
            {
                if (ErrorHelper(radiusTextBox,false))
                {
                    if (radiusTextBox.Text == "")
                    {
                        Radius = 0;
                    }
                    else
                    {
                        Radius = int.Parse(radiusTextBox.Text);
                        savedRadius = radiusTextBox.Text;
                    }                 
                }
                else
                {
                    radiusTextBox.Text = savedRadius;
                }
            };
            heightTextBox.TextChanged += (o, e) =>
            {
                if (ErrorHelper(heightTextBox,false))
                {
                    if (heightTextBox.Text == "")
                    {
                        Height = 0;
                    }
                    else
                    {
                        Height = int.Parse(heightTextBox.Text);
                        savedHeight = heightTextBox.Text;
                    }                    
                }
                else
                {
                    heightTextBox.Text = savedHeight;
                }
            };

            calculateButton.Click += Calculation;
            nextAppButton.Click += (o, e) =>
            {
                Terminate();
                NextApp.Initialize();
                NextApp.EventSetter();
            };
            lastAppButton.Click += (o, e) =>
            {
                Terminate();
                LastApp.Initialize();
                LastApp.EventSetter();
            };
        }
        override public bool Terminate()
        {
            if (!helper)
            {
                helper = true;
                Form.Controls.Remove(radiusTextBox);
                Form.Controls.Remove(heightTextBox);
                Form.Controls.Remove(answerVolTextBox);
                Form.Controls.Remove(answerAreaTextBox);

                Form.Controls.Remove(VolumeformulaLabel);
                Form.Controls.Remove(AreaformulaLabel);
                Form.Controls.Remove(radiusLabel);
                Form.Controls.Remove(heightLabel);
                Form.Controls.Remove(volumeLabel);
                Form.Controls.Remove(areaLabel);

                Form.Controls.Remove(calculateButton);
                Form.Controls.Remove(nextAppButton);
                Form.Controls.Remove(lastAppButton);
                return true;
            }
            else
                return false;
        }
        virtual public void Calculation(object o, EventArgs e)
        {
            if (Height == 0)
                heightTextBox.Text = "0";
            if (Radius == 0)
                radiusTextBox.Text = "0";
            answerVolTextBox.Text = (Math.Round(Radius * Radius * Height * Math.PI / 3, 2)).ToString();
            answerAreaTextBox.Text = (Math.Round(Radius * Radius * Math.PI, 2)).ToString();
            Form.Invalidate();
        }
    }
}
