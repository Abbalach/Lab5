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
    
    class Matrix : AppBase
    {
        protected override bool helper { get; set; }
        protected ValuePair<TextBox,int>[,] matrixElements { get; set; }
        protected ValuePair<TextBox,int>[] additionalElements { get; set; }

        protected virtual int Height { get; set; }
        protected virtual int Width { get; set; }
        protected int prevHeight { get; set; }
        protected int prevWidth { get; set; }

        protected string savedHeightdisplay { get; set; }
        protected string savedWidthdisplay { get; set; }

        protected Size StableBoxSize { get; }
        protected Point FirstBoxLocation { get; }

        protected TextBox matrixWidthTextBox;
        protected TextBox matrixHeightTextBox;
        protected TextBox answerTextBox;

        protected Label matrixWidthLabel;
        protected Label matrixHeightLabel;


        protected virtual Button createButton { get; set; }
        protected Button lastButton;
        protected Button CalculateButton;

        public ConeVolumeCalculator LastApp { get; set; }

        public Matrix(Form1 form, ConeVolumeCalculator lastapp)
        {
            Form = form;
            helper = true;
            LastApp = lastapp;
            Height = 0;
            Width = 0;
            StableBoxSize = new Size(46, 23);
            FirstBoxLocation = new Point(12, 120);
        }
        public Matrix(Form1 form)
        {
            Form = form;
            helper = true;
            Height = 0;
            Width = 0;
            StableBoxSize = new Size(46, 23);
            FirstBoxLocation = new Point(12, 120);
        }
        public override void Initialize()
        {
            if (helper)
            {
                helper = false;
                Form.Size = new Size(360, 190); 
                matrixWidthTextBox = new TextBox
                {
                    Location = new Point(76,38),
                    Size = new Size(100,23),                   
                };
                matrixHeightTextBox = new TextBox
                {
                    Location = new Point(76,80),
                    Size = new Size(100,23),
                };

                Form.Controls.Add(matrixHeightTextBox);
                Form.Controls.Add(matrixWidthTextBox);

                matrixWidthLabel = new Label 
                {
                    Location = new Point(33, 41),
                    Size = new Size(37, 15),
                    Text="width"
                };
                matrixHeightLabel = new Label
                {
                    Location = new Point(29, 83),
                    Size = new Size(41, 15),
                    Text = "height"
                };

                Form.Controls.Add(matrixWidthLabel);
                Form.Controls.Add(matrixHeightLabel);

                createButton = new Button
                {
                    Location = new Point(219, 41),
                    Size = new Size(109, 57),
                    Text = "Create"
                };
                lastButton = new Button
                {
                    Size = new Size(70, 23),
                    Location = new Point(5, Form.Height - 70),
                    Text = "<<",
                };
                
                Form.Controls.Add(createButton);
                Form.Controls.Add(lastButton);
                
            }
        }
        public override void EventSetter()
        {
            if (helper)
            {
                return;
            }
            matrixHeightTextBox.TextChanged += (o, e) => 
            {
                if (ErrorHelper(matrixHeightTextBox,false))
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
                if (ErrorHelper(matrixWidthTextBox,false))
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
            lastButton.Click += (o, e) => 
            {
                Terminate();
                LastApp.Initialize();
                LastApp.EventSetter();
            };
        }
        public override bool Terminate()
        {
            if (!helper)
            {
                helper = true;

                Form.Controls.Remove(matrixWidthTextBox);
                Form.Controls.Remove(matrixHeightTextBox);
                Form.Controls.Remove(answerTextBox);

                Form.Controls.Remove(matrixHeightLabel);
                Form.Controls.Remove(matrixWidthLabel);

                Form.Controls.Remove(lastButton);
                Form.Controls.Remove(createButton);
                Form.Controls.Remove(CalculateButton);
                if (matrixElements != null)
                {
                    foreach (var element in matrixElements)
                    {
                        Form.Controls.Remove(element.First);
                    }
                }
               
                return true;
            }
            else
                return false;
        }
        protected virtual void MatrixCreation(object o, EventArgs e)
        {
            bool localhelper = true;
            if (Height == 0)
            {
                matrixHeightTextBox.Text = "0";
                localhelper = false;
            }
                
            if (Width == 0)
            {
                matrixWidthTextBox.Text = "0";
                localhelper = false;
            }
            if (!localhelper)
            {
                return;
            }
            if (matrixElements != null)
            {
                for (int i = 0; i < prevWidth; i++)
                {
                    for (int j = 0; j < prevHeight; j++)
                    {
                        Form.Controls.Remove(matrixElements[i, j].First);                      
                    }
                }
                for (int i = 0; i < prevHeight; i++)
                {
                    Form.Controls.Remove(additionalElements[i].First);
                }
            }

            Form.Controls.Remove(CalculateButton);
            Form.Controls.Remove(answerTextBox);

            prevHeight = Height;
            prevWidth = Width;
            matrixElements = new ValuePair<TextBox, int>[Width, Height];
            additionalElements = new ValuePair<TextBox, int>[Height];


            if (Width*52<360)
                Form.Size = new Size(360, 260 + 29 * Height);
            else
                Form.Size = new Size(Width * 52+ 90, 260 + 29 * Height);

            lastButton.Location = new Point(5, Form.Height - 70);
            CalculateButton = new Button
            {
                Size = new Size(70, 23),
                Location = new Point(5, Form.Height - 140),
                Text = "calculate",
            };
            Form.Controls.Add(CalculateButton);
            CalculateButton.Click += Calculation;


            answerTextBox = new TextBox 
            {
                Size = new Size(Width * 52, 23),
                Location = new Point(5, Form.Height - 110),
                ReadOnly = true
            };
            Form.Controls.Add(answerTextBox);

            for (int i = 0; i < Height; i++)
            {              
                for (int j = 0; j < Width; j++)
                {
                    matrixElements[j, i] = new ValuePair<TextBox, int>(new TextBox
                    {
                        Size = StableBoxSize,
                        Location = new Point(FirstBoxLocation.X + j * (StableBoxSize.Width + 6),
                                             FirstBoxLocation.Y + i * (StableBoxSize.Height + 6)),
                        TextAlign = HorizontalAlignment.Right
                    }, 0);                   
                    Form.Controls.Add(matrixElements[j, i].First);                   
                }
                additionalElements[i] = new ValuePair<TextBox, int>(new TextBox
                {
                    Size = StableBoxSize,
                    Location = new Point(FirstBoxLocation.X + Width * (StableBoxSize.Width + 6),
                                             FirstBoxLocation.Y + i * (StableBoxSize.Height + 6)),
                    TextAlign = HorizontalAlignment.Right,
                    BackColor = Color.LightGray
                }, 0);
                Form.Controls.Add(additionalElements[i].First);
            }
            foreach (var element in matrixElements)
            {
                element.First.TextChanged += (obj, args) =>
                {
                    if (ErrorHelper(element.First,true))
                    {
                        if (element.First.Text == "")
                        {
                            element.Second = 0;
                        }
                        else
                        {
                            if (element.First.Text == "-")
                            {
                                element.Second = -1;
                            }
                            else
                            {
                                element.Second = int.Parse(element.First.Text);
                            }                           
                        }
                    }
                    else
                    {
                        element.First.Text = element.Second.ToString();
                    }
                };
            }
            foreach (var element in additionalElements)
            {
                element.First.TextChanged += (obj, args) =>
                {
                    if (ErrorHelper(element.First,true))
                    {
                        if (element.First.Text == "")
                        {
                            element.Second = 0;
                        }
                        else
                        {
                            if (element.First.Text == "-")
                            {
                                element.Second = -1;
                            }
                            else
                            {
                                element.Second = int.Parse(element.First.Text);
                            }
                        }
                    }
                    else
                    {
                        element.First.Text = element.Second.ToString();
                    }
                };
            }


        }
        protected void Calculation(object o, EventArgs e)
        {
            int[,] mainElements = new int[matrixElements.GetLength(0),matrixElements.GetLength(1)];
            int[] addElements = new int[additionalElements.Length];
            for (int i = 0; i < mainElements.GetLength(1); i++)
            {
                for (int j = 0; j < mainElements.GetLength(0); j++)
                {
                    mainElements[j, i] = matrixElements[j, i].Second;
                }
                addElements[i] = additionalElements[i].Second;
            }

            var sortedMatrix1 = MatrixCalculation.MatrixSorter(mainElements,addElements);
            if (sortedMatrix1 == null)
            {
                answerTextBox.Text = "Not possible to calculate";
                answerTextBox.Width = answerTextBox.Text.Length * 7;
            }
            else
            {
                int[,] turnedMains = new int[sortedMatrix1.First.GetLength(1), sortedMatrix1.First.GetLength(0)];
                for (int i = 0; i < sortedMatrix1.First.GetLength(1); i++)
                {
                    for (int j = 0; j < sortedMatrix1.First.GetLength(0); j++)
                    {
                        turnedMains[i, j] = sortedMatrix1.First[j, i];
                    }
                }
                var answers = MatrixCalculation.CalculateMatrix(turnedMains, sortedMatrix1.Second);
                if (answers == null)
                {
                    answerTextBox.Text = "Not possible to calculate";
                    answerTextBox.Width = answerTextBox.Text.Length * 7;
                }
                else
                {
                    string answerLine = "";
                    for (int i = 0; i < answers.Length; i++)
                    {
                        answerLine += "x" + (i + 1) + "= " + Math.Round(answers[i],2) + "  ";
                    }
                    answerTextBox.Text = answerLine;
                    answerTextBox.Width = answerTextBox.Text.Length * 7;
                    if (answerTextBox.Width > Form.Width)
                    {
                        Form.Width = answerTextBox.Width + 10;
                    }
                }
            }
            Form.Invalidate();
        }
    }
}
