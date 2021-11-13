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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            SquareMover squareMover = new SquareMover(this);
            TruncatedConeVolumeCalculator truncatedCone = new TruncatedConeVolumeCalculator(this, squareMover);
            squareMover.NextApp = truncatedCone;
            Matrix matrixCalculator = new Matrix(this,truncatedCone);
            truncatedCone.NextApp = matrixCalculator;
            squareMover.Initialize();
            squareMover.EventSetter();
            
        }

    }
}
