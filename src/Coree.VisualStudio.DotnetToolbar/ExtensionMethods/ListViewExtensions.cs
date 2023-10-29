using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Coree.VisualStudio.DotnetToolbar.ExtensionMethods
{
    public static class ListViewExtensions
    {
        public static int GetSelectedIndex(this ListView listView)
        {
            return listView.SelectedItems.Cast<ListViewItem>().FirstOrDefault()?.Index ?? -1;
        }

        public static void AddClass<T>(this ListView listView, List<T> dataList)
        {
            listView.View = View.Details;
            listView.FullRowSelect = true;
            listView.MultiSelect = false;
            

            // Clear existing columns
            listView.Columns.Clear();

            // Add columns
            var properties = typeof(T).GetProperties();
            var sortedProperties = properties
                .Select(p => new
                {
                    Property = p,
                    DisplayAttribute = (DisplayAttribute)p.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault()
                })
                .Where(x => x.DisplayAttribute != null)
                .OrderBy(x => x.DisplayAttribute.Order);

            foreach (var item in sortedProperties)
            {
                // Use DisplayAttribute.Name if available; otherwise, use Property.Name
                string columnName = item.DisplayAttribute?.Name ?? item.Property.Name;
                listView.Columns.Add(columnName);
            }

            foreach (var dataItem in dataList)
            {
                ListViewItem listViewItem = new ListViewItem();
                bool firstColumn = true;

                foreach (var item in sortedProperties)
                {
                    var value = item.Property.GetValue(dataItem)?.ToString() ?? string.Empty;

                    if (firstColumn)
                    {
                        listViewItem.Text = value;
                        firstColumn = false;
                    }
                    else
                    {
                        listViewItem.SubItems.Add(value);
                    }
                }

                listViewItem.Tag = dataItem;

                listView.Items.Add(listViewItem);
            }

            // Auto-resize columns
            for (int i = 0; i < listView.Columns.Count; i++)
            {
                listView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                int contentWidth = listView.Columns[i].Width;

                listView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.HeaderSize);
                int headerWidth = listView.Columns[i].Width;

                listView.Columns[i].Width = Math.Max(contentWidth, headerWidth);
            }

            // After populating items, select the first row
            if (listView.Items.Count > 0)
            {
                listView.Items[0].Selected = true;
                listView.Items[0].Focused = true;
            }
            
        }
    }
}