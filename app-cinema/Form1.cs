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

namespace app_cinema
{
    public partial class Form1 : Form
    {

        Room room;
        Person[] peopleStack;
        String logs = "";

        public Form1()
        {
            InitializeComponent();
            this.room = new Room();
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
                                } else
                                {
                                    phases[j] = PhaseBehaviorEnum.Y;
                                }
                            }
                            this.peopleStack[i].PhaseBehaviour = phases;
                            this.peopleStack[i].IfSeatIsUnavailable = data[3] == "T" ? IfSeatUnavailableEnum.TENTAR_NOVAMENTE : IfSeatUnavailableEnum.DESISTIR;
                            this.peopleStack[i].CustomerType = data[4] == "R" ? CustomerTypeEnum.REGULAR : data[4] == "C" ? CustomerTypeEnum.CLUBLE_DE_CINEMA : CustomerTypeEnum.MEIA_ENTRADA;
                            this.peopleStack[i].ActionTime = int.Parse(data[5]);
                        }
                        label_arquivo_compra_ingressos.Text = filePath[filePath.Length - 1];
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
            if (this.peopleStack == null || this.peopleStack.Length == 0) return;
            if (this.room.Seats == null || this.room.Seats.Length == 0 || this.room.Resolution == "") return;
            if (!validateCustomerSessionHour(this.peopleStack))
            {
                MessageBox.Show("Um cliente tentou entrar em uma sessao inexistente, corrija o arquivo e tente novamente", "Arquivo invalido");
                return;
            }

            this.logs = "";
            this.room.Seats = new Person[this.room.Seats.GetLength(0), this.room.Seats.GetLength(1)][];
            status_label.Text = "Simulando..."; // Sort de prioridades

            Thread simulationThread = new Thread(new ThreadStart(() => executeBuyRequests(this.peopleStack)));
            simulationThread.Name = "Simulation thread";
            simulationThread.Start();
        }


        // Algoritmo principal de tomada de decisao do cliente
        private void executeBuyRequests(Person[] _people)
        {
            Person[] people = new Person[_people.Length];
            for (int i = 0; i < _people.Length; i++)
            {
                people[i] = _people[i].clone();
            }
            var diagnosticsWatch = System.Diagnostics.Stopwatch.StartNew();

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

                String nextAvailableSeat = this.room.getNextSeat(nextPerson.SessionHour);
                if (nextAvailableSeat == null)
                {
                    nextPerson.ActionTime = 0;
                    this.addLog(nextPerson, "não confirmou");
                    continue;
                } else
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
                    } else
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
                this.addLog(nextPerson, "confirmou");
            }


            diagnosticsWatch.Stop();
            var runTime = diagnosticsWatch.ElapsedMilliseconds;

            MethodInvoker inv = delegate
            {
                this.label_runtime.Text = runTime.ToString() + "ms";
                this.logs_textbox.Text = this.logs;
                this.status_label.Text = "";
            };
            this.Invoke(inv);
        }


        // Adiciona logs para serem printados no final
        private void addLog(Person request, String status)
        {
            this.logs += request.Name + " " + request.TargetSeatName + " " + request.SessionHour + " " + status + "\n";
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
                File.WriteAllText(saveLogsDialog.FileName, this.logs);
            }
        }

      
    }
}
