using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace ErganhE7
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!EnsureUserSettingsAreHealthy())
                return;

            Application.Run(new Form1());
        }

        private static bool EnsureUserSettingsAreHealthy()
        {
            try
            {
                // Δοκιμαστική πρόσβαση σε ένα user setting
                var test = Properties.Settings.Default.ypiresiaSepe;
                return true;
            }
            catch (ConfigurationErrorsException ex)
            {
                string badConfigFile = FindBadConfigFile(ex);

                try
                {
                    if (!string.IsNullOrWhiteSpace(badConfigFile) && File.Exists(badConfigFile))
                    {
                        File.Delete(badConfigFile);
                    }
                    else
                    {
                        // fallback: σβήσε όλο το settings folder της εφαρμογής
                        DeleteUserSettingsFolder();
                    }
                }
                catch (Exception deleteEx)
                {
                    MessageBox.Show(
                        "Οι αποθηκευμένες ρυθμίσεις της εφαρμογής είναι κατεστραμμένες, " +
                        "αλλά δεν ήταν δυνατό να διαγραφούν αυτόματα.\r\n\r\n" +
                        deleteEx.Message,
                        "Σφάλμα ρυθμίσεων",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return false;
                }

                MessageBox.Show(
                    "Βρέθηκε κατεστραμμένο αρχείο ρυθμίσεων χρήστη και έγινε επαναφορά.\r\n" +
                    "Η εφαρμογή θα επανεκκινήσει.",
                    "Επαναφορά ρυθμίσεων",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Application.Restart();
                return false;
            }
        }

        private static string FindBadConfigFile(Exception ex)
        {
            Exception current = ex;

            while (current != null)
            {
                if (current is ConfigurationErrorsException cex &&
                    !string.IsNullOrWhiteSpace(cex.Filename))
                {
                    return cex.Filename;
                }

                current = current.InnerException;
            }

            return null;
        }

        private static void DeleteUserSettingsFolder()
        {
            string companyName = Application.CompanyName;
            string productName = Application.ProductName;

            string basePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                companyName);

            if (!Directory.Exists(basePath))
                return;

            // Συνήθως υπάρχουν φάκελοι τύπου:
            // LocalAppData\Company\Product.exe_Url_xxx\1.0.0.0\
            foreach (string dir in Directory.GetDirectories(basePath, productName + "*", SearchOption.AllDirectories))
            {
                try
                {
                    Directory.Delete(dir, true);
                }
                catch
                {
                    // συνεχίζουμε στους υπόλοιπους
                }
            }
        }
    }
}