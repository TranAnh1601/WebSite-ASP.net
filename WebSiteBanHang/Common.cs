using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WebSiteBanHang
{
    public class Common
    {
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach( DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value= row[valueField].ToString()
                });
             
            }
            return new SelectList(list, "Value", "Text");
        }
        public class ListtoDataTableConverter //?
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance); // |?
                foreach (PropertyInfo prop in Props)
                {
                    dataTable.Columns.Add(prop.Name); //?
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0 ; i < Props.Length; i++)
                    {
                        values[i] = Props[i].GetValue(item, null); //?

                    }
                    dataTable.Rows.Add(values); //?
                }
                return dataTable;

            }
        }
        public string Init_SlugName(string text)
        {
            for (int i = 32; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(".", "-");

            text = text.Replace(" ", "-");

            text = text.Replace(",", "-");

            text = text.Replace(";", "-");

            text = text.Replace(":", "-");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');

        }
    }
}