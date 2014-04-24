/*  Copyright 2014 Lexicum Ltd. and Pearson plc
 
    This file is part of Pearson Dictionary API Client for .NET 
    The API Client is free software distributed under the GNU Lesser General Public License
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pearson.Lexicum.ApiClient
{
    public class LookupResult
    {
        public LookupResult()
        {
            Definition = new List<Definition>() { };
            Examples = new List<String>() { };
        }
        public String Word { get; set; }
        public ICollection<String> Examples { get; set; }
        public ICollection<Definition> Definition { get; set; }
    }

    public class Definition
    {
        public String Text { get; set; }
        public String Meta { get; set; }

    }

}
