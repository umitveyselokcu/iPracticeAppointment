using System;
using System.Collections.Generic;
using System.Linq;
using iPractice.DataAccess.Models;

namespace iPractice.DataAccess
{
    public class SeedData
    {
        private const int NoClients = 50;
        private const int NoPsychologists = 20;
        private const int NoAppointments = 200;
        
        private readonly ApplicationDbContext _context;
        
        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            var psychologists = CreatePsychologists();
            _context.Psychologists.AddRange(psychologists);
            _context.SaveChanges();
            var clients = CreateClients(psychologists);
            _context.Clients.AddRange(clients);
            _context.SaveChanges();
            var appointmentSlots = CreateAppointmentSlots(clients, psychologists);
            _context.AppointmentSlots.AddRange(appointmentSlots);
            _context.SaveChanges();
        }

        private static List<Client> CreateClients(List<Psychologist> psychologists)
        {
            var random = new Random();
            
            List<Client> clients = new List<Client>();
            for (int i = 0; i < NoClients; i++)
            {
                clients.Add(new Client()
                {
                    Name = $"Client {i + 1}",
                    Psychologists = new List<Psychologist>(new[]
                    {
                        psychologists.Skip(random.Next(NoPsychologists)).First(),
                        psychologists.Skip(random.Next(NoPsychologists)).First()
                    })
                });
            }

            return clients;
        }
        
        private static List<AppointmentSlot> CreateAppointmentSlots(List<Client> clients, List<Psychologist> psychologists)
        {
            var random = new Random();
            var toDay = DateTime.Now;
            List<AppointmentSlot> appointments = new List<AppointmentSlot>();
            
            for (int i = 0; i < NoAppointments; i++)
            {
                appointments.Add(new AppointmentSlot()
                {
                    ClientId = random.Next(5) > 3 ? random.Next(1, NoClients-1) : (long?) null,
                    Psychologist = psychologists.Skip(random.Next(1, NoPsychologists-1)).First(),
                    TimeSlot = new DateTime(toDay.AddDays(i+1).Ticks)

                });
            }

            return appointments;
        }

        private static List<Psychologist> CreatePsychologists()
        {
            List<Psychologist> psychologists = new List<Psychologist>();
            for (int i = 0; i < NoPsychologists; i++)
            {
                psychologists.Add(new Psychologist
                {
                    Name = $"Psychologist {i + 1}"
                });
            }

            return psychologists;
        }
    }
}