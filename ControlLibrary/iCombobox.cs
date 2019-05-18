using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public class iCombobox : ComboBox
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // iCombobox
            // 
            this.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResumeLayout(false);

        }
    }
}
