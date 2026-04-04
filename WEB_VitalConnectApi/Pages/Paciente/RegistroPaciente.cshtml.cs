using Microsoft.Win32;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

@page "/Paciente/Registro"
@model WEB_VitalConnectApi.Pages.RegistroPacienteModel

<div class= "container mt-4" >
    < div class= "card shadow rounded-4 p-4" >
        < h2 > Registro de Paciente</h2>

        <form method = "post" >
            < div class= "mb-3" >
                < label > Nombre Completo </ label >
                < input asp -for= "Formulario.NombreCompleto" class= "form-control" />
            </ div >

            < div class= "mb-3" >
                < label > CI </ label >
                < input asp -for= "Formulario.CI" class= "form-control" />
            </ div >

            < div class= "mb-3" >
                < label > Alergias / Antecedentes </ label >
                < textarea asp -for= "Formulario.AlergiasAntecedentes" class= "form-control" ></ textarea >
            </ div >

            < button class= "btn btn-primary" > Guardar </ button >
        </ form >
    </ div >
</ div >