// For more information see https://aka.ms/fsharp-console-apps
open Domain
open PasswordValidation
open RulesEngine
open System.IO
open Newtonsoft.Json
open RulesEngine.Models

let email = createEmail "wwestlake@lagdaemon.com"

let check = validateEmail email
                       
                       // !@#$%^&*
let pw = RawPassword ( ClearPassword  "sdcawdcawdclajsldkjaskljdhckjhABCdef" )

let test = "12345"

let workflow = JsonConvert.DeserializeObject<List<Workflow>>((new StreamReader( File.OpenRead("PasswordRules.json") )).ReadToEnd())

let bre = new RulesEngine(List.toArray workflow)

let requirement = {
    lowerCase = 2;
    upperCase = 2;
    symbols = 2;
    numeric = 2;
    length = 10;
    score = 0;
    strength = None;
}



let metric = scorePassword (validatePassword pw) requirement

let resultList = bre.ExecuteAllRulesAsync("PasswordRules").Result



printfn "%A\n\n%O" metric



