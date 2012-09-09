﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Collections;
using System.Windows.Controls;
using System.ComponentModel;

namespace AppFrame.Extensions.ListBox
{
    /// <summary>
    /// Provides extended support for the ListBox and ListView controls.
    /// </summary>
    public static class ListBoxExtension
    {

        #region Attached Property Declaration

        /// <summary>
        /// Identifies the ListBoxExtension.SelectedItemsSource attached property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemsSourceProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItemsSource",
                typeof(IList),
                typeof(ListBoxExtension),
                new PropertyMetadata(
                    null,
                    new PropertyChangedCallback(OnSelectedItemsSourceChanged)));

        #endregion

        #region Attached Property Accessors

        /// <summary>
        /// Gets the IList that contains the values that should be selected.
        /// </summary>
        /// <param name="element">The ListBox to check.</param>
        public static IList GetSelectedItemsSource(DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return (IList)element.GetValue(SelectedItemsSourceProperty);
        }

        /// <summary>
        /// Sets the IList that contains the values that should be selected.
        /// </summary>
        /// <param name="element">The ListBox being set.</param>
        /// <param name="value">The value of the property.</param>
        public static void SetSelectedItemsSource(DependencyObject element, IList value)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            element.SetValue(SelectedItemsSourceProperty, value);
        }

        #endregion

        #region IsResynchingProperty

        // Used to set a flag on the ListBox to avoid reentry of SelectionChanged due to
        // a full syncronisation pass
        private static readonly DependencyPropertyKey IsResynchingPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "IsResynching",
                typeof(bool),
                typeof(ListBoxExtension),
                new PropertyMetadata(false));

        #endregion

        #region Private Static Methods

        private static void OnSelectedItemsSourceChanged(DependencyObject d,
                                                         DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Controls.ListBox listBox = d as System.Windows.Controls.ListBox;
            if (listBox == null)
                throw new InvalidOperationException("The ListBoxExtension.SelectedItemsSource attached " +
                                                    "property can only be applied to ListBox controls.");

            listBox.SelectionChanged -= new SelectionChangedEventHandler(OnListBoxSelectionChanged);

            if (e.NewValue != null)
            {
                ListenForChanges(listBox);
            }
        }

        private static void ListenForChanges(System.Windows.Controls.ListBox listBox)
        {
            // Wait until the element is initialised
            if (!listBox.IsInitialized)
            {
                EventHandler callback = null;
                callback = delegate
                               {
                                   listBox.Initialized -= callback;
                                   ListenForChanges(listBox);
                               };
                listBox.Initialized += callback;
                return;
            }

            listBox.SelectionChanged += new SelectionChangedEventHandler(OnListBoxSelectionChanged);
            ResynchList(listBox);
        }

        private static void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ListBox listBox = sender as System.Windows.Controls.ListBox;
            if (listBox != null)
            {
                bool isResynching = (bool)listBox.GetValue(IsResynchingPropertyKey.DependencyProperty);
                if (isResynching)
                    return;

                IList list = GetSelectedItemsSource(listBox);
                if (list != null)
                {
                    foreach (object obj in e.RemovedItems)
                    {
                        if (list.Contains(obj))
                            list.Remove(obj);
                    }
                    foreach (object obj in e.AddedItems)
                    {
                        if (!list.Contains(obj))
                            list.Add(obj);
                    }
                    //SetSelectedItemsSource(listBox,list);
                }
            }
        }

        private static void ResynchList(System.Windows.Controls.ListBox listBox)
        {
            if (listBox != null)
            {
                listBox.SetValue(IsResynchingPropertyKey, true);
                IList list = GetSelectedItemsSource(listBox);

                if (listBox.SelectionMode == SelectionMode.Single)
                {
                    listBox.SelectedItem = null;
                    if (list != null)
                    {
                        if (list.Count > 1)
                        {
                            // There is more than one item selected, but the listbox is in Single selection mode
                            throw new InvalidOperationException("ListBox is in Single selection mode, but was given more than one selected value.");
                        }

                        if (list.Count == 1)
                            listBox.SelectedItem = list[0];
                    }
                }
                else
                {
                    listBox.SelectedItems.Clear();
                    if (list != null)
                    {
                        foreach (object obj in listBox.Items)
                            if (list.Contains(obj))
                                listBox.SelectedItems.Add(obj);
                    }
                }

                listBox.SetValue(IsResynchingPropertyKey, false);
            }
        }

        #endregion

    }
}