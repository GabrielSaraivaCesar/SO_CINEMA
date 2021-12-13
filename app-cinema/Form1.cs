using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace app_cinema
{
    public partial class Form1 : Form
    {
        Mutex mutex = new Mutex();
        Room room;
        Person[] peopleStack;
        String logs = "";
        String threadLogs = "";

        public Form1()
        {
            InitializeComponent();
            this.room = new Room();
            this.txtInterpretador.KeyUp += InterpretadorSubmit;
        }


        // Le o arquivo de configuracao da sala e armazena informacoes
        private void button_config_sala_Click(object sender, EventArgs e)
        {
            try
            {
                status_label.Text = "Importando arquivo de texto...";
                salaFileDialog.Filter = "txt files (*.txt)|*.txt";
                DialogResult result = salaFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream fileStream = salaFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        String content = reader.ReadToEnd();
                        this.ConfigurarSala(content);
                    }
                }
            }
            catch
            {
                this.room.Seats = null;
                this.room.Sessions = null;
                label_info_dimensoes.Text = "";
                label_info_sessoes.Text = "";
                label_arquivo_sala.Text = "Nenhum arquivo selecionado";
                MessageBox.Show("Erro ao importar arquivo, tente novamente...", "Arquivo invalido");
            }
            status_label.Text = "";
        }

        private void ConfigurarSala(String content)
        {
            String[] seatsDimension = content.Split((char)'\n')[0].Split((char)'x');
            String sessions = content.Split((char)'\n')[1].Replace(" ", "");
            String[] sessionsList = sessions.Split((char)',');
            this.room.Seats = new Person[int.Parse(seatsDimension[0]), int.Parse(seatsDimension[1])][];
            label_info_dimensoes.Text = this.room.Resolution;
            this.room.Sessions = sessionsList;
            label_info_sessoes.Text = sessions;
            String[] filePath = salaFileDialog.FileName.Split((char)'\\');
            label_arquivo_sala.Text = filePath[filePath.Length - 1];
        }


        // Le o arquivo de requisicao de compra e armazena informacoes
        private void button_compras_Click(object sender, EventArgs e)
        {
            try
            {
                status_label.Text = "Importando arquivo de texto...";
                comprasFileDialog.Filter = "txt files (*.txt)|*.txt";
                DialogResult result = comprasFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream fileStream = comprasFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        String content = reader.ReadToEnd();
                        ConfigurarCompras(content);
                    }
                }
            }
            catch
            {
                this.peopleStack = null;
                compras_log.Text = "";
                label_arquivo_compra_ingressos.Text = "Nenhum arquivo selecionado";
                MessageBox.Show("Erro ao importar arquivo, tente novamente...", "Arquivo invalido");
            }

            status_label.Text = "";
        }

        private void ConfigurarCompras(String content)
        {
            compras_log.Text = content;
            String[] filePath = comprasFileDialog.FileName.Split((char)'\\');

            String[] people = content.Split((char)'\n');
            this.peopleStack = new Person[people.Length];
            for (int i = 0; i < people.Length; i++)
            {
                String person = people[i].Replace("\r", "");
                String[] data = person.Split((char)';');

                this.peopleStack[i] = new Person();
                this.peopleStack[i].Name = "Cliente " + (i + 1);
                if (data[0].Substring(1) == "00") data[0] = data[0].Substring(0, 1) + "01";
                this.peopleStack[i].TargetSeatName = data[0];
                this.peopleStack[i].SessionHour = data[1];
                PhaseBehaviorEnum[] phases = new PhaseBehaviorEnum[3];
                char[] phasesChar = data[2].ToCharArray();
                for (int j = 0; j < phasesChar.Length; j++)
                {
                    if (phasesChar[j].ToString().ToUpper() == "X")
                    {
                        phases[j] = PhaseBehaviorEnum.N;
                    }
                    else
                    {
                        phases[j] = PhaseBehaviorEnum.Y;
                    }
                }
                this.peopleStack[i].PhaseBehaviour = phases;
                this.peopleStack[i].IfSeatIsUnavailable = data[3] == "T" ? IfSeatUnavailableEnum.TENTAR_NOVAMENTE : IfSeatUnavailableEnum.DESISTIR;
                this.peopleStack[i].CustomerType = data[4] == "R" ? CustomerTypeEnum.REGULAR : data[4] == "C" ? CustomerTypeEnum.CLUBLE_DE_CINEMA : CustomerTypeEnum.MEIA_ENTRADA;
                this.peopleStack[i].ActionTime = int.Parse(data[5]);
                this.peopleStack[i].TimeToNextClient = int.Parse(data[6]);
            }
            label_arquivo_compra_ingressos.Text = filePath[filePath.Length - 1];
        }


        private bool validateCustomerSessionHour(Person[] people)
        {
            bool allValid = true;
            for (int i = 0; i < people.Length; i++)
            {
                bool isItValid = false;
                for (int j = 0; j < this.room.Sessions.Length; j++)
                {
                    if (this.room.Sessions[j] == people[i].SessionHour)
                    {
                        isItValid = true;
                    }
                }
                if (!isItValid)
                {
                    allValid = false;
                    break;
                }
            }
            return allValid;
        }

        // Inicia processo de simulacao
        private void btn_simular_Click(object sender, EventArgs e)
        {

            Simular();
        }

        private void Simular(int? pontos =  null)
        {
            if (this.peopleStack == null || this.peopleStack.Length == 0) return;
            if (this.room.Seats == null || this.room.Seats.Length == 0 || this.room.Resolution == "") return;
            if (!validateCustomerSessionHour(this.peopleStack))
            {
                MessageBox.Show("Um cliente tentou entrar em uma sessao inexistente, corrija o arquivo e tente novamente", "Arquivo invalido");
                return;
            }

            this.logs = "";
            this.threadLogs = "";
            this.room.Seats = new Person[this.room.Seats.GetLength(0), this.room.Seats.GetLength(1)][];
            status_label.Text = "Simulando..."; // Sort de prioridades

            int maxPeopleForThread = 500;
            if (pontos != null)
            {
                maxPeopleForThread = Decimal.ToInt32(Math.Ceiling(Convert.ToDecimal(this.peopleStack.Length) / 2));
            }
            ArrayList dividedPeopleList = new ArrayList();

            // Montar filas
            decimal filasAmount = Math.Ceiling(Convert.ToDecimal(this.peopleStack.Length) / maxPeopleForThread);
            for (int i = 0; i < filasAmount; i++)
            {
                dividedPeopleList.Add(new ArrayList());
            }

            // Distribuir pessoas nas filas a partir de tempo de chegada
            int filaAtual = 0;
            foreach (Person pessoa in this.peopleStack)
            {
                dynamic f = dividedPeopleList[filaAtual];
                f.Add(pessoa);
                if (filaAtual == dividedPeopleList.Count - 1)
                {
                    filaAtual = 0;
                }
                else
                {
                    filaAtual++;
                }
            }

            var diagnosticsWatch = System.Diagnostics.Stopwatch.StartNew();
            ArrayList openedThreads = new ArrayList();
            for (int j = 0; j < dividedPeopleList.Count; j++)
            {
                dynamic people = dividedPeopleList[j];
                Person[] p = new Person[maxPeopleForThread];
                for (int i = 0; i < people.Count; i++)
                {
                    p[i] = people[i];
                }
                Thread simulationThread = new Thread(new ThreadStart(() => executeBuyRequests(p)));
                simulationThread.Name = "Ponto " + (j + 1).ToString();
                simulationThread.Start();
                openedThreads.Add(simulationThread);
            }

            for (int i = 0; i < openedThreads.Count; i++)
            {
                Thread t = (Thread)openedThreads[i];
                t.Join();
            }

            diagnosticsWatch.Stop();

            var runTime = diagnosticsWatch.ElapsedMilliseconds;
            this.label_runtime.Text = runTime.ToString() + "ms";
            this.logs_textbox.Text = this.logs + "\nHorário de finalização:\n" + this.threadLogs;
            this.status_label.Text = "";
        }


        // Algoritmo principal de tomada de decisao do cliente
        private void executeBuyRequests(Person[] _people)
        {
            // Inicio das vendas
            DateTime threadTime = new DateTime(2021, 1, 1, 8, 0, 0); // Dia 01/01/2021 08:00:00
            
            

            int notNullCount = 0;
            foreach (Person person in _people)
            {
                if (person != null)
                {
                    notNullCount++;
                }
            }
            Person[] people = new Person[notNullCount];
            for (int i = 0; i < notNullCount; i++)
            {
                people[i] = _people[i].clone();
            }

            for (int i = 0; i < people.Length; i++)
            {
                Person nextPerson = Person.getNextInPriority(people, PriorityComparerEnum.TIME_AND_PRIORITY);
                if (nextPerson.CustomerType == CustomerTypeEnum.MEIA_ENTRADA)
                {
                    // Verificacao para ver se ele realmente tem a prioridade
                    int availableSeatsAmout = this.room.availableSeatsAmount(nextPerson.SessionHour);
                    double pers = ((Convert.ToDouble(availableSeatsAmout) / Convert.ToDouble(this.room.Seats.Length) - 1.0) * -1);
                    if (pers > 0.4)
                    {
                        nextPerson = Person.getNextInPriority(people, PriorityComparerEnum.TIME_AND_PRIORITY_WITHOUT_ME); // Pegar proxima pessoa sem vantagem de meia entrada
                    }
                }
                threadTime = threadTime.AddMinutes(nextPerson.TimeToNextClient + nextPerson.ActionTime);

                // <SINCRONIZAR>
                this.mutex.WaitOne();
                try
                {
                    String nextAvailableSeat = this.room.getNextSeat(nextPerson.SessionHour);
                    if (nextAvailableSeat == null)
                    {
                        nextPerson.ActionTime = 0;
                        this.addLog(nextPerson, "não confirmou");
                        continue;
                    }
                    else
                    {
                        nextPerson.ActionTime = 0;
                    }

                    // Checar assento
                    if (nextPerson.PhaseBehaviour[0] == PhaseBehaviorEnum.N) // Se nao confirmar nessa fase
                    {
                        // Nao confirma
                        this.addLog(nextPerson, "não confirmou");
                        continue;
                    }

                    Person personOnSeat = this.room.checkSeat(nextPerson.TargetSeatName, nextPerson.SessionHour);
                    if (personOnSeat != null) // Se existe alguem no assento desejado
                    {
                        if (nextPerson.IfSeatIsUnavailable == IfSeatUnavailableEnum.DESISTIR) // Se desiste caso ja tenha alguem no assento
                        {
                            // Desiste
                            this.addLog(nextPerson, "desistiu");
                            continue;
                        }
                        else
                        {
                            // Mira para o proximo assento disponivel
                            nextPerson.TargetSeatName = nextAvailableSeat;
                        }
                    }

                    // Selecionar assento
                    if (nextPerson.PhaseBehaviour[1] == PhaseBehaviorEnum.N) // Se nao confirmar nessa fase
                    {
                        // Nao confirma
                        this.addLog(nextPerson, "não confirmou");
                        continue;
                    }

                    // Pagar
                    if (nextPerson.PhaseBehaviour[2] == PhaseBehaviorEnum.N) // Se nao confirmar nessa fase
                    {
                        // Nao confirma
                        this.addLog(nextPerson, "não confirmou");
                        continue;
                    }
                    this.room.setSeatOwner(nextPerson.TargetSeatName, nextPerson);

                    Thread.Sleep(nextPerson.TimeToNextClient);
                } finally
                {
                    this.mutex.ReleaseMutex();
                }
                // <\SINCRONIZAR>

                this.mutex.WaitOne();
                this.addLog(nextPerson, "confirmou");
                this.mutex.ReleaseMutex();
            }


            // FIM DA EXECUCAO DA THREAD
            this.addThreadLog(threadTime);
            
        }


        // Adiciona logs para serem printados no final
        private void addLog(Person request, String status)
        {
            this.logs += request.Name + " " + Thread.CurrentThread.Name +  " " + request.TargetSeatName + " " + request.SessionHour + " " + status + "\n";
        }

        private void addThreadLog(DateTime time)
        {
            String t = "";
            t += (time.Hour < 10 ? "0" : "") + time.Hour+":";
            t += (time.Minute < 10 ? "0" : "") + time.Minute;
            this.threadLogs += Thread.CurrentThread.Name + ": " + t+"\n";
        }


        // Estrair logs para .txt
        private void btn_extrair_Click(object sender, EventArgs e)
        {
            saveLogsDialog.Filter = "txt files (*.txt)|*.txt";
            saveLogsDialog.Title = "Exportar logs";
            saveLogsDialog.ShowDialog();
            Debug.WriteLine(saveLogsDialog.FileName);
            if (saveLogsDialog.FileName != "")
            {
                File.WriteAllText(saveLogsDialog.FileName, this.logs + "\nHorário de finalização:\n" + this.threadLogs);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        String interpSalaFile = "";
        String interpComprasFile = "";
        String interpOutFile = "";
        int interpPontos = 1;
        String interpLogType = "tela";

        // Interpretador...
        private void InterpretadorSubmit(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ExecutarComando();
            }
            
        }

        private void ExecutarComando()
        {
            String value = this.txtInterpretador.Text;
            this.txtInterpretador.Text = "";
            InterpreterLog(value);

            String[] commands = value.Split(" ".ToCharArray()[0]);
            String mainCommand = commands[0];

            // Configuracoes via comandos
            for (int i = 1; i < commands.Length; i++)
            {
                String currentCommand = commands[i];
                if (mainCommand == "simular")
                {
                    switch (currentCommand)
                    {
                        case "-log":
                            if (commands.Length > i + 1) interpLogType = commands[i + 1];
                            break;
                        case "-pontos":
                            if (commands.Length > i + 1) interpPontos = int.Parse(commands[i + 1]);
                            break;
                    }
                }
                else if (mainCommand == "alterar")
                {
                    switch (currentCommand)
                    {
                        case "-out":
                            if (commands.Length > i + 1) interpOutFile = commands[i + 1];
                            break;
                        case "-sala":
                            if (commands.Length > i + 1) interpSalaFile = commands[i + 1];
                            break;
                        case "-in":
                            if (commands.Length > i + 1) interpComprasFile = commands[i + 1];
                            break;
                    }
                }
            }


            if (mainCommand == "simular")
            {
                try
                {
                    String salaContent = System.IO.File.ReadAllText(interpSalaFile);
                    ConfigurarSala(salaContent);
                }
                catch
                {
                    InterpreterLog("Arquivo de configuracao de sala invalido!");
                    return;
                }
                try
                {
                    String comprasContent = System.IO.File.ReadAllText(interpComprasFile);
                    ConfigurarCompras(comprasContent);
                }
                catch
                {
                    InterpreterLog("Arquivo de simulacao de compras invalido!");
                    return;
                }

                try
                {
                    Simular(interpPontos);
                }
                catch
                {
                    InterpreterLog("Erro na simulacao, tente novamente");
                }

                if (interpLogType == "tela")
                {
                    this.richTextBox1.Text += "\n" + logs_textbox.Text + "\n";
                }
            }
        }

        private void InterpreterLog(String text)
        {
            DateTime now = DateTime.Now;
            this.richTextBox1.Text += String.Format("[{0}] {1} \n", now.ToString(), text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecutarComando();
        }
    }
}
