using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace RoomBooking;

internal static class Program
{
    [STAThread] static void Main() { ApplicationConfiguration.Initialize(); Application.Run(new MainForm()); }
}

public class MainForm : Form
{
    TextBox name = new(), age = new(), nights = new(), guests = new();
    ComboBox room = new(); CheckBox breakfast = new(); TextBox output = new();
    public MainForm()
    {
        Text = "Room Booking System"; Width = 650; Height = 650; StartPosition = FormStartPosition.CenterScreen;
        var panel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(18), FlowDirection = FlowDirection.TopDown, WrapContents = false };
        Controls.Add(panel); panel.Controls.Add(new Label { Text = "ROOM BOOKING SYSTEM", Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, Margin = new Padding(3, 3, 3, 14) });
        AddRow(panel, "Guest name:", name); AddRow(panel, "Guest age (18–90):", age); AddRow(panel, "Number of nights (1–30):", nights); AddRow(panel, "Number of guests (1–6):", guests);
        room.DropDownStyle = ComboBoxStyle.DropDownList; room.Items.AddRange(new object[] { "Standard", "Normal", "Hight" }); room.SelectedIndex = 0; AddRow(panel, "Room type:", room);
        breakfast.Text = "Breakfast selected"; breakfast.AutoSize = true; panel.Controls.Add(breakfast);
        var buttons = new FlowLayoutPanel { AutoSize = true }; var calc = new Button { Text = "Calculate", AutoSize = true }; var clear = new Button { Text = "Clear", AutoSize = true }; var exit = new Button { Text = "Exit", AutoSize = true };
        calc.Click += Calculate; clear.Click += (_, _) => ClearForm(); exit.Click += (_, _) => Close(); buttons.Controls.AddRange(new Control[] { calc, clear, exit }); panel.Controls.Add(buttons);
        output.Multiline = true; output.ReadOnly = true; output.Width = 560; output.Height = 180; panel.Controls.Add(output);
        panel.Controls.Add(new Label { Text = "Rate mapping follows the rate table: Standard=$50, Normal=$80, Hight=$130. Capacity mapping: Standard=2, Normal=4, Hight=6 (Normal/Deluxe and Hight/Suite are assumptions because the PDF uses inconsistent names).", AutoSize = true, MaximumSize = new Size(560, 0) });
    }
    static void AddRow(FlowLayoutPanel p, string label, Control input) { var row = new FlowLayoutPanel { AutoSize = true, Width = 580, WrapContents = false }; row.Controls.Add(new Label { Text = label, Width = 210, TextAlign = ContentAlignment.MiddleLeft, AutoSize = false, Height = 28 }); input.Width = 330; row.Controls.Add(input); p.Controls.Add(row); }
    void Calculate(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(name.Text)) { MessageBox.Show("Guest name cannot be empty."); return; }
        if (!int.TryParse(age.Text, out int a) || !int.TryParse(nights.Text, out int n) || !int.TryParse(guests.Text, out int g)) { MessageBox.Show("Age, nights and guests must be valid whole numbers."); return; }
        if (a < 18 || a > 90) { MessageBox.Show("Guest age must be between 18 and 90."); return; }
        if (n < 1 || n > 30) { MessageBox.Show("Number of nights must be between 1 and 30."); return; }
        if (g < 1 || g > 6) { MessageBox.Show("Number of guests must be between 1 and 6."); return; }
        int capacity = room.SelectedItem!.ToString() switch { "Standard" => 2, "Normal" => 4, _ => 6 };
        bool bookingApproved = g <= capacity;
        if (!bookingApproved) { MessageBox.Show($"Booking rejected: {room.SelectedItem} room can accommodate at most {capacity} guests."); output.Text = "Booking Status: REJECTED"; return; }
        decimal rate = room.SelectedItem!.ToString() switch { "Standard" => 50m, "Normal" => 80m, _ => 130m };
        decimal basic = rate * n; decimal nightDiscountRate = n >= 10 ? .12m : n >= 5 ? .05m : 0m;
        decimal nightDiscount = basic * nightDiscountRate; decimal afterNightDiscount = basic - nightDiscount;
        decimal breakfastCost = breakfast.Checked ? 10m * g * n : 0m;
        decimal serviceSurcharge = g > 4 ? (afterNightDiscount + breakfastCost) * .05m : 0m;
        decimal seniorDiscount = a >= 60 ? afterNightDiscount * .05m : 0m;
        decimal total = afterNightDiscount + breakfastCost + serviceSurcharge - seniorDiscount;
        output.Text = $"Booking Status: APPROVED\r\nGuest: {name.Text.Trim()}\r\nRoom Type: {room.SelectedItem}\r\nBasic Room Cost: {basic:C2}\r\nNight Discount: -{nightDiscount:C2}\r\nBreakfast: {breakfastCost:C2}\r\nService Surcharge: {serviceSurcharge:C2}\r\nSenior Discount: -{seniorDiscount:C2}\r\nFINAL TOTAL: {total:C2}";
    }
    void ClearForm() { name.Clear(); age.Clear(); nights.Clear(); guests.Clear(); room.SelectedIndex = 0; breakfast.Checked = false; output.Clear(); name.Focus(); }
}
