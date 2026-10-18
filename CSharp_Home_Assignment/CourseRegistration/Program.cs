using System;
using System.Drawing;
using System.Windows.Forms;

namespace CourseRegistration;
internal static class Program { [STAThread] static void Main() { ApplicationConfiguration.Initialize(); Application.Run(new MainForm()); } }
public class MainForm : Form
{
    TextBox student = new(), id = new(), courses = new(); ComboBox level = new(); CheckBox accommodation = new(), transport = new(); TextBox output = new();
    public MainForm()
    {
        Text = "Course Registration Fee System"; Width = 650; Height = 650; StartPosition = FormStartPosition.CenterScreen;
        var p = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(18), FlowDirection = FlowDirection.TopDown, WrapContents = false }; Controls.Add(p);
        p.Controls.Add(new Label { Text = "COURSE REGISTRATION FEE SYSTEM", Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, Margin = new Padding(3, 3, 3, 14) });
        AddRow(p, "Student name:", student); AddRow(p, "Student ID:", id); AddRow(p, "Number of courses (1–8):", courses);
        level.DropDownStyle = ComboBoxStyle.DropDownList; level.Items.AddRange(new object[] { "Year 1", "Year 2", "Year 3", "Year 4" }); level.SelectedIndex = 0; AddRow(p, "Study level:", level);
        accommodation.Text = "Accommodation (+$120)"; accommodation.AutoSize = true; p.Controls.Add(accommodation);
        transport.Text = "Transport (+$50)"; transport.AutoSize = true; p.Controls.Add(transport);
        var buttons = new FlowLayoutPanel { AutoSize = true }; var calc = new Button { Text = "Calculate", AutoSize = true }; var clear = new Button { Text = "Clear", AutoSize = true }; var exit = new Button { Text = "Exit", AutoSize = true };
        calc.Click += Calculate; clear.Click += (_, _) => ClearForm(); exit.Click += (_, _) => Close(); buttons.Controls.AddRange(new Control[] { calc, clear, exit }); p.Controls.Add(buttons);
        output.Multiline = true; output.ReadOnly = true; output.Width = 560; output.Height = 190; p.Controls.Add(output);
    }
    static void AddRow(FlowLayoutPanel p, string label, Control input) { var row = new FlowLayoutPanel { AutoSize = true, Width = 580, WrapContents = false }; row.Controls.Add(new Label { Text = label, Width = 230, TextAlign = ContentAlignment.MiddleLeft, AutoSize = false, Height = 28 }); input.Width = 310; row.Controls.Add(input); p.Controls.Add(row); }
    void Calculate(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(student.Text) || string.IsNullOrWhiteSpace(id.Text)) { MessageBox.Show("Student name and Student ID cannot be empty."); return; }
        if (!int.TryParse(courses.Text, out int c)) { MessageBox.Show("Number of courses must be a valid whole number."); return; }
        if (c < 1 || c > 8) { MessageBox.Show("Number of courses must be between 1 and 8."); return; }
        decimal feePerCourse = level.SelectedItem!.ToString() switch { "Year 1" => 40m, "Year 2" => 45m, "Year 3" => 50m, _ => 55m };
        decimal tuition = feePerCourse * c; decimal accommodationFee = accommodation.Checked ? 120m : 0m; decimal transportFee = transport.Checked ? 50m : 0m;
        decimal courseDiscount = c >= 6 ? tuition * .10m : 0m;
        decimal serviceDiscount = accommodation.Checked && transport.Checked && c >= 5 ? (accommodationFee + transportFee) * .05m : 0m;
        decimal total = tuition - courseDiscount + accommodationFee + transportFee - serviceDiscount;
        output.Text = $"Student Name: {student.Text.Trim()}\r\nStudent ID: {id.Text.Trim()}\r\nStudy Level: {level.SelectedItem}\r\nTuition Fee: {tuition:C2}\r\nAccommodation Fee: {accommodationFee:C2}\r\nTransport Fee: {transportFee:C2}\r\nCourse Discount: -{courseDiscount:C2}\r\nService Discount: -{serviceDiscount:C2}\r\nFINAL REGISTRATION FEE: {total:C2}";
    }
    void ClearForm() { student.Clear(); id.Clear(); courses.Clear(); level.SelectedIndex = 0; accommodation.Checked = false; transport.Checked = false; output.Clear(); student.Focus(); }
}
