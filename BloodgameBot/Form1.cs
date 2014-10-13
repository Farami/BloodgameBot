// Author: Farami
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using BloodgameBot.Properties;
using iptb;
using Microsoft.Win32;

#region Informationen über Requests etc.

// Liste der Requests:
// AccountLogin - Login
// messagelist - Nachrichten
// RankList - Rangliste
// QuestAccept%3A(QuestNr) - Quest 1 2 oder 3 akzeptieren.
// WorkStop - Quest abbrechen

// Success:
// login - Login


//Success:login
//&SessionID:sP40DK97772128c06e1e0471y4RA521K
//&SaveGameDescription:Trage hier deine Charakterbeschreibung ein
//&SpecialAction:0
//&SaveGamePlayer:1965009275/54562/0/1308077324/-611561892/-611561892/-611561892/-611561892/-611561892/1308077324/1308077308/1308074682/1308070116/1308069433/1308064338/-611561892/0/0/0/0/0/0/0/0/1308077324/41/0/3/2/7/9038/70/1/40/50/0/1/2/15/0/5/0/0/3/0/0/0/67/0/0/0/0/0/0/0/0/0/0/0/0/3/2/15/0/2/0/0/2/0/0/0/2/0/0/0/0/0/0/0/0/0/0/0/0/5/1/8/0/0/0/0/0/0/0/0/9/0/0/0/0/0/0/0/0/0/0/0/0/7/1/10/0/0/0/0/0/0/0/0/1/0/0/0/0/0/0/0/0/0/0/0/0/9/2/6/18/2/0/0/2/0/0/0/20/10/15/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/1/3/13/0/2/0/0/3/0/0/0/16/0/0/0/0/0/0/0/0/0/0/0/0/7/3/8/0/0/0/0/0/0/0/0/5/0/0/0/0/0/0/0/0/0/0/0/0/9/3/4/12/0/0/0/0/0/0/0/17/0/0/0/0/0/0/0/0/0/0/0/0/2/100/0/0/0/0/0/0/0/0/0/1000/17/100/0/0/0/0/0/0/0/0/0/1000/17/100/0/0/0/0/0/0/0/0/0/1000/6/100/0/0/0/0/0/0/0/0/0/1000/2/100/0/0/0/0/0/0/0/0/0/1000/8/100/0/0/0/0/0/0/0/0/0/1000/9/2/3/9/0/0/0/0/0/0/0/1/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/12/22/12/14/13/0/3/0/2/0/10/19/10/16/10/0/0/0/0/0/0/4/0/0/3/13/40/84/24/53/6/3/10/22/72623/120/28/0/30/8/2/0/0/3/0/0/2/0/0/0/25/1/10/24/8336816/60/3/0/12/2/3/0/0/4/3/0/1/1/0/1/7/2/11/4/1657013/60/5/0/48/4/3/8/0/0/0/0/0/0/0/0/9/0/0/0/1/0/0/1/0/0/0/0/0/0/1/0/70/2/0/0/0/0/0/0/10/1/100/10/1/0/0/0/0/0/1/0/0/1/0/0/0/0/0/0/1/1/1/1/1/1/1/1/1/1/2/4/0/5/6/7/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/1308077324/12/375832/1000000/1308065632/1307977938/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/10/0/0/0/0/0/0/0/0/0/0/0/0/0/0/10/11/10/10/10/10/11/10/10/10/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/48/12665/12703/12857/12888/13277/18187/13908/54555/54571/12113/0/12113/0/0/0/0/1308067188/0/0/0/0/1/0/4/1308067188/0/0/0/0/2/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/
//&SaveGameName:Farami
//&SaveGameDescription:Trage hier deine Charakterbeschreibung ein


// Quests:
// Request: request.php?req=23995Bt6L15V60v8M4V9T059u5Z113R5;12;QuestAccept%3A3&rnd=96802727
// Antwort: 
//Success:QuestAccept
//&SaveGamePlayer:1965009275/54562/0/1308080547/-611561892/-611561892/-611561892/-611561892/-611561892/1308080496/1308080395/1308080386/1308080160/1308079882/1308064338/-611561892/0/0/0/0/0/0/0/0/1308080547/50/0/3/2/7/8515/80/2/66/100/0/1/2/15/0/5/0/0/3/0/0/0/67/0/0/0/0/0/0/0/0/0/0/0/0/3/2/15/0/2/0/0/2/0/0/0/2/0/0/0/0/0/0/0/0/0/0/0/0/5/1/8/0/0/0/0/0/0/0/0/9/0/0/0/0/0/0/0/0/0/0/0/0/7/1/10/0/0/0/0/0/0/0/0/1/0/0/0/0/0/0/0/0/0/0/0/0/9/2/6/18/2/0/0/2/0/0/0/20/10/15/0/0/0/0/0/0/0/0/0/0/2/3/0/0/4/3/0/1/1/0/1/7/2/2/0/0/1/0/0/2/0/0/0/25/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/1/3/13/0/2/0/0/3/0/0/0/16/0/0/0/0/0/0/0/0/0/0/0/0/7/3/8/0/0/0/0/0/0/0/0/5/0/0/0/0/0/0/0/0/0/0/0/0/9/3/4/12/0/0/0/0/0/0/0/17/0/0/0/0/0/0/0/0/0/0/0/0/16/1/0/0/16/0/0/1/0/0/0/50/15/18/0/0/15/0/0/20/0/0/1/20/14/1/0/0/14/100/0/25/7/0/5/100/8/1/0/0/4/0/0/2/0/0/0/25/6/2/0/0/3/0/0/2/0/0/0/25/6/3/0/0/1/0/0/2/0/0/0/25/9/2/3/9/0/0/0/0/0/0/0/1/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/13/24/13/15/14/0/3/0/2/0/10/19/10/16/10/0/0/0/0/0/0/4/0/0/3/14/41/91/39/70/7/1/2/14/3090864/60/12/0/90/6/3/0/0/3/0/0/2/0/0/0/25/2/4/4/3278936/60/12/0/15/2/2/0/0/3/0/0/2/0/0/0/25/3/13/3/3891444/60/8/0/60/0/2/0/0/1/0/0/2/0/0/0/25/3/0/0/1/0/0/1/0/0/0/0/0/0/1/0/70/4/0/0/0/0/0/0/10/1/100/10/1/0/0/0/0/0/1/0/0/1/0/0/0/0/0/0/1/1/1/2/1/1/1/1/1/1/2/4/0/5/6/7/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/2/4/1308080547/1308080607/1308080547/12/384508/1000000/1308065632/1308079784/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/25/0/0/0/0/0/0/0/0/0/0/0/0/0/0/11/11/10/10/10/10/12/10/10/10/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/48/12198/13050/14476/15499/15777/15895/15943/17101/52293/54510/1308079904/12113/0/0/0/0/1308067188/0/0/0/0/1/0/9/1308067188/0/0/0/0/2/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/


// 396 = BaseStärke
// 397 = BaseGeschicklichkeit
// 398 = BaseWillenskraft
// 399 = BaseKonstitution
// 400 = BaseCharisma
// 416 = ItemStärke
// 417 = ItemGeschicklichkeit
// 418 = ItemWillenskraft
// 419 = ItemKonstitution
// 420 = ItemCharisma

// 421 = MinSchaden
// 422 = MaxSchaden
// 423 = Angriff
// 424 = Läuterung
// 425 = Leben
// 426 = Einfluss

// 431 = Quest1 Zeit
// 432 = Quest1 Gold
// 433 = ???
// 434 = Quest1 Erfahrung
// 435 = Item Art

// 451 = Quest2 Zeit
// usw.

// 471 = Quest3 Zeit
// usw.

//1965009275/54562/0/1308358057/-684300612/-684300612/-684300612/-684300612/-684300612/1308358057/1308358013/1308358011/1308357861/1308357666/1308064338/-611561892/0/0/0/0/0/0/0/0/1308358057/3652/11/3/2/7/5693/328/17/547/719/0/1/2/98/0/5/0/0/3/0/0/0/70/2/2/0/0/1/0/0/3/0/0/0/11/3/4/87/0/1/0/0/14/0/0/0/101/4/1/61/0/2/0/0/4/0/0/0/62/5/1/87/0/2/5/0/7/5/0/0/97/6/3/0/0/3/0/0/2/0/0/0/25/7/2/85/0/1/0/0/12/0/0/0/108/8/3/0/0/3/0/0/2/0/0/0/25/9/15/58/102/2/0/0/16/0/0/0/184/10/15/0/0/0/0/0/0/0/0/0/0/1/3/56/0/4/3/0/7/5/0/3/347/5/4/103/0/2/1/0/11/9/0/5/596/9/12/48/72/5/2/0/12/8/0/0/708/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/9/9/46/58/2/4/0/2/2/0/1/987/1/3/98/0/4/2/0/9/7/0/0/698/9/8/44/68/3/5/0/16/10/0/6/3378/0/0/0/0/0/0/0/0/0/0/0/0/4/3/137/0/3/0/0/21/0/0/0/873/16/1/0/0/16/0/0/1/0/0/0/50/15/18/0/0/15/0/0/20/0/0/1/20/14/1/0/0/14/100/0/25/7/0/5/100/8/1/0/0/4/0/0/2/0/0/0/25/6/2/0/0/3/0/0/2/0/0/0/25/6/3/0/0/1/0/0/2/0/0/0/25/3/2/15/0/2/0/0/2/0/0/0/2/9/2/6/18/2/0/0/2/0/0/0/20/4/4/72/0/4/3/0/8/8/0/0/122/53/110/46/70/42/12/46/5/29/0/53/219/25/130/10/0/0/0/0/0/29/27/4/0/8/534/938/509/900/1285/40/2/5/21/8212904/1200/803/0/113/9/10/42/70/3/1/0/6/4/0/0/403/3/15/9/6011110/960/1101/0/91/0/3/83/0/1/0/0/2/0/0/0/57/3/11/20/5833361/240/103/0/79/0/12/48/72/5/2/0/12/8/0/0/708/2/3/3/1/1/1/3/3/0/0/0/0/0/2/3/146/49/30/0/0/0/0/0/10/5/250/50/50/0/0/0/0/0/1/1/1/6/6/0/0/0/0/0/1/1/1/2/1/1/1/1/1/1/2/4/0/5/6/7/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/2/3/1308358013/1308358973/1308358057/12/242752/1000000/1308315314/1308079784/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/430/0/0/0/0/0/0/0/0/0/0/0/0/0/0/17/17/18/15/17/14/21/10/10/10/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/418/11893/32334/24369/8157/25136/8894/24235/42363/45666/54836/1308315445/12113/10467/50796/0/0/1308067188/1308145641/1308158242/0/0/3/1308189917/15/1308158242/1308315356/0/0/0/15/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/0/&SaveGameName:Farami&SaveGameDescription:Blub.#7GET /request.php?req=Y3b42909365e10LSV55AX142Cj98n9f4;3;QuestAccept%3A2&rnd=25632505 HTTP/1.1

/* Item Arten:
     * 1 = Kopf
     * 2 = Amulett
     * 3 = Brust
     * 4 = Gürtel
     * 5 = Handschuhe
     * 6 = Ring
     * 7 = Schuhe
     * 8 = Accessoire
     * 9 = Waffe
     * 10 = Panzerung?
     */

// GET /request.php?req=12345678901234567890123456789012;1;AccountLogin%3AFarami/MD5Summe/0&rnd=22059216 HTTP/1.1\\r\\n
// Ziel: "+ Server.Text +".bloodgame.de/request.php
// req=12345678901234567890123456789012 -> Scheint die session ID zu sein, allerdings ist 1234567890 etwas merkwürdig
// AccountLogin%3AFarami/MD5Summe/0 -> AccountLogin zum einloggen, Farami login, rest MD5 passwort                
// rnd=22059216 -> Zufallszahl laut source int(100000000 * Math.random() also wohl Math.Next(10000000, 99999999)

#endregion

namespace BloodgameBot {
    public partial class Form1 : Form {
        #region Statics

        public static string Version = "0.8.1B";
        public static bool Debug = true;

        public static string SessionId;
        public static Spieler Player1;
        public static string ServerName = "";
        public static WebClient Wc = new WebClient();
        public static int RequestNr = 1;
        public static MD5 Md5 = new MD5CryptoServiceProvider();
        public static Random Rnd = new Random();
        public static string SPwMd5;
        public static string SLogin;
        public static int ZeitLeft;

        public static WebProxy Proxy = null;
        public static ICredentials Cred;
        public static TextBox ProxyLogin;
        public static TextBox ProxyPasswort;
        public static IPTextBox ProxyIp;
        public static TextBox ProxyPort;


        public static Timer EnergyTick = new Timer();
        public static Timer MissionTimer = new Timer();
        public static Timer WorkTimer = new Timer();
        public static Timer BarMissionTimer = new Timer();
        public static Timer UpdateTimer = new Timer();

        public static ToolTip TtExpPotion = new ToolTip();
        public static ToolTip TtSpeedPotion = new ToolTip();
        public static ToolTip InventarSlot1Tt = new ToolTip();
        public static ToolTip InventarSlot2Tt = new ToolTip();
        public static ToolTip InventarSlot3Tt = new ToolTip();
        public static ToolTip InventarSlot4Tt = new ToolTip();

        public static ProgressBar Bar;
        public static ProgressBar BarMissionProgress;
        public static ProgressBar BarExp;

        public static ListBox Logs;

        public static Label Level;
        public static Label Stärke;
        public static Label Geschicklichkeit;
        public static Label Willenskraft;
        public static Label Konstitution;
        public static Label Charisma;
        public static Label Gold;
        public static Label Blutsteine;
        public static Label MissionZeit;
        public static Label Ausdauer;
        public static Label Status;
        public static Label Exp;

        public static PictureBox ExpPotion;
        public static PictureBox SpeedPotion;
        public static PictureBox InventarSlot1;
        public static PictureBox InventarSlot2;
        public static PictureBox InventarSlot3;
        public static PictureBox InventarSlot4;

        public static CheckBox StärkeErhöhen;
        public static CheckBox GeschickErhöhen;
        public static CheckBox WilleErhöhen;
        public static CheckBox KonstErhöhen;
        public static CheckBox CharismaErhöhen;
        public static CheckBox ProxyJaNein;
        public static CheckBox ProxyAnonym;
        public static ComboBox Server;

        public static TextBox GoldBehalten;

        #region Consts

        public const int ITM_TYP = 0;
        public const int ITM_PICINDEX = 1;
        public const int ITM_DAMAGEMIN = 2;
        public const int ITM_DAMAGEMAX = 3;
        public const int ITM_ATTRIBUTTYP = 4;
        public const int ITM_ATTRIBUTVALUE = 7;
        public const int ITM_COINS = 10;
        public const int ITM_MONEY = 11;
        public const int ITM_SIZE = 12;
        public const int MIS_TYPE = 0;
        public const int MIS_TITEL = 1;
        public const int MIS_PLACE = 2;
        public const int MIS_TEXTSEED = 3;
        public const int MIS_DURATION = 4;
        public const int MIS_REWARD_MONEY = 5;
        public const int MIS_REWARD_COINS = 6;
        public const int MIS_REWARD_EXP = 7;
        public const int MIS_REWARD_ITEM = 8;
        public const int MIS_SIZE = 20;
        public const int SG_PAYMENTID = 0;
        public const int SG_PLAYERID = 1;
        public const int SG_RECRUITID = 2;
        public const int SG_LASTACTIONDATE = 3;
        public const int SG_LASTLOGINIP = 4;
        public const int SG_LASTLOGINDATE = 9;
        public const int SG_REGISTRATIONDATE = 14;
        public const int SG_REGISTRATIONIP = 15;
        public const int SG_VALIDATIONAGAINCOUNT = 16;
        public const int SG_VALIDATIONAGAINDATE = 17;
        public const int SG_VALIDATIONTYP = 18;
        public const int SG_VALIDATIONDATE = 19;
        public const int SG_VALIDATIONIP = 20;
        public const int SG_DELETEPROTECTIONDATE = 21;
        public const int SG_LOCKTYP = 22;
        public const int SG_LOCKDATE = 23;
        public const int SG_TIMESTAMP = 24;
        public const int SG_MONEY = 25;
        public const int SG_COINS = 26;
        public const int SG_CLASS = 27;
        public const int SG_GENDER = 28;
        public const int SG_FACE = 29;
        public const int SG_RANK = 30;
        public const int SG_HONOR = 31;
        public const int SG_LEVEL = 32;
        public const int SG_EXP = 33;
        public const int SG_EXP_NEXT = 34;
        public const int SG_GROUPID = 35;
        public const int SG_ITEM_EQUIP = 36;
        public const int SG_ITEM_BACKPACK = 156;
        public const int SG_ITEM_WEAPON = 216;
        public const int SG_ITEM_POTION = 288;
        public const int SG_ITEM_BUYBACK = 360;
        public const int SG_ATTRIBUT_VALUE = 396;
        public const int SG_ATTRIBUT_BUYED = 401;
        public const int SG_ATTRIBUT_PRICE = 406;
        public const int SG_ATTRIBUT_POTION = 411;
        public const int SG_ATTRIBUT_BONUS = 416;
        public const int SG_DAMAGEMIN = 421;
        public const int SG_DAMAGEMAX = 422;
        public const int SG_ATTACK = 423;
        public const int SG_RESIST = 424;
        public const int SG_LIFE = 425;
        public const int SG_INFLUENCE = 426;
        public const int SG_MISSION = 427;
        public const int SG_MISSIONINDEX = 487;
        public const int SG_MAINQUESTPROGRESS = 488;
        public const int SG_MAINQUESTDONEPROGRESS = 489;
        public const int SG_ARCHIVMENT_LEVEL = 490;
        public const int SG_ARCHIVMENT_VALUE = 500;
        public const int SG_ARCHIVMENT_NEXT = 510;
        public const int SG_ARCHIVMENT_BONUS = 520;
        public const int SG_HIDEOUT = 530;
        public const int SG_PERSONINSCREEN = 540;
        public const int SG_MESSAGECOUNT = 550;
        public const int SG_MESSAGEID = 551;
        public const int SG_ACTIONSTATUS = 651;
        public const int SG_ACTIONINDEX = 652;
        public const int SG_ACTIONSTARTTIME = 653;
        public const int SG_ACTIONENDTIME = 654;
        public const int SG_ENDURANCELASTCALCDATE = 655;
        public const int SG_ENDURANCEREGENERATIONPERSECOND = 656;
        public const int SG_ENDURANCE = 657;
        public const int SG_ENDURANCEMAX = 658;
        public const int SG_STORECALCULATIONDATE = 659;
        public const int SG_COINSGAINED = 661;
        public const int SG_COINSREALBOUGHT = 662;
        public const int SG_COINSBUYED = 663;
        public const int SG_COINSDATE = 664;
        public const int SG_COINSGEKAUFTSEITLETZTEMLOGIN = 665;
        public const int SG_FIRSTPAYMENTUSERFOR = 666;
        public const int SG_LOCKSTATUS = 667;
        public const int SG_LOCKDURATION = 668;
        public const int SG_DUNGEON_DONETOTAL = 669;
        public const int SG_DUNGEON_LEVELDONE = 670;
        public const int SG_DUNGEON_LEVELMAX = 690;
        public const int SG_MONEYBASEVALUE = 710;
        public const int SG_COINS_LASTBUYAMOUNT = 711;
        public const int SG_COINS_LASTBUYDATE = 712;
        public const int SG_MISSIONSTATUS = 713;
        public const int SG_COINSREALBOUGHTPRECALCULATION = 714;
        public const int SG_BUFFINDEX = 715;
        public const int SG_BUFFDURATION = 720;
        public const int SG_REPUTATION = 725;
        public const int SG_REPUTATIONEXP = 735;
        public const int SG_REPUTATIONNEXT = 745;
        public const int SG_ARMOR = 755;
        public const int SG_PVPENEMYID = 756;
        public const int SG_PVPENEMYCALCULATEDATE = 766;
        public const int SG_LAST5ID = 767;
        public const int SG_LAST5DATE = 772;
        public const int SG_LAST5COUNT = 777;
        public const int SG_PROTECTIONTIME = 778;
        public const int SG_TUTORIALSTATUS = 779;
        public const int SG_LASTPVPFIGHTDATE = 780;
        public const int SG_MAINQUESTTIMER = 781;
        public const int SG_DUFEHLSTUNS = 782;
        public const int SG_QUEST_TAKEANDLOSE = 783;
        public const int SG_MONEYREDUCE = 784;
        public const int SG_FREE = 785;
        public const int SG_SIZE = 931;
        public const int SERVER_STRUCTURE_CHECKSUM = 277304;

        #endregion

        #endregion

        public Form1() {
            if (!File.Exists("iptb.dll")) {
                MessageBox.Show("iptb.dll fehlt.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            InitializeComponent();
            Text = "Bloodgame Helper (V" + Version + ") by Farami";

            Player1 = new Spieler();

            Bar = barAusdauer;
            Logs = Log;
            Level = lblLevel;
            Gold = lblGold;
            Ausdauer = lblAusdauer;
            MissionZeit = lblZeit;
            Status = lblStatus;
            Stärke = lblStärke;
            Geschicklichkeit = lblGesch;
            Willenskraft = lblWille;
            Konstitution = lblKonst;
            Charisma = lblCharisma;
            BarMissionProgress = barMission;
            Blutsteine = lblBlutsteine;
            ExpPotion = pbExpPotion;
            SpeedPotion = pbSpeedPotion;
            InventarSlot1 = pbInventar1;
            InventarSlot2 = pbInventar2;
            InventarSlot3 = pbInventar3;
            InventarSlot4 = pbInventar4;
            BarExp = pbExp;
            Exp = lblExp;
            StärkeErhöhen = cbStärkeErhöhen;
            GeschickErhöhen = cbGeschErhöhen;
            WilleErhöhen = cbWilleErhöhen;
            KonstErhöhen = cbKonstErhöhen;
            CharismaErhöhen = cbCharErhöhen;
            GoldBehalten = tbGoldBehalten;
            ProxyJaNein = cbProxyJaNein;
            ProxyLogin = tbProxyLogin;
            ProxyPasswort = tbProxyPasswort;
            ProxyIp = tbIP;
            ProxyPort = tbPort;
            ProxyAnonym = cbAnonym;
            Server = cbServer;

            Server.SelectedIndex = 0;

            BarMissionTimer.Interval = 1000;
            UpdateTimer.Interval = 30000;

            MissionTimer.Tick += MissionFertig;
            UpdateTimer.Tick += UpdateChar;
            BarMissionTimer.Tick += UpdateMissionBar;
            WorkTimer.Tick += WorkFinished;

            LoadSavedProxy();

            cbDebugging.CheckState = Debug ? CheckState.Checked : CheckState.Unchecked;
        }

        public int TimeOut { set; get; }

        public override sealed string Text {
            get { return base.Text; }
            set { base.Text = value; }
        }


        private void Form1_Load(object sender, EventArgs e) {
            TtExpPotion.SetToolTip(pbExpPotion, "Erfahrungs Potion");
            TtSpeedPotion.SetToolTip(pbSpeedPotion, "Geschwindigkeits Potion");
        }

        private void btnLogin_Click(object sender, EventArgs e) {
            SLogin = tbLogin.Text;
            string sPw = tbPasswort.Text;
            SPwMd5 = BitConverter.ToString(Md5.ComputeHash(Encoding.Default.GetBytes(sPw))).Replace("-", "").ToLower();
            AddLog("Logge ein...", false);
            if (Login(SLogin, SPwMd5)) {
                AddLog("Eingeloggt!", false);

                Player1.UpdateSaveGame();
                try {
                    foreach (string part in Player1.LastRequest) {
                        if (part.Split(':')[0] == "SaveGameName")
                            Player1.Name = part.Split(':')[1];
                    }
                } catch (Exception ex) {
                    MessageBox.Show(
                        "Beim Abrufen ihres Charakternamens gab es einen Fehler. Vermutlich ist der verwendete Proxy nicht kompatibel.",
                        "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                lblName.Text = Player1.Name;
                lblLevel.Text = Player1.Level.ToString();

                EnergyTick.Interval = 1000;
                EnergyTick.Tick += Player1.UpdateEnergy;

                Player1.UpdateStats();

                if (!CheckForRunningQuest())
                    Player1.Update();

                EnergyTick.Start();
                UpdateTimer.Start();
                UpdateChar(null, null);

                if (Player1.ExpPotion) {
                    ExpPotion.Image = Resources.ExpPotion;
                    TtExpPotion.SetToolTip(pbExpPotion, "Erfahrungs Potion (aktiv)");
                } else {
                    ExpPotion.Image = Resources.ExpPotionOff;
                    TtExpPotion.SetToolTip(pbExpPotion, "Erfahrungs Potion (inaktiv)");
                }

                if (Player1.SpeedPotion) {
                    SpeedPotion.Image = Resources.SpeedPotion;
                    TtSpeedPotion.SetToolTip(pbSpeedPotion, "Geschwindigkeits Potion (aktiv)");
                } else {
                    SpeedPotion.Image = Resources.SpeedPotionOff;
                    TtSpeedPotion.SetToolTip(pbSpeedPotion, "Geschwindigkeits Potion (inaktiv)");
                }

                LoadSave();

                if (Player1.EPM)
                    rbEPM.Checked = true;
                if (Player1.GPM)
                    rbGPM.Checked = true;
                if (Player1.KZ)
                    rbKZ.Checked = true;
                if (Player1.ITEM)
                    rbItem.Checked = true;
            }
        }

        public static bool CheckForRunningQuest() {
            ZeitLeft = (Convert.ToInt32(Player1.SaveGamePlayer[SG_ACTIONENDTIME]) -
                        Convert.ToInt32(Player1.SaveGamePlayer[SG_TIMESTAMP]));

            if (Convert.ToInt32(Player1.SaveGamePlayer[SG_ACTIONSTARTTIME]) > 0) // Arbeit oder Quest aktiv
            {
                if (Convert.ToInt32(Player1.SaveGamePlayer[SG_MISSIONINDEX]) > 0) // Quest
                {
                    if (Convert.ToInt32(Player1.SaveGamePlayer[SG_TIMESTAMP]) >
                        Convert.ToInt32(Player1.SaveGamePlayer[SG_ACTIONENDTIME]) || ZeitLeft == 0) // Fertig?
                    {
                        MissionFertig(null, null);
                        return false;
                    }
                    MissionTimer.Stop();
                    AddLog("Quest anscheinend noch nicht fertig, setze Quest fort...", false);

                    MissionTimer.Interval = ZeitLeft*1000;
                    MissionTimer.Start();

                    BarMissionTimer.Stop();
                    BarMissionProgress.Maximum = ZeitLeft;
                    BarMissionTimer.Start();
                    Player1.inQuest = true;
                    Status.Text = "Queste";
                    return true;
                }
                if (Convert.ToInt32(Player1.SaveGamePlayer[SG_TIMESTAMP]) >
                    Convert.ToInt32(Player1.SaveGamePlayer[SG_ACTIONENDTIME]) || ZeitLeft == 0) // Fertig?
                {
                    WorkFinished(null, null);
                    return false;
                }
                MissionTimer.Stop();
                AddLog("Arbeit anscheinend noch nicht fertig, setze Arbeit fort...", false);
                MissionTimer.Interval = ZeitLeft*1000;
                MissionTimer.Start();

                BarMissionTimer.Stop();
                BarMissionProgress.Maximum = ZeitLeft;
                BarMissionTimer.Start();
                Player1.inWork = true;
                Status.Text = "Arbeite";
                return true;
            }
            return false;
        }

        public static void UpdateAusdauerBar() {
            Bar.Maximum = Player1.AusdauerMax;
            Bar.Minimum = 0;
            Bar.Value = Player1.Ausdauer;
            Ausdauer.Text = Player1.Ausdauer.ToString();
        }

        public static void UpdateChar(object sender, EventArgs e) {
            Player1.UpdateSaveGame();

            int strincr = 0;
            int geschincr = 0;
            int willeincr = 0;
            int konstincr = 0;
            int charincr = 0;

            if (Convert.ToInt32(GoldBehalten.Text) < Player1.Geld) {
                bool raus = false;
                while (raus == false) {
                    raus = true;

                    if (StärkeErhöhen.Checked &&
                        (Player1.Geld - Convert.ToInt32(Player1.SaveGamePlayer[406])) > Player1.GoldBehalten) {
                        RequestNr++;
                        string s =
                            Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                              ";AttributIncrease%3A1/" + Player1.BaseStr + "&rnd=" +
                                              Rnd.Next(10000000, 99999999));
                        Player1.LastRequest = s.Split('&');
                        Player1.UpdateSaveGame();
                        strincr++;
                        raus = false;
                    }

                    if (GeschickErhöhen.Checked &&
                        (Player1.Geld - Convert.ToInt32(Player1.SaveGamePlayer[407])) > Player1.GoldBehalten) {
                        RequestNr++;
                        string s =
                            Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                              ";AttributIncrease%3A2/" + Player1.BaseGesch + "&rnd=" +
                                              Rnd.Next(10000000, 99999999));
                        Player1.LastRequest = s.Split('&');
                        Player1.UpdateSaveGame();
                        geschincr++;
                        raus = false;
                    }
                    if (WilleErhöhen.Checked &&
                        (Player1.Geld - Convert.ToInt32(Player1.SaveGamePlayer[408])) > Player1.GoldBehalten) {
                        RequestNr++;
                        string s =
                            Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                              ";AttributIncrease%3A3/" + Player1.BaseWille + "&rnd=" +
                                              Rnd.Next(10000000, 99999999));
                        Player1.LastRequest = s.Split('&');
                        Player1.UpdateSaveGame();

                        willeincr++;
                        raus = false;
                    }
                    if (KonstErhöhen.Checked &&
                        (Player1.Geld - Convert.ToInt32(Player1.SaveGamePlayer[409])) > Player1.GoldBehalten) {
                        RequestNr++;
                        string s =
                            Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                              ";AttributIncrease%3A4/" + Player1.BaseKonst + "&rnd=" +
                                              Rnd.Next(10000000, 99999999));
                        Player1.LastRequest = s.Split('&');
                        Player1.UpdateSaveGame();

                        konstincr++;
                        raus = false;
                    }
                    if (CharismaErhöhen.Checked &&
                        (Player1.Geld - Convert.ToInt32(Player1.SaveGamePlayer[410])) > Player1.GoldBehalten) {
                        RequestNr++;
                        string s =
                            Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                              ";AttributIncrease%3A5/" + Player1.BaseChar + "&rnd=" +
                                              Rnd.Next(10000000, 99999999));
                        Player1.LastRequest = s.Split('&');
                        Player1.UpdateSaveGame();

                        charincr++;
                        raus = false;
                    }

                    Application.DoEvents();
                }
                if (strincr > 0)
                    AddLog("Stärke um " + strincr + " auf " + Player1.BaseStr + " erhöht.", false);
                if (geschincr > 0)
                    AddLog("Geschicklichkeit um " + geschincr + " auf " + Player1.BaseGesch + " erhöht.", false);
                if (willeincr > 0)
                    AddLog("Willenskraft um " + willeincr + " auf " + Player1.BaseWille + " erhöht.", false);
                if (konstincr > 0)
                    AddLog("Konstitution um " + konstincr + " auf " + Player1.BaseKonst + " erhöht.", false);
                if (charincr > 0)
                    AddLog("Charisma um " + geschincr + " auf " + Player1.BaseGesch + " erhöht.", false);
            }

            if (Player1.ExpPotion) {
                ExpPotion.Image = Resources.ExpPotion;
                TtExpPotion.SetToolTip(ExpPotion, "Erfahrungs Potion (aktiv)");
            } else {
                ExpPotion.Image = Resources.ExpPotionOff;
                TtExpPotion.SetToolTip(ExpPotion, "Erfahrungs Potion (inaktiv)");
            }

            if (Player1.SpeedPotion) {
                SpeedPotion.Image = Resources.SpeedPotion;
                TtSpeedPotion.SetToolTip(SpeedPotion, "Geschwindigkeits Potion (aktiv)");
            } else {
                SpeedPotion.Image = Resources.SpeedPotionOff;
                TtSpeedPotion.SetToolTip(SpeedPotion, "Geschwindigkeits Potion (inaktiv)");
            }

            Exp.Text = Player1.Exp + "/" + Player1.ExpNext;
            BarExp.Maximum = Player1.ExpNext;
            BarExp.Value = Player1.Exp;

            Player1.Update();
        }

        public static void WorkFinished(object sender, EventArgs e) {
            RequestNr++;
            string s =
                Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                  ";WorkFinished%3A&rnd=" + Rnd.Next(10000000, 99999999));
            Player1.LastRequest = s.Split('&');

            AddLog("Arbeit fertig.", false);
            Player1.inWork = false;
            Player1.inQuest = false;

            Player1.UpdateSaveGame();
            Parse();
        }

        public static void UpdateMissionBar(object sender, EventArgs e) {
            if (BarMissionProgress.Value < BarMissionProgress.Maximum)
                BarMissionProgress.Value += 1;

            MissionZeit.Text = TimeSpan.FromSeconds(BarMissionProgress.Maximum - BarMissionProgress.Value).ToString();
        }

        public static void MissionFertig(object sender, EventArgs e) {
            RequestNr++;
            AddLog("Mission fertig.", false);
            string s =
                Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                  ";TravelFinish%3A&rnd=" + Rnd.Next(10000000, 99999999));
            Player1.LastRequest = s.Split('&');
            Player1.UpdateSaveGame();
            Player1.inWork = false;
            Player1.inQuest = false;
            Parse();
        }

        public static void Parse() {
            try {
                string status = Player1.LastRequest[0].Split(':')[0];
                string art = Player1.LastRequest[0].Split(':')[1];


                switch (status) {
                    case "WorkMoney":
                    case "MissionCombat":
                    case "Success":
                        switch (art) {
                            case "login":
                                UpdateTimer.Start();
                                break;

                            case "MoveItem":
                                AddLog("Item erfolgreich verkauft/verschoben!", false);
                                Player1.UpdateSaveGame();
                                break;

                            case "QuestAccept":
                                Player1.inQuest = true;
                                MissionTimer.Start();
                                BarMissionTimer.Start();
                                AddLog("Mission gestartet...", false);
                                Status.Text = "Queste";
                                break;

                            case "QuestFinish":
                                Player1.inQuest = false;
                                Player1.inWork = false;
                                BarMissionTimer.Stop();
                                BarMissionProgress.Value = 0;
                                MissionZeit.Text = "00:00:00";
                                MissionTimer.Stop();
                                Status.Text = "Idle";
                                //AddLog("QuestFinish erreicht, Update Char.", true);
                                Player1.Update();
                                break;

                            case "WorkStart":
                                Player1.inWork = true;
                                BarMissionTimer.Start();
                                WorkTimer.Start();
                                AddLog("Arbeit gestartet...", false);
                                Status.Text = "Arbeite";
                                break;

                            case "WorkStop":
                                Player1.inWork = false;
                                BarMissionTimer.Stop();
                                BarMissionProgress.Value = 0;
                                WorkTimer.Stop();
                                Status.Text = "Idle";
                                Player1.Update();
                                break;

                            default:
                                if (status == "MissionCombat") {
                                    RequestNr++;
                                    string s =
                                        Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" +
                                                          RequestNr + ";QuestFinish%3A&rnd=" +
                                                          Rnd.Next(10000000, 99999999));
                                    Player1.LastRequest = s.Split('&');
                                    Player1.UpdateSaveGame();
                                    Status.Text = "Arbeit fertig";
                                    Player1.inQuest = false;
                                    Parse();
                                }
                                if (status == "WorkMoney") {
                                    Player1.inWork = false;
                                    BarMissionTimer.Stop();
                                    BarMissionProgress.Value = 0;
                                    MissionZeit.Text = "00:00";
                                    WorkTimer.Stop();
                                    Player1.Update();
                                    Status.Text = "Idle";
                                }
                                break;
                        }
                        break;
                    case "Error":
                        AddLog("Fehler: " + Player1.LastRequest[0].Split(':')[1], true);
                        MissionTimer.Stop();
                        BarMissionTimer.Stop();
                        BarMissionProgress.Value = 0;
                        MissionZeit.Text = "00:00:00";
                        switch (art) {
                            case "sessionidinvalid":
                            case "nottraveling":
                                AddLog("Logge neu ein...", false);
                                Login(SLogin, SPwMd5);
                                Player1.UpdateSaveGame();
                                Player1.UpdateStats();
                                CheckForRunningQuest();
                                UpdateTimer.Start();
                                Player1.Update();
                                break;

                            case "needmoreblood":
                                AddLog("Dieser Fehler sollte nicht vorkommen, starte Arbeit um es zu kompensieren.",
                                    true);
                                Player1.WorkStart(1);
                                break;

                            case "needfreeslot":
                                AddLog("Bitte einloggen und Platz im Inventar machen. Danach neu einloggen!", false);
                                break;

                            case "cannotstarttwoquests":
                            case "cannotacceptwhileworking":
                            case "AlreadyDoingSomething":

                                Status.Text = "Fehler";
                                CheckForRunningQuest();
                                break;

                            case "cannotfinishmissionnow":
                                MissionFertig(null, null);
                                break;

                            case "notworking":
                                AddLog("Breche momentane Arbeit ab...", false);
                                RequestNr++;
                                string antwort =
                                    Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" +
                                                      RequestNr + ";WorkStop%3A&rnd=" + Rnd.Next(10000000, 99999999));
                                Player1.LastRequest = antwort.Split('&');

                                Player1.inWork = false;
                                Player1.inQuest = false;

                                Parse();
                                Player1.Update();
                                break;
                        }
                        break;
                }
            } catch (Exception) {
                MessageBox.Show("Beim auswerten des Savegames gab es einen Fehler. (Fehlercode 01)", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        public static void ChangeSave() {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey rkS = rk.OpenSubKey("Software", true);

            if (rkS == null)
                return;

            RegistryKey temp = rkS.OpenSubKey("BloodgameBot", true);
            RegistryKey temp2 = null;

            if (SLogin != null) {
                if (temp != null) {
                    temp2 = temp.OpenSubKey(SLogin + " " + Server.Text, true);
                    if (temp2 == null)
                        temp.CreateSubKey(SLogin + " " + Server.Text);
                    temp2 = temp.OpenSubKey(SLogin + " " + Server.Text, true);
                }
            }

            if (temp2 != null) {
                temp2.SetValue("EPM", Player1.EPM.ToString());
                temp2.SetValue("GPM", Player1.GPM.ToString());
                temp2.SetValue("KZ", Player1.KZ.ToString());
                temp2.SetValue("ITEM", Player1.ITEM.ToString());
                temp2.SetValue("Debug", Debug.ToString());

                temp2.SetValue("Str", StärkeErhöhen.Checked.ToString());
                temp2.SetValue("Gesch", GeschickErhöhen.Checked.ToString());
                temp2.SetValue("Wille", WilleErhöhen.Checked.ToString());
                temp2.SetValue("Konst", KonstErhöhen.Checked.ToString());
                temp2.SetValue("Charisma", CharismaErhöhen.Checked.ToString());

                temp2.SetValue("GoldBehalten", GoldBehalten.Text);
            }

            if (temp != null) {
                temp.SetValue("ProxyLogin", ProxyLogin.Text);
                temp.SetValue("ProxyPasswort", ProxyPasswort.Text);
                temp.SetValue("ProxyIP", ProxyIp.Text);
                temp.SetValue("ProxyPort", ProxyPort.Text);
                temp.SetValue("ProxyEnabled", ProxyJaNein.Checked.ToString());
                temp.SetValue("ProxyAnonym", ProxyAnonym.Checked.ToString());
            }
        }

        public static void LoadSavedProxy() {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey rkS = rk.OpenSubKey("Software", true);

            if (rkS != null) {
                RegistryKey temp = rkS.OpenSubKey("BloodgameBot", true) ?? rkS.CreateSubKey("BloodgameBot");

                ProxyIp.Text = (string) temp.GetValue("ProxyIP");
                ProxyPort.Text = (string) temp.GetValue("ProxyPort");
                ProxyLogin.Text = (string) temp.GetValue("ProxyLogin");
                ProxyPasswort.Text = (string) temp.GetValue("ProxyPasswort");
                ProxyAnonym.Checked = Convert.ToBoolean((string) temp.GetValue("ProxyAnonym"));
                ProxyJaNein.Checked = Convert.ToBoolean((string) temp.GetValue("ProxyEnabled"));

                if (ProxyAnonym.CheckState == CheckState.Checked) {
                    ProxyLogin.Enabled = false;
                    ProxyPasswort.Enabled = false;
                } else {
                    ProxyLogin.Enabled = true;
                    ProxyPasswort.Enabled = true;
                }


                // Proxy einrichten!
                if (ProxyJaNein.CheckState == CheckState.Checked) {
                    ProxyIp.Enabled = true;
                    ProxyPort.Enabled = true;

                    if (ProxyAnonym.CheckState == CheckState.Unchecked) {
                        Cred = new NetworkCredential(ProxyLogin.Text, ProxyPasswort.Text);
                        Proxy = new WebProxy(ProxyIp.Text + ":" + ProxyPort.Text, true, null, Cred);
                        Wc.Proxy = Proxy;
                    } else {
                        Proxy = new WebProxy(ProxyIp.Text + ":" + ProxyPort.Text, true, null, null);
                        Wc.Proxy = Proxy;
                    }
                } else {
                    ProxyIp.Enabled = false;
                    ProxyPort.Enabled = false;
                }
            }
            AddLog("Proxy Einstellungen geladen.", true);
        }

        public static void LoadSave() {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey rkS = rk.OpenSubKey("Software", true);

            if (rkS == null) return;
            RegistryKey temp = rkS.OpenSubKey("BloodgameBot", true) ?? rkS.CreateSubKey("BloodgameBot");


            if (temp == null) return;
            RegistryKey temp2 = temp.OpenSubKey(SLogin + " " + Server.Text, true);


            if ((string) temp2.GetValue("EPM") == "True") {
                Player1.EPM = true;
                Player1.GPM = false;
                Player1.KZ = false;
                Player1.ITEM = false;
            }
            if ((string) temp2.GetValue("GPM") == "True") {
                Player1.EPM = false;
                Player1.GPM = true;
                Player1.KZ = false;
                Player1.ITEM = false;
            }
            if ((string) temp2.GetValue("KZ") == "True") {
                Player1.EPM = false;
                Player1.GPM = false;
                Player1.KZ = true;
                Player1.ITEM = false;
            }
            if ((string) temp2.GetValue("ITEM") == "True") {
                Player1.EPM = false;
                Player1.GPM = false;
                Player1.KZ = false;
                Player1.ITEM = true;
            }

            StärkeErhöhen.Checked = Convert.ToBoolean((string) temp2.GetValue("Str"));

            GeschickErhöhen.Checked = Convert.ToBoolean((string) temp2.GetValue("Gesch"));

            WilleErhöhen.Checked = Convert.ToBoolean((string) temp2.GetValue("Wille"));

            KonstErhöhen.Checked = Convert.ToBoolean((string) temp2.GetValue("Konst"));

            CharismaErhöhen.Checked = Convert.ToBoolean((string) temp2.GetValue("Charisma"));


            GoldBehalten.Text = (string) temp2.GetValue("GoldBehalten");
            if (GoldBehalten.Text == "")
                GoldBehalten.Text = "0";

            if ((string) temp2.GetValue("Debug") == Debug.ToString()) return;
            switch ((string) temp2.GetValue("Debug")) {
                case "True":
                    Debug = true;
                    break;
                case "False":
                    Debug = false;
                    break;
            }
        }

        private void Form1_Resize(object sender, EventArgs e) {
            notifyIcon.BalloonTipTitle = "Bloodgame Bot läuft noch!";
            notifyIcon.BalloonTipText = "Bloodgame Bot wurde in den Tray minimiert...";

            switch (WindowState) {
                case FormWindowState.Minimized:
                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(500);
                    Hide();
                    break;
                case FormWindowState.Normal:
                    notifyIcon.Visible = false;
                    Show();
                    break;
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e) {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void btnSaveErhöhungen_Click(object sender, EventArgs e) {
            ChangeSave();
            try {
                Player1.GoldBehalten = Convert.ToInt32(GoldBehalten.Text);
            } catch (FormatException ex) {
                MessageBox.Show("Bitte geben Sie nur Zahlen ein!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbInventar1_DoubleClick(object sender, EventArgs e) {
            if (Player1.inventar[0, 0] <= 0) return;

            //request.php?req=4pB9I8794f2A13t14P6b75x5A268v1p5;8;itemmove%3A2/3/5/0&rnd=29029377
            RequestNr++;
            string antwort =
                Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                  ";Itemmove%3A2/1/5/0&rnd=" + Rnd.Next(10000000, 99999999));
            Player1.LastRequest = antwort.Split('&');
            Parse();
        }

        private void pbInventar2_DoubleClick(object sender, EventArgs e) {
            if (Player1.inventar[1, 0] <= 0) return;

            //request.php?req=4pB9I8794f2A13t14P6b75x5A268v1p5;8;itemmove%3A2/3/5/0&rnd=29029377
            RequestNr++;
            string antwort =
                Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                  ";Itemmove%3A2/2/5/0&rnd=" + Rnd.Next(10000000, 99999999));
            Player1.LastRequest = antwort.Split('&');
            Parse();
        }

        private void pbInventar3_DoubleClick(object sender, EventArgs e) {
            if (Player1.inventar[2, 0] <= 0) return;

            //request.php?req=4pB9I8794f2A13t14P6b75x5A268v1p5;8;itemmove%3A2/3/5/0&rnd=29029377
            RequestNr++;
            string antwort =
                Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                  ";Itemmove%3A2/3/5/0&rnd=" + Rnd.Next(10000000, 99999999));
            Player1.LastRequest = antwort.Split('&');
            Parse();
        }

        private void pbInventar4_DoubleClick(object sender, EventArgs e) {
            if (Player1.inventar[3, 0] <= 0) return;

            //request.php?req=4pB9I8794f2A13t14P6b75x5A268v1p5;8;itemmove%3A2/3/5/0&rnd=29029377
            RequestNr++;
            string antwort =
                Wc.DownloadString("http://" + ServerName + "/request.php?req=" + SessionId + ";" + RequestNr +
                                  ";Itemmove%3A2/4/5/0&rnd=" + Rnd.Next(10000000, 99999999));
            Player1.LastRequest = antwort.Split('&');
            Parse();
        }

        private void beendenToolStripMenuItem1_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e) {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            Debug = cbDebugging.CheckState == CheckState.Checked;

            if (rbEPM.Checked) {
                Player1.EPM = true;
                Player1.GPM = false;
                Player1.KZ = false;
                Player1.ITEM = false;
            } else if (rbGPM.Checked) {
                Player1.EPM = false;
                Player1.GPM = true;
                Player1.KZ = false;
                Player1.ITEM = false;
            } else if (rbKZ.Checked) {
                Player1.EPM = false;
                Player1.GPM = false;
                Player1.KZ = true;
                Player1.ITEM = false;
            } else if (rbItem.Checked) {
                Player1.EPM = false;
                Player1.GPM = false;
                Player1.KZ = false;
                Player1.ITEM = true;
            }
            if (UpdateTimer.Enabled) {
                ChangeSave();
                MessageBox.Show("Änderungen gespeichert!", "Speichern", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
                MessageBox.Show("Bitte vor abspeichern mit Accountdaten einloggen!", "Fehler", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }

        private void btnProxySave_Click(object sender, EventArgs e) {
            ChangeSave();
            LoadSavedProxy();
            MessageBox.Show("Proxy Änderungen gespeichert!", "Speichern", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void cbAnonym_CheckedChanged(object sender, EventArgs e) {
            if (ProxyAnonym.CheckState == CheckState.Checked) {
                ProxyLogin.Enabled = false;
                ProxyPasswort.Enabled = false;
            } else {
                ProxyLogin.Enabled = true;
                ProxyPasswort.Enabled = true;
            }
        }

        private void cbProxyJaNein_CheckStateChanged(object sender, EventArgs e) {
            if (cbProxyJaNein.CheckState == CheckState.Unchecked) {
                Wc.Proxy = null;
                ProxyIp.Enabled = false;
                ProxyPort.Enabled = false;
            } else {
                ProxyIp.Enabled = true;
                ProxyPort.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
        }

        #region Login

        public static bool Login(string login, string pw) {
            try {
                Player1.inQuest = false;
                Player1.inWork = false;
                EnergyTick.Stop();
                MissionTimer.Stop();
                WorkTimer.Stop();
                BarMissionTimer.Stop();
                UpdateTimer.Stop();
                BarMissionProgress.Value = 0;
                MissionZeit.Text = "00:00";


                switch (Server.Text) {
                    case "RTL2":
                    case "S1":
                        ServerName = Server.Text + ".bloodgame.de";
                        break;
                    case "S1 GR":
                        ServerName = "s1.bloodmoon.gr";
                        break;
                    case "S1 COM":
                        ServerName = "s1.bloodmoon.com";
                        break;
                    default:
                        ServerName = Server.Text + ".bloodmoon.de";
                        break;
                }

                string s =
                    Wc.DownloadString("http://" + ServerName + "/request.php?req=12345678901234567890123456789012;" +
                                      RequestNr + ";AccountLogin%3A" + login + "/" + pw + "/0&rnd=" +
                                      Rnd.Next(10000000, 99999999) + " HTTP/1.1\\\\r\\\\n");

                RequestNr++;

                Player1.LastRequest = s.Split('&');

                if (Player1.LastRequest[0] == "Error:LoginFailed") {
                    AddLog("Falsche LoginDaten angegeben!", false);
                    return false;
                }

                SessionId = Player1.LastRequest[1].Split(':')[1];
                LoadSave();

                return true;
            } catch (Exception) {
                MessageBox.Show(
                    "Es konnte anscheinend keine Verbindung zum Server hergestellt werden. Stellen Sie sicher das eine Internetverbindung besteht und, falls vorhanden, der Proxy korrekt eingetragen ist.",
                    "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region AddLog fügt Text zur Listbox hinzu.

        public static void AddLog(String data, bool debugging) {
            if ((debugging && Debug) || (!debugging && !Debug) || (!debugging && Debug)) {
                Logs.Items.Add("[" + DateTime.Now.ToShortTimeString() + "] " + data);
                Logs.SelectedIndex = Logs.Items.Count - 1;
            }
        }

        #endregion
    }


    public class Spieler {
        #region Consts

        public const int ITM_TYP = 0;
        public const int ITM_PICINDEX = 1;
        public const int ITM_DAMAGEMIN = 2;
        public const int ITM_DAMAGEMAX = 3;
        public const int ITM_ATTRIBUTTYP = 4;
        public const int ITM_ATTRIBUTVALUE = 7;
        public const int ITM_COINS = 10;
        public const int ITM_MONEY = 11;
        public const int ITM_SIZE = 12;
        public const int MIS_TYPE = 0;
        public const int MIS_TITEL = 1;
        public const int MIS_PLACE = 2;
        public const int MIS_TEXTSEED = 3;
        public const int MIS_DURATION = 4;
        public const int MIS_REWARD_MONEY = 5;
        public const int MIS_REWARD_COINS = 6;
        public const int MIS_REWARD_EXP = 7;
        public const int MIS_REWARD_ITEM = 8;
        public const int MIS_SIZE = 20;
        public const int SG_PAYMENTID = 0;
        public const int SG_PLAYERID = 1;
        public const int SG_RECRUITID = 2;
        public const int SG_LASTACTIONDATE = 3;
        public const int SG_LASTLOGINIP = 4;
        public const int SG_LASTLOGINDATE = 9;
        public const int SG_REGISTRATIONDATE = 14;
        public const int SG_REGISTRATIONIP = 15;
        public const int SG_VALIDATIONAGAINCOUNT = 16;
        public const int SG_VALIDATIONAGAINDATE = 17;
        public const int SG_VALIDATIONTYP = 18;
        public const int SG_VALIDATIONDATE = 19;
        public const int SG_VALIDATIONIP = 20;
        public const int SG_DELETEPROTECTIONDATE = 21;
        public const int SG_LOCKTYP = 22;
        public const int SG_LOCKDATE = 23;
        public const int SG_TIMESTAMP = 24;
        public const int SG_MONEY = 25;
        public const int SG_COINS = 26;
        public const int SG_CLASS = 27;
        public const int SG_GENDER = 28;
        public const int SG_FACE = 29;
        public const int SG_RANK = 30;
        public const int SG_HONOR = 31;
        public const int SG_LEVEL = 32;
        public const int SG_EXP = 33;
        public const int SG_EXP_NEXT = 34;
        public const int SG_GROUPID = 35;
        public const int SG_ITEM_EQUIP = 36;
        public const int SG_ITEM_BACKPACK = 156;
        public const int SG_ITEM_WEAPON = 216;
        public const int SG_ITEM_POTION = 288;
        public const int SG_ITEM_BUYBACK = 360;
        public const int SG_ATTRIBUT_VALUE = 396;
        public const int SG_ATTRIBUT_BUYED = 401;
        public const int SG_ATTRIBUT_PRICE = 406;
        public const int SG_ATTRIBUT_POTION = 411;
        public const int SG_ATTRIBUT_BONUS = 416;
        public const int SG_DAMAGEMIN = 421;
        public const int SG_DAMAGEMAX = 422;
        public const int SG_ATTACK = 423;
        public const int SG_RESIST = 424;
        public const int SG_LIFE = 425;
        public const int SG_INFLUENCE = 426;
        public const int SG_MISSION = 427;
        public const int SG_MISSIONINDEX = 487;
        public const int SG_MAINQUESTPROGRESS = 488;
        public const int SG_MAINQUESTDONEPROGRESS = 489;
        public const int SG_ARCHIVMENT_LEVEL = 490;
        public const int SG_ARCHIVMENT_VALUE = 500;
        public const int SG_ARCHIVMENT_NEXT = 510;
        public const int SG_ARCHIVMENT_BONUS = 520;
        public const int SG_HIDEOUT = 530;
        public const int SG_PERSONINSCREEN = 540;
        public const int SG_MESSAGECOUNT = 550;
        public const int SG_MESSAGEID = 551;
        public const int SG_ACTIONSTATUS = 651;
        public const int SG_ACTIONINDEX = 652;
        public const int SG_ACTIONSTARTTIME = 653;
        public const int SG_ACTIONENDTIME = 654;
        public const int SG_ENDURANCELASTCALCDATE = 655;
        public const int SG_ENDURANCEREGENERATIONPERSECOND = 656;
        public const int SG_ENDURANCE = 657;
        public const int SG_ENDURANCEMAX = 658;
        public const int SG_STORECALCULATIONDATE = 659;
        public const int SG_COINSGAINED = 661;
        public const int SG_COINSREALBOUGHT = 662;
        public const int SG_COINSBUYED = 663;
        public const int SG_COINSDATE = 664;
        public const int SG_COINSGEKAUFTSEITLETZTEMLOGIN = 665;
        public const int SG_FIRSTPAYMENTUSERFOR = 666;
        public const int SG_LOCKSTATUS = 667;
        public const int SG_LOCKDURATION = 668;
        public const int SG_DUNGEON_DONETOTAL = 669;
        public const int SG_DUNGEON_LEVELDONE = 670;
        public const int SG_DUNGEON_LEVELMAX = 690;
        public const int SG_MONEYBASEVALUE = 710;
        public const int SG_COINS_LASTBUYAMOUNT = 711;
        public const int SG_COINS_LASTBUYDATE = 712;
        public const int SG_MISSIONSTATUS = 713;
        public const int SG_COINSREALBOUGHTPRECALCULATION = 714;
        public const int SG_BUFFINDEX = 715;
        public const int SG_BUFFDURATION = 720;
        public const int SG_REPUTATION = 725;
        public const int SG_REPUTATIONEXP = 735;
        public const int SG_REPUTATIONNEXT = 745;
        public const int SG_ARMOR = 755;
        public const int SG_PVPENEMYID = 756;
        public const int SG_PVPENEMYCALCULATEDATE = 766;
        public const int SG_LAST5ID = 767;
        public const int SG_LAST5DATE = 772;
        public const int SG_LAST5COUNT = 777;
        public const int SG_PROTECTIONTIME = 778;
        public const int SG_TUTORIALSTATUS = 779;
        public const int SG_LASTPVPFIGHTDATE = 780;
        public const int SG_MAINQUESTTIMER = 781;
        public const int SG_DUFEHLSTUNS = 782;
        public const int SG_QUEST_TAKEANDLOSE = 783;
        public const int SG_MONEYREDUCE = 784;
        public const int SG_FREE = 785;
        public const int SG_SIZE = 931;
        public const int SERVER_STRUCTURE_CHECKSUM = 277304;

        public string[] ITEMTYP = {
            "Helm", "Amulett", "Brustrüstung", "Gürtel", "Handschuhe", "Ring", "Schuhe",
            "Accessoire", "Waffe", "???", "???", "???", "???", "???", "Trank"
        };

        public string[] STATTYP = {
            "Stärke", "Geschick", "Wille", "Konst", "Charisma", "???", "???", "???", "???",
            "???", "???", "???", "???", "???", "Ausdauer"
        };

        #endregion

        public int Angriff;
        public int Ausdauer;
        public int AusdauerMax;
        public int AusdauerMaxRaw;
        public int AusdauerRaw;
        public int AusdauerRegProSec;
        public int BaseChar;

        public int BaseGesch;
        public int BaseKonst;
        public int BaseStr;
        public int BaseWille;
        public int Class;
        public int Coins;
        public bool EPM = true;
        public int Einfluss;
        public int Exp;
        public int ExpNext;
        public bool ExpPotion = false;

        public bool GPM = false;
        public int Geld;
        public int GesamtChar;
        public int GesamtGesch;
        public int GesamtKonst;
        public int GesamtStr;
        public int GesamtWille;
        public int GoldBehalten;
        public bool ITEM = false;
        public bool KZ = false;
        public string[] LastRequest;
        public int Leben;
        public int Level;
        public int Läuterung;
        public int MaxSchaden;
        public int MinSchaden;
        public string Name;
        public int Rank;
        public string[] SaveGamePlayer;

        public bool SpeedPotion = false;

        public bool inQuest = false;
        public bool inWork = false;
        public int[,] inventar = new int[4, 9];

        public int pickedQuest;
        public int questTime;

        public int[,] quests = new int[3, 5];
        public int workTime;


        public void Update() {
            if (inQuest == false) {
                CheckQuests();
                bool result = PickQuest();

                if (result == false && inWork == false) {
                    double temp = Math.Ceiling((double) quests[pickedQuest, 0]/60);
                    // Zeit die die Mission dauert / 60 = Zeit in Minuten
                    temp = temp - Ausdauer; // Auf nächsthöhere Zahl runden
                    if (temp < 1)
                        temp = 1;

                    int regProStunde = 4;

                    temp = Math.Ceiling(temp/regProStunde);
                    if (temp < 1)
                        temp = 1;

                    if (temp > 10)
                        temp = 10;


                    Form1.AddLog(
                        "Nicht genug Ausdauer für die gewünschte Quest. Arbeite für " + temp + " Stunde(n)...", false);
                    WorkStart((int) temp);
                }
            }

            if (inQuest)
                Form1.Status.Text = "Queste";
            if (inWork)
                Form1.Status.Text = "Arbeite";
            if (inQuest == false && inWork == false)
                Form1.Status.Text = "Idle";

            Form1.LoadSave();
        }

        public void UpdateStats() {
            BaseStr = Convert.ToInt32(SaveGamePlayer[396]);
            BaseGesch = Convert.ToInt32(SaveGamePlayer[397]);
            BaseWille = Convert.ToInt32(SaveGamePlayer[398]);
            BaseKonst = Convert.ToInt32(SaveGamePlayer[399]);
            BaseChar = Convert.ToInt32(SaveGamePlayer[400]);

            GesamtStr = BaseStr + Convert.ToInt32(SaveGamePlayer[416]);
            GesamtGesch = BaseGesch + Convert.ToInt32(SaveGamePlayer[417]);
            GesamtWille = BaseWille + Convert.ToInt32(SaveGamePlayer[418]);
            GesamtKonst = BaseKonst + Convert.ToInt32(SaveGamePlayer[419]);
            GesamtChar = BaseChar + Convert.ToInt32(SaveGamePlayer[420]);

            MinSchaden = Convert.ToInt32(SaveGamePlayer[SG_DAMAGEMIN]);
            MaxSchaden = Convert.ToInt32(SaveGamePlayer[SG_DAMAGEMAX]);
            Angriff = Convert.ToInt32(SaveGamePlayer[SG_ATTACK]);
            Läuterung = Convert.ToInt32(SaveGamePlayer[SG_RESIST]);
            Leben = Convert.ToInt32(SaveGamePlayer[SG_LIFE]);
            Einfluss = Convert.ToInt32(SaveGamePlayer[SG_INFLUENCE]);

            Geld = Convert.ToInt32(SaveGamePlayer[SG_MONEY]);
            Coins = Convert.ToInt32(SaveGamePlayer[SG_COINS]);
            Class = Convert.ToInt32(SaveGamePlayer[SG_CLASS]);
            Rank = Convert.ToInt32(SaveGamePlayer[SG_RANK]);
            Level = Convert.ToInt32(SaveGamePlayer[SG_LEVEL]);
            Exp = Convert.ToInt32(SaveGamePlayer[SG_EXP]);
            ExpNext = Convert.ToInt32(SaveGamePlayer[SG_EXP_NEXT]);

            AusdauerRegProSec = Convert.ToInt32(SaveGamePlayer[SG_ENDURANCEREGENERATIONPERSECOND]);

            AusdauerRaw = Convert.ToInt32(SaveGamePlayer[SG_ENDURANCE]);
            Ausdauer = AusdauerRaw/10000;
            AusdauerMaxRaw = Convert.ToInt32(SaveGamePlayer[SG_ENDURANCEMAX]);
            AusdauerMax = AusdauerMaxRaw/10000;

            GoldBehalten = Convert.ToInt32(Form1.GoldBehalten.Text);

            if (Convert.ToInt32(SaveGamePlayer[715]) == 1)
                ExpPotion = true;
            else
                ExpPotion = false;

            if (Convert.ToInt32(SaveGamePlayer[716]) == 1)
                SpeedPotion = true;
            else
                SpeedPotion = false;

            Form1.Gold.Text = Geld.ToString();
            Form1.Level.Text = Level.ToString();
            Form1.Ausdauer.Text = Ausdauer.ToString();
            Form1.Blutsteine.Text = Coins.ToString();

            Form1.Stärke.Text = "Stärke:  " + GesamtStr;
            Form1.Geschicklichkeit.Text = "Gesch:  " + GesamtGesch;
            Form1.Willenskraft.Text = "Wille:     " + GesamtWille;
            Form1.Konstitution.Text = "Konst:    " + GesamtKonst;
            Form1.Charisma.Text = "Char:     " + GesamtChar;

            #region Inventar

            inventar[0, 0] = Convert.ToInt32(SaveGamePlayer[156]); // ItemTyp
            inventar[0, 1] = Convert.ToInt32(SaveGamePlayer[158]); // Rüstung
            inventar[0, 2] = Convert.ToInt32(SaveGamePlayer[160]); // AttributTyp1
            inventar[0, 3] = Convert.ToInt32(SaveGamePlayer[161]); // AttributTyp2
            inventar[0, 4] = Convert.ToInt32(SaveGamePlayer[162]); // AttributTyp3
            inventar[0, 5] = Convert.ToInt32(SaveGamePlayer[163]); // Attribut1
            inventar[0, 6] = Convert.ToInt32(SaveGamePlayer[164]); // Attribut2
            inventar[0, 7] = Convert.ToInt32(SaveGamePlayer[165]); // Attribut3
            inventar[0, 8] = Convert.ToInt32(SaveGamePlayer[167]); // Geld

            inventar[1, 0] = Convert.ToInt32(SaveGamePlayer[168]); // ItemTyp
            inventar[1, 1] = Convert.ToInt32(SaveGamePlayer[170]); // Rüstung
            inventar[1, 2] = Convert.ToInt32(SaveGamePlayer[172]); // AttributTyp1
            inventar[1, 3] = Convert.ToInt32(SaveGamePlayer[173]); // AttributTyp2
            inventar[1, 4] = Convert.ToInt32(SaveGamePlayer[174]); // AttributTyp3
            inventar[1, 5] = Convert.ToInt32(SaveGamePlayer[175]); // Attribut1
            inventar[1, 6] = Convert.ToInt32(SaveGamePlayer[176]); // Attribut2
            inventar[1, 7] = Convert.ToInt32(SaveGamePlayer[177]); // Attribut3
            inventar[1, 8] = Convert.ToInt32(SaveGamePlayer[179]); // Geld

            inventar[2, 0] = Convert.ToInt32(SaveGamePlayer[180]); // ItemTyp
            inventar[2, 1] = Convert.ToInt32(SaveGamePlayer[182]); // Rüstung
            inventar[2, 2] = Convert.ToInt32(SaveGamePlayer[184]); // AttributTyp1
            inventar[2, 3] = Convert.ToInt32(SaveGamePlayer[185]); // AttributTyp2
            inventar[2, 4] = Convert.ToInt32(SaveGamePlayer[186]); // AttributTyp3
            inventar[2, 5] = Convert.ToInt32(SaveGamePlayer[187]); // Attribut1
            inventar[2, 6] = Convert.ToInt32(SaveGamePlayer[188]); // Attribut2
            inventar[2, 7] = Convert.ToInt32(SaveGamePlayer[189]); // Attribut3
            inventar[2, 8] = Convert.ToInt32(SaveGamePlayer[191]); // Geld

            inventar[3, 0] = Convert.ToInt32(SaveGamePlayer[192]); // ItemTyp
            inventar[3, 1] = Convert.ToInt32(SaveGamePlayer[194]); // Rüstung
            inventar[3, 2] = Convert.ToInt32(SaveGamePlayer[196]); // AttributTyp1
            inventar[3, 3] = Convert.ToInt32(SaveGamePlayer[197]); // AttributTyp2
            inventar[3, 4] = Convert.ToInt32(SaveGamePlayer[198]); // AttributTyp3
            inventar[3, 5] = Convert.ToInt32(SaveGamePlayer[199]); // Attribut1
            inventar[3, 6] = Convert.ToInt32(SaveGamePlayer[200]); // Attribut2
            inventar[3, 7] = Convert.ToInt32(SaveGamePlayer[201]); // Attribut3
            inventar[3, 8] = Convert.ToInt32(SaveGamePlayer[203]); // Geld


            switch (inventar[0, 0]) {
                case 0:
                    Form1.InventarSlot1.Image = Resources.InventarLeer;
                    break;
                case 1:
                    Form1.InventarSlot1.Image = Resources.Helm;
                    break;
                case 2:
                    Form1.InventarSlot1.Image = Resources.Amulett;
                    break;
                case 3:
                    Form1.InventarSlot1.Image = Resources.Brust;
                    break;
                case 4:
                    Form1.InventarSlot1.Image = Resources.Gürtel;
                    break;
                case 5:
                    Form1.InventarSlot1.Image = Resources.Handschuhe;
                    break;
                case 6:
                    Form1.InventarSlot1.Image = Resources.Ring;
                    break;
                case 7:
                    Form1.InventarSlot1.Image = Resources.Schuhe;
                    break;
                case 8:
                    Form1.InventarSlot1.Image = Resources.Accessoire;
                    break;
                case 9:
                    Form1.InventarSlot1.Image = Resources.Waffe;
                    break;
                case 15:
                    Form1.InventarSlot1.Image = Resources.Trank;
                    break;
            }
            if (inventar[0, 0] == 0)
                if (inventar[0, 0] > 0 && inventar[0, 2] == 0 && inventar[0, 3] == 0 && inventar[0, 4] == 0)
                    Form1.InventarSlot1Tt.SetToolTip(Form1.InventarSlot1,
                        ITEMTYP[(inventar[0, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[0, 1] + "\r\n" + "\r\nGold: " +
                        inventar[0, 8]);
            if (inventar[0, 0] > 0 && inventar[0, 2] > 0 && inventar[0, 3] == 0 && inventar[0, 4] == 0)
                Form1.InventarSlot1Tt.SetToolTip(Form1.InventarSlot1,
                    ITEMTYP[(inventar[0, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[0, 1] + "\r\n" +
                    STATTYP[(inventar[0, 2] - 1)] + ": " + inventar[0, 5] + "\r\nGold: " + inventar[0, 8]);
            if (inventar[0, 0] > 0 && inventar[0, 2] > 0 && inventar[0, 3] > 0 && inventar[0, 4] == 0)
                Form1.InventarSlot1Tt.SetToolTip(Form1.InventarSlot1,
                    ITEMTYP[(inventar[0, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[0, 1] + "\r\n" +
                    STATTYP[(inventar[0, 2] - 1)] + ": " + inventar[0, 5] + "\r\n" + STATTYP[(inventar[0, 3] - 1)] +
                    ": " + inventar[0, 6] + "\r\nGold: " + inventar[0, 8]);
            if (inventar[0, 0] > 0 && inventar[0, 2] > 0 && inventar[0, 3] > 0 && inventar[0, 4] > 0)
                Form1.InventarSlot1Tt.SetToolTip(Form1.InventarSlot1,
                    ITEMTYP[(inventar[0, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[0, 1] + "\r\n" +
                    STATTYP[(inventar[0, 2] - 1)] + ": " + inventar[0, 5] + "\r\n" + STATTYP[(inventar[0, 3] - 1)] +
                    ": " + inventar[0, 6] + "\r\n" + STATTYP[(inventar[0, 4] - 1)] + ": " + inventar[0, 7] +
                    "\r\nGold: " + inventar[0, 8]);

            switch (inventar[1, 0]) {
                case 0:
                    Form1.InventarSlot2.Image = Resources.InventarLeer;
                    break;
                case 1:
                    Form1.InventarSlot2.Image = Resources.Helm;
                    break;
                case 2:
                    Form1.InventarSlot2.Image = Resources.Amulett;
                    break;
                case 3:
                    Form1.InventarSlot2.Image = Resources.Brust;
                    break;
                case 4:
                    Form1.InventarSlot2.Image = Resources.Gürtel;
                    break;
                case 5:
                    Form1.InventarSlot2.Image = Resources.Handschuhe;
                    break;
                case 6:
                    Form1.InventarSlot2.Image = Resources.Ring;
                    break;
                case 7:
                    Form1.InventarSlot2.Image = Resources.Schuhe;
                    break;
                case 8:
                    Form1.InventarSlot2.Image = Resources.Accessoire;
                    break;
                case 9:
                    Form1.InventarSlot2.Image = Resources.Waffe;
                    break;
                case 15:
                    Form1.InventarSlot2.Image = Resources.Trank;
                    break;
            }
            if (inventar[1, 0] == 0)
                Form1.InventarSlot2Tt.SetToolTip(Form1.InventarSlot2, "Leer");
            if (inventar[1, 0] > 0 && inventar[1, 2] == 0 && inventar[1, 3] == 0 && inventar[1, 4] == 0)
                Form1.InventarSlot2Tt.SetToolTip(Form1.InventarSlot2,
                    ITEMTYP[(inventar[1, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[1, 1] + "\r\n" + "\r\nGold: " +
                    inventar[1, 8]);
            if (inventar[1, 0] > 0 && inventar[1, 2] > 0 && inventar[1, 3] == 0 && inventar[1, 4] == 0)
                Form1.InventarSlot2Tt.SetToolTip(Form1.InventarSlot2,
                    ITEMTYP[(inventar[1, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[1, 1] + "\r\n" +
                    STATTYP[(inventar[1, 2] - 1)] + ": " + inventar[1, 5] + "\r\nGold: " + inventar[1, 8]);
            if (inventar[1, 0] > 0 && inventar[1, 2] > 0 && inventar[1, 3] > 0 && inventar[1, 4] == 0)
                Form1.InventarSlot2Tt.SetToolTip(Form1.InventarSlot2,
                    ITEMTYP[(inventar[1, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[1, 1] + "\r\n" +
                    STATTYP[(inventar[1, 2] - 1)] + ": " + inventar[1, 5] + "\r\n" + STATTYP[(inventar[1, 3] - 1)] +
                    ": " + inventar[1, 6] + "\r\nGold: " + inventar[1, 8]);
            if (inventar[1, 0] > 0 && inventar[1, 2] > 0 && inventar[1, 3] > 0 && inventar[1, 4] > 0)
                Form1.InventarSlot2Tt.SetToolTip(Form1.InventarSlot2,
                    ITEMTYP[(inventar[1, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[1, 1] + "\r\n" +
                    STATTYP[(inventar[1, 2] - 1)] + ": " + inventar[1, 5] + "\r\n" + STATTYP[(inventar[1, 3] - 1)] +
                    ": " + inventar[1, 6] + "\r\n" + STATTYP[(inventar[1, 4] - 1)] + ": " + inventar[1, 7] +
                    "\r\nGold: " + inventar[1, 8]);


            switch (inventar[2, 0]) {
                case 0:
                    Form1.InventarSlot3.Image = Resources.InventarLeer;
                    break;
                case 1:
                    Form1.InventarSlot3.Image = Resources.Helm;
                    break;
                case 2:
                    Form1.InventarSlot3.Image = Resources.Amulett;
                    break;
                case 3:
                    Form1.InventarSlot3.Image = Resources.Brust;
                    break;
                case 4:
                    Form1.InventarSlot3.Image = Resources.Gürtel;
                    break;
                case 5:
                    Form1.InventarSlot3.Image = Resources.Handschuhe;
                    break;
                case 6:
                    Form1.InventarSlot3.Image = Resources.Ring;
                    break;
                case 7:
                    Form1.InventarSlot3.Image = Resources.Schuhe;
                    break;
                case 8:
                    Form1.InventarSlot3.Image = Resources.Accessoire;
                    break;
                case 9:
                    Form1.InventarSlot3.Image = Resources.Waffe;
                    break;
                case 15:
                    Form1.InventarSlot3.Image = Resources.Trank;
                    break;
            }
            if (inventar[2, 0] == 0)
                Form1.InventarSlot3Tt.SetToolTip(Form1.InventarSlot3, "Leer");
            if (inventar[2, 0] > 0 && inventar[2, 2] == 0 && inventar[2, 3] == 0 && inventar[2, 4] == 0)
                Form1.InventarSlot3Tt.SetToolTip(Form1.InventarSlot3,
                    ITEMTYP[(inventar[2, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[2, 1] + "\r\n" + "\r\nGold: " +
                    inventar[2, 8]);
            if (inventar[2, 0] > 0 && inventar[2, 2] > 0 && inventar[2, 3] == 0 && inventar[2, 4] == 0)
                Form1.InventarSlot3Tt.SetToolTip(Form1.InventarSlot3,
                    ITEMTYP[(inventar[2, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[2, 1] + "\r\n" +
                    STATTYP[(inventar[2, 2] - 1)] + ": " + inventar[2, 5] + "\r\nGold: " + inventar[2, 8]);
            if (inventar[2, 0] > 0 && inventar[2, 2] > 0 && inventar[2, 3] > 0 && inventar[2, 4] == 0)
                Form1.InventarSlot3Tt.SetToolTip(Form1.InventarSlot3,
                    ITEMTYP[(inventar[2, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[2, 1] + "\r\n" +
                    STATTYP[(inventar[2, 2] - 1)] + ": " + inventar[2, 5] + "\r\n" + STATTYP[(inventar[2, 3] - 1)] +
                    ": " + inventar[2, 6] + "\r\nGold: " + inventar[2, 8]);
            if (inventar[2, 0] > 0 && inventar[2, 2] > 0 && inventar[2, 3] > 0 && inventar[2, 4] > 0)
                Form1.InventarSlot3Tt.SetToolTip(Form1.InventarSlot3,
                    ITEMTYP[(inventar[2, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[2, 1] + "\r\n" +
                    STATTYP[(inventar[2, 2] - 1)] + ": " + inventar[2, 5] + "\r\n" + STATTYP[(inventar[2, 3] - 1)] +
                    ": " + inventar[2, 6] + "\r\n" + STATTYP[(inventar[2, 4] - 1)] + ": " + inventar[2, 7] +
                    "\r\nGold: " + inventar[2, 8]);

            switch (inventar[3, 0]) {
                case 0:
                    Form1.InventarSlot4.Image = Resources.InventarLeer;
                    break;
                case 1:
                    Form1.InventarSlot4.Image = Resources.Helm;
                    break;
                case 2:
                    Form1.InventarSlot4.Image = Resources.Amulett;
                    break;
                case 3:
                    Form1.InventarSlot4.Image = Resources.Brust;
                    break;
                case 4:
                    Form1.InventarSlot4.Image = Resources.Gürtel;
                    break;
                case 5:
                    Form1.InventarSlot4.Image = Resources.Handschuhe;
                    break;
                case 6:
                    Form1.InventarSlot4.Image = Resources.Ring;
                    break;
                case 7:
                    Form1.InventarSlot4.Image = Resources.Schuhe;
                    break;
                case 8:
                    Form1.InventarSlot4.Image = Resources.Accessoire;
                    break;
                case 9:
                    Form1.InventarSlot4.Image = Resources.Waffe;
                    break;
                case 15:
                    Form1.InventarSlot4.Image = Resources.Trank;
                    break;
            }
            if (inventar[3, 0] == 0)
                Form1.InventarSlot4Tt.SetToolTip(Form1.InventarSlot4, "Leer");
            if (inventar[3, 0] > 0 && inventar[3, 2] == 0 && inventar[3, 3] == 0 && inventar[3, 4] == 0)
                Form1.InventarSlot4Tt.SetToolTip(Form1.InventarSlot4,
                    ITEMTYP[(inventar[3, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[3, 1] + "\r\n" + "\r\nGold: " +
                    inventar[3, 8]);
            if (inventar[3, 0] > 0 && inventar[3, 2] > 0 && inventar[3, 3] == 0 && inventar[3, 4] == 0)
                Form1.InventarSlot4Tt.SetToolTip(Form1.InventarSlot4,
                    ITEMTYP[(inventar[3, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[3, 1] + "\r\n" +
                    STATTYP[(inventar[3, 2] - 1)] + ": " + inventar[3, 5] + "\r\nGold: " + inventar[3, 8]);
            if (inventar[3, 0] > 0 && inventar[3, 2] > 0 && inventar[3, 3] > 0 && inventar[3, 4] == 0)
                Form1.InventarSlot4Tt.SetToolTip(Form1.InventarSlot4,
                    ITEMTYP[(inventar[3, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[3, 1] + "\r\n" +
                    STATTYP[(inventar[3, 2] - 1)] + ": " + inventar[3, 5] + "\r\n" + STATTYP[(inventar[3, 3] - 1)] +
                    ": " + inventar[3, 6] + "\r\nGold: " + inventar[3, 8]);
            if (inventar[3, 0] > 0 && inventar[3, 2] > 0 && inventar[3, 3] > 0 && inventar[3, 4] > 0)
                Form1.InventarSlot4Tt.SetToolTip(Form1.InventarSlot4,
                    ITEMTYP[(inventar[3, 0] - 1)] + "\r\n" + "Rüstung: " + inventar[3, 1] + "\r\n" +
                    STATTYP[(inventar[3, 2] - 1)] + ": " + inventar[3, 5] + "\r\n" + STATTYP[(inventar[3, 3] - 1)] +
                    ": " + inventar[3, 6] + "\r\n" + STATTYP[(inventar[3, 4] - 1)] + ": " + inventar[3, 7] +
                    "\r\nGold: " + inventar[3, 8]);

            #endregion

            Form1.UpdateAusdauerBar();
        }

        public void UpdateEnergy(object sender, EventArgs e) {
            if (AusdauerRaw < AusdauerMaxRaw)
                AusdauerRaw += AusdauerRegProSec;
            else
                AusdauerRaw = AusdauerMaxRaw;

            Form1.UpdateAusdauerBar();
        }

        public void CheckQuests() {
            #region QuestNummern

            // 431 = Quest1 Zeit
            // 432 = Quest1 Gold
            // 433 = ???
            // 434 = Quest1 Erfahrung
            // 435 = Item Art

            // 451 = Quest2 Zeit
            // usw.

            // 471 = Quest3 Zeit
            // usw.

            #endregion

            if (inQuest == false && inWork == false) {
                for (int i = 0; i < 3; i++) {
                    if (i == 0) {
                        quests[0, 0] = Convert.ToInt32(SaveGamePlayer[431]);
                        quests[0, 1] = Convert.ToInt32(SaveGamePlayer[432]);
                        quests[0, 2] = Convert.ToInt32(SaveGamePlayer[434]);
                        quests[0, 3] = Convert.ToInt32(SaveGamePlayer[435]);
                        quests[0, 4] = Convert.ToInt32(SaveGamePlayer[433]);
                    }
                    if (i == 1) {
                        quests[1, 0] = Convert.ToInt32(SaveGamePlayer[451]);
                        quests[1, 1] = Convert.ToInt32(SaveGamePlayer[452]);
                        quests[1, 2] = Convert.ToInt32(SaveGamePlayer[454]);
                        quests[1, 3] = Convert.ToInt32(SaveGamePlayer[455]);
                        quests[1, 4] = Convert.ToInt32(SaveGamePlayer[453]);
                    }
                    if (i == 2) {
                        quests[2, 0] = Convert.ToInt32(SaveGamePlayer[471]);
                        quests[2, 1] = Convert.ToInt32(SaveGamePlayer[472]);
                        quests[2, 2] = Convert.ToInt32(SaveGamePlayer[474]);
                        quests[2, 3] = Convert.ToInt32(SaveGamePlayer[475]);
                        quests[2, 4] = Convert.ToInt32(SaveGamePlayer[473]);
                    }

                    if (Convert.ToInt32(SaveGamePlayer[716]) == 1) {
                        quests[i, 0] /= 2;
                    }
                }

                Form1.AddLog("Verfügbare Quests:", false);
                if (EPM) {
                    Form1.AddLog(
                        "    (1) " + "Zeit: " + TimeSpan.FromSeconds(quests[0, 0]) + " - Gold: " + quests[0, 1] +
                        " - Exp: " + quests[0, 2] + " - Item: " + quests[0, 3] + " - EPS: " +
                        Math.Round(quests[0, 2]/(float) quests[0, 0], 2) + " - Blutsteine: " + quests[0, 4], false);
                    Form1.AddLog(
                        "    (2) " + "Zeit: " + TimeSpan.FromSeconds(quests[1, 0]) + " - Gold: " + quests[1, 1] +
                        " - Exp: " + quests[1, 2] + " - Item: " + quests[1, 3] + " - EPS: " +
                        Math.Round(quests[1, 2]/(float) quests[1, 0], 2) + " - Blutsteine: " + quests[1, 4], false);
                    Form1.AddLog(
                        "    (3) " + "Zeit: " + TimeSpan.FromSeconds(quests[2, 0]) + " - Gold: " + quests[2, 1] +
                        " - Exp: " + quests[2, 2] + " - Item: " + quests[2, 3] + " - EPS: " +
                        Math.Round(quests[2, 2]/(float) quests[2, 0], 2) + " - Blutsteine: " + quests[2, 4], false);
                } else if (GPM) {
                    Form1.AddLog(
                        "    (1) " + "Zeit: " + TimeSpan.FromSeconds(quests[0, 0]) + " - Gold: " + quests[0, 1] +
                        " - Exp: " + quests[0, 2] + " - Item: " + quests[0, 3] + " - GPS: " +
                        Math.Round(quests[0, 1]/(float) quests[0, 0], 2) + " - Blutsteine: " + quests[0, 4], false);
                    Form1.AddLog(
                        "    (2) " + "Zeit: " + TimeSpan.FromSeconds(quests[1, 0]) + " - Gold: " + quests[1, 1] +
                        " - Exp: " + quests[1, 2] + " - Item: " + quests[1, 3] + " - GPS: " +
                        Math.Round(quests[1, 1]/(float) quests[1, 0], 2) + " - Blutsteine: " + quests[1, 4], false);
                    Form1.AddLog(
                        "    (3) " + "Zeit: " + TimeSpan.FromSeconds(quests[2, 0]) + " - Gold: " + quests[2, 1] +
                        " - Exp: " + quests[2, 2] + " - Item: " + quests[2, 3] + " - GPS: " +
                        Math.Round(quests[2, 1]/(float) quests[2, 0], 2) + " - Blutsteine: " + quests[2, 4], false);
                } else if (KZ || ITEM) {
                    Form1.AddLog(
                        "    (1) " + "Zeit: " + TimeSpan.FromSeconds(quests[0, 0]) + " - Gold: " + quests[0, 1] +
                        " - Exp: " + quests[0, 2] + " - Item: " + quests[0, 3] + " - Blutsteine: " + quests[0, 4], false);
                    Form1.AddLog(
                        "    (2) " + "Zeit: " + TimeSpan.FromSeconds(quests[1, 0]) + " - Gold: " + quests[1, 1] +
                        " - Exp: " + quests[1, 2] + " - Item: " + quests[1, 3] + " - Blutsteine: " + quests[1, 4], false);
                    Form1.AddLog(
                        "    (3) " + "Zeit: " + TimeSpan.FromSeconds(quests[2, 0]) + " - Gold: " + quests[2, 1] +
                        " - Exp: " + quests[2, 2] + " - Item: " + quests[2, 3] + " - Blutsteine: " + quests[2, 4], false);
                }
            }
        }

        public bool PickQuest() {
            if (inQuest == false && inWork == false) {
                int temp = 0;
                for (int i = 0; i < 3; i++) {
                    float eps = quests[i, 2]/(float) quests[i, 0];
                    float epstemp = quests[temp, 2]/(float) quests[temp, 0];
                    float gps = quests[i, 1]/(float) quests[i, 0];
                    float gpstemp = quests[temp, 1]/(float) quests[temp, 0];

                    if (eps > epstemp && EPM) {
                        temp = i;
                    } else if (gps > gpstemp && GPM) {
                        temp = i;
                    } else if (quests[i, 0] < quests[temp, 0] && KZ) {
                        temp = i;
                    } else if (quests[i, 3] > 0 && ITEM) {
                        temp = i;
                    }
                    if (quests[i, 4] > 0) {
                        temp = i;
                        break;
                    }
                }
                pickedQuest = temp;

                if (((float) quests[temp, 0]/60) < Ausdauer) {
                    Form1.AddLog("Wähle Quest (" + (temp + 1) + ")...", false);

                    Form1.RequestNr++;

                    string antwort =
                        Form1.Wc.DownloadString("http://" + Form1.ServerName + "/request.php?req=" + Form1.SessionId +
                                                ";" + Form1.RequestNr + ";QuestAccept%3A" + (temp + 1) + "&rnd=" +
                                                Form1.Rnd.Next(10000000, 99999999));
                    LastRequest = antwort.Split('&');

                    Form1.MissionTimer.Interval = ((quests[temp, 0]*1000));
                    Form1.BarMissionProgress.Maximum = (quests[temp, 0]);
                    Form1.Parse();

                    UpdateSaveGame();
                    return true;
                }
                return false;
            }
            return false;
        }

        public void UpdateSaveGame() {
            try {
                foreach (string t in LastRequest.Where(t => t.Split(':')[0] == "SaveGamePlayer")) {
                    SaveGamePlayer = t.Split('/');
                }

                UpdateStats();
            } catch (Exception) {
                MessageBox.Show("Beim Auswerten des Savegames gab es einen Fehler. (Fehlercode 02)", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void WorkStart(int zeit) {
            if (inQuest == false && inWork == false) {
                Form1.Status.Text = "Arbeite";
                Form1.RequestNr++;
                string antwort =
                    Form1.Wc.DownloadString("http://" + Form1.ServerName + "/request.php?req=" + Form1.SessionId + ";" +
                                            Form1.RequestNr + ";WorkStart%3A" + zeit + "&rnd=" +
                                            Form1.Rnd.Next(10000000, 99999999));
                LastRequest = antwort.Split('&');
                Form1.WorkTimer.Interval = ((zeit*60*60)*1000);
                Form1.BarMissionProgress.Maximum = (zeit*60*60);
                Form1.Parse();
            }
        }
    }

    public class WebClientTimeOut : WebClient {
        protected override WebRequest GetWebRequest(Uri address) {
            WebRequest webRequest = base.GetWebRequest(address);
            if (webRequest != null) {
                webRequest.Timeout = 10;
                return webRequest;
            }
            return null;
        }
    }
}