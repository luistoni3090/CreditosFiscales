using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data;

namespace Px_Controles.GridPrinter
{
    //internal class DataGridPrinter
    //{
    //}

    using System;
    using System.Collections;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Principal;
    using System.Windows.Forms;

    #region DataGridPrinter
    // \\ --[DataGridPrinter]----------------------------------------------
    // \\ Provides a way to print a nicely formatted page from a data grid
    // \\ control.
    // \\ -----------------------------------------------------------------
    public partial class DataGridPrinter
    {

        #region Private enumerated types
        public enum CellTextHorizontalAlignment
        {
            LeftAlign = 1,
            CentreAlign = 2,
            RightAlign = 3
        }

        public enum CellTextVerticalAlignment
        {
            TopAlign = 1,
            MiddleAlign = 2,
            BottomAlign = 3
        }
        #endregion

        #region Private properties

        // \\ Printing the report related
        private PrintDocument __GridPrintDocument;

        private PrintDocument _GridPrintDocument
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return __GridPrintDocument;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (__GridPrintDocument != null)
                {
                    __GridPrintDocument.BeginPrint -= _GridPrintDocument_BeginPrint;
                    __GridPrintDocument.PrintPage -= _GridPrintDocument_PrintPage;
                }

                __GridPrintDocument = value;
                if (__GridPrintDocument != null)
                {
                    __GridPrintDocument.BeginPrint += _GridPrintDocument_BeginPrint;
                    __GridPrintDocument.PrintPage += _GridPrintDocument_PrintPage;
                }
            }
        }
        private readonly DataGrid _DataGrid;
        private readonly DataGridView _dataGridView;

        // \\ Print progress variables
        private int _CurrentPrintGridLine;
        private int _CurrentPageDown;
        private int _CurrentPageAcross = 1;

        // \\ Fonts to use to do the printing...
        private Font _PrintFont = new Font(System.Drawing.FontFamily.GenericSansSerif, 9);
        private Font _HeaderFont = new Font(System.Drawing.FontFamily.GenericSansSerif, 12);
        private Font _FooterFont = new Font(System.Drawing.FontFamily.GenericSansSerif, 10);

        private Rectangle _HeaderRectangle;
        private Rectangle _FooterRectangle;
        private Rectangle _PageContentRectangle;
        private double _Rowheight;

        // \\ Column widths related
        private int _PagesAcross = 1;
        private ColumnBounds _ColumnBounds = new ColumnBounds();


        private Global.System.Drawing.StringFormat _Textlayout;

        private int _FooterHeightPercent = 3;
        private int _HeaderHeightPercent = 7;
        private int _InterSectionSpacingPercent = 2;
        private int _CellGutter = 5;

        // \\ Pens to draw the sections with
        private Pen _FooterPen = new Pen(Color.Green);
        private Pen _HeaderPen = new Pen(Color.RoyalBlue);
        private Pen _GridPen = new Pen(Color.Black);

        // \\ Brushes to fill the sections with
        private Brush _HeaderBrush = Brushes.White;
        private Brush _FooterBrush = Brushes.White;
        private Brush _ColumnHeaderBrush = Brushes.White;
        private Brush _OddRowBrush = Brushes.White;
        private Brush _EvenRowBrush = Brushes.White;

        private string _HeaderText;
        private string _LoggedInUsername;


        private int _GridRowCount;
        private int _GridColumnCount;

        #endregion

        #region Public interface

        #region Properties

        #region PagesAcross
        public int PagesAcross
        {
            get
            {
                return _PagesAcross;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("PagesAcross", "Must be one or more pages across");
                }
                _PagesAcross = value;
            }
        }
        #endregion

        #region FooterHeightPercent
        public int FooterHeightPercent
        {
            get
            {
                return _FooterHeightPercent;
            }
            set
            {
                if (value < 0 || value >= 30)
                {
                    throw new ArgumentException("FooterHeightPercent must be between 0 and 30");
                }
                _FooterHeightPercent = value;
            }
        }
        #endregion
        #region HeaderHeightPercent
        public int HeaderHeightPercent
        {
            get
            {
                return _HeaderHeightPercent;
            }
            set
            {
                if (value < 0 || value >= 30)
                {
                    throw new ArgumentException("HeaderHeightPercent must be between 0 and 30");
                }
                _HeaderHeightPercent = value;
            }
        }
        #endregion
        #region InterSectionSpacingPercent
        public int InterSectionSpacingPercent
        {
            get
            {
                return _InterSectionSpacingPercent;
            }
            set
            {
                if (value < 0 || value >= 20)
                {
                    throw new ArgumentException("InterSectionSpacingPercent must be between 0 and 20");
                }
                _InterSectionSpacingPercent = value;
            }
        }
        #endregion

        #region CellGutter
        public int CellGutter
        {
            get
            {
                return _CellGutter;
            }
            set
            {
                if (value < 0 || value >= 10)
                {
                    throw new ArgumentException("CellGutter must be between 0 and 10");
                }
                _CellGutter = value;
            }
        }
        #endregion

        #region HeaderFont
        public Font HeaderFont
        {
            get
            {
                return _HeaderFont;
            }
            set
            {
                // \\ Possible font size validation here..
                _HeaderFont = value;
            }
        }
        #endregion
        #region PrintFont
        public Font PrintFont
        {
            get
            {
                return _PrintFont;
            }
            set
            {
                // \\ Possible font size validation here
                _PrintFont = value;
            }
        }
        #endregion
        #region FooterFont
        public Font FooterFont
        {
            get
            {
                return _FooterFont;
            }
            set
            {
                // \\ Possible font size validation here
                _FooterFont = value;
            }
        }
        #endregion

        #region HeaderText
        public string HeaderText
        {
            get
            {
                return _HeaderText;
            }
            set
            {
                _HeaderText = value;
            }
        }
        #endregion

        #region HeaderPen
        public Pen HeaderPen
        {
            get
            {
                return _HeaderPen;
            }
            set
            {
                _HeaderPen = value;
            }
        }
        #endregion
        #region FooterPen
        public Pen FooterPen
        {
            get
            {
                return _FooterPen;
            }
            set
            {
                _FooterPen = value;
            }
        }
        #endregion
        #region GridPen
        public Pen GridPen
        {
            get
            {
                return _GridPen;
            }
            set
            {
                _GridPen = value;
            }
        }
        #endregion

        #region HeaderBrush
        public Brush HeaderBrush
        {
            get
            {
                return _HeaderBrush;
            }
            set
            {
                _HeaderBrush = value;
            }
        }
        #endregion
        #region FooterBrush
        public Brush FooterBrush
        {
            get
            {
                return _FooterBrush;
            }
            set
            {
                _FooterBrush = value;
            }
        }
        #endregion
        #region ColumnHeaderBrush
        public Brush ColumnHeaderBrush
        {
            get
            {
                return _ColumnHeaderBrush;
            }
            set
            {
                _ColumnHeaderBrush = value;
            }
        }
        #endregion
        #region OddRowBrush
        public Brush OddRowBrush
        {
            get
            {
                return _OddRowBrush;
            }
            set
            {
                _OddRowBrush = value;
            }
        }
        #endregion
        #region EvenRowBrush
        public Brush EvenRowBrush
        {
            get
            {
                return _EvenRowBrush;
            }
            set
            {
                _EvenRowBrush = value;
            }
        }
        #endregion

        #region PrintDocument
        public PrintDocument PrintDocument
        {
            get
            {
                return _GridPrintDocument;
            }
        }
        #endregion

        #region Data Source

        public DataTable SourceDataTable
        {
            get
            {
                if (_DataGrid is not null)
                {
                    return (System.Data.DataTable)_DataGrid.DataSource;
                }
                else if (_dataGridView is not null)
                {
                    return (System.Data.DataTable)_dataGridView.DataSource;
                }
                else
                {
                    throw new ArgumentNullException("No data source set to print");
                }
            }
        }

        public int TotalRows
        {
            get
            {
                return SourceDataTable.DefaultView.Count;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Shared methods
        // \\ --[StripDomainFromFullUsername]------------------------------------------------
        // \\ Returns just the username bit from a username that includes a domain name
        // \\ e.g. "DEVELOPMENT\Duncan" -> "Duncan"
        // \\ (c) 2005 - Merrion Computing Ltd
        // \\ -------------------------------------------------------------------------------
        public static string StripDomainFromFullUsername(string FullUsername)
        {

            if (FullUsername.IndexOf(@"\") == -1)
            {
                return FullUsername;
            }
            else
            {
                char[] sep = new char[] { char.Parse(@"\") };
                string[] chaf = FullUsername.Split(sep);
                return chaf[chaf.Length - 1];
            }

        }
        #endregion

        #region Print
        public void Print()
        {
            _GridPrintDocument.Print();
        }
        #endregion
        #endregion


        #endregion

        #region _GridPrintDocument events
        private void _GridPrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {

            // \\ Initialise the current page and current grid line variables
            _CurrentPrintGridLine = 1;
            _CurrentPageDown = 1;
            _CurrentPageAcross = 1;

            if (_Textlayout is null)
            {
                _Textlayout = new System.Drawing.StringFormat();
                _Textlayout.Trimming = StringTrimming.EllipsisCharacter;
            }

        }

        private void _GridPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {

            if (_CurrentPageDown == 1 & _CurrentPageAcross == 1)
            {
                // _HeaderRectangle -  The top 10% of the page
                _HeaderRectangle = e.MarginBounds;
                _HeaderRectangle.Height = (int)(e.MarginBounds.Height * _HeaderHeightPercent * 0.01d);

                // _FooterRectangle - the bottom 10% of the page
                _FooterRectangle = e.MarginBounds;
                _FooterRectangle.Height = (int)(e.MarginBounds.Height * _FooterHeightPercent * 0.01d);
                _FooterRectangle.Y += (int)(e.MarginBounds.Height * (1d - 0.01d * _FooterHeightPercent));

                // _PageContentRectangle - The middle 80% of the page
                _PageContentRectangle = e.MarginBounds;
                _PageContentRectangle.Y += (int)(_HeaderRectangle.Height + e.MarginBounds.Height * (_InterSectionSpacingPercent * 0.01d));
                _PageContentRectangle.Height = (int)(e.MarginBounds.Height * 0.8d);

                _Rowheight = e.Graphics.MeasureString("a", _PrintFont).Height;

                // \\ Create the _ColumnBounds array
                int nColumn;
                var TotalWidth = default(double);

                if (_DataGrid is not null)
                {
                    if (_DataGrid.DataSource is null)
                    {
                        // \\ Nothing in the grid to print
                        return;
                    }
                }

                if (_dataGridView is not null)
                {
                    if (_dataGridView.DataSource is null)
                    {
                        // \\ Nothing in the grid to print
                        return;
                    }
                }

                int ColumnCount = GridColumnCount();

                var rcLastCell = default(Rectangle);
                var loopTo = ColumnCount - 1;
                for (nColumn = 0; nColumn <= loopTo; nColumn++)
                {
                    if (_DataGrid is not null)
                    {
                        rcLastCell = _DataGrid.GetCellBounds(0, nColumn);
                    }
                    else if (_dataGridView is not null)
                    {
                        rcLastCell = _dataGridView.GetCellDisplayRectangle(nColumn, 0, false);
                        if (rcLastCell.Width == 0)
                        {
                            // The grid is not visible so returns a 0 size
                            rcLastCell.Width = _dataGridView.Columns(nColumn).Width;
                            rcLastCell.Height = _dataGridView.Rows(nColumn).Height;
                        }
                    }

                    if (rcLastCell.Width > 0)
                    {
                        TotalWidth += rcLastCell.Width;
                    }
                }

                int TotalWidthOfAllPages = e.MarginBounds.Width * PagesAcross;
                _ColumnBounds.Clear();
                var rcCell = default(Rectangle);
                var loopTo1 = ColumnCount - 1;
                for (nColumn = 0; nColumn <= loopTo1; nColumn++)
                {
                    // \\ Calculate the column start point
                    var NextColumn = new ColumnBound();
                    if (nColumn == 0)
                    {
                        NextColumn.Left = e.MarginBounds.Left;
                    }
                    else
                    {
                        NextColumn.Left = _ColumnBounds.RightExtents;
                        // \\ Set this column's width
                    }
                    if (_DataGrid is not null)
                    {
                        rcCell = _DataGrid.GetCellBounds(0, nColumn);
                    }
                    else if (_dataGridView is not null)
                    {
                        // Note that this may go awry if the data grid view is not visible
                        rcCell = _dataGridView.GetCellDisplayRectangle(nColumn, 0, false);
                        if (rcCell.Width == 0)
                        {
                            // The grid is not visible so returns a 0 size
                            rcCell.Width = _dataGridView.Columns(nColumn).Width;
                            rcCell.Height = _dataGridView.Rows(nColumn).Height;
                        }
                    }
                    if (rcCell.Width > 0)
                    {
                        rcCell.Width = rcCell.Width - 1;
                        NextColumn.Width = rcCell.Width / TotalWidth * TotalWidthOfAllPages;
                        if (NextColumn.Width > e.MarginBounds.Width)
                        {
                            NextColumn.Width = e.MarginBounds.Width;
                        }
                    }
                    if (_ColumnBounds.RightExtents + NextColumn.Width > e.MarginBounds.Left + e.MarginBounds.Width)
                    {
                        _ColumnBounds.NextPage();
                        NextColumn.Left = e.MarginBounds.Left;
                    }
                    _ColumnBounds.Add(NextColumn);
                }
                if (_ColumnBounds.TotalPages > PagesAcross)
                {
                    PagesAcross = _ColumnBounds.TotalPages;
                }
            }

            // \\ Print the document header
            PrintHeader(e);

            // \\ Print as many grid lines as can fit
            int nextLine;
            PrintGridHeaderLine(e);
            int StartOfpage = _CurrentPrintGridLine;

            var loopTo2 = Min(_CurrentPrintGridLine + RowsPerPage(_PrintFont, e.Graphics), TotalRows);
            for (nextLine = _CurrentPrintGridLine; nextLine <= loopTo2; nextLine++)
                PrintGridLine(e, nextLine);
            _CurrentPrintGridLine = nextLine;

            // \\ Print the document footer
            PrintFooter(e);

            if (_CurrentPageAcross == PagesAcross)
            {
                _CurrentPageAcross = 1;
                _CurrentPageDown += 1;
            }
            else
            {
                _CurrentPageAcross += 1;
                _CurrentPrintGridLine = StartOfpage;
            }

            // \\ If there are more lines to print, set the HasMorePages property to true
            if (_CurrentPrintGridLine < GridRowCount())
            {
                e.HasMorePages = true;
            }

        }
        #endregion

        #region Private methods
        private void PrintHeader(Global.System.Drawing.Printing.PrintPageEventArgs e)
        {

            if (_HeaderRectangle.Height > 0)
            {
                e.Graphics.FillRectangle(_HeaderBrush, _HeaderRectangle);
                e.Graphics.DrawRectangle(_HeaderPen, _HeaderRectangle);
                DrawCellString(_HeaderText, CellTextHorizontalAlignment.CentreAlign, CellTextVerticalAlignment.MiddleAlign, _HeaderRectangle, false, e.Graphics, _HeaderFont, _HeaderBrush);
            }

        }

        private void PrintFooter(Global.System.Drawing.Printing.PrintPageEventArgs e)
        {

            if (_FooterRectangle.Height > 0)
            {
                e.Graphics.FillRectangle(_FooterBrush, _FooterRectangle);
                e.Graphics.DrawRectangle(_FooterPen, _FooterRectangle);
                DrawCellString("Printed by " + _LoggedInUsername, CellTextHorizontalAlignment.LeftAlign, CellTextVerticalAlignment.MiddleAlign, _FooterRectangle, false, e.Graphics, _PrintFont, Brushes.White);
                DrawCellString(DateTime.Now.ToLongDateString(), CellTextHorizontalAlignment.CentreAlign, CellTextVerticalAlignment.MiddleAlign, _FooterRectangle, false, e.Graphics, _PrintFont, Brushes.White);
                DrawCellString("Page " + ((_CurrentPageDown - 1) * PagesAcross + _CurrentPageAcross).ToString(), CellTextHorizontalAlignment.RightAlign, CellTextVerticalAlignment.MiddleAlign, _FooterRectangle, false, e.Graphics, _PrintFont, Brushes.White);
            }

        }

        private void PrintGridLine(Global.System.Drawing.Printing.PrintPageEventArgs e, int RowNumber)
        {

            int RowFromTop = RowNumber + 1 - _CurrentPrintGridLine;
            double Top = _PageContentRectangle.Top + RowFromTop * (_CellGutter * 2 + _Rowheight);
            double Bottom = Top + _Rowheight + 2 * _CellGutter;

            Top = RoundTo(Top, 2);
            Bottom = RoundTo(Bottom, 2);

            object[] Items;
            try
            {
                Items = GetRowItems(RowNumber);

                Brush RowBrush;
                if (RowNumber % 2 == 0)
                {
                    RowBrush = _OddRowBrush;
                }
                else
                {
                    RowBrush = _EvenRowBrush;
                }
                int nColumn;
                var loopTo = Items.Length - 1;
                for (nColumn = 0; nColumn <= loopTo; nColumn++)
                {
                    if (_ColumnBounds[nColumn].Page == _CurrentPageAcross)
                    {
                        var rcCell = new Rectangle((int)Math.Round(_ColumnBounds[nColumn].Left), (int)Math.Round(Top), (int)Math.Round(_ColumnBounds[nColumn].Width), (int)Math.Round(Bottom - Top));
                        if (rcCell.Width > 0)
                        {
                            string Columntext = "";
                            try
                            {
                                Columntext = Convert.ToString(Items[MappedColumnToBaseColumn(nColumn)]);
                            }
                            catch
                            {
                            }
                            DrawCellString(Columntext, CellTextHorizontalAlignment.CentreAlign, CellTextVerticalAlignment.MiddleAlign, rcCell, true, e.Graphics, _PrintFont, RowBrush);
                        }
                    }
                }
            }
            catch (Exception exIndex)
            {
                Trace.WriteLine(exIndex.ToString(), GetType().ToString());
            }

        }

        private object[] GetRowItems(int rowNumber)
        {

            if (_DataGrid != null)
            {
                if (_DataGrid.DataSource is DataTable)
                {
                    return ((DataTable)_DataGrid.DataSource).DefaultView.Item(rowNumber - 1).Row.ItemArray;
                }
                else if (_DataGrid.DataSource is DataSet)
                {
                    return ((System.Data.DataSet)_DataGrid.DataSource).Tables(_DataGrid.DataMember).DefaultView.Item(rowNumber - 1).Row.ItemArray;
                }
                else if (_DataGrid.DataSource is DataView)
                {
                    return ((System.Data.DataView)_DataGrid.DataSource).Table.DefaultView.Item(rowNumber - 1).Row.ItemArray;
                }
                else
                {
                    throw new ArgumentException("Data Grid has unknown backing data source");
                }
            }
            else if (_dataGridView.DataSource is DataTable)
            {
                return ((System.Data.DataTable)_dataGridView.DataSource).DefaultView.Item(rowNumber - 1).Row.ItemArray;
            }
            else if (_dataGridView.DataSource is DataSet)
            {
                return ((System.Data.DataSet)_dataGridView.DataSource).Tables(_DataGrid.DataMember).DefaultView.Item(rowNumber - 1).Row.ItemArray;
            }
            else if (_dataGridView.DataSource is DataView)
            {
                return ((System.Data.DataView)_dataGridView.DataSource).Table.DefaultView.Item(rowNumber - 1).Row.ItemArray;
            }
            else
            {
                throw new ArgumentException("Data Grid View has unknown backing data source");
            }

        }

        private void PrintGridHeaderLine(PrintPageEventArgs e)
        {

            double Top = _PageContentRectangle.Top;
            double Bottom = Top + _Rowheight + 2 * _CellGutter;

            Top = RoundTo(Top, 2);
            Bottom = RoundTo(Bottom, 2);

            int nColumn;

            var loopTo = GridColumnCount() - 1;
            for (nColumn = 0; nColumn <= loopTo; nColumn++)
            {
                if (_ColumnBounds[nColumn].Page == _CurrentPageAcross)
                {
                    var rcCell = new Rectangle((int)Math.Round(_ColumnBounds[nColumn].Left), (int)Math.Round(Top), (int)Math.Round(_ColumnBounds[nColumn].Width), (int)Math.Round(Bottom - Top));
                    if (rcCell.Width > 0)
                    {
                        DrawCellString(GetColumnHeadingText(nColumn), CellTextHorizontalAlignment.CentreAlign, CellTextVerticalAlignment.MiddleAlign, rcCell, true, e.Graphics, _PrintFont, _ColumnHeaderBrush);
                    }
                }
            }


        }

        private int RowsPerPage(Font GridLineFont, Graphics e)
        {

            return (int)Math.Round(_PageContentRectangle.Height / (_CellGutter * 2 + _Rowheight) - 2d);

        }

        public void DrawCellString(string s, CellTextHorizontalAlignment HorizontalAlignment, CellTextVerticalAlignment VerticalAlignment, Rectangle BoundingRect, bool DrawRectangle, Graphics Target, Font PrintFont, Brush FillColour)






        {


            float x;
            float y;

            if (DrawRectangle)
            {
                Target.FillRectangle(FillColour, BoundingRect);
                Target.DrawRectangle(_GridPen, BoundingRect);
            }

            // \\ Set the text alignment
            if (HorizontalAlignment == CellTextHorizontalAlignment.LeftAlign)
            {
                _Textlayout.Alignment = StringAlignment.Near;
            }
            else if (HorizontalAlignment == CellTextHorizontalAlignment.RightAlign)
            {
                _Textlayout.Alignment = StringAlignment.Far;
            }
            else
            {
                _Textlayout.Alignment = StringAlignment.Center;
            }

            var BoundingRectF = new RectangleF(BoundingRect.X + _CellGutter, BoundingRect.Y + _CellGutter, BoundingRect.Width - 2 * _CellGutter, BoundingRect.Height - 2 * _CellGutter);

            Target.DrawString(s, PrintFont, System.Drawing.Brushes.Black, BoundingRectF, _Textlayout);

        }

        // \\ --[RoundTo]-----------------------------------------------------------------------------
        // \\ Rounds the input number to the nearest modulus of NearsetMultiple
        // \\ ----------------------------------------------------------------------------------------
        private int RoundTo(double Input, int NearestMultiple)
        {

            if (Input % NearestMultiple > NearestMultiple / 2d)
            {
                return (int)Math.Round(Input) / NearestMultiple * NearestMultiple + NearestMultiple;
            }
            else
            {
                return (int)Math.Round(Input) / NearestMultiple * NearestMultiple;
            }

        }

        // \\ --[Min]------------------------------------------------------------
        // \\ Returns the minimum of two numbers
        // \\ -------------------------------------------------------------------
        private int Min(int a, int b)
        {
            if (a < b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        private int GridColumnCount()
        {

            if (_GridColumnCount == 0)
            {
                if (_DataGrid is not null)
                {
                    if (_DataGrid.DataSource is DataTable)
                    {
                        _GridColumnCount = ((DataTable)_DataGrid.DataSource).Columns.Count;
                    }
                    else if (_DataGrid.DataSource is DataSet)
                    {
                        _GridColumnCount = ((DataSet)_DataGrid.DataSource).Tables(_DataGrid.DataMember).Columns.Count;
                    }
                    else if (_DataGrid.DataSource is DataView)
                    {
                        _GridColumnCount = ((DataView)_DataGrid.DataSource).Table.Columns.Count;
                    }
                    else
                    {
                        // TODO : Get the column count....
                    }
                }
                else if (_dataGridView is not null)
                {
                    if (_dataGridView.DataSource is DataTable)
                    {
                        _GridColumnCount = ((DataTable)_dataGridView.DataSource).Columns.Count;
                    }
                    else if (_dataGridView.DataSource is DataSet)
                    {
                        _GridColumnCount = ((DataSet)_dataGridView.DataSource).Tables(_DataGrid.DataMember).Columns.Count;
                    }
                    else if (_dataGridView.DataSource is DataView)
                    {
                        _GridColumnCount = ((DataView)_dataGridView.DataSource).Table.Columns.Count;
                    }
                    else
                    {
                        // TODO : Get the column count....
                    }
                }
            }

            return _GridColumnCount;

        }

        private int GridRowCount()
        {

            if (_GridRowCount == 0)
            {
                if (_DataGrid is not null)
                {
                    if (_DataGrid.DataSource is DataTable)
                    {
                        _GridRowCount = ((DataTable)_DataGrid.DataSource).DefaultView.Count;
                    }
                    else if (_DataGrid.DataSource is DataSet)
                    {
                        _GridRowCount = ((DataSet)_DataGrid.DataSource).Tables(_DataGrid.DataMember).DefaultView.Count;
                    }
                    else if (_DataGrid.DataSource is DataView)
                    {
                        _GridRowCount = ((DataView)_DataGrid.DataSource).Table.DefaultView.Count;
                    }
                    else
                    {
                        // TODO : Get the column count....
                    }
                }
                else if (_dataGridView is not null)
                {
                    if (_dataGridView.DataSource is DataTable)
                    {
                        _GridRowCount = ((DataTable)_dataGridView.DataSource).DefaultView.Count;
                    }
                    else if (_dataGridView.DataSource is DataSet)
                    {
                        _GridRowCount = ((DataSet)_dataGridView.DataSource).Tables(_DataGrid.DataMember).DefaultView.Count;
                    }
                    else if (_dataGridView.DataSource is DataView)
                    {
                        _GridRowCount = ((DataView)_dataGridView.DataSource).Table.DefaultView.Count;
                    }
                    else
                    {
                        // TODO : Get the column count....
                    }
                }
            }
            return _GridRowCount;

        }

        private string GetColumnHeadingText(int Column)
        {

            if (_DataGrid is not null)
            {
                if (_DataGrid.TableStyles.Count > 0)
                {
                    return _DataGrid.TableStyles(_DataGrid.TableStyles.Count - 1).GridColumnStyles(Column).HeaderText;
                }
                else if (_DataGrid.DataSource is DataTable)
                {
                    return ((DataTable)_DataGrid.DataSource).Columns(Column).Caption;
                }
                else if (_DataGrid.DataSource is DataSet)
                {
                    return ((DataSet)_DataGrid.DataSource).Tables(0).Columns(Column).Caption;
                }
                else if (_DataGrid.DataSource is DataView)
                {
                    return ((DataView)_DataGrid.DataSource).Table.Columns(Column).Caption;
                }
            }
            else if (_dataGridView is not null)
            {

                if (_dataGridView.DataSource is DataTable)
                {
                    return ((DataTable)_dataGridView.DataSource).Columns(Column).Caption;
                }
                else if (_dataGridView.DataSource is DataSet)
                {
                    return ((DataSet)_dataGridView.DataSource).Tables(0).Columns(Column).Caption;
                }
                else if (_dataGridView.DataSource is DataView)
                {
                    return ((DataView)_dataGridView.DataSource).Table.Columns(Column).Caption;
                }
            }

            else
            {
                return string.Empty;
            }

            return default;

        }

        private int MappedColumnToBaseColumn(int MappedColumn)
        {

            if (_DataGrid is not null)
            {
                if (_DataGrid.TableStyles.Count <= 1)
                {
                    return MappedColumn;
                }
                else
                {
                    // \\ Need to map from the column in the default to the column in the active map..
                    return _DataGrid.TableStyles(0).GridColumnStyles.IndexOf(_DataGrid.TableStyles(_DataGrid.TableStyles.Count - 1).GridColumnStyles(MappedColumn));
                }
            }
            else
            {
                return MappedColumn;
            }

        }

        #endregion

        #region Public constructors

        public DataGridPrinter(DataGrid Grid)
        {
            // \\ Initialise the bits we need to use later
            _GridPrintDocument = new PrintDocument();
            _DataGrid = Grid;

            var LoggedInuser = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            _LoggedInUsername = StripDomainFromFullUsername(WindowsIdentity.GetCurrent().Name);

        }

        public DataGridPrinter(DataGridView Grid)
        {
            // \\ Initialise the bits we need to use later
            _GridPrintDocument = new PrintDocument();
            _dataGridView = Grid;

            var LoggedInuser = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            _LoggedInUsername = StripDomainFromFullUsername(WindowsIdentity.GetCurrent().Name);

        }
        #endregion

    }
    #endregion

    #region ColumnBound
    public partial class ColumnBound
    {

        #region Private properties
        private int _Page = 1;
        private double _Left;
        private double _Width;
        #endregion

        #region Public interface
        public double Left
        {
            get
            {
                return _Left;
            }
            set
            {
                if (value < 0d)
                {
                    throw new ArgumentException("Left must be greater than zero");
                }
                _Left = value;
            }
        }

        public double Width
        {
            get
            {
                return _Width;
            }
            set
            {
                if (value < 0d)
                {
                    throw new ArgumentException("Width must be greater than zero");
                }
                _Width = value;
            }
        }

        public int Page
        {
            get
            {
                return _Page;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Page", "Must be greater than zero");
                }
                _Page = value;
            }
        }
        #endregion

    }
    #endregion
    #region ColumnBounds
    // \\ Type safe collection of "ColumnBound" objects
    public partial class ColumnBounds : ArrayList
    {

        #region Private properties
        private int _CurrentPage = 1;
        private double _RightExtents; // \\ How far right does this column set reach?
        #endregion

        #region ArrayList overrides
        public int Add(ColumnBound ColumnBound)
        {
            if (ColumnBound.Left + ColumnBound.Width > _RightExtents)
            {
                _RightExtents = ColumnBound.Left + ColumnBound.Width;
            }
            ColumnBound.Page = _CurrentPage;
            return base.Add(ColumnBound);
        }

        public new void Clear()
        {
            _CurrentPage = 1;
            _RightExtents = 0d;
            base.Clear();
        }

        public void NextPage()
        {
            _CurrentPage += 1;
            _RightExtents = 0d;
        }

        internal int TotalPages
        {
            get
            {
                return _CurrentPage;
            }
        }

        public new ColumnBound this[int Index]
        {
            get
            {
                return (ColumnBound)base[Index];
            }
            set
            {
                base[Index] = value;
            }
        }
        #endregion

        #region Public interface
        public double RightExtents
        {
            get
            {
                return _RightExtents;
            }
        }
        #endregion

    }
    #endregion



}
