using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthHorizon_API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AIChatLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIChatLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AIChatLogs_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllergyTest",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Peanut = table.Column<bool>(type: "bit", nullable: false),
                    TreeNut = table.Column<bool>(type: "bit", nullable: false),
                    Dairy = table.Column<bool>(type: "bit", nullable: false),
                    Egg = table.Column<bool>(type: "bit", nullable: false),
                    Wheat = table.Column<bool>(type: "bit", nullable: false),
                    Soy = table.Column<bool>(type: "bit", nullable: false),
                    Fish = table.Column<bool>(type: "bit", nullable: false),
                    Shellfish = table.Column<bool>(type: "bit", nullable: false),
                    Pollen = table.Column<bool>(type: "bit", nullable: false),
                    DustMites = table.Column<bool>(type: "bit", nullable: false),
                    Mold = table.Column<bool>(type: "bit", nullable: false),
                    PetDander = table.Column<bool>(type: "bit", nullable: false),
                    InsectStings = table.Column<bool>(type: "bit", nullable: false),
                    Penicillin = table.Column<bool>(type: "bit", nullable: false),
                    Aspirin = table.Column<bool>(type: "bit", nullable: false),
                    NSAIDs = table.Column<bool>(type: "bit", nullable: false),
                    SulfaDrugs = table.Column<bool>(type: "bit", nullable: false),
                    Latex = table.Column<bool>(type: "bit", nullable: false),
                    Fragrances = table.Column<bool>(type: "bit", nullable: false),
                    Nickel = table.Column<bool>(type: "bit", nullable: false),
                    Preservatives = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergyTest", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_AllergyTest_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergyTest_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BodyMeasurement",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    ChestCircumference = table.Column<double>(type: "float", nullable: false),
                    WaistCircumference = table.Column<double>(type: "float", nullable: false),
                    HipCircumference = table.Column<double>(type: "float", nullable: false),
                    NeckCircumference = table.Column<double>(type: "float", nullable: false),
                    UpperArmCircumference = table.Column<double>(type: "float", nullable: false),
                    ForearmCircumference = table.Column<double>(type: "float", nullable: false),
                    ThighCircumference = table.Column<double>(type: "float", nullable: false),
                    CalfCircumference = table.Column<double>(type: "float", nullable: false),
                    BodyFatPercentage = table.Column<double>(type: "float", nullable: false),
                    MuscleMass = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurement", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_BodyMeasurement_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyMeasurement_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardiacTest",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Electrocardiogram = table.Column<bool>(type: "bit", nullable: false),
                    Echocardiogram = table.Column<bool>(type: "bit", nullable: false),
                    StressTest = table.Column<bool>(type: "bit", nullable: false),
                    HolterMonitor = table.Column<bool>(type: "bit", nullable: false),
                    EventMonitor = table.Column<bool>(type: "bit", nullable: false),
                    CardiacMRI = table.Column<bool>(type: "bit", nullable: false),
                    CardiacCT = table.Column<bool>(type: "bit", nullable: false),
                    CoronaryAngiogram = table.Column<bool>(type: "bit", nullable: false),
                    CalciumScoreTest = table.Column<bool>(type: "bit", nullable: false),
                    BloodPressureMonitoring = table.Column<bool>(type: "bit", nullable: false),
                    RestingHeartRate = table.Column<int>(type: "int", nullable: false),
                    MaxHeartRate = table.Column<int>(type: "int", nullable: false),
                    SystolicBP = table.Column<double>(type: "float", nullable: false),
                    DiastolicBP = table.Column<double>(type: "float", nullable: false),
                    CholesterolLevel = table.Column<double>(type: "float", nullable: false),
                    TriglycerideLevel = table.Column<double>(type: "float", nullable: false),
                    LDLCholesterol = table.Column<double>(type: "float", nullable: false),
                    HDLCholesterol = table.Column<double>(type: "float", nullable: false),
                    EjectionFraction = table.Column<double>(type: "float", nullable: false),
                    CoronaryCalciumScore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardiacTest", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_CardiacTest_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardiacTest_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndocrineTest",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    ThyroidFunctionTest = table.Column<bool>(type: "bit", nullable: false),
                    CortisolTest = table.Column<bool>(type: "bit", nullable: false),
                    GrowthHormoneTest = table.Column<bool>(type: "bit", nullable: false),
                    InsulinTest = table.Column<bool>(type: "bit", nullable: false),
                    CPeptideTest = table.Column<bool>(type: "bit", nullable: false),
                    ACTHStimulationTest = table.Column<bool>(type: "bit", nullable: false),
                    DHEASulfateTest = table.Column<bool>(type: "bit", nullable: false),
                    TestosteroneTest = table.Column<bool>(type: "bit", nullable: false),
                    EstrogenTest = table.Column<bool>(type: "bit", nullable: false),
                    ProgesteroneTest = table.Column<bool>(type: "bit", nullable: false),
                    ParathyroidHormoneTest = table.Column<bool>(type: "bit", nullable: false),
                    ProlactinTest = table.Column<bool>(type: "bit", nullable: false),
                    TSH = table.Column<double>(type: "float", nullable: false),
                    FreeT3 = table.Column<double>(type: "float", nullable: false),
                    FreeT4 = table.Column<double>(type: "float", nullable: false),
                    CortisolLevel = table.Column<double>(type: "float", nullable: false),
                    InsulinLevel = table.Column<double>(type: "float", nullable: false),
                    BloodGlucose = table.Column<double>(type: "float", nullable: false),
                    HemoglobinA1c = table.Column<double>(type: "float", nullable: false),
                    DHEASulfate = table.Column<double>(type: "float", nullable: false),
                    Testosterone = table.Column<double>(type: "float", nullable: false),
                    Estrogen = table.Column<double>(type: "float", nullable: false),
                    Progesterone = table.Column<double>(type: "float", nullable: false),
                    Prolactin = table.Column<double>(type: "float", nullable: false),
                    ParathyroidHormone = table.Column<double>(type: "float", nullable: false),
                    CalciumLevel = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndocrineTest", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_EndocrineTest_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndocrineTest_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneticTest",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    CarrierScreening = table.Column<bool>(type: "bit", nullable: false),
                    WholeExomeSequencing = table.Column<bool>(type: "bit", nullable: false),
                    WholeGenomeSequencing = table.Column<bool>(type: "bit", nullable: false),
                    PharmacogeneticTest = table.Column<bool>(type: "bit", nullable: false),
                    CancerGeneticTest = table.Column<bool>(type: "bit", nullable: false),
                    CardiovascularGeneticTest = table.Column<bool>(type: "bit", nullable: false),
                    NeurologicalGeneticTest = table.Column<bool>(type: "bit", nullable: false),
                    RareDiseaseTest = table.Column<bool>(type: "bit", nullable: false),
                    BRCA1Mutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BRCA2Mutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MTHFRMutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APCMutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LRRK2Mutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CFTRMutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HBBMutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HTTMutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LDLRMutation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiabetesRiskScore = table.Column<double>(type: "float", nullable: false),
                    HeartDiseaseRiskScore = table.Column<double>(type: "float", nullable: false),
                    AlzheimerRiskScore = table.Column<double>(type: "float", nullable: false),
                    ObesityRiskScore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneticTest", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_GeneticTest_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneticTest_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagingReport",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    XRay = table.Column<bool>(type: "bit", nullable: false),
                    CTScan = table.Column<bool>(type: "bit", nullable: false),
                    MRI = table.Column<bool>(type: "bit", nullable: false),
                    Ultrasound = table.Column<bool>(type: "bit", nullable: false),
                    Mammogram = table.Column<bool>(type: "bit", nullable: false),
                    PETScan = table.Column<bool>(type: "bit", nullable: false),
                    BoneDensityScan = table.Column<bool>(type: "bit", nullable: false),
                    Echocardiogram = table.Column<bool>(type: "bit", nullable: false),
                    DopplerUltrasound = table.Column<bool>(type: "bit", nullable: false),
                    GeneralFindings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Impression = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RadiologistNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TumorSize = table.Column<double>(type: "float", nullable: false),
                    AneurysmDiameter = table.Column<double>(type: "float", nullable: false),
                    BoneDensity = table.Column<double>(type: "float", nullable: false),
                    EjectionFraction = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagingReport", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_ImagingReport_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagingReport_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfectiousDiseaseTest",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Covid19 = table.Column<bool>(type: "bit", nullable: false),
                    Influenza = table.Column<bool>(type: "bit", nullable: false),
                    Tuberculosis = table.Column<bool>(type: "bit", nullable: false),
                    HepatitisB = table.Column<bool>(type: "bit", nullable: false),
                    HepatitisC = table.Column<bool>(type: "bit", nullable: false),
                    HIV = table.Column<bool>(type: "bit", nullable: false),
                    Syphilis = table.Column<bool>(type: "bit", nullable: false),
                    Malaria = table.Column<bool>(type: "bit", nullable: false),
                    Dengue = table.Column<bool>(type: "bit", nullable: false),
                    LymeDisease = table.Column<bool>(type: "bit", nullable: false),
                    ZikaVirus = table.Column<bool>(type: "bit", nullable: false),
                    EpsteinBarrVirus = table.Column<bool>(type: "bit", nullable: false),
                    Chlamydia = table.Column<bool>(type: "bit", nullable: false),
                    Gonorrhea = table.Column<bool>(type: "bit", nullable: false),
                    MRSA = table.Column<bool>(type: "bit", nullable: false),
                    TestMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfectiousDiseaseTest", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_InfectiousDiseaseTest_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfectiousDiseaseTest_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryTest",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Hemoglobin = table.Column<double>(type: "float", nullable: false),
                    WhiteBloodCellCount = table.Column<double>(type: "float", nullable: false),
                    PlateletCount = table.Column<double>(type: "float", nullable: false),
                    RedBloodCellCount = table.Column<double>(type: "float", nullable: false),
                    Glucose = table.Column<double>(type: "float", nullable: false),
                    Sodium = table.Column<double>(type: "float", nullable: false),
                    Potassium = table.Column<double>(type: "float", nullable: false),
                    Calcium = table.Column<double>(type: "float", nullable: false),
                    Cholesterol = table.Column<double>(type: "float", nullable: false),
                    ALT = table.Column<double>(type: "float", nullable: false),
                    AST = table.Column<double>(type: "float", nullable: false),
                    Bilirubin = table.Column<double>(type: "float", nullable: false),
                    Creatinine = table.Column<double>(type: "float", nullable: false),
                    BloodUreaNitrogen = table.Column<double>(type: "float", nullable: false),
                    GFR = table.Column<double>(type: "float", nullable: false),
                    UrineProtein = table.Column<bool>(type: "bit", nullable: false),
                    UrineGlucose = table.Column<bool>(type: "bit", nullable: false),
                    UrineKetones = table.Column<bool>(type: "bit", nullable: false),
                    TestLab = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryTest", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_LaboratoryTest_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaboratoryTest_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NeurologicalTests",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    BabinskiSign = table.Column<bool>(type: "bit", nullable: false),
                    RombergTest = table.Column<bool>(type: "bit", nullable: false),
                    FingerNoseTest = table.Column<bool>(type: "bit", nullable: false),
                    GaitAssessment = table.Column<bool>(type: "bit", nullable: false),
                    MuscleWeakness = table.Column<bool>(type: "bit", nullable: false),
                    Tremors = table.Column<bool>(type: "bit", nullable: false),
                    LightTouchTest = table.Column<bool>(type: "bit", nullable: false),
                    PainSensationTest = table.Column<bool>(type: "bit", nullable: false),
                    TemperatureSensationTest = table.Column<bool>(type: "bit", nullable: false),
                    VibratorySensationTest = table.Column<bool>(type: "bit", nullable: false),
                    PositionSenseTest = table.Column<bool>(type: "bit", nullable: false),
                    MiniMentalStateExam = table.Column<bool>(type: "bit", nullable: false),
                    MMSEScore = table.Column<int>(type: "int", nullable: false),
                    ClockDrawingTest = table.Column<bool>(type: "bit", nullable: false),
                    TrailMakingTest = table.Column<bool>(type: "bit", nullable: false),
                    VerbalFluencyTest = table.Column<bool>(type: "bit", nullable: false),
                    VisionAbnormality = table.Column<bool>(type: "bit", nullable: false),
                    FacialNerveWeakness = table.Column<bool>(type: "bit", nullable: false),
                    HearingLoss = table.Column<bool>(type: "bit", nullable: false),
                    SwallowingDifficulty = table.Column<bool>(type: "bit", nullable: false),
                    EEGPerformed = table.Column<bool>(type: "bit", nullable: false),
                    EEGResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMGPerformed = table.Column<bool>(type: "bit", nullable: false),
                    EMGResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NCVPerformed = table.Column<bool>(type: "bit", nullable: false),
                    NCVResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestLab = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeurologicalTests", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_NeurologicalTests_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NeurologicalTests_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespiratoryTest",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    SpirometryPerformed = table.Column<bool>(type: "bit", nullable: false),
                    FEV1 = table.Column<float>(type: "real", nullable: false),
                    FVC = table.Column<float>(type: "real", nullable: false),
                    FEV1FVC = table.Column<float>(type: "real", nullable: false),
                    PeakFlowTest = table.Column<bool>(type: "bit", nullable: false),
                    PeakFlowRate = table.Column<float>(type: "real", nullable: false),
                    BloodGasTest = table.Column<bool>(type: "bit", nullable: false),
                    OxygenSaturation = table.Column<float>(type: "real", nullable: false),
                    ArterialPaO2 = table.Column<float>(type: "real", nullable: false),
                    ArterialPaCO2 = table.Column<float>(type: "real", nullable: false),
                    BloodPH = table.Column<float>(type: "real", nullable: false),
                    ChestXRayPerformed = table.Column<bool>(type: "bit", nullable: false),
                    ChestXRayResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CTScanPerformed = table.Column<bool>(type: "bit", nullable: false),
                    CTScanResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BronchoscopyPerformed = table.Column<bool>(type: "bit", nullable: false),
                    BronchoscopyResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestLab = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespiratoryTest", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_RespiratoryTest_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespiratoryTest_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VitalSign",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    HeartRate = table.Column<float>(type: "real", nullable: false),
                    BloodPressureSystolic = table.Column<float>(type: "real", nullable: false),
                    BloodPressureDiastolic = table.Column<float>(type: "real", nullable: false),
                    RespiratoryRate = table.Column<float>(type: "real", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    OxygenSaturation = table.Column<float>(type: "real", nullable: false),
                    MeasurementLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSign", x => new { x.MedicalRecordId, x.Id });
                    table.ForeignKey(
                        name: "FK_VitalSign_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VitalSign_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AIChatLogs_PatientId",
                table: "AIChatLogs",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyTest_DoctorId",
                table: "AllergyTest",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AppointmentId",
                table: "Bills",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurement_DoctorId",
                table: "BodyMeasurement",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CardiacTest_DoctorId",
                table: "CardiacTest",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_EndocrineTest_DoctorId",
                table: "EndocrineTest",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneticTest_DoctorId",
                table: "GeneticTest",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagingReport_DoctorId",
                table: "ImagingReport",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_InfectiousDiseaseTest_DoctorId",
                table: "InfectiousDiseaseTest",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryTest_DoctorId",
                table: "LaboratoryTest",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_NeurologicalTests_DoctorId",
                table: "NeurologicalTests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AppointmentId",
                table: "Prescriptions",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RespiratoryTest_DoctorId",
                table: "RespiratoryTest",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_RoleId",
                table: "Staff",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSign_DoctorId",
                table: "VitalSign",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIChatLogs");

            migrationBuilder.DropTable(
                name: "AllergyTest");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "BodyMeasurement");

            migrationBuilder.DropTable(
                name: "CardiacTest");

            migrationBuilder.DropTable(
                name: "EndocrineTest");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "GeneticTest");

            migrationBuilder.DropTable(
                name: "ImagingReport");

            migrationBuilder.DropTable(
                name: "InfectiousDiseaseTest");

            migrationBuilder.DropTable(
                name: "LaboratoryTest");

            migrationBuilder.DropTable(
                name: "NeurologicalTests");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "RespiratoryTest");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "VitalSign");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
