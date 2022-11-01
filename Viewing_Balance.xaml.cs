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

namespace Creolin_Gopal_Easy_Games_Developer_Test
{
    /// <summary>
    /// Interaction logic for Viewing_Balance.xaml
    /// </summary>
    public partial class Viewing_Balance : UserControl
    {
        public Viewing_Balance()
        {
            InitializeComponent();
            DataContext = this;
        }
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        DragDrop.DoDragDrop(this, this, DragDropEffects.Move);
        //    }
        //}
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Viewing_Balance), new PropertyMetadata(null));

    }
}
