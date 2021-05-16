using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Inlamning_3_ra_kod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CStack cs;
        int letterIndex = -1;
        string userInputDigit;
        public MainWindow()
        {
            // This line must be executed first:
            InitializeComponent();

            // Own stuff here:
            cs = new CStack();
            LetterField.Text = "T:\nZ:\nY:\nX:\n▹";
            VarLetterField.Text = "A=\nB=\nC=\nD=\nE=\nF=\nG=\nH=\n▹";
            UpdateNumberField();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            // Own stuff here:
            cs.Exit();

            // This line must be executed last:
            base.OnClosing(e);
        }
        private void UpdateNumberField()
        {
            NumberField.Text = cs.StackString();
            VarField.Text = cs.VarString();
        }
        private void NumBtn(object sender, RoutedEventArgs e)
        {
            Button B = sender as Button;
            userInputDigit = B.Content.ToString();
            cs.EntryAddNum(userInputDigit);
            UpdateNumberField();
        }
        private void NumComma(object sender, RoutedEventArgs e)
        {
            cs.EntryAddComma();
            UpdateNumberField();
        }
        private void NumCHS(object sender, RoutedEventArgs e)
        {
            cs.EntryChangeSign();
            UpdateNumberField();
        }
        private void EnterBtnClick(object sender, RoutedEventArgs e)
        {
            cs.Enter();
            UpdateNumberField();
        }
        private void OpBtn_binop(object sender, RoutedEventArgs e)
        {
            string op = (sender as Button).Content.ToString();
            cs.BinOp(op);
            UpdateNumberField();
        }
        private void OpBtn_unop(object sender, RoutedEventArgs e)
        {
            string op = (sender as Button).Content.ToString();
            cs.Unop(op);
            UpdateNumberField();
        }
        private void OpBtn_nilop(object sender, RoutedEventArgs e)
        {
            string op = (sender as Button).Content.ToString();
            cs.Nilop(op);
            UpdateNumberField();
        }
        private void Clr_Btn(object sender, RoutedEventArgs e)
        {
            cs.entry = "";
            UpdateNumberField();
        }
        private void Clx_Btn(object sender, RoutedEventArgs e)
        {
            cs.X = 0;
            UpdateNumberField();
        }
        private void Clst_Btn(object sender, RoutedEventArgs e)
        {
            cs.Y = cs.Z = cs.T = 0;
            UpdateNumberField();
        }
        private void OpBtn_swap(object sender, RoutedEventArgs e)
        {
            double tmp = cs.X; cs.X = cs.Y; cs.Y = tmp;
            UpdateNumberField();
        }
        private void OpBtn_dup(object sender, RoutedEventArgs e)
        {
            cs.RollSetX(cs.X);
            UpdateNumberField();
        }
        private void OpBtn_drop(object sender, RoutedEventArgs e)
        {
            cs.Drop();
            UpdateNumberField();
        }
        private void OpBtn_roll(object sender, RoutedEventArgs e)
        {
            cs.Roll();
            UpdateNumberField();
        }
        private void Call_but(object sender, RoutedEventArgs e)
        {
            string op = (sender as Button).Content.ToString();
            letterIndex = cs.SetAddress(op);

            UpdateNumberField();
        }

        private void OpBtn_sto(object sender, RoutedEventArgs e)
        {
            cs.SetVar(cs.X, letterIndex);
            letterIndex = -1;
            UpdateNumberField();
        }
        private void OpBtn_rcl(object sender, RoutedEventArgs e)
        {

            cs.GetVar(letterIndex);
            letterIndex = -1;
            UpdateNumberField();
        }
    }
}
