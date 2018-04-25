module Users.DTO.Tests

open System
open Xunit
open DA.Doer.Users.RegisterOrg
open DA.FSX.ReaderTask

open Setup
open System.Threading.Tasks
open DA.FSX
open DA.Doer.Domain.Errors
open FsUnit.Xunit

[<Fact>]
let ``Register org from dto must work`` () =
    
    let payload =  """ 
        { "firstName" : 1 }
    """

    let assert' actual = 
        actual 
        |> should equal
            [
                "firstName", "NOT_STRING_ERROR"
                "lastName", "NULL_ERROR"
                "middleName", "NULL_ERROR"
                "orgName", "NULL_ERROR"
                "email", "NULL_ERROR"
                "phone", "NULL_ERROR"
                "password", "NULL_ERROR"
            ] 
    
    let task = (registerOrgDTO payload) context

    task.ContinueWith(fun (t: Task<_>) -> 
            Assert.IsType<AggregateException>(t.Exception) |> ignore
            Assert.IsType<ValidationException>(t.Exception.InnerException) |> ignore
            assert' (t.Exception.InnerException :?> ValidationException).errors
            0
        )





    
