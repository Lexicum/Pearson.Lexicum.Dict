/*  Copyright 2014 Lexicum Ltd. and Pearson plc
 
    This file is part of Pearson Dictionary API Client for .NET 

    The API Client is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    The API Client is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with the API Client.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pearson.Lexicum.ApiClient
{
    public class DictLookup
    {
        public const String ApiKey = "<not_set>"; //add your API key here - otherwise sandbox is used

        const Boolean DEBUG = false; // set to true to dump the json responses to a file

        public static LookupResult Get(String term)
        {
            String requestBody = "https://api.pearson.com/v2/dictionaries/ldoce5/entries?headword.exact=<term>&offset=0";
            
            String requestUri = requestBody.Replace("<term>", term);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            String responseJson = reader.ReadToEnd();
            reader.Close();

            if (DEBUG)
            {
                StreamWriter writer = new StreamWriter("deubg.json");
                writer.Write(responseJson);
                writer.Close();
            }

            LookupResult retVal = new LookupResult();
            retVal.Word = term;
            return Deserialize(responseJson, retVal);
        }
        private static LookupResult Deserialize(String json, LookupResult retVal)
        {
            var definition = new
            {
                results = new[] {new {
                    part_of_speech = "",
                    senses = new []{new {
                        definition="",
                        examples = new []{new {text=""}}}}
                } }
            };
            var obj = JsonConvert.DeserializeAnonymousType(json, definition);

            foreach (var res in obj.results)
            {
                foreach(var sense in res.senses)
                {
                    retVal.Definition.Add(new Definition { Text = sense.definition, Meta = res.part_of_speech });
                    if (sense.examples!=null) 
                        foreach (var exampleObj in sense.examples)
                        retVal.Examples.Add(exampleObj.text.ToString() );
                }

            }

            return retVal;
        }

    }
}
