/*  Copyright 2014 Lexicum Ltd. and Pearson plc
 
    This file is part of Pearson Dictionary API Client for .NET 
    The API Client is free software distributed under the GNU Lesser General Public License
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pearson.Lexicum.ApiClient;

namespace Pearson.Lexicum.ApiTest
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            var lookup = Pearson.Lexicum.ApiClient.DictLookup.Get(txtTerm.Text);
            foreach (Definition def in lookup.Definition)
            {
                if (!String.IsNullOrEmpty(def.Meta))
                {
                    txtOutput.Text += "(" + def.Meta + ") ";
                }
                txtOutput.Text += def.Text + "\r\n";
            }
            if (lookup.Examples.Count > 0)
            {
                txtOutput.Text += "\r\nExamples:\r\n";
                foreach (var example in lookup.Examples)
                {
                    txtOutput.Text += example + "\r\n";
                }
            }
            if (DictLookup.ApiKey == "<not_set>")
            {
                txtOutput.Text += "\r\nEnter your API key in DictLookup.cs!";
            }

        }
    }
}
