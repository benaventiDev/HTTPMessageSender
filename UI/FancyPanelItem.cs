using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HTTPMessageSender.Form1;

namespace HTTPMessageSender.UI
{
    public class FancyPanelItem : Panel
    {
        public CheckBox checkBox { get; private set; }
        public FancyPanelItem(string fileName, int i, FileType type, Form1 f)
        {
            // Set the background color and border style
            var fileType = type;
            this.BackColor = Color.LightBlue;
            this.BorderStyle = BorderStyle.FixedSingle;
            Form1 form = f;

            // Add the file name label
            var fileNameLabel = new Label
            {
                Text = fileName,
                Location = new Point(10, 5),
                AutoSize = true
            };
            this.Padding = new Padding(15, 0, 0, 0);
            this.Controls.Add(fileNameLabel);

            // Add the checkbox
            checkBox = new CheckBox
            {
                Text = "",
                Location = new Point(170, 0),
                Tag = i // store the index in the Tag property
            };
            checkBox.CheckedChanged += (sender, e) =>
            {
                
            };
            this.Controls.Add(checkBox);

            // Set the size of the panel
            this.Size = new Size(200, checkBox.Bottom);
        }
    }

}
