using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.DTOs;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Endpoints
{
    public static class SurgeryEndpoint
    {
        //TODO:  add additional endpoints in here according to the requirements in the README.md 
        public static void ConfigurePatientEndpoint(this WebApplication app)
        {
            var surgeryGroup = app.MapGroup("surgery");

            surgeryGroup.MapGet("/patients", GetPatients);
            surgeryGroup.MapGet("/doctors", GetDoctors);
            surgeryGroup.MapGet("/appointmentsbydoctor/{id}", GetAppointmentsByDoctor);
            surgeryGroup.MapGet("/appointmentsbypatient/{id}", GetAppointmentsByPatient);
            surgeryGroup.MapGet("/doctors{id}", GetDoctorById);

            surgeryGroup.MapGet("/patients{id}", GetPatientById);
            surgeryGroup.MapPost("/patients", CreatePatient);

            surgeryGroup.MapPost("/appointments", CreateAppointment);
            surgeryGroup.MapPost("/doctors", CreateDoctor);
            surgeryGroup.MapGet("/appointments", GetAppointments);
        }

        private static async Task<IResult> GetAppointmentsByPatient(IRepository repository, int id)
        {
            var apps = await repository.GetAppointmentsByPatient(id);
            var appGets = new List<AppointmentGet>();
            foreach (var a in apps)
            {
                appGets.Add(new AppointmentGet(a));
            }
            return TypedResults.Ok(appGets);
        }

        private static async Task<IResult> GetAppointments(IRepository repository)
        {
            var appointments = await repository.GetAppointments();
            var appointmentGets = new List<AppointmentGet>();

            foreach (var a in appointments)
            {
                appointmentGets.Add(new AppointmentGet(a));
            }
            return TypedResults.Ok(appointmentGets);
        }

        
        private static async Task<IResult> CreateDoctor(IRepository repository, DoctorPost model)
        {
            Doctor entity = new Doctor(model) { Id = await repository.GetLastDoctorId() + 1 };

            var result = await repository.AddDoctor(entity);
            return TypedResults.Created("", new DoctorGet(result));
        }
        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetDoctorById(IRepository repository, int id)
        {
            var entity = await repository.GetDoctorById(id);
            if (entity is null) return TypedResults.NotFound();
            return TypedResults.Ok(new DoctorGet(entity));
        }

        private static async Task<IResult> CreateAppointment(IRepository repository, AppointmentPost model)
        {
            Appointment entity = new Appointment(model) { Id = await repository.GetLastAppointmentId() + 1 };

            var result = await repository.AddAppointment(entity);
            return TypedResults.Created("", new AppointmentGet(result));
        }

        private static async Task<IResult> CreatePatient(IRepository repository, PatientPost pp)
        {
            Patient entity = new Patient(pp) { Id = await repository.GetLastPatientId() + 1 };



            await repository.AddPatient(entity);

            return TypedResults.Ok(new PatientGet(entity));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetPatientById(IRepository repository, int id)
        {
            var entity = await repository.GetPatientById(id);
            if (entity is null) return TypedResults.NotFound();
            return TypedResults.Ok(new PatientGet(entity));                      
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPatients(IRepository repository)
        {
            var patients = await repository.GetPatients();
            var patientGets = new List<PatientGet>();
            foreach (Patient p in patients)
            {
                patientGets.Add(new PatientGet(p));
            }
            return TypedResults.Ok(patientGets);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetDoctors(IRepository repository)
        {
            var doctors = await repository.GetDoctors();
            var doctorGets = new List<DoctorGet>();
            foreach(Doctor d in doctors)
            {
                doctorGets.Add(new DoctorGet(d));
            }
            return TypedResults.Ok(doctorGets);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsByDoctor(IRepository repository, int id)
        {
            var apps = await repository.GetAppointmentsByDoctor(id);
            var appGets = new List<AppointmentGet>();
            foreach (var app in apps)
            {
                appGets.Add(new AppointmentGet(app));
            }
            return TypedResults.Ok(appGets);
        }
    }
}
