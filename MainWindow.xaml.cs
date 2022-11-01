using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Creolin_Gopal_Easy_Games_Developer_Test.Models;
using Creolin_Gopal_Easy_Games_Developer_Test.Objects;
using Creolin_Gopal_Easy_Games_Developer_Test.Utility;

namespace Creolin_Gopal_Easy_Games_Developer_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            populateComboboxUsers();
            populateComboboxTransType();
            populateDataGrid(-1);
            populateBalance(-1);
            uiBalance.Visibility = Visibility.Hidden;

        }

        private static EasyGames_Developer_AssesmentContext CONTEXT = new EasyGames_Developer_AssesmentContext();
        private static List<Transaction_Display> transactions = new List<Transaction_Display>();
        private static short filterType =0;
        private static decimal filterAmount =0 ;
        public static bool amountAsc, amountDesc , clientAsc,clientDesc;
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text


        private void btnSortTransaction_Click(object sender, RoutedEventArgs e)
        {
            grpCreate.Visibility = Visibility.Hidden;
            grpEdit.Visibility = Visibility.Hidden;
            grpFilter.Visibility = Visibility.Hidden;
            grpSort.Visibility = Visibility.Visible;
        }

        private void btnFilterTransaction_Click(object sender, RoutedEventArgs e)
        {
            grpCreate.Visibility = Visibility.Hidden;
            grpEdit.Visibility = Visibility.Hidden;
            grpFilter.Visibility = Visibility.Visible;
            grpSort.Visibility = Visibility.Hidden;
        }

        private void btnAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            grpCreate.Visibility = Visibility.Visible;
            grpEdit.Visibility = Visibility.Hidden;
            grpFilter.Visibility = Visibility.Hidden;
            grpSort.Visibility = Visibility.Hidden;
           
        }

        private void btnEditTransaction_Click(object sender, RoutedEventArgs e)
        {
            grpCreate.Visibility = Visibility.Hidden;
            grpEdit.Visibility = Visibility.Visible;
            grpFilter.Visibility = Visibility.Hidden;
            grpSort.Visibility = Visibility.Hidden;
        }

        public void populateDataGrid(int clientID)
        {
            List<TransactionTbl> transaction = CONTEXT.TransactionTbls.ToList();
            string transactionalType = "";
            string clientName = "";
            transactions.Clear();
            grdTransactions.ItemsSource = null;
            if (amountAsc == true)
            {
                transaction = CONTEXT.TransactionTbls.OrderBy(x => x.TransactionAmount).ToList(); 
            }
            else if (amountDesc == true)
            {
                transaction = CONTEXT.TransactionTbls.OrderByDescending(x => x.TransactionAmount).ToList();
            }
            else if (clientAsc == true)
            {
                transaction = CONTEXT.TransactionTbls.OrderBy(x => x.ClientId).ToList();
            }
            else if (clientDesc == true)
            {
                transaction = CONTEXT.TransactionTbls.OrderByDescending(x => x.ClientId).ToList();
            }




        //TODO: FIX
        if (clientID == -1)
            {
                if (filterAmount != 0 && filterType != 0)
                {
                    foreach (TransactionTbl transactionTbl in transaction.Where(x => x.TransactionAmount == filterAmount && x.TransactionTypeId == filterType))
                    {
                            var TType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            if (TType != null)
                            {
                                transactionalType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            }

                            var C = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            if (C != null)
                            {
                                clientName = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            }

                            Transaction_Display td = new Transaction_Display(Convert.ToInt32(transactionTbl.TransactionId), "R " + transactionTbl.TransactionAmount.ToString(), transactionalType, clientName, transactionTbl.TransactionComment.ToString());
                            transactions.Add(td);
                        

                    }
                }
                else if (filterAmount != 0 && filterType == 0)
                {
                    foreach (TransactionTbl transactionTbl in transaction.Where(x => x.TransactionAmount == filterAmount))
                    {
                            var TType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            if (TType != null)
                            {
                                transactionalType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            }

                            var C = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            if (C != null)
                            {
                                clientName = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            }

                            Transaction_Display td = new Transaction_Display(Convert.ToInt32(transactionTbl.TransactionId), "R " + transactionTbl.TransactionAmount.ToString(), transactionalType, clientName, transactionTbl.TransactionComment.ToString());
                            transactions.Add(td);
                        

                    }
                }
                else if (filterAmount == 0 && filterType != 0)
                {
                    foreach (TransactionTbl transactionTbl in transaction.Where(x => x.TransactionTypeId == filterType))
                    {

                            var TType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            if (TType != null)
                            {
                                transactionalType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            }

                            var C = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            if (C != null)
                            {
                                clientName = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            }

                            Transaction_Display td = new Transaction_Display(Convert.ToInt32(transactionTbl.TransactionId), "R " + transactionTbl.TransactionAmount.ToString(), transactionalType, clientName, transactionTbl.TransactionComment.ToString());
                            transactions.Add(td);
                        

                    }
                }
                else
                {
                    foreach (TransactionTbl transactionTbl in transaction)
                    {
                        var TType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                        if (TType != null)
                        {
                            transactionalType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                        }

                        var C = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                        if (C != null)
                        {
                            clientName = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                        }

                        Transaction_Display td = new Transaction_Display(Convert.ToInt32(transactionTbl.TransactionId), "R " + transactionTbl.TransactionAmount.ToString(), transactionalType, clientName, transactionTbl.TransactionComment.ToString());
                        transactions.Add(td);
                    }
                }
            }
        else
            {
                if (filterAmount != 0 && filterType != 0)
                {
                    foreach (TransactionTbl transactionTbl in transaction.Where(x => x.TransactionAmount == filterAmount && x.TransactionTypeId == filterType))
                    {

                        if (transactionTbl.ClientId == clientID)
                        {
                            var TType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            if (TType != null)
                            {
                                transactionalType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            }

                            var C = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            if (C != null)
                            {
                                clientName = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            }

                            Transaction_Display td = new Transaction_Display(Convert.ToInt32(transactionTbl.TransactionId), "R " + transactionTbl.TransactionAmount.ToString(), transactionalType, clientName, transactionTbl.TransactionComment.ToString());
                            transactions.Add(td);
                        }

                    }
                }
                else if (filterAmount != 0 && filterType == 0)
                {
                    foreach (TransactionTbl transactionTbl in transaction.Where(x => x.TransactionAmount == filterAmount))
                    {

                        if (transactionTbl.ClientId == clientID)
                        {
                            var TType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            if (TType != null)
                            {
                                transactionalType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            }

                            var C = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            if (C != null)
                            {
                                clientName = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            }

                            Transaction_Display td = new Transaction_Display(Convert.ToInt32(transactionTbl.TransactionId), "R " + transactionTbl.TransactionAmount.ToString(), transactionalType, clientName, transactionTbl.TransactionComment.ToString());
                            transactions.Add(td);
                        }

                    }
                }
                else if (filterAmount == 0 && filterType != 0)
                {
                    foreach (TransactionTbl transactionTbl in transaction.Where(x => x.TransactionTypeId == filterType))
                    {

                        if (transactionTbl.ClientId == clientID)
                        {
                            var TType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            if (TType != null)
                            {
                                transactionalType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            }

                            var C = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            if (C != null)
                            {
                                clientName = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            }

                            Transaction_Display td = new Transaction_Display(Convert.ToInt32(transactionTbl.TransactionId), "R " + transactionTbl.TransactionAmount.ToString(), transactionalType, clientName, transactionTbl.TransactionComment.ToString());
                            transactions.Add(td);
                        }

                    }
                }
                else
                {
                    foreach (TransactionTbl transactionTbl in transaction)
                    {

                        if (transactionTbl.ClientId == clientID)
                        {
                            var TType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            if (TType != null)
                            {
                                transactionalType = CONTEXT.TransactionTypes.Where(x => x.TransactionTypeId == transactionTbl.TransactionTypeId).Select(x => x.TransactionTypeName).First();
                            }

                            var C = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            if (C != null)
                            {
                                clientName = CONTEXT.Clients.Where(x => x.ClientId == transactionTbl.ClientId).Select(x => x.ClientName + " " + x.ClientSurname).First();
                            }

                            Transaction_Display td = new Transaction_Display(Convert.ToInt32(transactionTbl.TransactionId), "R " + transactionTbl.TransactionAmount.ToString(), transactionalType, clientName, transactionTbl.TransactionComment.ToString());
                            transactions.Add(td);
                        }

                    }
                }

            }

            grdTransactions.ItemsSource = transactions;
        }

        public void populateBalance(int ClientID)
        {

            if (ClientID == -1)
            {
                uiBalance.Visibility = Visibility.Hidden;
            }
            else
            {
                uiBalance.Visibility = Visibility.Visible;
                uiBalance.Text = "R" + (CONTEXT.Clients.Where(x => x.ClientId == ClientID).Select(x => x.ClientBalance).FirstOrDefault());

            }
        }


        public void refreshComponents()
        {
            grpCreate.Visibility = Visibility.Hidden;
            grpEdit.Visibility = Visibility.Hidden;
            grpFilter.Visibility = Visibility.Hidden;
            grpSort.Visibility = Visibility.Hidden;
            cmbClients.SelectedItem = null;
            populateDataGrid(-1);
        }

        public void populateComboboxUsers()
        {
            foreach (Client item in CONTEXT.Clients.ToList())
            {
                cmbClients.Items.Add("(" + item.ClientId + ") " + item.ClientName + " " + item.ClientSurname);
            }

        }

        public void populateComboboxTransType()
        {
            foreach (TransactionType item in CONTEXT.TransactionTypes.ToList())
            {
                cmbTransactionType.Items.Add("(" + item.TransactionTypeId + ") " + item.TransactionTypeName);
                cmbFilterTransactionType.Items.Add("(" + item.TransactionTypeId + ") " + item.TransactionTypeName); 
            }

        }

        private void cmbClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbClients.SelectedItem != null)
            {
            string client = cmbClients.SelectedItem.ToString();
            string clientid = client.Substring(1,1);
            populateDataGrid(Convert.ToInt32(clientid));
            populateBalance(Convert.ToInt32(clientid));
            }

        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private async void btnSubmitAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            int clientID = 0;
            try
            {
                if (IsTextAllowed(txbTransactionAmount.Text))
                {
                    decimal transactionAmount = Convert.ToDecimal(txbTransactionAmount.Text);
                    string transType = cmbTransactionType.SelectedItem.ToString();
                    string transName = transType.Substring(4).Trim();
                    short transactionTypeID = Convert.ToInt16(transType.Substring(1, 1));
                    string client = cmbClients.SelectedItem.ToString();
                    clientID = Convert.ToInt32(client.Substring(1, 1));
                    string TransactionComment = txbTransactionComment.Text;

                    Client SerchedEntry = CONTEXT.Clients.Where(x => x.ClientId == clientID).FirstOrDefault();
                    if (transName.Equals("Debit"))
                    {
                        SerchedEntry.ClientBalance += transactionAmount;
                        TransactionTbl trans = new TransactionTbl(ValidationHndeler.getTransID(), transactionAmount, transactionTypeID, clientID, TransactionComment);
                        CONTEXT.Clients.Update(SerchedEntry);
                        CONTEXT.AddAsync(trans);
                        await CONTEXT.SaveChangesAsync();
                        populateDataGrid(clientID);
                        populateBalance(clientID);
                    }
                    if (transName.Equals("Credit"))
                    {
                        SerchedEntry.ClientBalance -= transactionAmount;
                        TransactionTbl trans = new TransactionTbl(ValidationHndeler.getTransID(), -transactionAmount, transactionTypeID, clientID, TransactionComment);
                        CONTEXT.Clients.Update(SerchedEntry);
                        CONTEXT.AddAsync(trans);
                        await CONTEXT.SaveChangesAsync();
                        populateDataGrid(clientID);
                        populateBalance(clientID);
                    }
                }
                else
                {
                    MessageBox.Show("An Error occured \n\n Please ensure all fields were filled correctly");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error occured \n\n Please ensure all fields were filled");
            }
            //finally
            //{
            //    populateDataGrid(clientID);
            //    populateBalance(clientID);
            //}

        }

        private void btnSortbyClientDecending_Click(object sender, RoutedEventArgs e)
        {
            amountDesc = false;
            amountAsc = false;
            clientDesc = true;
            clientAsc = false;
            if (cmbClients.SelectedItem != null)
            {
                string client = cmbClients.SelectedItem.ToString();
                string clientid = client.Substring(1, 1);
                populateDataGrid(Convert.ToInt32(clientid));
                populateBalance(Convert.ToInt32(clientid));
            }
            else
            {
                populateDataGrid(-1);
                populateBalance(-1);
            }
        }

        private void btnSortbyAmountAscending_Click(object sender, RoutedEventArgs e)
        {
            amountDesc = false;
            amountAsc = true;
            clientDesc = false;
            clientAsc = false;
            if (cmbClients.SelectedItem != null)
            {
                string client = cmbClients.SelectedItem.ToString();
                string clientid = client.Substring(1, 1);
                populateDataGrid(Convert.ToInt32(clientid));
                populateBalance(Convert.ToInt32(clientid));
            }
            else
            {
                populateDataGrid(-1);
                populateBalance(-1);
            }
        }

        private void btnSortbyClientAscending_Click(object sender, RoutedEventArgs e)
        {
            amountDesc = false;
            amountAsc = false;
            clientDesc = false;
            clientAsc = true;
            if (cmbClients.SelectedItem != null)
            {
                string client = cmbClients.SelectedItem.ToString();
                string clientid = client.Substring(1, 1);
                populateDataGrid(Convert.ToInt32(clientid));
                populateBalance(Convert.ToInt32(clientid));
            }
            else
            {
                populateDataGrid(-1);
                populateBalance(-1);
            }
        }

        private async void btnSubmitEditTransaction_ClickAsync(object sender, RoutedEventArgs e)
        {
            int clientID = 0;
            try
            {
                long transID = ((Transaction_Display)grdTransactions.SelectedValue).TransactionId;
                TransactionTbl SerchedEntry = CONTEXT.TransactionTbls.Where(x => x.TransactionId == transID).FirstOrDefault();
                clientID = SerchedEntry.ClientId;

                SerchedEntry.TransactionComment = txbEditTransactionComment.Text;
                CONTEXT.TransactionTbls.Update(SerchedEntry);
                await CONTEXT.SaveChangesAsync();
                populateDataGrid(clientID);
                populateBalance(clientID);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
        }

        private void btnSubmitFilterTransaction_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (txbFilterTransactionAmount.Text.Equals("") || txbFilterTransactionAmount.Text.Equals(null))
                {
                    filterAmount = 0;
                }
                else
                {
                    filterAmount = Convert.ToDecimal(txbFilterTransactionAmount.Text);
                }

            }
            catch (Exception)
            {

            }

            try
            {
                if (cmbFilterTransactionType.SelectedItem == null)
                {
                     filterType = 0;
                }
                else
                {
                    string type = cmbFilterTransactionType.SelectedItem.ToString();
                    string typeid = type.Substring(1, 1);
                    filterType = Convert.ToInt16(typeid);
                }

            }
            catch (Exception)
            {
                
            }
            finally
            {
                if (cmbClients.SelectedItem != null)
                {
                    string client = cmbClients.SelectedItem.ToString();
                    string clientid = client.Substring(1, 1);
                    populateDataGrid(Convert.ToInt32(clientid));
                    populateBalance(Convert.ToInt32(clientid));
                }
                else
                {
                    populateDataGrid(-1);
                    populateBalance(-1);
                }
            }



        }

        private void btnSortbyAmountDecending_Click(object sender, RoutedEventArgs e)
        {
            amountDesc = true;
            amountAsc = false;
            clientDesc = false;
            clientAsc = false;
            if (cmbClients.SelectedItem != null)
            {
                string client = cmbClients.SelectedItem.ToString();
                string clientid = client.Substring(1, 1);
                populateDataGrid(Convert.ToInt32(clientid));
                populateBalance(Convert.ToInt32(clientid));
            }
            else
            {
                populateDataGrid(-1);
                populateBalance(-1);
            }
        }
    }
}
