using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace nxk161830Asg4
{
    public partial class Form1 : Form
    {
        // BackgroundWorker
        BackgroundWorker backgroundWorker1 = null;

        // Calculating the percentage for the progress bar
        private int highestPercentageReached = 0;

        // Calculating Number of Searched Matched Occurences
        private int countSearchResults = 0;

        // Thread Safe Queue to store the found results
        ConcurrentQueue<SearchResult> qSearchResults = null;

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            
            resetForm();
        }

        /**
         * This method prepares the form for a New Search
         **/
        private void resetForm()
        {
            backgroundWorker1 = null;
            
            listViewSearchResults.Items.Clear();
            textBoxFileName.Text = "";
            //textBoxSearchString.Text = "";
            progressBar1.Value = 0;
            btnSearch.Text = "Search";
            labelError.Text = "Please Select a File and Enter a String to search in the file";
            labelSearchCount.Text = "";
            labelSearchCount.Enabled = false;

            textBoxFileName.Enabled = true;
            textBoxSearchString.Enabled = true;
            btnBrowse.Enabled = true;

            highestPercentageReached = 0;
            countSearchResults = 0;

            qSearchResults = new ConcurrentQueue<SearchResult>();

            InitializeBackgroundWorker();
        }

        /**
         * Initializing the background worker
         **/
        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        /**
         * This method actually finds the string in the file
         * And displays the result in the data grid view
         **/
        // This event handler is where the actual,
        // potentially time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("backgroundWorker1_DoWork() Called...");
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.

            // Call the SearchFunction from Here
            ChildThreadSearch(sender, e, worker);
        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                labelError.Text = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Cancelled Called !");
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                labelError.Text = "Serach Cancelled";

                btnSearch.Text = "Search";
                textBoxFileName.Enabled = true;
                textBoxSearchString.Enabled = true;
                btnBrowse.Enabled = true;
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                labelError.Text = "Search Completed";
                progressBar1.Value = 100;

                btnSearch.Text = "Search";
                textBoxFileName.Enabled = true;
                textBoxSearchString.Enabled = true;
                btnBrowse.Enabled = true;

                if(listViewSearchResults.Items.Count == 0)
                {
                    labelSearchCount.Text = "No Matches Found";
                }
            }
        }

        // This event handler updates the progress bar.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage > 100 ? 100 : e.ProgressPercentage;
        }

      
        /**
         * On clicking the Browse button
         * opens the file dialog to select a file
         **/
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            
            // Open the Browse File Dialog Box
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                resetForm();

                String fileName = openFileDialog1.FileName;
                textBoxFileName.Text = fileName;
            }
        }


        /**
         * On clicking the Search button
         * Starts the searching for the string in the file
         **/
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Search" && backgroundWorker1!= null)
            {
                Console.WriteLine("Search Clicked");

                if (!SearchInitiate())
                {
                    return;
                }

                backgroundWorker1.RunWorkerAsync();
            }
            else if (btnSearch.Text == "Cancel" && backgroundWorker1 != null && backgroundWorker1.IsBusy)
            {
                Console.WriteLine("Cancel Clicked");
                backgroundWorker1.CancelAsync();
            }
        }

        /**
         * On successful Entry enable the search the Button
         **/
        private void enableSearchButton(object sender, EventArgs e)
        {
            if ((textBoxFileName.TextLength > 0) && (textBoxSearchString.TextLength > 0))
            {
                btnSearch.Enabled = true;
            }
            else
            {
                btnSearch.Enabled = false;
            }
        }

        /**
         * This method initiates the search
         * for the string in the file
         **/
        public Boolean SearchInitiate()
        {
            progressBar1.Value = 0;

            if (textBoxFileName.Text == "")
            {
                this.Invoke((MethodInvoker)(() => labelError.Text = "Please Select the File to Search"));
                textBoxFileName.Focus();
                return false;
            }

            if (textBoxSearchString.Text == "")
            {
                this.Invoke((MethodInvoker)(() => labelError.Text = "Please Enter the String to Search"));
                textBoxSearchString.Focus();
                return false;
            }

            String fileName = textBoxFileName.Text.Trim();
            if (!isFileExists(fileName))
            {
                this.Invoke((MethodInvoker)(() => labelError.Text = "File Does not Exists!"));
                return false;
            }

            this.Invoke((MethodInvoker)(() => btnSearch.Text = "Cancel"));
            this.Invoke((MethodInvoker)(() => labelError.Text = "Searching....."));
            this.Invoke((MethodInvoker)(() => labelSearchCount.Text = "Matches Found: 0"));

            this.Invoke((MethodInvoker)(() => labelSearchCount.Enabled = true));

            this.Invoke((MethodInvoker)(() => textBoxFileName.Enabled = false));
            this.Invoke((MethodInvoker)(() => textBoxSearchString.Enabled = false));
            this.Invoke((MethodInvoker)(() => btnBrowse.Enabled = false));

            this.Invoke((MethodInvoker)(() => listViewSearchResults.Items.Clear()));
            this.Invoke((MethodInvoker)(() => progressBar1.Value = 0));

            backgroundWorker1 = null;
            highestPercentageReached = 0;
            countSearchResults = 0;

            qSearchResults = new ConcurrentQueue<SearchResult>();

            InitializeBackgroundWorker();

            return true;
        }

        /**
         * This method gives the animated dots ....
         **/
        public String getSearchAnimateDot(int index)
        {
            if(index < 1)
            {
                return ".....";
            }

            String dot = "";

            int index_1 = index / 100;
            int position = (index_1 % 10);
            
            for(int i = 1; i <= position; i++)
            {
                dot += ". ";
            }
            return dot;
        }

        /**
         * This method contains the logic for search
         * And to display as soon as a result is found
         **/
        public void ChildThreadSearch(object sender, DoWorkEventArgs e, BackgroundWorker worker)
        {
            String fileName = textBoxFileName.Text;
            Console.WriteLine(fileName);
            String searchString = textBoxSearchString.Text.Trim().ToLower();
            Console.WriteLine(searchString);
            String line = null;
            int lineIndex = 0;
            System.IO.StreamReader file = new System.IO.StreamReader(@fileName);

            long fileSize = new System.IO.FileInfo(@fileName).Length;
            int lineSize = 0;
            Console.WriteLine("fileSize = " + fileSize);

            try
            {
                while ((line = file.ReadLine()) != null)
                {
                    lineIndex += 1;
                    if (line.Trim().ToLower().Contains(searchString))
                    {
                        countSearchResults += 1;
                        Console.WriteLine("Search Results => LineNo = " + lineIndex + ", line = " + line);
                        SearchResult objSearchResult = new SearchResult();
                        objSearchResult.LineNo = lineIndex;
                        objSearchResult.LineText = line;
                        qSearchResults.Enqueue(objSearchResult);

                        ChildThreadDisplay();
                    }

                    // Report progress as a percentage of the total task.
                    this.Invoke((MethodInvoker)(() => labelError.Text = "Searching" + getSearchAnimateDot(lineIndex)));
                    lineSize += (line.Length);
                    int percentComplete = (int)((float)lineSize / (float)fileSize * 100);
                    if (percentComplete > highestPercentageReached)
                    {
                        highestPercentageReached = percentComplete;
                        worker.ReportProgress(percentComplete);
                    }

                    // Check if Cancel Button Action is pending
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred " + ex);
            }
            finally
            {
                file.Close();
            }
        }

        /**
         * This method displays the search result onto the datagrid
         **/
        public void ChildThreadDisplay()
        {
            if (qSearchResults.Count > 0)
            {
                SearchResult objSearchResult = null;
                qSearchResults.TryDequeue(out objSearchResult);

                String[] row = new String[] { Convert.ToString(objSearchResult.LineNo), objSearchResult.LineText };
                updateDataGridSearchResults(row);
            }
           
        }

        /**
         * This method update the listView everytime a new search result is found
         **/
        private void updateDataGridSearchResults(String[] row)
        {
            Console.WriteLine("updateDataGridSearchResults() - Display [ " + row[0] + ", " + row[1] + " ]");
            this.Invoke((MethodInvoker)(() => listViewSearchResults.Items.Add(new ListViewItem(new[] { row[0], row[1] }))));
            this.Invoke((MethodInvoker)(() => labelSearchCount.Text = "Matches Found: " + listViewSearchResults.Items.Count));
        }

        /**
        * This method checks if the
        * File to be evaluated either exists
        **/
        public Boolean isFileExists(String fileName)
        {
            if (!System.IO.File.Exists(@fileName))
            {
                Console.WriteLine("FileNot Found!!!");
                return false;
            }

            return true;
        }

        /**
         * This method handles 
         * the pressing of the Enter key on the search text box
         **/
        private void onEnterKeySearch(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        //https://msdn.microsoft.com/en-us/library/dd267265(v=vs.110).aspx
        //https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-make-thread-safe-calls-to-windows-forms-controls
        //https://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker(v=vs.110).aspx
    }
}
