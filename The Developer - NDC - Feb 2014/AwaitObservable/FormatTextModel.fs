module FormatTextModel

open System.Threading

let private isVowel isWvowel isYvowel character = 
    let isV = match character with 
                | 'a' | 'e' | 'i' | 'o' | 'u' | 'A' | 'E' | 'I' | 'O' | 'U'-> true
                | 'w' | 'W' when isWvowel -> true
                | 'y' | 'Y' when isYvowel -> true
                | _ -> false
    (character, isV)
    
let CheckForVowels isWvowel isYvowel text = async {
        Thread.Sleep(500)
        return Array.map (isVowel isWvowel isYvowel) text
    }
