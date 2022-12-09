using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;

namespace GametypePackager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string our_appdata_folder_name = "bin_mglo_convertor";
        public MainWindow()
        {
            InitializeComponent();
            // then find the prefs stuff
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = System.IO.Path.Combine(folder, our_appdata_folder_name);
            if (!Directory.Exists(specificFolder))
                Directory.CreateDirectory(specificFolder);

            string data_file = System.IO.Path.Combine(specificFolder, "prefs.txt");
            if (File.Exists(data_file))
            {
                string[] frogos = File.ReadAllLines(data_file);
                if (frogos.Length >= 1)
                {
                    binbox.Text = frogos[0];
                    if (frogos.Length >= 2)
                    {
                        mglobox.Text = frogos[1];
                        if (frogos.Length >= 3)
                            mglofolderbox.Text = frogos[2];
                    }
                }
            }
            is_intializing = false; // so we dont have unneeded write calls to the text file
        }
        bool is_intializing = true;
        void update_saved_files()
        {
            if (is_intializing) return;

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = System.IO.Path.Combine(folder, our_appdata_folder_name);
            if (!Directory.Exists(specificFolder))
                Directory.CreateDirectory(specificFolder);

            string data_file = System.IO.Path.Combine(specificFolder, "prefs.txt");
            File.WriteAllLines(data_file, new string[3] { binbox.Text, mglobox.Text, mglofolderbox.Text } );
        }
        
        static int reach_length = 20480;
        static int H4_2A_length = 31744;   

        static byte[] header = { 0x5F, 0x62, 0x6C, 0x66, 0x00, 0x00, 0x00, 0x30, 0x00, 0x01, 0x00, 0x02, 0xFF, 0xFE, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x63, 0x68, 0x64, 0x72, 0x00, 0x00, 0x02, 0xC0, 0x00, 0x0A, 0x00, 0x02, 0xFF, 0xFF, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x29, 0x53, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x03, 0x02, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x6D, 0x70, 0x76, 0x72, 0x00, 0x00, 0x50, 0x28, 0x00, 0x36, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 };
        static byte[] ender = { 0x5F, 0x65, 0x6F, 0x66, 0x00, 0x00, 0x00, 0x11, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x53, 0x18, 0x00 };


        private void Unpack_bin_to(object sender, RoutedEventArgs e)
        {
            int bin_length = reach_length;
            if (bombobox.SelectedIndex == 1) // then its H4/2A
                bin_length = H4_2A_length;

            if (File.Exists(binbox.Text))
            {
                byte[] binbytes = File.ReadAllBytes(binbox.Text);
                if (binbytes != null)
                {
                    if (binbytes.Length > bin_length + header.Length) // make sure the file is long enough
                    {
                        // strip the first 793 bytes
                        // strip the last 17 bytes
                        // incorporate hashing to find the start & end of the file
                        // left shift everything 4 bits

                        //bool skipped_something = binbytes[0] != 0;

                        // pull the bytes from the file
                        byte[] sus = new byte[bin_length - 1];
                        for (int i = 0; i < sus.Length; i++)
                            sus[i] = binbytes[i + header.Length];
                        // check to at least warn if we're squishing a bit at the end there, unlikely though
                        bool skipped_something = sus[sus.Length - 1] != 0;

                        // bitshift all of the bytes, so they're nice and unaligned
                        BA_rightshift(ref sus, sus.Length);

                        if (string.IsNullOrEmpty(mglobox.Text)) // then write their directory based off the  binbox
                            mglobox.Text = System.IO.Path.GetDirectoryName(binbox.Text) + "\\" + System.IO.Path.GetFileNameWithoutExtension(binbox.Text) + ".mglo";
                        if (Directory.Exists(System.IO.Path.GetDirectoryName(mglobox.Text)))
                        {
                            try
                            {
                                if (skipped_something)
                                    System.Windows.MessageBox.Show("whoops, we completely ignored the data in the last byte of the .mglo, we probably didnt need that anyway");
                                if (File.Exists(mglobox.Text))
                                    File.WriteAllBytes(mglobox.Text, sus);
                                else // create a new file to deposit the bytes
                                    File.WriteAllBytes(mglobox.Text, sus);
                                outputbox.Text = "Successfully converted the .bin to the .mglo";
                                hide_message_in_1_seocnds();
                            }
                            catch (Exception ex)
                            {
                                outputbox.Text = ex.Message;
                            }
                        }
                        else
                            System.Windows.MessageBox.Show(mglobox.Text + " is not a valid outpath");
                    }
                    else
                        System.Windows.MessageBox.Show(binbytes.Length + " is too few bytes, its probably not actually a bin file");
                }
                else
                    System.Windows.MessageBox.Show(binbox.Text + " is an empty/invalid file");
            }
            else
                System.Windows.MessageBox.Show(binbox.Text + " is an invalid path");
        }


        private void Pack_mglos_to(object sender, RoutedEventArgs e)
        {
            int mglo_length = reach_length;
            if (bombobox.SelectedIndex == 1) // then its H4/2A
                mglo_length = H4_2A_length;

            if (Directory.Exists(mglofolderbox.Text))
            {
                foreach (var file in Directory.GetFiles(mglofolderbox.Text))
                {
                    var ext = System.IO.Path.GetExtension(file);
                    if (ext == ".mglo") // then this is probably the right file to convert
                    {
                        sausage(mglo_length, file, file+".bin");
                    }// else this is not a file
                }
            }
            else
                System.Windows.MessageBox.Show(mglofolderbox.Text + " is an invalid folder");
        }
        private void Pack_mglo_to(object sender, RoutedEventArgs e)
        {
            int mglo_length = reach_length;
            if (bombobox.SelectedIndex == 1) // then its H4/2A
                mglo_length = H4_2A_length;

            sausage(mglo_length, mglobox.Text, binbox.Text);
        }
        private void sausage(int mglo_length, string directory, string outputdir)
        {
            if (File.Exists(directory))
            {
                byte[] mglobytes = File.ReadAllBytes(directory);
                if (mglobytes != null)
                {
                    if (mglobytes.Length < mglo_length) // make sure the file is long enough
                    {
                        // pad with 793 bytes: 
                        // right shift everything 4 bits
                        // expand byte array to be 20480 bytes
                        // end pad with 17 bytes: 5F 65 6F 66 00 00 00 11 00 01 00 01 00 00 53 18 00
                        int og_length = mglobytes.Length;
                        Array.Resize(ref mglobytes, mglo_length);
                        BA_rightshift(ref mglobytes, og_length);

                        bool skipped_something = mglobytes[0] != 0;


                        byte[] sus = new byte[header.Length + ender.Length + mglo_length];
                        for (int i = 0; i < header.Length; i++)
                            sus[i] = header[i];
                        int insertion_index = header.Length;
                        for (int i = 1; i < mglobytes.Length; i++)
                            sus[(i - 1) + insertion_index] = mglobytes[i];
                        insertion_index += mglobytes.Length - 1;
                        for (int i = 0; i < ender.Length; i++)
                            sus[i + insertion_index] = ender[i];

                        if (string.IsNullOrEmpty(outputdir)) // then write their directory based off the megalobox
                            outputdir = System.IO.Path.GetDirectoryName(directory) + "\\" + System.IO.Path.GetFileNameWithoutExtension(directory) + ".bin";
                        if (Directory.Exists(System.IO.Path.GetDirectoryName(outputdir)))
                        {
                            try
                            {

                                if (skipped_something)
                                    System.Windows.MessageBox.Show("whoops, we completely ignored the data in the first byte of the .mglo, we probably didnt need that anyway");
                                if (File.Exists(outputdir))
                                {
                                    if (System.Windows.MessageBox.Show("NOTE:\r\nYou are about to overwrite '" + System.IO.Path.GetFileName(outputdir) + "'.\r\nContinue?", "Overwrite .bin?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                                        File.WriteAllBytes(outputdir, sus);
                                }
                                else // create a new file to deposit the bytes
                                    File.WriteAllBytes(outputdir, sus);
                                outputbox.Text = "Successfully converted the .mglo to the .bin";
                                hide_message_in_1_seocnds();
                            }
                            catch (Exception ex)
                            {
                                outputbox.Text = ex.Message;
                            }
                        }
                        else
                            System.Windows.MessageBox.Show(outputdir + " is not a valid outpath");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(mglobytes.Length + " is too many bytes, its probably not actually a mglo file");
                    }

                }
                else
                    System.Windows.MessageBox.Show(mglobox.Text + " is an empty/invalid file");
            }
            else
                System.Windows.MessageBox.Show(mglobox.Text + " is an invalid path");
        }

        void BA_rightshift(ref byte[] array, int earlystop)
        {
            // we need to resize this array to have one empty slot at the end
            //Array.Resize(ref array, array.Length + 1);

            byte prev_upper_bits = 0;
            for (int i = 0; i<earlystop; i++)
            {
                byte curr_lower_bits = (byte)(array[i] << 4); // record the 4 values that are about to be squished
                array[i] = (byte)(array[i] >> 4); // do the rightshift
                array[i] |= prev_upper_bits; // could also be addition, but this is cooler
                prev_upper_bits = curr_lower_bits; // record the bits that were squished, to be moved to the next one
            }
        }

        async void hide_message_in_1_seocnds()
        {
            outputbox.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            outputbox.Visibility = Visibility.Hidden;
        }

        // file explorer selection
        string Save_file_dialog(string default_ext, string filter, string desc)
        {
            Microsoft.Win32.SaveFileDialog theoretical_file_dialog = new Microsoft.Win32.SaveFileDialog();
            theoretical_file_dialog.Filter = filter;
            theoretical_file_dialog.Title = desc;
            theoretical_file_dialog.OverwritePrompt = false;
            theoretical_file_dialog.DefaultExt = default_ext; // Default file extension

            // Show save file dialog box
            Nullable<bool> result = theoretical_file_dialog.ShowDialog();
            // Process save file dialog box results
            if (result == true)
                return theoretical_file_dialog.FileName;
            return "";
        }
        string Select_folder_dialog()
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result.ToString() != string.Empty)
                return folderDlg.SelectedPath;
            return "";
        }

        private void select_mglofolder(object sender, RoutedEventArgs e)
        {
            mglofolderbox.Text = Select_folder_dialog();
        }
        private void select_mglo(object sender, RoutedEventArgs e)
        {
            mglobox.Text = Save_file_dialog(".mglo", "Megalo file|*.mglo", "Select the .mglo input/output");
        }
        private void select_bin(object sender, RoutedEventArgs e)
        {
            binbox.Text = Save_file_dialog(".bin", "Binary file|*.bin", "Select the .bin input/output");
        }

        private void mglofolderbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            update_saved_files();
        }
        private void mglobox_TextChanged(object sender, TextChangedEventArgs e)
        {
            update_saved_files();
        }
        private void binbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            update_saved_files();
        }
    }
}
