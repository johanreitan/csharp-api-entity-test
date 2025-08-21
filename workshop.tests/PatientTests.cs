using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using workshop.wwwapi.Models;

namespace workshop.tests;

public class Tests
{

    [Test]
    public async Task GetAllPatientsNameFromSeed()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/surgery/patients");

        var patients = await response.Content.ReadFromJsonAsync<List<Patient>>();

        // Assert
        Assert.That(patients.Any(p => p.FullName.Equals("Johan Reitan")));
        Assert.That(patients.Any(p => p.FullName.Equals("Georg Reitan")));
        Assert.That(patients.Any(p => p.FullName.Equals("Greta Thunberg")));
    }

    [Test]
    public async Task GetAllDoctorsNameFromSeed()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/surgery/doctors");

        var doctors = await response.Content.ReadFromJsonAsync<List<Doctor>>();

        // Assert
        Assert.That(doctors.Any(p => p.FullName.Equals("Tora Klippen")));
        Assert.That(doctors.Any(p => p.FullName.Equals("Inger Hovland")));
        Assert.That(doctors.Any(p => p.FullName.Equals("Slim Kamel")));
    }

    [Test]
    public async Task GetAllAppointmentsBookingFromSeed()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/surgery/appointments");

        var appointments = await response.Content.ReadFromJsonAsync<List<Appointment>>();

        // Assert
        Assert.That(appointments.Any(a => a.Booking.Equals(DateTime.Parse("2020-05-04 20:04:30").ToUniversalTime())));
        Assert.That(appointments.Any(a => a.Booking.Equals(DateTime.Parse("2020-05-04 21:04:30").ToUniversalTime())));
        
    }
}