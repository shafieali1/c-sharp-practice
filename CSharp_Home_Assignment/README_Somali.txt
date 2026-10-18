C# HOME ASSIGNMENT — WINDOWS FORMS
Sharaxaad Somali ah + 4 mashruuc oo kala madax-bannaan
========================================================

WAXA KU JIRA
1. RoomBooking       — Room Booking System
2. PassengerTicket   — Passenger Ticket Booking
3. VehicleRental     — Vehicle Rental Management System
4. CourseRegistration— Course Registration Fee System

SIDA LOO FURTO
1. Extract All ku samee ZIP-ka.
2. Ku rakib Visual Studio 2022 oo leh workload-ka “.NET desktop development” iyo .NET 8 SDK.
3. Gal folder-ka mashruuca aad rabto.
4. Fur faylka .csproj (tusaale VehicleRental.csproj).
5. Riix F5 ama Start.
6. Geli xogta oo riix Calculate. Clear wuxuu nadiifiyaa form-ka; Exit wuu xiraa.
Mashruuc kasta gooni ayuu u shaqeeyaa; ha isku darin Program.cs-yada.

CONCEPTS MUHIIM AH
- int.TryParse(text, out value): wuxuu isku dayaa inuu qoraal u beddelo tiro dhan. Haddii qoraalku khaldan yahay, wuxuu soo celiyaa false si app-ku u muujiyo fariin qalad.
- decimal: waxaa loo isticmaalaa lacagta si xisaabtu u noqoto mid ku habboon qiimaha lacagta.
- bool bookingApproved: true haddii booking-ku buuxiyo shuruudaha; false haddii la diido.
- if / else if / else: waxay go'aamiyaan shuruudaha da'da, maalmaha, tirada koorsooyinka, discounts-ka, iwm.
- switch expression: wuxuu doortaa rate-ka ku xiran nooca la doortay.
- CheckBox: adeeg ikhtiyaari ah, sida breakfast ama insurance.
- ComboBox: wuxuu qofka siinayaa doorashooyin hore loo diyaariyey.
- string interpolation ($"..."): wuxuu xogta iyo natiijada ku daraa qoraal.
- :C2: wuxuu lacagta u qaabeeyaa laba meelood oo jajab ah; calaamadda lacagtu waxay ku xiran tahay Windows regional settings.

1. ROOM BOOKING SYSTEM
Inputs: Guest Name, Age, Nights, Guests, Room Type, Breakfast.
Xaqiijin: magaca ma bannaanaan karo; da'du 18–90; habeenadu 1–30; martidu 1–6.
Capacity: Standard ilaa 2; Normal ilaa 4; Hight ilaa 6.
Rates: Standard $50/night; Normal $80/night; Hight $130/night.
Xisaab: basic cost = rate × nights. 5–9 habeen = 5% discount; 10+ habeen = 12%. Breakfast = $10 qofkiiba habeenkiiba. Haddii martidu ka badan tahay 4, 5% service surcharge. Da'da 60+ waxay helaysaa 5% senior discount.
bool bookingApproved ayaa go'aamiya in la oggolaado ama la diido.

2. PASSENGER TICKET BOOKING
Inputs: Passenger Name, Age, Number of Tickets, Ticket Price, Baggage Weight, First Class, Travel Insurance.
Xaqiijin: age 1–100; tickets 1–5; baggage ma noqon karo negative, mana dhaafi karo 50 kg passenger-kiiba. Qof ka yar 12 sano ma dooran karo First Class.
Xisaab: basic ticket cost = ticket price × ticket count. 3–4 tickets = 5% discount; 5 tickets = 10%. Ka yar 12 sano = 20% child discount; 60+ = 10% senior discount. Insurance = $25 ticket-kiiba. Baggage ilaa 20 kg waa free.
PDF-gu ma bixin qiimaha tigidhka, sidaas darteed app-ku wuxuu ka qaataa input ahaan. PDF-gu ma qeexin lacagta baggage-ka ka badan 20 kg; app-ku wuxuu hubiyaa xadka 50 kg laakiin fee dheeraad ah ma samaynayo.

3. VEHICLE RENTAL MANAGEMENT SYSTEM
Inputs: Customer Name, Age, Rental Days, Driving Experience, Vehicle Type, Insurance.
Xaqiijin: age 18–80; days 1–30; driving experience ma noqon karto negative. Macmiil ka yar 21 lama oggola. Ugu yaraan 1 sano oo driving experience ah ayaa loo baahan yahay. Luxury wuxuu u baahan yahay age 25+ iyo experience 3+ years.
Rates: Economy $35/day; SUV $60/day; Luxury $100/day.
Xisaab: basic = daily rate × days. 7–13 days = 5% discount; 14+ = 10%. Insurance = $15/day. Age 21–24 = 10% young-driver surcharge.
Code-ku wuxuu young-driver surcharge ku xisaabiyaa basic cost ka dib discount-ka, insurance-na wuxuu ku daraa wadarta. PDF-gu ma qeexin kala-horraynta xisaabta; tani waa assumption la doortay.

4. COURSE REGISTRATION FEE SYSTEM
Inputs: Student Name, Student ID, Number of Courses, Study Level, Accommodation, Transport.
Xaqiijin: number of courses waa inuu u dhexeeyaa 1–8; TryParse ayaa hubiya inuu yahay integer sax ah.
Fee per course: Year 1 $40; Year 2 $45; Year 3 $50; Year 4 $55.
Xisaab: tuition = courses × fee per course. Accommodation = $120 haddii la doorto; Transport = $50 haddii la doorto. 6+ courses = 10% tuition discount. Haddii accommodation iyo transport labadaba la doorto, isla markaana courses-ku yihiin 5+, waxaa jira 5% service discount.
PDF-gu si faahfaahsan uma sheegin base-ka 5% service discount; code-ku wuxuu ka jaraa accommodation + transport fees.

ASSUMPTIONS / WAXYAABAHA PDF-GU AAN CADDEYN
1. Room Booking: rates-ku waxay isticmaalaan Standard, Normal, Hight, laakiin capacity rules-ku waxay PDF-ga ku qoran yihiin Standard, Deluxe, Suite. Code-ku wuxuu Normal u qaataa capacity-ga Deluxe (4), Hight-na capacity-ga Suite (6). Fadlan macallinka weydii haddii magacyadu kala duwan yihiin.
2. Passenger Ticket: ticket price iyo fee-ga baggage ka badan 20 kg PDF-gu ma sheegin. Sidaas darteed ticket price waxaa loo gelinayaa input ahaan; fee baggage cusub lama samayn.
3. Room Booking: breakfast waxaa lagu daraa ka hor service surcharge; senior discount waxaa laga jaraa room cost-ka ka dib night discount. PDF-gu ma qeexin kala-horraynta faahfaahsan.
4. Vehicle Rental: young-driver surcharge waxaa lagu xisaabiyaa rental cost ka dib discount; insurance waxaa lagu daraa kadib.
5. Course Registration: 10% discount waxaa laga jaraa tuition; 5% service discount waxaa laga jaraa accommodation + transport fees. PDF-gu ma qeexin base-ka discount-ka si buuxda.

