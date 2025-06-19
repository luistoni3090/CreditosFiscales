using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Px_Controles.Controls.ComboBoxCheck
{
    public partial class UIComboBoxCheck : System.Windows.Forms.ComboBox
    {
        private TextBox textBox;
        private Button dropDownButton;
        private Form dropDownForm;
        private CheckedListBox checkedListBox;

        private List<ComboBoxItem> items = new List<ComboBoxItem>();
        private List<ComboBoxItem> selectedItems = new List<ComboBoxItem>();

        public List<ComboBoxItem> SelectedItems => selectedItems;

        public UIComboBoxCheck()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.textBox = new TextBox
            {
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(240,240,240),
                ForeColor = Color.FromArgb(9, 9, 9),
                Location = new Point(0,0),
                Dock = DockStyle.Fill
            };

            this.dropDownButton = new Button
            {
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.FromArgb(9,9,9),
                Dock = DockStyle.Right,
                Width = 18,
                Text = "▼"
            };

            this.Controls.Add(textBox);
            this.Controls.Add(dropDownButton);

            this.checkedListBox = new CheckedListBox
            {
                CheckOnClick = true,
                Dock = DockStyle.Fill
            };

            this.dropDownForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                Size = new Size(this.Width, 200)
            };

            this.dropDownForm.Controls.Add(checkedListBox);

            this.dropDownButton.Click += DropDownButton_Click;
            this.checkedListBox.ItemCheck += CheckedListBox_ItemCheck;
            this.dropDownForm.Deactivate += DropDownForm_Deactivate;
            this.GotFocus += UIComboBoxCheck_GotFocus;

            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = Color.FromArgb(240, 240, 240);


            textBox.BringToFront();

        }

        private void UIComboBoxCheck_GotFocus(object sender, EventArgs e)
        {
            UpdateTextBox();
            //textBox.BringToFront();
            //textBox.Focus();

        }

        public void SetDataSource<T>(List<T> dataList, string displayMember, string valueMember)
        {
            items.Clear();
            checkedListBox.Items.Clear();

            foreach (var item in dataList)
            {
                PropertyInfo displayProp = typeof(T).GetProperty(displayMember);
                PropertyInfo valueProp = typeof(T).GetProperty(valueMember);

                if (displayProp == null || valueProp == null)
                    throw new ArgumentException("Las propiedades especificadas no existen en el tipo genérico.");

                var displayValue = displayProp.GetValue(item, null)?.ToString() ?? "";
                var value = valueProp.GetValue(item, null);

                var comboBoxItem = new ComboBoxItem
                {
                    Text = displayValue,
                    Value = value
                };

                items.Add(comboBoxItem);
                checkedListBox.Items.Add(comboBoxItem);
            }
        }

        private void DropDownButton_Click(object sender, EventArgs e)
        {
            if (!dropDownForm.Visible)
            {
                ShowDropDown();
            }
            else
            {
                HideDropDown();
            }
        }

        private void ShowDropDown()
        {
            Point location = this.Parent.PointToScreen(this.Location);
            location.Y += this.Height;

            dropDownForm.Location = location;
            dropDownForm.Width = this.Width;
            dropDownForm.Show();

            // Sincronizar los ítems seleccionados
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                var item = (ComboBoxItem)checkedListBox.Items[i];
                checkedListBox.SetItemChecked(i, selectedItems.Contains(item));
            }
        }

        private void HideDropDown()
        {
            dropDownForm.Hide();
        }

        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var item = (ComboBoxItem)checkedListBox.Items[e.Index];

            this.BeginInvoke(new Action(() =>
            {
                if (e.NewValue == CheckState.Checked)
                {
                    if (!selectedItems.Contains(item))
                        selectedItems.Add(item);
                }
                else
                {
                    if (selectedItems.Contains(item))
                        selectedItems.Remove(item);
                }

                UpdateTextBox();
            }));
        }

        private void UpdateTextBox()
        {
            textBox.Text = string.Join(", ", selectedItems.Select(i => i.Text));
        }

        private void DropDownForm_Deactivate(object sender, EventArgs e)
        {
            HideDropDown();
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ComboBoxItem))
                return false;

            ComboBoxItem other = (ComboBoxItem)obj;
            return EqualityComparer<object>.Default.Equals(this.Value, other.Value);
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }
    }

}


//public partial class UIComboBoxCheck : UserControl
//{
//    private TextBox textBox;
//    private Button dropDownButton;
//    private Form dropDownForm;
//    private CheckedListBox checkedListBox;

//    private List<ComboBoxItem> items = new List<ComboBoxItem>();
//    private List<ComboBoxItem> selectedItems = new List<ComboBoxItem>();

//    public List<ComboBoxItem> SelectedItems => selectedItems;

//    public UIComboBoxCheck()
//    {
//        InitializeComponents();
//    }

//    private void InitializeComponents()
//    {
//        // Establecer el color de fondo blanco
//        this.BackColor = Color.White;
//        this.BorderStyle = BorderStyle.FixedSingle;

//        // Configurar el TextBox
//        this.textBox = new TextBox
//        {
//            ReadOnly = true,
//            BorderStyle = BorderStyle.None,
//            BackColor = Color.White,
//            Dock = DockStyle.Fill,
//            Margin = new Padding(0),
//            TextAlign = HorizontalAlignment.Left
//        };

//        // Configurar el botón de despliegue
//        this.dropDownButton = new Button
//        {
//            Dock = DockStyle.Right,
//            Width = SystemInformation.VerticalScrollBarWidth,
//            FlatStyle = FlatStyle.Flat,
//            BackColor = Color.White,
//            Margin = new Padding(0),
//            Text = string.Empty // Sin texto
//        };

//        // Quitar bordes del botón
//        this.dropDownButton.FlatAppearance.BorderSize = 0;

//        // Dibujar el triángulo en el botón
//        this.dropDownButton.Paint += DropDownButton_Paint;

//        // Agregar controles al UserControl
//        this.Controls.Add(textBox);
//        this.Controls.Add(dropDownButton);

//        // Configurar el CheckedListBox
//        this.checkedListBox = new CheckedListBox
//        {
//            CheckOnClick = true,
//            Dock = DockStyle.Fill,
//            BorderStyle = BorderStyle.None
//        };

//        // Configurar el formulario desplegable
//        this.dropDownForm = new Form
//        {
//            FormBorderStyle = FormBorderStyle.FixedSingle,
//            ShowInTaskbar = false,
//            StartPosition = FormStartPosition.Manual,
//            Size = new Size(this.Width, 200)
//        };

//        this.dropDownForm.Controls.Add(checkedListBox);

//        // Eventos
//        this.dropDownButton.Click += DropDownButton_Click;
//        this.checkedListBox.ItemCheck += CheckedListBox_ItemCheck;
//        this.dropDownForm.Deactivate += DropDownForm_Deactivate;
//        this.SizeChanged += UIComboBoxCheck_SizeChanged;
//    }

//    private void UIComboBoxCheck_SizeChanged(object sender, EventArgs e)
//    {
//        // Ajustar tamaños de controles internos
//        this.textBox.Width = this.Width - this.dropDownButton.Width;
//        this.textBox.Height = this.Height;
//        this.dropDownButton.Height = this.Height;
//    }

//    private void DropDownButton_Paint(object sender, PaintEventArgs e)
//    {
//        // Dibujar el triángulo en el centro del botón
//        int middleX = dropDownButton.Width / 2;
//        int middleY = dropDownButton.Height / 2;
//        Point[] points = new Point[]
//        {
//            new Point(middleX - 5, middleY - 2),
//            new Point(middleX + 5, middleY - 2),
//            new Point(middleX, middleY + 3)
//        };
//        e.Graphics.FillPolygon(Brushes.Black, points);
//    }

//    public void SetDataSource<T>(List<T> dataList, string displayMember, string valueMember)
//    {
//        items.Clear();
//        checkedListBox.Items.Clear();
//        selectedItems.Clear();

//        foreach (var item in dataList)
//        {
//            PropertyInfo displayProp = typeof(T).GetProperty(displayMember);
//            PropertyInfo valueProp = typeof(T).GetProperty(valueMember);

//            if (displayProp == null || valueProp == null)
//                throw new ArgumentException("Las propiedades especificadas no existen en el tipo genérico.");

//            var displayValue = displayProp.GetValue(item, null)?.ToString() ?? "";
//            var value = valueProp.GetValue(item, null);

//            var comboBoxItem = new ComboBoxItem
//            {
//                Text = displayValue,
//                Value = value
//            };

//            items.Add(comboBoxItem);
//            checkedListBox.Items.Add(comboBoxItem);
//        }

//        UpdateTextBox();
//    }

//    private void DropDownButton_Click(object sender, EventArgs e)
//    {
//        if (!dropDownForm.Visible)
//        {
//            ShowDropDown();
//        }
//        else
//        {
//            HideDropDown();
//        }
//    }

//    private void ShowDropDown()
//    {
//        Point location = this.PointToScreen(new Point(0, this.Height));

//        dropDownForm.Location = location;
//        dropDownForm.Width = this.Width;
//        dropDownForm.Show();

//        // Sincronizar los ítems seleccionados
//        for (int i = 0; i < checkedListBox.Items.Count; i++)
//        {
//            var item = (ComboBoxItem)checkedListBox.Items[i];
//            checkedListBox.SetItemChecked(i, selectedItems.Contains(item));
//        }
//    }

//    private void HideDropDown()
//    {
//        dropDownForm.Hide();
//    }

//    private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
//    {
//        var item = (ComboBoxItem)checkedListBox.Items[e.Index];

//        this.BeginInvoke(new Action(() =>
//        {
//            if (e.NewValue == CheckState.Checked)
//            {
//                if (!selectedItems.Contains(item))
//                    selectedItems.Add(item);
//            }
//            else
//            {
//                if (selectedItems.Contains(item))
//                    selectedItems.Remove(item);
//            }

//            UpdateTextBox();
//        }));
//    }

//    private void UpdateTextBox()
//    {
//        textBox.Text = string.Join(", ", selectedItems.Select(i => i.Text));
//    }

//    private void DropDownForm_Deactivate(object sender, EventArgs e)
//    {
//        HideDropDown();
//    }
//}

//public class ComboBoxItem
//{
//    public string Text { get; set; }
//    public object Value { get; set; }

//    public override string ToString()
//    {
//        return Text;
//    }

//    public override bool Equals(object obj)
//    {
//        if (obj == null || !(obj is ComboBoxItem))
//            return false;

//        ComboBoxItem other = (ComboBoxItem)obj;
//        return EqualityComparer<object>.Default.Equals(this.Value, other.Value);
//    }

//    public override int GetHashCode()
//    {
//        return Value?.GetHashCode() ?? 0;
//    }
//}


//    private TextBox textBox;
//    private Button dropDownButton;
//    private Form dropDownForm;
//    private CheckedListBox checkedListBox;

//    private List<ComboBoxItem> items = new List<ComboBoxItem>();
//    private List<ComboBoxItem> selectedItems = new List<ComboBoxItem>();

//    public List<ComboBoxItem> SelectedItems => selectedItems;

//    public UIComboBoxCheck()
//    {
//        InitializeComponents();
//        InitializeComponents();

//        // Establecer el estilo plano
//        //this.BorderStyle = BorderStyle.FixedSingle;
//    }

//    private void InitializeComponents()
//    {
//        this.textBox = new TextBox
//        {
//            ReadOnly = true,
//            BorderStyle = BorderStyle.None,
//            Dock = DockStyle.Fill,
//            BackColor = this.BackColor,
//            TabStop = false
//        };

//        this.dropDownButton = new Button
//        {
//            Dock = DockStyle.Right,
//            Width = SystemInformation.VerticalScrollBarWidth,
//            FlatStyle = FlatStyle.Flat,
//            BackColor = this.BackColor,
//            TabStop = false
//        };

//        // Remover bordes del botón
//        this.dropDownButton.FlatAppearance.BorderSize = 0;

//        this.Controls.Add(textBox);
//        this.Controls.Add(dropDownButton);

//        this.checkedListBox = new CheckedListBox
//        {
//            CheckOnClick = true,
//            Dock = DockStyle.Fill
//        };

//        this.dropDownForm = new Form
//        {
//            FormBorderStyle = FormBorderStyle.None,
//            ShowInTaskbar = false,
//            StartPosition = FormStartPosition.Manual,
//            Size = new Size(this.Width, 200)
//        };

//        this.dropDownForm.Controls.Add(checkedListBox);

//        this.dropDownButton.Click += DropDownButton_Click;
//        this.checkedListBox.ItemCheck += CheckedListBox_ItemCheck;
//        this.dropDownForm.Deactivate += DropDownForm_Deactivate;
//        this.textBox.SizeChanged += TextBox_SizeChanged;
//        this.SizeChanged += MultiSelectComboBox_SizeChanged;
//        this.BackColorChanged += MultiSelectComboBox_BackColorChanged;

//        // Dibujar el triángulo en el botón
//        this.dropDownButton.Paint += DropDownButton_Paint;
//    }

//    private void MultiSelectComboBox_BackColorChanged(object sender, EventArgs e)
//    {
//        this.textBox.BackColor = this.BackColor;
//        this.dropDownButton.BackColor = this.BackColor;
//    }

//    private void TextBox_SizeChanged(object sender, EventArgs e)
//    {
//        AdjustControls();
//    }

//    private void MultiSelectComboBox_SizeChanged(object sender, EventArgs e)
//    {
//        AdjustControls();
//    }

//    private void AdjustControls()
//    {
//        this.textBox.Width = this.Width - this.dropDownButton.Width;
//        this.dropDownButton.Height = this.Height;
//    }

//    private void DropDownButton_Paint(object sender, PaintEventArgs e)
//    {
//        // Dibujar el triángulo del botón de despliegue
//        int middleX = dropDownButton.Width / 2;
//        int middleY = dropDownButton.Height / 2;

//        Point[] points = new Point[]
//        {
//        new Point(middleX - 5, middleY - 2),
//        new Point(middleX + 5, middleY - 2),
//        new Point(middleX, middleY + 3)
//        };

//        e.Graphics.FillPolygon(Brushes.Black, points);
//    }

//    public void SetDataSource<T>(List<T> dataList, string displayMember, string valueMember)
//    {
//        items.Clear();
//        checkedListBox.Items.Clear();

//        foreach (var item in dataList)
//        {
//            PropertyInfo displayProp = typeof(T).GetProperty(displayMember);
//            PropertyInfo valueProp = typeof(T).GetProperty(valueMember);

//            if (displayProp == null || valueProp == null)
//                throw new ArgumentException("Las propiedades especificadas no existen en el tipo genérico.");

//            var displayValue = displayProp.GetValue(item, null)?.ToString() ?? "";
//            var value = valueProp.GetValue(item, null);

//            var comboBoxItem = new ComboBoxItem
//            {
//                Text = displayValue,
//                Value = value
//            };

//            items.Add(comboBoxItem);
//            checkedListBox.Items.Add(comboBoxItem);
//        }
//    }

//    private void DropDownButton_Click(object sender, EventArgs e)
//    {
//        if (!dropDownForm.Visible)
//        {
//            ShowDropDown();
//        }
//        else
//        {
//            HideDropDown();
//        }
//    }

//    private void ShowDropDown()
//    {
//        Point location = this.PointToScreen(new Point(0, this.Height));

//        dropDownForm.Location = location;
//        dropDownForm.Width = this.Width;
//        dropDownForm.Show();

//        // Sincronizar los ítems seleccionados
//        for (int i = 0; i < checkedListBox.Items.Count; i++)
//        {
//            var item = (ComboBoxItem)checkedListBox.Items[i];
//            checkedListBox.SetItemChecked(i, selectedItems.Contains(item));
//        }
//    }

//    private void HideDropDown()
//    {
//        dropDownForm.Hide();
//    }

//    private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
//    {
//        var item = (ComboBoxItem)checkedListBox.Items[e.Index];

//        this.BeginInvoke(new Action(() =>
//        {
//            if (e.NewValue == CheckState.Checked)
//            {
//                if (!selectedItems.Contains(item))
//                    selectedItems.Add(item);
//            }
//            else
//            {
//                if (selectedItems.Contains(item))
//                    selectedItems.Remove(item);
//            }

//            UpdateTextBox();
//        }));
//    }

//    private void UpdateTextBox()
//    {
//        textBox.Text = string.Join(", ", selectedItems.Select(i => i.Text));
//    }

//    private void DropDownForm_Deactivate(object sender, EventArgs e)
//    {
//        HideDropDown();
//    }
//}

//public class ComboBoxItem
//{
//    public string Text { get; set; }
//    public object Value { get; set; }

//    public override string ToString()
//    {
//        return Text;
//    }

//    public override bool Equals(object obj)
//    {
//        if (obj == null || !(obj is ComboBoxItem))
//            return false;

//        ComboBoxItem other = (ComboBoxItem)obj;
//        return EqualityComparer<object>.Default.Equals(this.Value, other.Value);
//    }

//    public override int GetHashCode()
//    {
//        return Value?.GetHashCode() ?? 0;
//    }
//}
//}
