
using System;
using FlaUI.Core;
using FlaUI.UIA3;
using System.Diagnostics;
using FlaUI.Core.AutomationElements;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Startet Notepad
        var process = Process.Start(@"D:\temp\PrimaStart\SRVAGISAP02_TEST\sprenger-test-prima\Prima6.exe");

        // UIA3-Automatisierung verwenden
        using (var automation = new UIA3Automation())
        {

            //var app = Application.Attach(process);

            var app = Application.Attach(process);

            var loginDialog = app.GetMainWindow(automation);

            foreach (var element in loginDialog.FindAllDescendants())
            {
                Console.WriteLine($"Element: {element.Name}, Typ: {element.ControlType}");
            }

            var button = loginDialog.FindFirstDescendant(cf => cf.ByName("OK")).AsButton();
            button.Click();
            Thread.Sleep(5000);

            // Projektübersicht öffnen
            var mainWindow = app.GetMainWindow(automation);
            var prj = mainWindow.FindFirstDescendant(cf => cf.ByName("Projekte")).AsButton();
            prj.Click();
            Thread.Sleep(2000);

            var fk = mainWindow.FindFirstDescendant(cf => cf.ByName("Abbrechen")).AsButton();
            fk.Click();
            Thread.Sleep(2000);




            for (int i = 0; i < 10; i++)
            {
                var nP = mainWindow.FindFirstDescendant(cf => cf.ByName("m_menuItem_Neu")).AsButton();
                nP.Click();

                Thread.Sleep(2000);
                var projektDialog = app.GetMainWindow(automation);
                Thread.Sleep(2000);
                var prjNum = projektDialog.FindFirstDescendant(cf => cf.ByName("Projektnummer"));
                FlaUI.Core.Input.Keyboard.Type("HBE00?");

                var prjBez = projektDialog.FindFirstDescendant(cf => cf.ByName("Bezeichnung")).AsTextBox();
                prjBez.Enter("Testlauf Autuomat00" + i);

                var kunde = projektDialog.FindFirstDescendant(cf => cf.ByName("Kunde wählen/ändern")).AsButton();
                kunde.Click();

                var nKundeDialog = app.GetMainWindow(automation);

                var nA = nKundeDialog.FindFirstDescendant(cf => cf.ByName("Adresse suchen")).AsTextBox();
                nA.Enter("Agis");
                Thread.Sleep(2000);
                var nAenter = projektDialog.FindFirstDescendant(cf => cf.ByName("Übernehmen")).AsButton();
                nAenter.Click();

                var ok = projektDialog.FindFirstDescendant(cf => cf.ByName("OK")).AsButton();
                ok.Click();
            }




            //  Console.WriteLine("-------------------------------------------------------------------");
            // foreach (var element in nKundeDialog.FindAllDescendants())
            // {
            //     Console.WriteLine($"Element: {element.Name}, Typ: {element.ControlType}");
            // }

        }
    }
}
