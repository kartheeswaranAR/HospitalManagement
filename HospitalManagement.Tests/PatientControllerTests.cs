using System.Linq;
using Azure;
using HospitalManagement.Api.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace HospitalManagement.Tests;


public class PatientControllerTests
{
    private IMapper _mapper;
    private IPatientRepository _ipatientrepo;
    private PatientController _patientcontroller;
    private Patient patient = new Patient();
    private PatientEntity patiententity = new PatientEntity();
    private readonly CustomHttpException httpException = new CustomHttpException();

    [SetUp]
    public void Setup()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new Mapping());
        });
        _mapper = mappingConfig.CreateMapper();

        _ipatientrepo = Substitute.For<IPatientRepository>();
        _patientcontroller = new PatientController(_ipatientrepo, _mapper);
    }


    private Patient ListPatient()
    {
        patient = new Patient
        {
            Id = Guid.NewGuid(),
            Name = "karthi",
            DOB = DateTime.Now,
            Gender = "male",
            Address = "223a2/6,rajiv nagar,kovilpatti",
            MobileNumber = 91827635490,
            Email = "karthi@gmail.com",
            Description = "fever and cough",
            Password = "karthi"


        };

        return patient;
    }


    private PatientEntity ListPatientEntity()
    {
        patiententity = new PatientEntity
        {
            Id = Guid.NewGuid(),
            Name = "karthi",
            DOB = DateTime.Now,
            Email = "karthi@gmail.com",
            Description = "fever and cough",

        };

        return patiententity;
    }



    /// <summary>
    /// Get All Patients Mock data with controller base
    /// </summary>



    [Test]
    public void ViewAllPatients()
    {
        //Arrange
        List<Patient> patientlist = new List<Patient>
        {
            new Patient
            {
                Id = Guid.NewGuid(),
                Name = "karthi",
                DOB = DateTime.Now,
                Gender = "male",
                Address = "223a2/6,rajiv nagar,kovilpatti",
                MobileNumber = 91827635490,
                Email = "karthi@gmail.com",
                Description = "fever and cough",
                Password = "karthi"

            }
        };
        

        var patients =_ipatientrepo.GetAllPatients().Returns(patientlist);
        
        //Act
        
        var response = _patientcontroller.ViewAllPatients();

        //Assert
        Assert.IsTrue(response.IsCompleted);

        var responseresult = response.Result;
        
        var result = responseresult.Result as OkObjectResult;
        var actualresult = result.Value as List<PatientEntity>;
        Assert.IsInstanceOf<List<PatientEntity>>(actualresult);

        Assert.AreEqual(patientlist.Count,actualresult.Count);

        Assert.IsFalse(actualresult.Equals(patientlist));
        
    }


    [Test]
    public void List_Patient_Valid_ID()
    { 
        var patientllist = ListPatient();
        var patients = _ipatientrepo.GetPatientByID(patientllist.Id).Returns(patientllist);

        //Act
        var response = _patientcontroller.ViewPatient(patientllist.Id);

        //Assert
        Assert.IsTrue(response.IsCompleted);

        var responseresult = response.Result;
        Assert.IsInstanceOf<Patient>(patientllist);
        var result = responseresult.Result as OkObjectResult;
        var actualresult = result.Value as Patient;

        Assert.AreEqual(patientllist.Name,"karthi");
        Assert.AreEqual(actualresult.Name,"karthi");
        
        Assert.IsFalse(actualresult.Equals(patientllist.Id));


    }

    [Test]
    public void Patient_ID_Not_Found_Throws_Exception()
    {
        //Arrange
        var actuallist = ListPatient();
        var response = _patientcontroller.ViewPatient(actuallist.Id);
        //Act
        var actionresult = response.Exception;
        
        Assert.IsTrue(response.IsCompleted);
        //Assert

        Assert.IsTrue(actionresult.Message?.Contains("patient is not found !!"));
    }

    [Test]
    public void PatientList_Creation()
    {
        //Arrange
        var patientlist = ListPatient();
        var actualresponse = _ipatientrepo.AddPatient(patientlist).Returns(patientlist);
        var controller_response = _patientcontroller.AddPatients(patientlist);

        //Act
        var result = controller_response.Result;
        var Result = result.Result as OkObjectResult;
        Assert.IsTrue(Result.StatusCode==200);
        var expectedresult = Result.Value;

        //Assert
        Assert.IsTrue(controller_response.IsCompleted);
        Assert.IsTrue(expectedresult.Equals(201));

    }

    [Test]
    public void AddPatient_Not_Created_Throws_Exception()
    {

        //Arrange
        var patientlist = ListPatient();
        var actualresponse = _ipatientrepo.AddPatient(patientlist);
        var controller_response = _patientcontroller.AddPatients(patientlist);

        //Act
        var result = controller_response.Exception;
        var actualresult = result.InnerException;

        //Assert
        Assert.IsTrue(controller_response.IsCompleted);
        Assert.IsTrue(actualresult.Message?.Contains("patient is not created !!"));
    }

    [Test]
    public void PatientList_Update()
    {
        //Arrange
        var patientlist = ListPatient();
        var actualresponse = _ipatientrepo.EditPatient(patientlist.Id,patientlist).Returns(patientlist);
        var controller_response = _patientcontroller.EditPatient(patientlist.Id, patientlist);

        //Act
        var result = controller_response.Result;
        var Result = result.Result as OkObjectResult;
        Assert.IsTrue(Result.StatusCode == 200);
        var expectedresult = Result.Value;

        //Assert
        Assert.IsTrue(controller_response.IsCompleted);
        Assert.IsTrue(expectedresult.Equals(200));

    }
    [Test]
    public void Patientlist_update_throws_exception()
    {
        //Arrange
        var patientlist = ListPatient();
        var actualresponse = _ipatientrepo.EditPatient(patientlist.Id,patientlist);
        var controller_response = _patientcontroller.EditPatient(patientlist.Id, patientlist);

        //Act
        var result = controller_response.Exception;
        var actualresult = result.InnerException;

        //Assert
        Assert.IsTrue(controller_response.IsCompleted);
        Assert.IsTrue(actualresult.Message?.Contains("patient is not Found!!"));
    }


    [Test]
    public void PatientList_Delete()
    {
        //Arrange
        var patientlist = ListPatient();
        var actualresponse = _ipatientrepo.DeletePatient(patientlist.Id).Returns(patientlist);
        var controller_response = _patientcontroller.DeletePatient(patientlist.Id);

        //Act
        var result = controller_response.Result;
        var Result = result.Result as OkObjectResult;
        Assert.IsTrue(Result.StatusCode == 200);
        var expectedresult = Result.Value;

        //Assert
        Assert.IsTrue(controller_response.IsCompleted);
        Assert.IsTrue(expectedresult.Equals(200));
        Assert.IsTrue(Result.ContentTypes.Count==0);

    }

    [Test]
    public void Patientlist_Delete_throws_exception()
    {
        //Arrange
        var patientlist = ListPatient();
        Guid Id = Guid.NewGuid();
        var actualresponse = _ipatientrepo.DeletePatient(Id);
        var controller_response = _patientcontroller.DeletePatient(Id);

        //Act
        
        var result = controller_response.Exception;
        var actualresult = result.InnerException;

        //Assert
        Assert.IsTrue(controller_response.IsCompleted);
        Assert.IsTrue(actualresult.Message?.Contains("patient is not deleted !!"));
    }

}