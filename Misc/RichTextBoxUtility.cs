using NotesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace NotesApp.Misc
{
    public static class RichTextBoxUtility
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached(
                "Text",
                typeof(string),
                typeof(RichTextBoxUtility),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

        public static string GetText(DependencyObject obj) => (string)obj.GetValue(TextProperty);
        public static void SetText(DependencyObject obj, string value) => obj.SetValue(TextProperty, value);

        public static readonly DependencyProperty IsUpdatingInternalProperty =
            DependencyProperty.RegisterAttached(
                "IsUpdatingInternal",
                typeof(bool),
                typeof(RichTextBoxUtility),
                new PropertyMetadata(false));

        public static bool GetIsUpdatingInternal(DependencyObject obj) =>
            (bool)obj.GetValue(IsUpdatingInternalProperty);

        public static void SetIsUpdatingInternal(DependencyObject obj, bool value) =>
            obj.SetValue(IsUpdatingInternalProperty, value);

        public static readonly DependencyProperty MonitorFormattingProperty =
            DependencyProperty.RegisterAttached(
                "MonitorFormatting",
                typeof(bool),
                typeof(RichTextBoxUtility),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMonitorFormattingChanged));
        public static bool GetMonitorFormatting(DependencyObject obj) =>
            (bool)obj.GetValue(MonitorFormattingProperty);
        public static void SetMonitorFormatting(DependencyObject obj, bool value) =>
            obj.SetValue(MonitorFormattingProperty, value);
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox rtb && !GetIsUpdatingInternal(rtb))
            {
                SetIsUpdatingInternal(rtb, true);

                string newText = e.NewValue as string ?? string.Empty;
                rtb.Document.Blocks.Clear();

                if (!string.IsNullOrEmpty(newText))
                {
                    TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(newText)))
                    {
                        textRange.Load(ms, DataFormats.Rtf);
                    }
                }

                rtb.TextChanged -= Rtb_TextChanged;
                rtb.TextChanged += Rtb_TextChanged;

                SetIsUpdatingInternal(rtb, false);
            }

        }

        private static void Rtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is RichTextBox rtb && !GetIsUpdatingInternal(rtb))
            {
                SetIsUpdatingInternal(rtb, true);

                TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                using (MemoryStream ms = new MemoryStream())
                {
                    textRange.Save(ms, DataFormats.Rtf);
                    SetText(rtb, Encoding.UTF8.GetString(ms.ToArray()));
                }

                SetIsUpdatingInternal(rtb, false);
            }
        }

        private static void OnMonitorFormattingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox rtb)
            {
                if ((bool)e.NewValue)
                {
                    rtb.SelectionChanged += Rtb_SelectionChanged;
                }
                else
                {
                    rtb.SelectionChanged -= Rtb_SelectionChanged;
                }
            }
        }

        private static void Rtb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is RichTextBox rtb && rtb.DataContext is NoteViewModel nvm)
            {
                {
                    object currentWeight = rtb.Selection.GetPropertyValue(Inline.FontWeightProperty);
                    bool isBold = false;
                    if (currentWeight != DependencyProperty.UnsetValue && currentWeight is FontWeight fw)
                    {
                        isBold = fw == FontWeights.Bold;
                    }

                    nvm.IsBoldActive = isBold;
                }

                {
                    object currentStyle = rtb.Selection.GetPropertyValue(Inline.FontStyleProperty);
                    bool isItalic = false;
                    if (currentStyle != DependencyProperty.UnsetValue && currentStyle is FontStyle fs)
                    {
                        isItalic = fs == FontStyles.Italic;
                    }

                    nvm.IsItalicActive = isItalic;
                }

                {
                    object currentDeco = rtb.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                    bool isUnderline = false;
                    if (currentDeco != DependencyProperty.UnsetValue && currentDeco is TextDecorationCollection tdc)
                    {
                        foreach (TextDecoration deco in tdc)
                        {
                            if (deco.Location == TextDecorationLocation.Underline)
                            {
                                isUnderline = true;
                                break;
                            }
                        }
                    }

                    nvm.IsUnderlineActive = isUnderline;
                }
            }
        }
    }
}
