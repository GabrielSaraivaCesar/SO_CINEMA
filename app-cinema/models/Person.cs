using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_cinema
{   
    enum IfSeatUnavailableEnum
    {
        TENTAR_NOVAMENTE,
        DESISTIR
    }
    enum CustomerTypeEnum
    {
        REGULAR,
        MEIA_ENTRADA,
        CLUBLE_DE_CINEMA
    }
    enum PhaseBehaviorEnum
    {
        Y,
        N
    }

    enum PriorityComparerEnum
    {
        TIME_AND_PRIORITY,
        TIME_AND_PRIORITY_WITHOUT_ME
    }

    class Person 
    {
        private String targetSeatName;
        private String sessionHour;
        private PhaseBehaviorEnum[] phaseBehaviour;
        private IfSeatUnavailableEnum ifSeatIsUnavailable;
        private CustomerTypeEnum customerType;
        private int actionTime;
        private String name;

        public string Name { get => name; set => name = value; }
        public string TargetSeatName { get => targetSeatName; set => targetSeatName = value; }
        public string SessionHour { get => sessionHour; set => sessionHour = value; }
        public PhaseBehaviorEnum[] PhaseBehaviour { get => phaseBehaviour; set => phaseBehaviour = value; }
        public IfSeatUnavailableEnum IfSeatIsUnavailable { get => ifSeatIsUnavailable; set => ifSeatIsUnavailable = value; }
        public CustomerTypeEnum CustomerType { get => customerType; set => customerType = value; }
        public int ActionTime { get => actionTime; set => actionTime = value; }

        public Person()
        {

        }


        static public Person getNextInPriority(Person[] _people, PriorityComparerEnum comparer)
        {
            // Shallow copy
            Person[] people = new Person[_people.Length];
            for (int i = 0; i < _people.Length; i++)
            {
                people[i] = _people[i];
            }

            people = Array.FindAll(people, p => p.actionTime > 0);
            if (comparer == PriorityComparerEnum.TIME_AND_PRIORITY)
            {
                Array.Sort(people, Person.TypeAndTimePriorityComparer);
            } else
            {
                Array.Sort(people, Person.TypeAndTimePriorityComparerWithoutME);
            }


            if (people.Length > 0)
            {
                return people[0];
            } else
            {
                return null;
            }
        }


        // Metodo de sort para, ordena por prioridade (1->CLUBE_DE_CINEMA, 2->MEIA_ENTRADA, 3->REGULAR) e por ordem crescente de tempo de execucao
        static public int TypeAndTimePriorityComparer(Person p1, Person p2)
        {
            if (p1.customerType == CustomerTypeEnum.CLUBLE_DE_CINEMA && ((p2.customerType == CustomerTypeEnum.MEIA_ENTRADA) || p2.customerType == CustomerTypeEnum.REGULAR))
            {
                return -1;
            }
            else if (p1.customerType == CustomerTypeEnum.MEIA_ENTRADA && p2.customerType == CustomerTypeEnum.CLUBLE_DE_CINEMA)
            {
                return 1;
            }
            else if (p1.customerType == CustomerTypeEnum.MEIA_ENTRADA && p2.customerType == CustomerTypeEnum.REGULAR)
            {
                return -1;
            }
            else if (p1.customerType == CustomerTypeEnum.REGULAR && ((p2.customerType == CustomerTypeEnum.CLUBLE_DE_CINEMA) || p2.customerType == CustomerTypeEnum.MEIA_ENTRADA))
            {
                return 1;
            }
            else if (p1.customerType == p2.customerType)
            {
                if (p1.actionTime < p2.actionTime) return -1;
                else if (p1.actionTime > p2.actionTime) return 1;
                else return 0;
            }
            else
            {
                return 0;
            }
        }


        static public int TypeAndTimePriorityComparerWithoutME(Person p1, Person p2)
        {
            if (p1.customerType == CustomerTypeEnum.CLUBLE_DE_CINEMA && ((p2.customerType == CustomerTypeEnum.MEIA_ENTRADA) || p2.customerType == CustomerTypeEnum.REGULAR))
            {
                return -1;
            }
            else if ((p1.customerType == CustomerTypeEnum.MEIA_ENTRADA || p1.customerType == CustomerTypeEnum.REGULAR) && p2.customerType == CustomerTypeEnum.CLUBLE_DE_CINEMA)
            {
                return 1;
            }
            else if (p1.customerType == p2.customerType || (p1.customerType == CustomerTypeEnum.MEIA_ENTRADA && p2.customerType == CustomerTypeEnum.REGULAR) || (p2.customerType == CustomerTypeEnum.MEIA_ENTRADA && p1.customerType == CustomerTypeEnum.REGULAR))
            {
                if (p1.actionTime < p2.actionTime) return -1;
                else if (p1.actionTime > p2.actionTime) return 1;
                else return 0;
            }
            else
            {
                return 0;
            }
        }


        public Person clone()
        {
            Person theClone = new Person();
            theClone.TargetSeatName = this.targetSeatName;
            theClone.SessionHour = this.sessionHour;
            theClone.PhaseBehaviour = this.phaseBehaviour;
            theClone.IfSeatIsUnavailable = this.ifSeatIsUnavailable;
            theClone.CustomerType = this.customerType;
            theClone.ActionTime = this.actionTime;
            theClone.Name = this.name;
            return theClone;
        }
    }
}
