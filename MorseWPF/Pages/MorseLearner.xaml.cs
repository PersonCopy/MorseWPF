using System.Windows.Controls;

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
