﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary
{
    public class Hospital
    {
        private Queue<ExtraDoctor> extraDoctors;
        public AfterLife AfterLife { get; private set; }
        public CheckedOut CheckedOut { get; private set; }
        public Sanatorium Sanatorium { get; private set; }
        public IVA Iva { get; private set; }
        public PatientQueue PatientQueue { get; private set; }
        internal int CurrentDay { get; private set; }

        public event EventHandler<SendReportEventArgs> SendReport;

        public Hospital(int nrOfPatients)
        {
            extraDoctors = HospitalManager.GenerateExtraDoctors();
            CurrentDay = 1;
            AfterLife = new AfterLife();
            CheckedOut = new CheckedOut();

            PatientQueue = new PatientQueue(nrOfPatients);
            Iva = new IVA(this);
            Sanatorium = new Sanatorium(this);
            
        }
        private Hospital()
        {

        }
        public void OnTick(int currentTick)
        {
            CurrentDay = currentTick;
            Iva.OnTickChanges(this);
            Sanatorium.OnTickChanges(this);
            PatientQueue.OnTickChanges();
            OnSendReport(currentTick);

        }
        public void OnSendReport(int currentTick)
        {
            bool noMorePatients = false;
            if (Iva.PatientsCount() == 0 && Sanatorium.PatientsCount() == 0)
            {
                noMorePatients = true;
            }
            SendReportEventArgs eArgs = new SendReportEventArgs(currentTick, noMorePatients);
            SendReport?.Invoke(this.Clone(), eArgs);
        }
        public Hospital Clone()
        {
            var hp = new Hospital();
            hp.PatientQueue = (PatientQueue)this.PatientQueue.Clone();
            hp.Iva = (IVA)this.Iva.Clone();
            hp.Sanatorium = (Sanatorium)this.Sanatorium.Clone();
            hp.AfterLife = this.AfterLife.Clone();
            hp.CheckedOut = this.CheckedOut.Clone();
            hp.extraDoctors = new Queue<ExtraDoctor>();
            CopyExtraDoctorsToArray().ToList().ForEach(doctor => hp.extraDoctors.Enqueue(doctor.Clone()));
            return hp;
        }
        internal ExtraDoctor DequeueExtraDoctor()
        {
            return extraDoctors.Dequeue();
        }
        public int ExtraDoctorsCount()
        {
            return extraDoctors.Count;
        }
        public ExtraDoctor[] CopyExtraDoctorsToArray()
        {
            var tempDocArray = new ExtraDoctor[extraDoctors.Count];
            extraDoctors.CopyTo(tempDocArray, 0);
            return tempDocArray;
        }
    }
}
