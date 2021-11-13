using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    abstract class AppBase
    {
        protected abstract bool helper { get; set; }
        protected Form1 Form { get; set; }
        abstract public void Initialize();
        abstract public void EventSetter();
        abstract public bool Terminate();
        protected bool ErrorHelper(TextBox textBox, bool choose)
        {
            if (choose)
            {
                try
                {
                    int.Parse(textBox.Text);
                }
                catch
                {
                    if (textBox.Text == "" || textBox.Text == "-")
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            else
            {
                try
                {
                    int.Parse(textBox.Text);
                }
                catch
                {
                    if (textBox.Text == "")
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
            
        }
    }
}
