using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TabPro
{
    /// <summary>
    /// Interaction logic for InscriptionPage.xaml
    /// </summary>
    public partial class InscriptionPage : Window
    {
        public InscriptionPage()
        {
            InitializeComponent();
        }

        private void RegionComboBox_DropDownOpened(object sender, EventArgs e)
        {
            RegionComboBox.Items.Clear(); // Efface les éléments existants

            string[] regions = new string[]
            {
        "Adamaoua",
        "Centre",
        "Est",
        "Extrême-Nord",
        "Littoral",
        "Nord",
        "Nord-Ouest",
        "Ouest",
        "Sud",
        "Sud-Ouest"
            };

            foreach (var region in regions)
            {
                RegionComboBox.Items.Add(region);
            }
        }


    }

}
