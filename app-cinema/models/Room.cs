using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_cinema
{
    class Room
    {   
        private String[] seatLetters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private Person[,][] seats;
        private String[] sessions;

        private String resolution = "";


        internal Person[,][] Seats { get => seats; set {
                seats = value;
                if (seats != null)
                {
                    resolution = seats.GetLength(0) + "x" + seats.GetLength(1);
                } else
                {
                    resolution = "";
                }
            } }
        public string[] Sessions { get => sessions; set => sessions = value; }
        public String Resolution { get { return resolution; } }

        public Room()
        {
            
        }


        // Pega indice da letra no alfabeto
        private int getLetterIndex(String letter)
        {
            int letterIndex = -1;
            for (int i = 0; i < this.seatLetters.Length; i++)
            {
                if (this.seatLetters[i] == letter.ToUpper())
                {
                    letterIndex = i;
                }
            }
            return letterIndex;
        }

        


        // Verifica se o assento ja foi pego
        public Person checkSeat(String seat, String sessionHour)
        {
            String letter = seat.Substring(0, 1);
            int number = int.Parse(seat.Substring(1, 2));

            if (number < 1) number = 1;
            int letterIndex = this.getLetterIndex(letter);
            Person[] peopleInSeat = this.seats[letterIndex, number-1];
            Person personInSeatAtSessionHour = null;

            if (peopleInSeat == null)
            {
                Person[] p = { };
                this.seats[letterIndex, number-1] = p;
                peopleInSeat = p;
            }
            for (int i = 0; i < peopleInSeat.Length; i++)
            {
                Person person = peopleInSeat[i];
                if (person.SessionHour == sessionHour)
                {
                    personInSeatAtSessionHour = person;
                }
            }
            return personInSeatAtSessionHour;
        }


        // Pega a proxima cadeira vazia para a hora da sessao escolhida
        public String getNextSeat(String sessionHour)
        {
            int first_available_f = -1, first_vailable_c = -1;
            for (int fila = 0; fila < this.seats.GetLength(0); fila++)
            {
                for (int cadeira = 0; cadeira < this.seats.GetLength(1); cadeira++)
                {
                    bool availableForThisSession = true;
                    Person[] peopleInSeat = this.seats[fila, cadeira];
                    if (peopleInSeat == null)
                    {
                        Person[] p = { };
                        this.seats[fila, cadeira] = p;
                        peopleInSeat = p;
                    }
                    for (int p = 0; p < peopleInSeat.Length; p++)
                    {
                        if (peopleInSeat[p].SessionHour == sessionHour)
                        {
                            availableForThisSession = false;
                            break;
                        }
                    }
                    if (availableForThisSession)
                    {
                        first_available_f = fila;
                        first_vailable_c = cadeira;
                        break;
                    }
                }
                if (first_available_f != -1 && first_vailable_c != -1)
                {
                    break;
                }
            }
            if (first_available_f != -1 && first_vailable_c != -1)
            {
                return this.seatLetters[first_available_f] + "" + (first_vailable_c+1 < 10 ? "0" : "") + (first_vailable_c+1);
            } else
            {
                return null;
            }
        }


        // Ocupa assento
        public void setSeatOwner(String seat, Person owner)
        {
            String letter = seat.Substring(0, 1);
            int number = int.Parse(seat.Substring(1, 2));

            if (number < 1) number = 1;
            int letterIndex = this.getLetterIndex(letter);
            if (this.seats[letterIndex, number-1] == null)
            {
                Person[] p = { };
                this.seats[letterIndex, number-1] = p;
            }
            Person[] newPersonListOnSeat = new Person[this.seats[letterIndex, number-1].Length+1];
            for (int i = 0; i < newPersonListOnSeat.Length; i++)
            {
                if (i < newPersonListOnSeat.Length-1)
                {
                    newPersonListOnSeat[i] = this.seats[letterIndex, number-1][i];
                } else
                {
                    newPersonListOnSeat[i] = owner;
                }
            }
            this.seats[letterIndex, number-1] = newPersonListOnSeat;
        }
       
    }
}
