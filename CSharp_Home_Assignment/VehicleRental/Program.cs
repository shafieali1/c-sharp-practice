using System;
using System.Drawing;
using System.Windows.Forms;

namespace VehicleRental;
internal static class Program { [STAThread] static void Main() { ApplicationConfiguration.Initialize(); Application.Run(new MainForm()); } }
public class MainForm : Form
{
    TextBox customer = new(), age = new(), days = new(), experience = new(); ComboBox vehicle = new(); CheckBox insurance = new(); TextBox output = new();
    public MainForm()
    {
        Text = "Vehicle Rental Management System"; Width = 650; Height = 650; StartPosition = FormStartPosition.CenterScreen;
        var p = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(18), FlowDirection = FlowDirection.TopDown, WrapContents = false }; Controls.Add(p);
        p.Controls.Add(new Label { Text = "VEHICLE RENTAL MANAGEMENT", Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, Margin = new Padding(3, 3, 3, 14) });
        AddRow(p, "Customer name:", customer); AddRow(p, "Customer age (18–80):", age); AddRow(p, "Rental days (1–30):", days); AddRow(p, "Driving experience (years):", experience);
        vehicle.DropDownStyle = ComboBoxStyle.DropDownList; vehicle.Items.AddRange(new object[] { "Economy", "SUV", "Luxury" }); vehicle.SelectedIndex = 0; AddRow(p, "Vehicle type:", vehicle);
        insurance.Text = "Insurance (+$15 per day)"; insurance.AutoSize = true; p.Controls.Add(insurance);
        var buttons = new FlowLayoutPanel { AutoSize = true }; var calc = new Button { Text = "Calculate", AutoSize = true }; var clear = new Button { Text = "Clear", AutoSize = true }; var exit = new Button { Text = "Exit", AutoSize = true };
        calc.Click += Calculate; clear.Click += (_, _) => ClearForm(); exit.Click += (_, _) => Close(); buttons.Controls.AddRange(new Control[] { calc, clear, exit }); p.Controls.Add(buttons);
        output.Multiline = true; output.ReadOnly = true; output.Width = 560; output.Height = 190; p.Controls.Add(output);
    }
    static void AddRow(FlowLayoutPanel p, string label, Control input) { var row = new FlowLayoutPanel { AutoSize = true, Width = 580, WrapContents = false }; row.Controls.Add(new Label { Text = label, Width = 230, TextAlign = ContentAlignment.MiddleLeft, AutoSize = false, Height = 28 }); input.Width = 310; row.Controls.Add(input); p.Controls.Add(row); }
    void Calculate(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(customer.Text)) { MessageBox.Show("Customer name cannot be empty."); return; }
        if (!int.TryParse(age.Text, out int a) || !int.TryParse(days.Text, out int d) || !int.TryParse(experience.Text, out int exp)) { MessageBox.Show("Age, rental days and driving experience must be whole numbers."); return; }
        if (a < 18 || a > 80) { MessageBox.Show("Customer age must be between 18 and 80."); return; }
        if (d < 1 || d > 30) { MessageBox.Show("Rental days must be between 1 and 30."); return; }
        if (exp < 0) { MessageBox.Show("Driving experience cannot be negative."); return; }
        bool bookingApproved = true; string reason = "";
        if (a < 21) { bookingApproved = false; reason = "Customers below 21 years are not eligible."; }
        else if (exp < 1) { bookingApproved = false; reason = "At least 1 year of driving experience is required."; }
        else if (vehicle.SelectedItem!.ToString() == "Luxury" && (a < 25 || exp < 3)) { bookingApproved = false; reason = "Luxury vehicles require age 25+ and at least 3 years of driving experience."; }
        if (!bookingApproved) { output.Text = $"Booking Status: REJECTED\r\nReason: {reason}"; MessageBox.Show(reason); return; }
        decimal rate = vehicle.SelectedItem!.ToString() switch { "Economy" => 35m, "SUV" => 60m, _ => 100m };
        decimal basic = rate * d; decimal discountRate = d >= 14 ? .10m : d >= 7 ? .05m : 0m; decimal discount = basic * discountRate;
        decimal insuranceCost = insurance.Checked ? 15m * d : 0m; decimal youngSurcharge = a >= 21 && a <= 24 ? (basic - discount) * .10m : 0m;
        decimal total = basic - discount + insuranceCost + youngSurcharge;
        output.Text = $"Booking Status: APPROVED\r\nCustomer Name: {customer.Text.Trim()}\r\nVehicle Type: {vehicle.SelectedItem}\r\nBasic Rental Cost: {basic:C2}\r\nDiscount: -{discount:C2}\r\nInsurance: {insuranceCost:C2}\r\nYoung-Driver Surcharge: {youngSurcharge:C2}\r\nFINAL RENTAL COST: {total:C2}";
    }
    void ClearForm() { customer.Clear(); age.Clear(); days.Clear(); experience.Clear(); vehicle.SelectedIndex = 0; insurance.Checked = false; output.Clear(); customer.Focus(); }
}
