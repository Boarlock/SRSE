using System.Diagnostics;
using System.Text;

namespace SinsEntityConverter
{
    public partial class MainForm : Form
    {
        private readonly Color nearBlackBG = Color.FromArgb(18, 18, 18);
        private readonly Color lightBlackBG = Color.FromArgb(30, 30, 30);
        private readonly Color nearWhiteFG = Color.FromArgb(224, 224, 224);
        private readonly Color amberFG = Color.FromArgb(255, 191, 0);
        private readonly Color blueFG = Color.FromArgb(96, 125, 139);

        Color buttonBack = Color.FromArgb(0, 122, 204);
        Color buttonBorder = Color.FromArgb(85, 85, 85);

        private readonly DataRepresentations data = new();
        private Process? conversionProcess;
        private string? activeDir;
        private bool loaded;
        private bool isXSITab;
        private bool isJSONTab;
        private readonly char[] invalidChars = Path.GetInvalidPathChars();

        public static readonly string appDir;
        
        public MainForm()
        {
            InitializeComponent();

            BackColor = nearBlackBG;
            MaximumSize = Size;
            MinimumSize = Size;

            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = buttonBack;
                    button.ForeColor = nearWhiteFG;
                    button.FlatAppearance.BorderSize = 0;
                    button.FlatAppearance.BorderColor = buttonBorder;
                }
            }

            outputBox.BackColor = lightBlackBG;
            outputBox.ForeColor = nearWhiteFG;
            outputBox.BorderStyle = BorderStyle.None;
            outputBox.TabStop = false;

            panelBorder.BackColor = buttonBack;
        }

        static MainForm()
        {
            appDir = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath)!;
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conversionProcess != null && !conversionProcess.HasExited)
            {
                conversionProcess.Kill();
                conversionProcess.Dispose();
            }
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            loaded = false;

            if (isXSITab)
            {
                xsiSelectFile_Click(sender, e);
                return;
            }

            if (isJSONTab)
            {
                jsonSelectFile_Click(sender, e);
                return;
            }

            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter =
                    "All Supported Files (*.brushes;*.entity;*.mesh;*.particle)|*.brushes;*.entity;*.mesh;*.particle|" +
                    "Brushes (*.brushes)|*.brushes|" +
                    "Entities (*.entity)|*.entity|" +
                    "Meshes (*.mesh)|*.mesh|" +
                    "Particles (*.particle)|*.particle|" +
                    "All Files (*.*)|*.*";

                dialog.InitialDirectory = (activeDir == "") ? appDir : activeDir;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                activeDir = Path.GetDirectoryName(dialog.FileName)!;

                data.curFiles = new List<SinsFile>();
                data.curFiles.Add(new SinsFile()
                {
                    Path = dialog.FileName,
                    OperationCode = OperationCode.Pending
                });
            }

            FileLoader();
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            loaded = false;

            if (isJSONTab)
            {
                jsonSelectFiles_Click(sender, e);
                return;
            }

            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter =
                    "All Supported Files (*.brushes;*.entity;*.mesh;*.particle)|*.brushes;*.entity;*.mesh;*.particle|" +
                    "Brushes (*.brushes)|*.brushes|" +
                    "Entities (*.entity)|*.entity|" +
                    "Meshes (*.mesh)|*.mesh|" +
                    "Particles (*.particle)|*.particle|" +
                    "All Files (*.*)|*.*";

                dialog.InitialDirectory = (activeDir == "") ? appDir : activeDir;
                dialog.Multiselect = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                if (dialog.FileNames.Length > 0)
                    activeDir = Path.GetDirectoryName(dialog.FileNames[0]);

                data.curFiles = new List<SinsFile>();

                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    data.curFiles.Add(new SinsFile()
                    {
                        Path = dialog.FileNames[i],
                        OperationCode = OperationCode.Pending
                    });
                }
            }

            FileLoader();
        }

        private void btnSelectDir_Click(object sender, EventArgs e)
        {
            loaded = false;

            if (isJSONTab)
            {
                jsonSelectDir_Click(sender, e);
                return;
            }

            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = (activeDir == "") ? appDir : activeDir;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                activeDir = dialog.SelectedPath;

                data.curFiles = new List<SinsFile>();

                foreach (string path in Directory.EnumerateFiles(dialog.SelectedPath))
                {
                    data.curFiles.Add(new SinsFile()
                    {
                        Path = path,
                        OperationCode = OperationCode.Pending
                    });
                }
            }

            FileLoader();
        }

        private void FileLoader()
        {
            data.loadedFiles = new List<SinsFile>();
            Render(RenderCode.LoadingStart);

            for (int i = 0; i < data.curFiles!.Count; i++)
            {
                data.curFile = new SinsFile()
                {
                    Path = data.curFiles[i].Path,
                    OperationCode = OperationCode.Pending
                };

                PathChecker();

                if (data.curFile.OperationCode != OperationCode.Pending)
                {
                    Render(RenderCode.LoadFailed, data.curFile);
                    continue;
                }

                if (!isXSITab)
                {
                    data.curFile.Conversion = BinTxtIdentifier();

                    if (data.curFile.Conversion == "")
                    {
                        data.curFile.OperationCode = OperationCode.InvalidContent;
                        Render(RenderCode.LoadFailed, data.curFile);
                        continue;
                    }
                }

                data.loadedFiles.Add(data.curFile);
                Render(RenderCode.LoadSuccess, data.curFile);
            }

            Render(RenderCode.LoadingComplete);

            if (data.loadedFiles.Count <= 0)
                return;

            loaded = true;
        }

        private void PathChecker()
        {
            if (!System.IO.File.Exists(data.curFile!.Path))
            {
                data.curFile.OperationCode = OperationCode.FileNotFound;
                return;
            }
            if (new FileInfo(data.curFile.Path).Length == 0)
            {
                data.curFile.OperationCode = OperationCode.InvalidPath;
                return;
            }

            string fullFileName = Path.GetFileName(data.curFile.Path);
            int dotIndex = fullFileName.LastIndexOf('.');

            if (dotIndex <= 0)
            {
                data.curFile.OperationCode = OperationCode.InvalidExtension;
                return;
            }

            data.curFile.FileName = fullFileName.Substring(0, dotIndex);
            data.curFile.Ext = fullFileName.Substring(dotIndex + 1);
            string ext = data.curFile.Ext;

            if (!isXSITab &&
                !isJSONTab)
            {
                if (ext != "brushes" &&
                    ext != "entity" &&
                    ext != "mesh" &&
                    ext != "MESH" &&
                    ext != "particle")
                {
                    data.curFile.OperationCode = OperationCode.InvalidExtension;
                    return;
                }
            }
            else if (isJSONTab)
            {
                if (ext != "entity")
                {
                    data.curFile.OperationCode = OperationCode.InvalidExtension;
                    return;
                }
            }
            else
            {
                if (ext != "xsi")
                {
                    data.curFile.OperationCode = OperationCode.InvalidExtension;
                    return;
                }
            }

            foreach (Char c in invalidChars)
            {
                if (data.curFile.Path.Contains<char>(c))
                {
                    data.curFile.OperationCode = OperationCode.InvalidPath;
                    return;
                }
            }

            return;
        }

        private string BinTxtIdentifier(string path = "")
        {
            string targetPath = path == "" ? data.curFile!.Path! : path!;

            byte[] buffer = new byte[4];

            try
            {
                using (FileStream fs = new FileStream(
                    targetPath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read))
                {
                    int bytesRead = fs.Read(buffer, 0, 4);

                    if (bytesRead < 4)
                        return "";
                }

                string header = Encoding.ASCII.GetString(buffer);

                if (header == "BIN2")
                    return "txt";

                // If it isn't BIN2, check the text file's beginning.
                using (StreamReader reader = new StreamReader(targetPath))
                {
                    string firstLine = reader.ReadLine()!;

                    if (firstLine == "TXT2")
                        return "bin";
                }
            }
            catch (IOException)
            {
                MessageBox.Show(
                    $"Error while parsing target file {targetPath}",
                    "Sins Converter",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                if (data.curFile != null && targetPath == data.curFile.Path)
                    data.curFile.OperationCode = OperationCode.InvalidContent;
            }

            return "";
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (!loaded || data.loadedFiles!.Count <= 0)
                return;

            Render(RenderCode.ConversionStarted);

            data.curFile = null;
            loaded = false;

            data.curFiles = new(data.loadedFiles);

            
            string tempDir = Path.Combine(appDir, "temp");
            Directory.CreateDirectory(tempDir);

            if (!isJSONTab)
            {
                var processInfo = new ProcessStartInfo();

                if (!isXSITab)
                    processInfo.FileName = Path.Combine(appDir, "ConvertData_Rebellion.exe");
                else
                    processInfo.FileName = Path.Combine(appDir, "ConvertXSI.exe");

                processInfo.UseShellExecute = false;
                processInfo.CreateNoWindow = true;
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processInfo.WorkingDirectory = tempDir;

                string outputText = "";
                string outputBin = "";
                string outputMesh = "";

                if (!isXSITab)
                {
                    outputText = Path.Combine(appDir, "Output Text");

                    if (!Directory.Exists(outputText))
                        Directory.CreateDirectory(outputText);

                    outputBin = Path.Combine(appDir, "Output Bin");

                    if (!Directory.Exists(outputBin))
                        Directory.CreateDirectory(outputBin);
                }
                else
                {
                    outputMesh = Path.Combine(appDir, "Output Mesh");

                    if (!Directory.Exists(outputMesh))
                        Directory.CreateDirectory(outputMesh);
                }

                foreach (SinsFile file in data.loadedFiles)
                {
                    string outputDir = "";
                    string tempFilePath = Path.Combine(tempDir, Path.GetFileName(file.Path)!);

                    File.Copy(file.Path!, tempFilePath, true);

                    if (file.Conversion == null)
                        outputDir = outputMesh;
                    else if (file.Conversion == "txt")
                        outputDir = outputText;
                    else if (file.Conversion == "bin")
                        outputDir = outputBin;

                    if (!isXSITab)
                    {
                        processInfo.Arguments = $"{file.Ext} " +
                        $"{file.FileName}.{file.Ext} " +
                        $"{file.FileName}.{file.Ext} " +
                        $"{file.Conversion}";
                    }
                    else
                    {
                        processInfo.Arguments = $"{file.FileName}.{file.Ext} {file.FileName}.mesh";
                    }

                    conversionProcess = Process.Start(processInfo)!;

                    if (!conversionProcess!.WaitForExit(30000))
                    {
                        conversionProcess.Kill();
                        conversionProcess.Dispose();
                        file.OperationCode = OperationCode.ConversionTimeout;
                        Render(RenderCode.ConversionFailed, file);
                        data.curFiles.Remove(file);
                        continue;
                    }

                    string ext;
                    string newFilePath;

                    if (!isXSITab)
                    {
                        ext = "." + file.Ext;
                        newFilePath = Path.Combine(outputDir, file.FileName + ext);
                    }
                    else
                    {
                        ext = ".mesh";
                        tempFilePath = Path.Combine(tempDir, file.FileName + ext);
                        newFilePath = Path.Combine(outputDir, file.FileName + ext);
                    }

                    if (!File.Exists(tempFilePath))
                    {
                        file.OperationCode = OperationCode.ConversionFailed;
                        Render(RenderCode.ConversionFailed, file);
                        data.curFiles.Remove(file);
                        continue;
                    }

                    if (File.Exists(newFilePath))
                    {
                        for (int i = 0; true; i++)
                        {
                            newFilePath = Path.Combine(outputDir, file.FileName + i.ToString() + ext);

                            if (!File.Exists(newFilePath))
                                break;
                        }
                    }

                    if (!isXSITab)
                    {
                        if (BinTxtIdentifier(tempFilePath) == file.Conversion)
                        {
                            file.OperationCode = OperationCode.ConversionFailed;
                            Render(RenderCode.ConversionFailed, file);
                            data.curFiles.Remove(file);
                            continue;
                        }
                    }

                    File.Copy(tempFilePath, newFilePath, true);
                    file.OperationCode = OperationCode.Success;
                    Render(RenderCode.ConversionSuccess, file, newFilePath);
                }

                conversionProcess!.Dispose();
                Directory.Delete(tempDir, true);
                Render(RenderCode.Cleanup);
                Render(RenderCode.ConversionComplete);
            }
            else
            {
                var deserializer = new Deserializer(data, Render);

                deserializer.Deserialize(appDir, tempDir);
            }
        }

        private void Render(RenderCode code, SinsFile? file = null, string extraText = "")
        {
            string path = "";

            if (file != null)
                path = file.Path!;

            if (code == RenderCode.LoadFailed)
            {
                switch (file!.OperationCode)
                {
                    case OperationCode.FileNotFound:
                        extraText = "File not found.";
                        break;
                    case OperationCode.InvalidPath:
                        extraText = "Invalid path.";
                        break;
                    case OperationCode.InvalidExtension:
                        extraText = "Invalid file type.";
                        break;
                    case OperationCode.InvalidContent:
                        extraText = "Invalid file content.";
                        break;
                }
            }
            else if (code == RenderCode.ConversionFailed)
            {
                switch (file!.OperationCode)
                {
                    case OperationCode.ConversionTimeout:
                        extraText = "Converter took too long.";
                        break;
                    case OperationCode.ConversionFailed:
                        extraText = "Error converting file.";
                        break;
                    case OperationCode.InvalidExtension:
                        extraText = "Invalid file type.";
                        break;
                    case OperationCode.FileNotFound:
                        extraText = "File not found.";
                        break;
                }
            }
            else if (code == RenderCode.ConversionSuccess && file!.OperationCode == OperationCode.Success)
            {
                extraText = $"Output -> {extraText}";
            }

            switch (code)
            {
                case RenderCode.LoadingStart:
                    AppendText("[STAGE] ", blueFG, false);
                    AppendText("Loading selected files..", nearWhiteFG);
                    break;
                case RenderCode.LoadSuccess:
                    AppendText("[LOADED] ", amberFG, false);
                    AppendText($"{path}", nearWhiteFG);
                    break;
                case RenderCode.LoadFailed:
                    AppendText("[FAILED] ", Color.Red, false);
                    AppendText($"{path}", nearWhiteFG);
                    AppendText("[ERROR] ", Color.Red, false);
                    AppendText($"{extraText}", nearWhiteFG);
                    break;
                case RenderCode.LoadingComplete:
                    AppendText();
                    AppendText($"Queue: {data.loadedFiles!.Count} loaded, {data.curFiles!.Count - data.loadedFiles.Count} failed.", nearWhiteFG);
                    AppendText();
                    break;
                case RenderCode.ConversionStarted:
                    AppendText("[STAGE] ", blueFG, false);
                    AppendText("Converting selected files..", nearWhiteFG);
                    break;
                case RenderCode.ConversionSuccess:
                    AppendText("[CONVERTING] ", blueFG, false);
                    AppendText($"{path}", nearWhiteFG);
                    AppendText("[SUCCESS] ", Color.Green, false);
                    AppendText($"{extraText}", nearWhiteFG);
                    break;
                case RenderCode.ConversionFailed:
                    AppendText("[CONVERTING] ", nearWhiteFG, false);
                    AppendText($"{path}", nearWhiteFG);
                    AppendText("[FAILED] ", Color.Red, false);
                    AppendText($"{extraText}", nearWhiteFG);
                    break;
                case RenderCode.Cleanup:
                    AppendText("[CLEANUP] ", blueFG, false);
                    AppendText("Deleted temporary folder and failed converted files.", nearWhiteFG);
                    break;
                case RenderCode.ConversionComplete:
                    AppendText();
                    AppendText($"Batch: {data.curFiles!.Count} converted, {data.loadedFiles!.Count - data.curFiles.Count} failed.", nearWhiteFG);
                    AppendText();
                    break;

            }
        }

        private void AppendText(string text = "", Color? color = null, bool newLine = true)
        {
            if (text == "")
            {
                outputBox.AppendText(Environment.NewLine);
                return;
            }
            else
            {
                outputBox.SelectionStart = outputBox.TextLength;
                outputBox.SelectionLength = 0;
                outputBox.SelectionColor = color ?? nearWhiteFG;

                if (newLine)
                    outputBox.AppendText(text + Environment.NewLine);
                else
                    outputBox.AppendText(text);
            }
        }

        private void darkTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isXSITab = darkTabControl.SelectedTab == xsiConvert;
            isJSONTab = darkTabControl.SelectedTab == jsonConvert;

            btnSelectFiles.Visible = !isXSITab;
            btnSelectDir.Visible = !isXSITab;
        }

        private void jsonSelectFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter =
                    "All Supported Files (*.entity)|*.entity|" +
                    "Entities (*.entity)|*.entity|" +
                    "All Files (*.*)|*.*";

                dialog.InitialDirectory = (activeDir == "") ? appDir : activeDir;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                activeDir = Path.GetDirectoryName(dialog.FileName)!;

                data.curFiles = new List<SinsFile>();
                data.curFiles.Add(new SinsFile()
                {
                    Path = dialog.FileName,
                    OperationCode = OperationCode.Pending
                });
            }

            FileLoader();
        }

        private void jsonSelectFiles_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter =
                    "All Supported Files (*.entity)|*.entity|" +
                    "Entities (*.entity)|*.entity|" +
                    "All Files (*.*)|*.*";

                dialog.InitialDirectory = (activeDir == "") ? appDir : activeDir;
                dialog.Multiselect = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                if (dialog.FileNames.Length > 0)
                    activeDir = Path.GetDirectoryName(dialog.FileNames[0])!;

                data.curFiles = new List<SinsFile>();

                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    data.curFiles.Add(new SinsFile()
                    {
                        Path = dialog.FileNames[i],
                        OperationCode = OperationCode.Pending
                    });
                }
            }

            FileLoader();
        }

        private void jsonSelectDir_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = (activeDir == "") ? appDir : activeDir;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                activeDir = dialog.SelectedPath;

                data.curFiles = new List<SinsFile>();

                foreach (string path in Directory.EnumerateFiles(dialog.SelectedPath))
                {
                    data.curFiles.Add(new SinsFile()
                    {
                        Path = path,
                        OperationCode = OperationCode.Pending
                    });
                }
            }

            FileLoader();
        }

        private void xsiSelectFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter =
                    "All Supported Files (*.xsi)|*.xsi|" +
                    "XSI (*.xsi)|*.xsi|" +
                    "All Files (*.*)|*.*";

                dialog.InitialDirectory = (activeDir == "") ? appDir : activeDir;

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                activeDir = Path.GetDirectoryName(dialog.FileName)!;

                data.curFiles = new List<SinsFile>();
                data.curFiles.Add(new SinsFile()
                {
                    Path = dialog.FileName,
                    OperationCode = OperationCode.Pending
                });
            }

            FileLoader();
        }
    }
}
