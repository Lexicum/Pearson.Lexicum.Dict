/*  Copyright 2014 Lexicum Ltd. and Pearson plc
 
    This file is part of Pearson Dictionary API Client for .NET 
    The API Client is free software distributed under the GNU Lesser General Public License
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pearson.Lexicum.ApiClient;

namespace Pearson.Lexicum.ApiTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (DictLookup.ApiKey == "<not_set>")
            {
                Console.WriteLine("Enter your API key in DictLookup.cs! You are now running in sandbox mode.");
                Console.WriteLine();
            }

            Console.WriteLine("Enter word to look up: ");
            String term = Console.ReadLine();

            var lookup = Pearson.Lexicum.ApiClient.DictLookup.Get(term);
            foreach (Definition def in lookup.Definition)
            {
                if (!String.IsNullOrEmpty(def.Meta))
                {
                    Console.Write("(" + def.Meta + ") ");
                }
                Console.WriteLine(def.Text);
            }
        }
    }
}
