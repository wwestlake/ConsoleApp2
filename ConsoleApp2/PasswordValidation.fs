module PasswordValidation
open Domain
open System.Text.RegularExpressions

type PasswordStrength =
    | Zero
    | VeryWeak
    | Weak
    | Medium
    | Strong
    | VeryStrong

type PasswordMetrics = {
    lowerCase: int;
    upperCase: int;
    symbols: int;
    numeric: int;
    length: int;
    score: int;
    strength: PasswordStrength option
}

let validatePassword (RawPassword (ClearPassword pw)) =
    let lowerCaseRegex = new Regex( @"\p{Ll}" )
    let upperCaseRegex = new Regex( @"\p{Lu}" )
    let symbolsRegex = new Regex( @"[!@#$%^&*]{1}" )
    let numericRegex = new Regex( @"[0-9]{1}" )

    let countMatchs (regex: Regex) pw =
        let lst = regex.Matches(pw)
        lst.Count

    {
        lowerCase = countMatchs lowerCaseRegex pw;
        upperCase = countMatchs upperCaseRegex pw;
        symbols = countMatchs symbolsRegex pw;
        numeric = countMatchs numericRegex pw;
        length = pw.Length;
        score = 0;
        strength = None;
    }

let scorePassword (metric: PasswordMetrics) (requirement: PasswordMetrics) =
    let score = if metric.lowerCase >= requirement.lowerCase then 1 else 0
                   + if metric.upperCase >= requirement.upperCase then 1 else 0
                   + if metric.symbols >= requirement.symbols then 1 else 0
                   + if metric.numeric >= requirement.numeric then 1 else 0
                   + if metric.length >= requirement.length then 1 else 0
    {
        metric with 
            score = score;
            strength = match score with
                        | 0 -> Some Zero
                        | 1 -> Some VeryWeak
                        | 2 -> Some Weak
                        | 3 -> Some Medium
                        | 4 -> Some Strong
                        | 5 -> Some VeryStrong
                        | _ -> None
    }


