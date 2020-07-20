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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MorseWPF.Pages
{
    /// <summary>
    /// Interaktionslogik für MorseTable.xaml
    /// </summary>
    public partial class MorseTable : Page
    {
        private static MorseTable instance = null;
        public static MorseTable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MorseTable();
                }
                return instance;
            }
        }

        private MorseTable()
        {
            InitializeComponent();
        }
    }
}
