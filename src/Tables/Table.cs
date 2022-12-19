using System;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Utility.Data.Tables
{
    public class Table
    {
        List<Row> rows;
        Row header;

        public Table(List<string> _headers)
        {
            header = new Row(_headers);
            rows = new List<Row>();
        }

        public Table()
        {
            new Table(new List<string>());
        }

        public void Add(List<string> row)
        {
            rows.Add(new Row(row));
        }

        public string Display
        (
            string rowSeperator = null,
            string colSeperator = "\t"
        )
        {
            if(rowSeperator == null) { rowSeperator = Environment.NewLine; }

            var colSizes = new List<int>();

            rows.Insert(0, header);

            foreach (var r in rows)
            {
                for (int i = 0; i < r.Cols.Count; i++)
                {
                    if(i + 1 > colSizes.Count) { colSizes.Add(r.Cols[i].Length); }
                    else if (r.Cols[i].Length > colSizes[i])
                    { 
                        colSizes[i] = r.Cols[i].Length;
                    }
                }
            }

            var rowStrings = new List<string>();
            
            foreach (var r in rows)
            {
                string rowString = "";
                for (int i = 0; i < r.Cols.Count; i++)
                {
                    rowString += r.Cols[i].PadRight(colSizes[i]) + colSeperator;
                }
                rowStrings.Add(rowString);
            }

            return String.Join(rowSeperator, rowStrings);
        }
        
        public string DisplayHtml()
        {
            string tabChars = "&nbsp;&nbsp;&nbsp;&nbsp;";
            var sb = new StringBuilder();
            
            sb.Append("<table>");
            sb.Append("<tr>");
            foreach(var s in header.Cols)
            {
                sb.Append("<th>"); 
                sb.Append(s); 
                sb.Append(tabChars); 
                sb.Append("</th>"); 
            }
            sb.Append("</tr>");
            foreach(var r in rows)
            {
                sb.Append("<tr>");
                foreach(var s in r.Cols)
                {
                   sb.Append("<td>"); 
                   sb.Append(s); 
                   sb.Append(tabChars); 
                   sb.Append("</td>"); 
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return sb.ToString();
        }

        class Row
        {
            public List<string> Cols;

            public Row(List<string> cols)
            {
                Cols = cols;
            }
        }
    }
}