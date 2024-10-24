
using System;
using FlaUI.Core;
using FlaUI.UIA3;
using System.Diagnostics;
using FlaUI.Core.AutomationElements;
using System.Threading;
using System.Reflection.PortableExecutable;
using FlaUI.Core.Definitions;

class Program
{
    static void Main(string[] args)
    {
        // Startet Notepad
        var process = Process.Start(@"D:\temp\PrimaStart\SRVAGISAP02_TEST\sprenger-test-prima\Prima6.exe");

        // UIA3-Automatisierung verwenden
        using (var automation = new UIA3Automation())
        {

            var app = Application.Attach(process);

            var loginDialog = app.GetMainWindow(automation);

         
            var button = loginDialog.FindFirstDescendant(cf => cf.ByName("OK")).AsButton();
            button.Click();
            Thread.Sleep(4000);

            // Projektübersicht öffnen
            var mainWindow = app.GetMainWindow(automation);
            var prj = mainWindow.FindFirstDescendant(cf => cf.ByName("Projekte")).AsButton();
            prj.Click();
            Thread.Sleep(2500);

            var fk = mainWindow.FindFirstDescendant(cf => cf.ByName("Abbrechen")).AsButton();
            fk.Click();
            Thread.Sleep(2500);


            // Projekt erfassen-------------------------------------------------------------------
            var nP = mainWindow.FindFirstDescendant(cf => cf.ByName("m_menuItem_Neu")).AsButton();
            nP.Click();

            Thread.Sleep(2500);
            var projektDialog = app.GetMainWindow(automation);
            Thread.Sleep(2500);
            var prjNum = projektDialog.FindFirstDescendant(cf => cf.ByName("Projektnummer"));
            FlaUI.Core.Input.Keyboard.Type("HBE???");

            var prjBez = projektDialog.FindFirstDescendant(cf => cf.ByName("Bezeichnung")).AsTextBox();
            prjBez.Enter("Testlauf Autuomat001");

            var kunde = projektDialog.FindFirstDescendant(cf => cf.ByName("Kunde wählen/ändern")).AsButton();
            kunde.Click();

            var nKundeDialog = app.GetMainWindow(automation);

            var nA = nKundeDialog.FindFirstDescendant(cf => cf.ByName("Adresse suchen")).AsTextBox();
            nA.Enter("Agis");
            Thread.Sleep(2500);

            var nAenter = projektDialog.FindFirstDescendant(cf => cf.ByName("Übernehmen")).AsButton();
            nAenter.Click();

            var ok = projektDialog.FindFirstDescendant(cf => cf.ByName("OK")).AsButton();
            ok.Click();
            Console.WriteLine("Projekt wurde erfasst");

            // Projekt kopieren und löschen--------------------------------------------------------------------------
            var projektListe = app.GetMainWindow(automation);
            var kopProjekt = projektListe.FindFirstDescendant(cf => cf.ByName("m_menuItem_Kopieren")).AsButton();
            kopProjekt.Click();
            prjNum = projektDialog.FindFirstDescendant(cf => cf.ByName("Projektnummer"));
            FlaUI.Core.Input.Keyboard.Type("HBE???");
            Thread.Sleep(2500);

            projektDialog = app.GetMainWindow(automation);
            kopProjekt = projektDialog.FindFirstDescendant(cf => cf.ByName("OK")).AsButton();
            kopProjekt.Click();
            Thread.Sleep(2500);

            projektListe = app.GetMainWindow(automation);
            var aenProjekt = projektListe.FindFirstDescendant(cf => cf.ByName("m_menuItem_AendernDD")).AsButton();
            aenProjekt.Click();

            projektDialog = app.GetMainWindow(automation);
            var loeProjekt = projektDialog.FindFirstDescendant(cf => cf.ByName("Löschen")).AsButton();
            loeProjekt.Click();

            var abfrage = app.GetMainWindow(automation);
            loeProjekt = abfrage.FindFirstDescendant(cf => cf.ByName("Ja")).AsButton();
            loeProjekt.Click();

            // Neuer Mitarbeiter erfassen, kopieren und löschen
            var marListe = app.GetMainWindow(automation);
            var mar = marListe.FindFirstDescendant(cf => cf.ByName("Mitarbeiter")).AsButton();
            mar.Click();

            mar = marListe.FindFirstDescendant(cf => cf.ByName("m_menuItem_Neu")).AsButton();
            mar.Click();

            var marDialog = app.GetMainWindow(automation);
            var kzz = marDialog.FindFirstDescendant(cf => cf.ByName("Kurzzeichen")).AsTextBox();
            FlaUI.Core.Input.Keyboard.Type("MAX");
                  
            var name = marDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByName("Name")));
            name.Focus();
            FlaUI.Core.Input.Keyboard.Type("Markus");

            var vorname = marDialog.FindFirstDescendant(cf => cf.ByName("Vorname").And(cf.ByControlType(ControlType.Edit)));
            vorname.Focus();
            FlaUI.Core.Input.Keyboard.Type("Muster");
            
            var titel = marDialog.FindFirstDescendant(cf => cf.ByName("Titel").And(cf.ByControlType(ControlType.Edit)));
            titel.Focus();
            FlaUI.Core.Input.Keyboard.Type("Doktor");

            mar = marDialog.FindFirstDescendant(cf => cf.ByName("OK")).AsButton();
            mar.Click();


            //  Console.WriteLine("-------------------------------------------------------------------");
            // foreach (var element in marDialog.FindAllDescendants())
            // {
            //     Console.WriteLine($"Element: {element.Name}, Typ: {element.ControlType}");
            // }

        }
    }
}
