using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace PeriodicSystem.View
{
    public class pTableMap : Table
    {
        private Panel CreatePanel()
        {
            Panel p = new Panel();
            p.Background = Brushes.Transparent;
            p.Size = new Size(B, H); // B og H er hardcodede values, som du finder på forhånd via designeren.
            p.Click += PeriodicTableClick;
            return p;
        }
        private void PeriodicTableClick(object sender, EventArgs e)
        {
            Panel p = (Panel) sender;
            string element = p.Tag;
        }

        private void GenerateClickables()
        {
            foreach (KeyValuePair<int, int> pair in RowDictionary)
            {
                int rowNum = pair.Key;
                int elemCount = pair.Value;
                if (rowNum == 1)
                {
                    // Generer grundstof 1 og 2.
                    panel.Tag = PTableMap.Get(1);
                }
                else if (rowNum == 2 || rowNum == 3)
                {
                    // Generer elementerne for grundstofferne i den pågældende række. Tag højde for af der skal et offset til efter de første to elementer.
                }
                else if (rowNum > 3 && rowNum < 8)
                {
                    // for (i < elemCount) generer elementerne. Tag ikke højde for en skid.
                }
                else if (rowNum > 7)
                {
                    // Generer elemterne. Tag højde for at både X og Y skal offsettes. Brug elemCount i et for-loop.
                }
            }
        }
    }
}
