﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace AppFrame.Helpers
{
    /// <summary>
    /// Extension methods for DataGrid
    /// </summary>
    public static class DataGridHelper
    {
        /// <summary>
        /// Gets the visual child of an element
        /// </summary>
        /// <typeparam name="T">Expected type</typeparam>
        /// <param name="parent">The parent of the expected element</param>
        /// <returns>A visual child</returns>
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }



        /// <summary>
        /// Gets the specified cell of the DataGrid
        /// </summary>
        /// <param name="grid">The DataGrid instance</param>
        /// <param name="row">The row of the cell</param>
        /// <param name="column">The column index of the cell</param>
        /// <returns>A cell of the DataGrid</returns>
        public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);

                return cell;
            }
            return null;
        }

        /// <summary>
        /// Gets the specified cell of the DataGrid
        /// </summary>
        /// <param name="grid">The DataGrid instance</param>
        /// <param name="row">The row index of the cell</param>
        /// <param name="column">The column index of the cell</param>
        /// <returns>A cell of the DataGrid</returns>
        public static DataGridCell GetCell(this DataGrid grid, int row, int column)
        {
            DataGridRow rowContainer = grid.GetRow(row);
            return grid.GetCell(rowContainer, column);
        }

        /// <summary>
        /// Gets the specified row of the DataGrid
        /// </summary>
        /// <param name="grid">The DataGrid instance</param>
        /// <param name="index">The index of the row</param>
        /// <returns>A row of the DataGrid</returns>
        public static DataGridRow GetRow(this DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.Items[index]);
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        /// <summary>
        /// Gets the selected row of the DataGrid
        /// </summary>
        /// <param name="grid">The DataGrid instance</param>
        /// <returns></returns>
        public static DataGridRow GetSelectedRow(this DataGrid grid)
        {
            return (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem);
        }

        public static DataGridRow GetRow(this DataGrid grid, DataGridCellInfo info)
        {
            return (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(info.Item);
        }

        public static object GetValue(this DataGrid grid, DataGridCellInfo item)
        {
            string val = "";
            var col = item.Column;
            var fc = col.GetCellContent(item.Item);
            if (fc is CheckBox)
            {
                return (fc as CheckBox).IsChecked;
            }
            else if (fc is TextBlock)
            {
                return (fc as TextBlock).Text;
            }
            //// Like this for all available types of cells
            return val;
        }

        public static void SetValue(this DataGrid grid, DataGridCellInfo item, object value)
        {
            var col = item.Column;
            var fc = col.GetCellContent(item.Item);
            DataGridCell cell = GetCell(grid, GetRow(grid, item), item.Column.DisplayIndex);
            grid.CurrentCell = item;
            if (fc is CheckBox)
            {
                (fc as CheckBox).IsChecked = (bool)value;
            }
            else if (fc is TextBlock)
            {
                TextBlock block = fc as TextBlock;
                (fc as TextBlock).Text = (string)value;
            }

            // Like this for all available types of cells
            
        }
    }
}
