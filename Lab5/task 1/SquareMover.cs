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
    class SquareMover : AppBase
    {
        protected PaintEventHandler PaintEventHandler;
        protected int squareSize { set; get; }
        protected int[] squarePosition { set; get; }
        protected string[] displayTexts { set; get; }
        protected override bool helper { get; set; }

        protected TextBox ssTextBox;
        protected TextBox splTextBox;
        protected TextBox spuTextBox;
        protected TextBox display;

        protected Button crSquare;
        protected Button dlSquare;
        protected Button nextAppButton;

        protected Label ssLabel;
        protected Label splLabel;
        protected Label spuLabel;

        public ConeVolumeCalculator NextApp { get; set; }

        public SquareMover(Form1 form, ConeVolumeCalculator nextapp)
        {
            Form = form;
            helper = true;
            NextApp = nextapp;
            displayTexts = new string[3];
        }
        public SquareMover(Form1 form)
        {
            Form = form;
            helper = true;
            displayTexts = new string[3];
        }
        override public void Initialize()
        {
            if (helper)
            {
                Form.Size = new Size(816, 489);
                helper = false;
                ssTextBox = new TextBox
                {
                    Size = new Size(152, 23),
                    Location = new Point(89, 353),
                };
                splTextBox = new TextBox
                {
                    Size = new Size(152, 23),
                    Location = new Point(89, 385),
                };
                spuTextBox = new TextBox
                {
                    Size = new Size(152, 23),
                    Location = new Point(89, 415),
                };
                display = new TextBox
                {
                    Size = new Size(135, 23),
                    Location = new Point(561, 385),
                    ReadOnly = true
                };

                Form.Controls.Add(ssTextBox);
                Form.Controls.Add(splTextBox);
                Form.Controls.Add(spuTextBox);
                Form.Controls.Add(display);

                crSquare = new Button
                {
                    Location = new Point(257, 353),
                    Size = new Size(142, 85),
                    Text = "Create square"
                };
                dlSquare = new Button
                {
                    Location = new Point(405, 353),
                    Size = new Size(142, 85),
                    Text = "Delete square"
                };
                nextAppButton = new Button 
                {
                    Size= new Size(70,23),
                    Location= new Point(Form.Width-100,Form.Height-73),
                    Text=">>",
                };

                Form.Controls.Add(crSquare);
                Form.Controls.Add(dlSquare);
                Form.Controls.Add(nextAppButton);

                ssLabel = new Label
                {
                    Size = new Size(64, 15),
                    Location = new Point(19, 356),
                    Text = "square size"
                };
                splLabel = new Label
                {
                    Size = new Size(61, 15),
                    Location = new Point(22, 388),
                    Text = "sq pos left"
                };
                spuLabel = new Label
                {
                    Size = new Size(74, 15),
                    Location = new Point(9, 418),
                    Text = "sq pos down"
                };

                Form.Controls.Add(ssLabel);
                Form.Controls.Add(splLabel);
                Form.Controls.Add(spuLabel);
            }
        }
        override public void EventSetter()
        {
            if (helper)
            {
                return;
            }
            ssTextBox.TextChanged += (o, e) =>
            {
                if (ErrorHelper(ssTextBox,false))
                {
                    if (ssTextBox.Text == "")
                    {
                        squareSize = 100;
                    }
                    else
                    {
                        squareSize = int.Parse(ssTextBox.Text);
                        displayTexts[0] = ssTextBox.Text;
                    }                 
                }
                else
                {
                    ssTextBox.Text = displayTexts[0];
                }               
            };
            splTextBox.TextChanged += (o, e) =>
            {
                if (ErrorHelper(splTextBox,false))
                {
                    if (splTextBox.Text == "")
                    {
                        squarePosition[0] = 10;
                    }
                    else
                    {
                        squarePosition[0] = int.Parse(splTextBox.Text);
                        displayTexts[1] = splTextBox.Text;
                    }                   
                }
                else
                {
                    splTextBox.Text = displayTexts[1];
                }               
            };
            spuTextBox.TextChanged += (o, e) =>
            {
                if (ErrorHelper(spuTextBox,false))
                {
                    if (spuTextBox.Text == "")
                    {
                        squarePosition[1] = 10;
                    }
                    else
                    {
                        squarePosition[1] = int.Parse(spuTextBox.Text);
                        displayTexts[2] = spuTextBox.Text;
                    }               
                }
                else
                {
                    spuTextBox.Text = displayTexts[2];
                }               
            };

            crSquare.Click += (o, e) =>
            {
                if (squareSize == 100)
                    ssTextBox.Text = "100";
                if (squarePosition[0] == 10)
                    splTextBox.Text = "10";
                if (squarePosition[1] == 10)
                    spuTextBox.Text = "10"; 
                Form.Paint += PaintEventHandler;
                display.Text = squarePosition[0] + "  " + squarePosition[1] + "  " +  (squarePosition[0]+squareSize) + "  " + (squarePosition[1]+squareSize);
                Form.Invalidate();
            };
            dlSquare.Click += (o, e) =>
            {
                Form.Paint -= PaintEventHandler;
                display.Text = "";
                Form.Invalidate();
            };
            nextAppButton.Click += (o, e) =>
            {
                Terminate();
                NextApp.Initialize();
                NextApp.EventSetter();
            };

            squarePosition = new int[] { 10, 10 };
            squareSize = 100;
            crSquare.KeyUp += SquareMove;
            dlSquare.KeyUp += SquareMove;
            PaintEventHandler = (o, e) =>
            {
                Graphics graphics = e.Graphics;               
                graphics.DrawRectangle(new Pen(Color.Black), squarePosition[0], squarePosition[1], squareSize, squareSize);                
            };
        }
        override public bool Terminate()
        {
            if (!helper)
            {
                helper = true;
                Form.Paint -= PaintEventHandler;
                Form.Controls.Remove(ssTextBox);
                Form.Controls.Remove(spuTextBox);
                Form.Controls.Remove(splTextBox);
                Form.Controls.Remove(display);

                Form.Controls.Remove(crSquare);
                Form.Controls.Remove(dlSquare);

                Form.Controls.Remove(ssLabel);
                Form.Controls.Remove(spuLabel);
                Form.Controls.Remove(splLabel);
                Form.Invalidate();
                return true;
            }
            else
                return false;
        }
        protected virtual void SquareMove(object o, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    {
                        squarePosition[0] -= 10;
                        Form.Invalidate();
                        break;
                    }
                case Keys.D:
                    {
                        squarePosition[0] += 10;
                        Form.Invalidate();
                        break;
                    }
            }
        }
        
    }
}
