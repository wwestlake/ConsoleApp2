module Domain

open System
open System.Text.RegularExpressions
open System.Security.Cryptography;
open System.Text
open CheckPasswordStrength

let regexString = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])"
let regex = new Regex(regexString)

let sha = SHA256.Create()

type EmailAddress = EmailAddress of string

type Email =
    | RawEmail of EmailAddress
    | ValidEmail of EmailAddress
    | InvalidEmail of EmailAddress
    | VerifiedEmail of EmailAddress

let createEmail (emailStr: string) =
        emailStr |> EmailAddress |> RawEmail

let validateEmail email =
    match email with
    | RawEmail (EmailAddress em) ->
        if regex.IsMatch(em) then ValidEmail (EmailAddress em)
        else InvalidEmail (EmailAddress em)
    | _ -> email

type ClearPassword = ClearPassword of string
type HashedPassword = HashedPassword of byte array
type Password =
    | RawPassword of ClearPassword
    | SecurePassword of HashedPassword
    | InvalidPassword of string

let checkStrength (ClearPassword pw) =
    pw.PasswordStrength() 

let HashPassword (ClearPassword pw) =
    HashedPassword (sha.ComputeHash(Encoding.ASCII.GetBytes(pw))) 
    
     






