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
    /// Interaktionslogik für MorseLearner.xaml
    /// </summary>
    public partial class MorseLearner : Page
    {
        // Singleton so it doesn't get created a bunch of times
        // by the navigation menu
        private static MorseLearner instance = null;
        public static MorseLearner Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MorseLearner();
                }
                return instance;
            }
        }
        private MorseLearner()
        {
            InitializeComponent();
        }
    }
}
