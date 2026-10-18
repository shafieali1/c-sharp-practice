using System;
using System.Drawing;
using System.Windows.Forms;

namespace PassengerTicket;
internal static class Program { [STAThread] static void Main() { ApplicationConfiguration.Initialize(); Application.Run(new MainForm()); } }
public class MainForm : Form
{
    TextBox age = new(), tickets = new(), baggage = new(), price = new(); ComboBox travelClass = new(); CheckBox insurance = new(); TextBox output = new();
    public MainForm()
    {
        Text = "Passenger Ticket Booking"; Width = 650; Height = 610; StartPosition = FormStartPosition.CenterScreen;
        var p = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(18), FlowDirection = FlowDirection.TopDown, WrapContents = false }; Controls.Add(p);
        p.Controls.Add(new Label { Text = "PASSENGER TICKET BOOKING", Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true, Margin = new Padding(3, 3, 3, 14) });
        AddRow(p, "Passenger age (1–100):", age); AddRow(p, "Number of tickets (1–5):", tickets); AddRow(p, "Baggage weight per passenger (kg):", baggage); AddRow(p, "Price per ticket ($):", price);
        travelClass.DropDownStyle = ComboBoxStyle.DropDownList; travelClass.Items.AddRange(new object[] { "Economy", "First Class" }); travelClass.SelectedIndex = 0; AddRow(p, "Travel class:", travelClass);
        insurance.Text = "Travel insurance (+$25 per ticket)"; insurance.AutoSize = true; p.Controls.Add(insurance);
        var buttons = new FlowLayoutPanel { AutoSize = true }; var calc = new Button { Text = "Calculate", AutoSize = true }; var clear = new Button { Text = "Clear", AutoSize = true }; var exit = new Button { Text = "Exit", AutoSize = true };
        calc.Click += Calculate; clear.Click += (_, _) => ClearForm(); exit.Click += (_, _) => Close(); buttons.Controls.AddRange(new Control[] { calc, clear, exit }); p.Controls.Add(buttons);
        output.Multiline = true; output.ReadOnly = true; output.Width = 560; output.Height = 160; p.Controls.Add(output);
        p.Controls.Add(new Label { Text = "The PDF does not provide a ticket price or a fee for baggage over 20 kg. Enter the ticket price; baggage is validated but no extra baggage fee is invented.", AutoSize = true, MaximumSize = new Size(560, 0) });
    }
    static void AddRow(FlowLayoutPanel p, string label, Control input) { var row = new FlowLayoutPanel { AutoSize = true, Width = 580, WrapContents = false }; row.Controls.Add(new Label { Text = label, Width = 250, TextAlign = ContentAlignment.MiddleLeft, AutoSize = false, Height = 28 }); input.Width = 290; row.Controls.Add(input); p.Controls.Add(row); }
    void Calculate(object? sender, EventArgs e)
    {
        if (!int.TryParse(age.Text, out int a) || !int.TryParse(tickets.Text, out int t) || !decimal.TryParse(baggage.Text, out decimal kg) || !decimal.TryParse(price.Text, out decimal ticketPrice)) { MessageBox.Show("Age, tickets, baggage and ticket price must be valid numbers."); return; }
        if (a < 1 || a > 100) { MessageBox.Show("Passenger age must be between 1 and 100."); return; }
        if (t < 1 || t > 5) { MessageBox.Show("Number of tickets must be between 1 and 5."); return; }
        if (kg < 0 || kg > 50) { MessageBox.Show("Baggage must be between 0 and 50 kg per passenger."); return; }
        if (ticketPrice < 0) { MessageBox.Show("Ticket price cannot be negative."); return; }
        bool bookingApproved = true; string reason = "";
        if (a < 12 && travelClass.SelectedItem!.ToString() == "First Class") { bookingApproved = false; reason = "Passengers below 12 years cannot book First Class."; }
        if (!bookingApproved) { output.Text = "Booking Status: REJECTED\r\nReason: " + reason; MessageBox.Show(reason); return; }
        decimal basic = ticketPrice * t; decimal ticketDiscountRate = t == 5 ? .10m : t >= 3 ? .05m : 0m; decimal ticketDiscount = basic * ticketDiscountRate;
        decimal afterTicketDiscount = basic - ticketDiscount; decimal ageDiscountRate = a < 12 ? .20m : a >= 60 ? .10m : 0m; decimal ageDiscount = afterTicketDiscount * ageDiscountRate;
        decimal insuranceCost = insurance.Checked ? 25m * t : 0m; decimal total = afterTicketDiscount - ageDiscount + insuranceCost;
        output.Text = $"Booking Status: APPROVED\r\nPassenger Age: {a}\r\nTravel Class: {travelClass.SelectedItem}\r\nBasic Ticket Cost: {basic:C2}\r\nTicket Discount: -{ticketDiscount:C2}\r\nAge Discount: -{ageDiscount:C2}\r\nInsurance: {insuranceCost:C2}\r\nBaggage: {kg:0.##} kg per passenger (no fee rate specified)\r\nFINAL TOTAL: {total:C2}";
    }
    void ClearForm() { age.Clear(); tickets.Clear(); baggage.Clear(); price.Clear(); travelClass.SelectedIndex = 0; insurance.Checked = false; output.Clear(); age.Focus(); }
}
