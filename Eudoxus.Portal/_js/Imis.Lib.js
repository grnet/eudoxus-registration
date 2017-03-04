Type.registerNamespace("Imis.Lib");

Imis.Lib.ToEn = function(s, e) {
    var change = 0;
    var Str;

    if (s.isASPxClientControl)
        Str = s.GetText();
    else
        Str = s.value;

    var StrL = Str.length;
    var StrOut = "";

    var c = ' ';
    var cOut = ' ';

    for (var i = 0; i < StrL; i++) {
        c = Str.substring(i, i + 1);
        cOut = '';
        if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9') { change = 1; cOut = c; }
        if (c >= 'A' && c <= 'Z') { cOut = c; }
        if (c == ' ' || c == '-' || c == '_' || c == ',' || c == '.' || c == '(' || c == ')' || c == '/' || c == '!' || c == '@' || c == '#' ||
            c == '$' || c == '%' || c == '^' || c == '&' || c == '*' || c == '+' || c == '=' || c == '\'' || c == '\"') { cOut = c; }

        //Lowercase letters
        if (c == 'α' || c == 'ά' || c == 'a') { change = 1; cOut = 'a'; }
        if (c == 'β' || c == 'b') { change = 1; cOut = 'b'; }
        if (c == 'ψ' || c == 'c') { change = 1; cOut = 'c'; }
        if (c == 'δ' || c == 'd') { change = 1; cOut = 'd'; }
        if (c == 'ε' || c == 'έ' || c == 'e') { change = 1; cOut = 'e'; }
        if (c == 'φ' || c == 'f') { change = 1; cOut = 'f'; }
        if (c == 'γ' || c == 'g') { change = 1; cOut = 'g'; }
        if (c == 'η' || c == 'ή' || c == 'h') { change = 1; cOut = 'h'; }
        if (c == 'ι' || c == 'ί' || c == 'ϊ' || c == 'ΐ' || c == 'i') { change = 1; cOut = 'i'; }
        if (c == 'ξ' || c == 'j') { change = 1; cOut = 'j'; }
        if (c == 'κ' || c == 'k') { change = 1; cOut = 'k'; }
        if (c == 'λ' || c == 'l') { change = 1; cOut = 'l'; }
        if (c == 'μ' || c == 'm') { change = 1; cOut = 'm'; }
        if (c == 'ν' || c == 'n') { change = 1; cOut = 'n'; }
        if (c == 'ο' || c == 'ό' || c == 'o') { change = 1; cOut = 'o'; }
        if (c == 'π' || c == 'p') { change = 1; cOut = 'p'; }
        if (c == 'q') { change = 1; cOut = 'q'; }
        if (c == 'ρ' || c == 'r') { change = 1; cOut = 'r'; }
        if (c == 'σ' || c == 's') { change = 1; cOut = 's'; }
        if (c == 'τ' || c == 't') { change = 1; cOut = 't'; }
        if (c == 'θ' || c == 'u') { change = 1; cOut = 'u'; }
        if (c == 'ω' || c == 'ώ' || c == 'v') { change = 1; cOut = 'v'; }
        if (c == 'ς' || c == 'w') { change = 1; cOut = 'w'; }
        if (c == 'χ' || c == 'x') { change = 1; cOut = 'x'; }
        if (c == 'υ' || c == 'ύ' || c == 'ϋ' || c == 'ΰ' || c == 'y') { change = 1; cOut = 'y'; }
        if (c == 'ζ' || c == 'z') { change = 1; cOut = 'z'; }

        //Uppercase letters
        if (c == 'Ά' || c == 'Α') { change = 1; cOut = 'A'; }
        if (c == 'Β') { change = 1; cOut = 'B'; }
        if (c == 'Ψ') { change = 1; cOut = 'C'; }
        if (c == 'Δ') { change = 1; cOut = 'D'; }
        if (c == 'Έ' || c == 'Ε') { change = 1; cOut = 'E'; }
        if (c == 'Φ') { change = 1; cOut = 'F'; }
        if (c == 'Γ') { change = 1; cOut = 'G'; }
        if (c == 'Ή' || c == 'Η') { change = 1; cOut = 'H'; }
        if (c == 'Ί' || c == 'Ϊ' || c == 'Ι') { change = 1; cOut = 'I'; }
        if (c == 'Ξ') { change = 1; cOut = 'J'; }
        if (c == 'Κ') { change = 1; cOut = 'K'; }
        if (c == 'Λ') { change = 1; cOut = 'L'; }
        if (c == 'Μ') { change = 1; cOut = 'M'; }
        if (c == 'Ν') { change = 1; cOut = 'N'; }
        if (c == 'Ό' || c == 'Ο') { change = 1; cOut = 'Ο'; }
        if (c == 'Π') { change = 1; cOut = 'P'; }
        if (c == 'Q' || c == ';') { change = 1; cOut = 'Q'; }
        if (c == 'Ρ') { change = 1; cOut = 'R'; }
        if (c == 'Σ') { change = 1; cOut = 'S'; }
        if (c == 'Τ') { change = 1; cOut = 'T'; }
        if (c == 'Θ') { change = 1; cOut = 'U'; }
        if (c == 'Ώ' || c == 'Ω') { change = 1; cOut = 'V'; }
        if (c == 'Χ') { change = 1; cOut = 'X'; }
        if (c == 'Ύ' || c == 'Ϋ' || c == 'Υ') { change = 1; cOut = 'Y'; }
        if (c == 'Ζ') { change = 1; cOut = 'Z'; }

        if (cOut == '') { change = 1; }

        StrOut = StrOut + cOut;
    };

    if (change == 1) {
        if (s.isASPxClientControl)
            s.SetText(StrOut);
        else
            s.value = StrOut;
    }
    return;
}

Imis.Lib.ToEnUpper = function(s, e) {
    var change = 0;
    var Str;

    if (s.isASPxClientControl)
        Str = s.GetText();
    else
        Str = s.value;

    var StrL = Str.length;
    var StrOut = "";

    var c = ' ';
    var cOut = ' ';

    for (var i = 0; i < StrL; i++) {
        c = Str.substring(i, i + 1);
        cOut = '';
        if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9') { change = 1; cOut = c; }
        if (c >= 'A' && c <= 'Z') { cOut = c; }
        if (c == ' ' || c == '-' || c == '_' || c == ',' || c == '.' || c == '(' || c == ')' || c == '/') { cOut = c; }
        if (c == 'Ά' || c == 'α' || c == 'ά' || c == 'a' || c == 'A') { change = 1; cOut = 'A'; }
        if (c == 'β' || c == 'b' || c == 'Β') { change = 1; cOut = 'B'; }
        if (c == 'ψ' || c == 'c' || c == 'Ψ') { change = 1; cOut = 'C'; }
        if (c == 'δ' || c == 'd' || c == 'Δ') { change = 1; cOut = 'D'; }
        if (c == 'Έ' || c == 'ε' || c == 'έ' || c == 'e' || c == 'Ε') { change = 1; cOut = 'E'; }
        if (c == 'φ' || c == 'f' || c == 'Φ') { change = 1; cOut = 'F'; }
        if (c == 'γ' || c == 'g' || c == 'Γ') { change = 1; cOut = 'G'; }
        if (c == 'Ή' || c == 'η' || c == 'ή' || c == 'h' || c == 'Η') { change = 1; cOut = 'H'; }
        if (c == 'Ί' || c == 'Ϊ' || c == 'ι' || c == 'ί' || c == 'ϊ' || c == 'ΐ' || c == 'i' || c == 'Ι') { change = 1; cOut = 'I'; }
        if (c == 'ξ' || c == 'j' || c == 'Ξ') { change = 1; cOut = 'J'; }
        if (c == 'κ' || c == 'k' || c == 'Κ') { change = 1; cOut = 'K'; }
        if (c == 'λ' || c == 'l' || c == 'Λ') { change = 1; cOut = 'L'; }
        if (c == 'μ' || c == 'm' || c == 'Μ') { change = 1; cOut = 'M'; }
        if (c == 'ν' || c == 'n' || c == 'Ν') { change = 1; cOut = 'N'; }
        if (c == 'Ό' || c == 'ο' || c == 'ό' || c == 'o' || c == 'Ο') { change = 1; cOut = 'Ο'; }
        if (c == 'π' || c == 'p' || c == 'Π') { change = 1; cOut = 'P'; }
        if (c == 'q' || c == 'Q' || c == ';') { change = 1; cOut = 'Q'; }
        if (c == 'ρ' || c == 'r' || c == 'Ρ') { change = 1; cOut = 'R'; }
        if (c == 'σ' || c == 's' || c == 'Σ') { change = 1; cOut = 'S'; }
        if (c == 'τ' || c == 't' || c == 'Τ') { change = 1; cOut = 'T'; }
        if (c == 'θ' || c == 'u' || c == 'Θ') { change = 1; cOut = 'U'; }
        if (c == 'Ώ' || c == 'ω' || c == 'ώ' || c == 'v' || c == 'Ω') { change = 1; cOut = 'V'; }
        if (c == 'ς' || c == 'w') { change = 1; cOut = 'W'; }
        if (c == 'χ' || c == 'x' || c == 'Χ') { change = 1; cOut = 'X'; }
        if (c == 'Ύ' || c == 'Ϋ' || c == 'υ' || c == 'ύ' || c == 'ϋ' || c == 'ΰ' || c == 'y' || c == 'Υ') { change = 1; cOut = 'Y'; }
        if (c == 'ζ' || c == 'z' || c == 'Ζ') { change = 1; cOut = 'Z'; }
        if (cOut == '') { change = 1; }

        StrOut = StrOut + cOut;
    };

    if (change == 1) {
        if (s.isASPxClientControl)
            s.SetText(StrOut);
        else
            s.value = StrOut;
    }
    return;
}

Imis.Lib.ToElUpper = function(s, e) {
    var change = 0;
    var Str;

    if (s.isASPxClientControl)
        Str = s.GetText();
    else
        Str = s.value;

    var StrL = Str.length;
    var StrOut = "";

    var c = ' ';
    var cOut = ' ';

    for (var i = 0; i < StrL; i++) {
        c = Str.substring(i, i + 1);
        cOut = '';
        if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9') { change = 1; cOut = c; }
        if (c >= 'Α' && c <= 'Ω') { cOut = c; }
        if (c == ' ' || c == '-' || c == '_' || c == ',' || c == '.' || c == '(' || c == ')' || c == '/') { cOut = c; }
        if (c == 'Ά' || c == 'α' || c == 'ά' || c == 'a' || c == 'A') { change = 1; cOut = 'Α'; }
        if (c == 'β' || c == 'b' || c == 'B') { change = 1; cOut = 'Β'; }
        if (c == 'γ' || c == 'g' || c == 'G') { change = 1; cOut = 'Γ'; }
        if (c == 'δ' || c == 'd' || c == 'D') { change = 1; cOut = 'Δ'; }
        if (c == 'Έ' || c == 'ε' || c == 'έ' || c == 'e' || c == 'E') { change = 1; cOut = 'Ε'; }
        if (c == 'ζ' || c == 'z' || c == 'Z') { change = 1; cOut = 'Ζ'; }
        if (c == 'Ή' || c == 'η' || c == 'ή' || c == 'h' || c == 'H') { change = 1; cOut = 'Η'; }
        if (c == 'θ' || c == 'u' || c == 'U') { change = 1; cOut = 'Θ'; }
        if (c == 'Ί' || c == 'Ϊ' || c == 'ι' || c == 'ί' || c == 'ϊ' || c == 'ΐ' || c == 'i' || c == 'I') { change = 1; cOut = 'Ι'; }
        if (c == 'κ' || c == 'k' || c == 'K') { change = 1; cOut = 'Κ'; }
        if (c == 'λ' || c == 'l' || c == 'L') { change = 1; cOut = 'Λ'; }
        if (c == 'μ' || c == 'm' || c == 'M') { change = 1; cOut = 'Μ'; }
        if (c == 'ν' || c == 'n' || c == 'N') { change = 1; cOut = 'Ν'; }
        if (c == 'ξ' || c == 'j' || c == 'J') { change = 1; cOut = 'Ξ'; }
        if (c == 'Ό' || c == 'ο' || c == 'ό' || c == 'o' || c == 'O') { change = 1; cOut = 'Ο'; }
        if (c == 'π' || c == 'p' || c == 'P') { change = 1; cOut = 'Π'; }
        if (c == 'ρ' || c == 'r' || c == 'R') { change = 1; cOut = 'Ρ'; }
        if (c == 'σ' || c == 'ς' || c == 's' || c == 'S') { change = 1; cOut = 'Σ'; }
        if (c == 'τ' || c == 't' || c == 'T') { change = 1; cOut = 'Τ'; }
        if (c == 'Ύ' || c == 'Ϋ' || c == 'υ' || c == 'ύ' || c == 'ϋ' || c == 'ΰ' || c == 'y' || c == 'Y') { change = 1; cOut = 'Υ'; }
        if (c == 'φ' || c == 'f' || c == 'F') { change = 1; cOut = 'Φ'; }
        if (c == 'χ' || c == 'x' || c == 'X') { change = 1; cOut = 'Χ'; }
        if (c == 'ψ' || c == 'c' || c == 'C') { change = 1; cOut = 'Ψ'; }
        if (c == 'Ώ' || c == 'ω' || c == 'ώ' || c == 'v' || c == 'V') { change = 1; cOut = 'Ω'; }
        if (c == 'ς' || c == 'w' || c == 'W') { change = 1; cOut = 'Σ'; }
        if (cOut == '') { change = 1; }

        StrOut = StrOut + cOut;
    };

    if (change == 1) {
        if (s.isASPxClientControl)
            s.SetText(StrOut);
        else
            s.value = StrOut;
    }
    return;
}

Imis.Lib.ToUpper = function(s, e) {
    var change = 0;
    var Str;

    if (s.isASPxClientControl)
        Str = s.GetText();
    else
        Str = s.value;

    var StrL = Str.length;
    var StrOut = "";

    var c = ' ';
    var cOut = ' ';

    for (var i = 0; i < StrL; i++) {
        c = Str.substring(i, i + 1);
        cOut = '';

        /* English Characters ToUpperCase */
        if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9') { change = 1; cOut = c; }
        if (c >= 'A' && c <= 'Z') { cOut = c; }
        if (c == ' ' || c == '-' || c == '_' || c == ',' || c == '.' || c == '(' || c == ')' || c == '/' || c == '\'') { cOut = c; }
        if (c == 'a') { change = 1; cOut = 'A'; }
        if (c == 'b') { change = 1; cOut = 'B'; }
        if (c == 'c') { change = 1; cOut = 'C'; }
        if (c == 'd') { change = 1; cOut = 'D'; }
        if (c == 'e') { change = 1; cOut = 'E'; }
        if (c == 'f') { change = 1; cOut = 'F'; }
        if (c == 'g') { change = 1; cOut = 'G'; }
        if (c == 'h') { change = 1; cOut = 'H'; }
        if (c == 'i') { change = 1; cOut = 'I'; }
        if (c == 'j') { change = 1; cOut = 'J'; }
        if (c == 'k') { change = 1; cOut = 'K'; }
        if (c == 'l') { change = 1; cOut = 'L'; }
        if (c == 'm') { change = 1; cOut = 'M'; }
        if (c == 'n') { change = 1; cOut = 'N'; }
        if (c == 'o') { change = 1; cOut = 'Ο'; }
        if (c == 'p') { change = 1; cOut = 'P'; }
        if (c == 'q') { change = 1; cOut = 'Q'; }
        if (c == 'r') { change = 1; cOut = 'R'; }
        if (c == 's') { change = 1; cOut = 'S'; }
        if (c == 't') { change = 1; cOut = 'T'; }
        if (c == 'u') { change = 1; cOut = 'U'; }
        if (c == 'v') { change = 1; cOut = 'V'; }
        if (c == 'w') { change = 1; cOut = 'W'; }
        if (c == 'x') { change = 1; cOut = 'X'; }
        if (c == 'y') { change = 1; cOut = 'Y'; }
        if (c == 'z') { change = 1; cOut = 'Z'; }

        /* Greek Characters ToUpperCase */
        if (c >= 'Α' && c <= 'Ω') { cOut = c; }
        if (c == 'Ά' || c == 'α' || c == 'ά') { change = 1; cOut = 'Α'; }
        if (c == 'β') { change = 1; cOut = 'Β'; }
        if (c == 'γ') { change = 1; cOut = 'Γ'; }
        if (c == 'δ') { change = 1; cOut = 'Δ'; }
        if (c == 'Έ' || c == 'ε' || c == 'έ') { change = 1; cOut = 'Ε'; }
        if (c == 'ζ') { change = 1; cOut = 'Ζ'; }
        if (c == 'Ή' || c == 'η' || c == 'ή') { change = 1; cOut = 'Η'; }
        if (c == 'θ') { change = 1; cOut = 'Θ'; }
        if (c == 'Ί' || c == 'Ϊ' || c == 'ι' || c == 'ί' || c == 'ϊ' || c == 'ΐ') { change = 1; cOut = 'Ι'; }
        if (c == 'κ') { change = 1; cOut = 'Κ'; }
        if (c == 'λ') { change = 1; cOut = 'Λ'; }
        if (c == 'μ') { change = 1; cOut = 'Μ'; }
        if (c == 'ν') { change = 1; cOut = 'Ν'; }
        if (c == 'ξ') { change = 1; cOut = 'Ξ'; }
        if (c == 'Ό' || c == 'ο' || c == 'ό') { change = 1; cOut = 'Ο'; }
        if (c == 'π') { change = 1; cOut = 'Π'; }
        if (c == 'ρ') { change = 1; cOut = 'Ρ'; }
        if (c == 'σ' || c == 'ς') { change = 1; cOut = 'Σ'; }
        if (c == 'τ') { change = 1; cOut = 'Τ'; }
        if (c == 'Ύ' || c == 'Ϋ' || c == 'υ' || c == 'ύ' || c == 'ϋ' || c == 'ΰ') { change = 1; cOut = 'Υ'; }
        if (c == 'φ') { change = 1; cOut = 'Φ'; }
        if (c == 'χ') { change = 1; cOut = 'Χ'; }
        if (c == 'ψ') { change = 1; cOut = 'Ψ'; }
        if (c == 'Ώ' || c == 'ω' || c == 'ώ') { change = 1; cOut = 'Ω'; }
        if (cOut == '') { change = 1; }

        StrOut = StrOut + cOut;
    };

    if (change == 1) {
        if (s.isASPxClientControl)
            s.SetText(StrOut);
        else
            s.value = StrOut;
    }
    return;
}

Imis.Lib.ToUpperOnlyLetters = function(s, e) {
    var change = 0;
    var Str;

    if (s.isASPxClientControl)
        Str = s.GetText();
    else
        Str = s.value;

    var StrL = Str.length;
    var StrOut = "";

    var c = ' ';
    var cOut = ' ';

    for (var i = 0; i < StrL; i++) {
        c = Str.substring(i, i + 1);
        cOut = '';

        /* English Characters ToUpperCase */
        if (c >= 'A' && c <= 'Z') { cOut = c; }
        if (c == ' ' || c == '-' || c == '_' || c == ',' || c == '.' || c == '(' || c == ')' || c == '/') { cOut = c; }
        if (c == 'a') { change = 1; cOut = 'A'; }
        if (c == 'b') { change = 1; cOut = 'B'; }
        if (c == 'c') { change = 1; cOut = 'C'; }
        if (c == 'd') { change = 1; cOut = 'D'; }
        if (c == 'e') { change = 1; cOut = 'E'; }
        if (c == 'f') { change = 1; cOut = 'F'; }
        if (c == 'g') { change = 1; cOut = 'G'; }
        if (c == 'h') { change = 1; cOut = 'H'; }
        if (c == 'i') { change = 1; cOut = 'I'; }
        if (c == 'j') { change = 1; cOut = 'J'; }
        if (c == 'k') { change = 1; cOut = 'K'; }
        if (c == 'l') { change = 1; cOut = 'L'; }
        if (c == 'm') { change = 1; cOut = 'M'; }
        if (c == 'n') { change = 1; cOut = 'N'; }
        if (c == 'o') { change = 1; cOut = 'Ο'; }
        if (c == 'p') { change = 1; cOut = 'P'; }
        if (c == 'q') { change = 1; cOut = 'Q'; }
        if (c == 'r') { change = 1; cOut = 'R'; }
        if (c == 's') { change = 1; cOut = 'S'; }
        if (c == 't') { change = 1; cOut = 'T'; }
        if (c == 'u') { change = 1; cOut = 'U'; }
        if (c == 'v') { change = 1; cOut = 'V'; }
        if (c == 'w') { change = 1; cOut = 'W'; }
        if (c == 'x') { change = 1; cOut = 'X'; }
        if (c == 'y') { change = 1; cOut = 'Y'; }
        if (c == 'z') { change = 1; cOut = 'Z'; }

        /* Greek Characters ToUpperCase */
        if (c >= 'Α' && c <= 'Ω') { cOut = c; }
        if (c == 'Ά' || c == 'α' || c == 'ά') { change = 1; cOut = 'Α'; }
        if (c == 'β') { change = 1; cOut = 'Β'; }
        if (c == 'γ') { change = 1; cOut = 'Γ'; }
        if (c == 'δ') { change = 1; cOut = 'Δ'; }
        if (c == 'Έ' || c == 'ε' || c == 'έ') { change = 1; cOut = 'Ε'; }
        if (c == 'ζ') { change = 1; cOut = 'Ζ'; }
        if (c == 'Ή' || c == 'η' || c == 'ή') { change = 1; cOut = 'Η'; }
        if (c == 'θ') { change = 1; cOut = 'Θ'; }
        if (c == 'Ί' || c == 'Ϊ' || c == 'ι' || c == 'ί' || c == 'ϊ' || c == 'ΐ') { change = 1; cOut = 'Ι'; }
        if (c == 'κ') { change = 1; cOut = 'Κ'; }
        if (c == 'λ') { change = 1; cOut = 'Λ'; }
        if (c == 'μ') { change = 1; cOut = 'Μ'; }
        if (c == 'ν') { change = 1; cOut = 'Ν'; }
        if (c == 'ξ') { change = 1; cOut = 'Ξ'; }
        if (c == 'Ό' || c == 'ο' || c == 'ό') { change = 1; cOut = 'Ο'; }
        if (c == 'π') { change = 1; cOut = 'Π'; }
        if (c == 'ρ') { change = 1; cOut = 'Ρ'; }
        if (c == 'σ' || c == 'ς') { change = 1; cOut = 'Σ'; }
        if (c == 'τ') { change = 1; cOut = 'Τ'; }
        if (c == 'Ύ' || c == 'Ϋ' || c == 'υ' || c == 'ύ' || c == 'ϋ' || c == 'ΰ') { change = 1; cOut = 'Υ'; }
        if (c == 'φ') { change = 1; cOut = 'Φ'; }
        if (c == 'χ') { change = 1; cOut = 'Χ'; }
        if (c == 'ψ') { change = 1; cOut = 'Ψ'; }
        if (c == 'Ώ' || c == 'ω' || c == 'ώ') { change = 1; cOut = 'Ω'; }
        if (cOut == '') { change = 1; }

        StrOut = StrOut + cOut;
    };

    if (change == 1) {
        if (s.isASPxClientControl)
            s.SetText(StrOut);
        else
            s.value = StrOut;
    }
    return;
}

Imis.Lib.NoGreekCharacters = function(elem, upperCase) {
    var change = 0;
    var Str = elem.value;
    var StrL = Str.length;
    var StrOut = "";

    var c = ' ';
    var cOut = ' ';

    for (var i = 0; i < StrL; i++) {
        c = Str.substring(i, i + 1);
        cOut = '';

        if ((c >= 'Α' && c <= 'Ω') || (c >= 'α' && c <= 'ω') ||
             c == 'Ά' || c == 'ά' ||
             c == 'Έ' || c == 'ε' || c == 'έ' ||
             c == 'Ί' || c == 'Ϊ' || c == 'ι' || c == 'ί' || c == 'ϊ' || c == 'ΐ' ||
             c == 'Ό' || c == 'ο' || c == 'ό' || c == 'Ο' ||
             c == 'Ύ' || c == 'Ϋ' || c == 'υ' || c == 'ύ' || c == 'ϋ' || c == 'ΰ' ||
             c == 'Ώ' || c == 'ω' || c == 'ώ' ||
             c == ' ') {
            alert('Επιτρέπονται μόνο λατινικοί χαρακτήρες χωρίς κενά!');
            elem.value = StrOut;
            return;
        } else {
            change = 1;
            if (upperCase) {
                cOut = c.toUpperCase();
            } else {
                cOut = c;
            }
        }

        StrOut = StrOut + cOut;
    };

    if (change == 1) elem.value = StrOut;

    return;
}

Imis.Lib.CheckAFM = function(val, e) {    
    if (!e || !e.Value || e.Value == null) return;
    if (e.Value) {
        var afm = e.Value;
        e.IsValid = false;
        if (!afm.match(/^\d{9}$/)) {
            e.IsValid = false;
            return;
        }
        afm = afm.split('').reverse().join('');

        var Num1 = 0;
        for (var iDigit = 1; iDigit <= 8; iDigit++) {
            Num1 += afm.charAt(iDigit) << iDigit;
        }
        e.IsValid = (Num1 % 11) % 10 == afm.charAt(0);
    }
}

Imis.Lib.OnlyCharacters = function(s, e) {
    var noCharacters = new RegExp('[^a-zA-zα-ωΑ-Ω\\s]', 'g');
    var input = new String();
    if (s.GetText)
        input = s.GetText();
    else
        input = s.value;
    input = input.replace(noCharacters, '');
    if (s.SetText)
        s.SetText(input);
    else
        s.value = input;
    if (s.Validate) {
        s.Validate();
    }
}

Imis.Lib.OnlyDigits = function(s, e) {
    var noDigits = new RegExp('\\D', 'g');
    var input = new String();
    if (s.GetText)
        input = s.GetText();
    else
        input = s.value;
    input = input.replace(noDigits, '');
    if (s.SetText)
        s.SetText(input);
    else
        s.value = input;
    if (s.Validate) {
        s.Validate();
    }
}

Imis.Lib.CheckUri = function(s, e) {
    if (!e || !e.value || e.value == null) return;
    var regexp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
    e.isValid = e.value.match(regexp);
}

Imis.Lib.CheckPostalCode = function(s, e) {
    if (!e || !e.value || e.value == null) return;
    var regexp = /^\d{5}$/;
    e.isValid = e.value.match(regexp);
}

Imis.Lib.CheckFixedPhone = function(s, e) {
    if (!e || !e.value || e.value == null) return;
    var regexp = /^2\d{9}$/;
    e.isValid = e.value.match(regexp);
}

Imis.Lib.CheckMobilePhone = function(s, e) {
    if (!e || !e.value || e.value == null) return;
    var regexp = /^69\d{8}$/;
    e.isValid = e.value.match(regexp);
}

Imis.Lib.CheckPhone = function(s, e) {
    if (!e || !e.value || e.value == null) return;
    var regexp = /^2\d{9}|69\d{8}$/;
    e.isValid = e.value.match(regexp);
}

Imis.Lib.CheckCheckbox = function(s, e) {
    var cb = document.getElementById(s.checkboxToValidate);
    e.IsValid = cb.checked;
    ValidatorUpdateDisplay(s);
}

Imis.Lib.DisableGreekKeyPress = function(e) {
    e = e || window.event || {};
    var charCode = e.keyCode || e.charCode || 0;
    //it's 902 actually but just in case we miss something we make it 900
    var isValid = charCode < 900;
    if (!isValid) {
        $.prompt('Παρακαλώ χρησιμοποιήστε μόνο λατινικούς χαρακτήρες.');
    }
    return isValid;
}

Imis.Lib.ValidateDateTime = function(s, e) {

    var inputDate = Date.parseLocale(e.Value.substr(0, e.Value.length - 6));
    var x = e.Value.substr(e.Value.length - 5, e.Value.length - 3);
    inputDate.setHours(x.split(/:/)[0]);
    inputDate.setMinutes(x.split(/:/)[1]);
    if (inputDate > new Date()) {
        e.IsValid = false;
    }
}

Imis.Lib.CheckEmail = function(s, e) {
    if (e.value == null) return;
    var regexp = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
    e.isValid = e.value.match(regexp);
}

function RemoveTags(Obj) {
    var Str = Obj.value;
    var change = 0;
    var StrL = Str.length;
    var StrOut = "";

    var c = ' ';
    var cOut = ' ';

    for (var i = 0; i < StrL; i++) {
        c = Str.substring(i, i + 1);
        cOut = c;

        if (c == '<' || c == '>') { change = 1; cOut = ''; }

        StrOut = StrOut + cOut;
    };

    if (change == 1)
        Obj.value = StrOut;

    return;
}

function getDimensions(elementId) {
    var elHeight, elWidth;
    var element = document.getElementById(elementId);
    if (element) {
        elHeight = element.offsetHeight;
        elWidth = element.offsetWidth;
    }
    return {
        width: elWidth,
        height: elHeight
    };
}

function cover(elID) {
    var bounds = Sys.UI.DomElement.getBounds($get(elID));
    var dimension = getDimensions(elID);
    var cover = $get('postBackCover');
    Sys.UI.DomElement.setLocation(cover, bounds.x, bounds.y);
    cover.style.width = (dimension.width - 1) + 'px';
    cover.style.height = (dimension.height - 1) + 'px';
    cover.style.display = 'block';
}

function keydownEnterHandler(e, callBackFunction) {
    var keynum

    if (window.event) { // IE
        keynum = e.keyCode
    } else if (e.which) { // Netscape/Firefox/Opera
        keynum = e.which
    }

    if (keynum == 13) {
        e.returnValue = false;
        e.cancel = true;
        if (typeof callBackFunction != 'undefined')
            window.setTimeout(callBackFunction, 0);
    }
}

Imis.Lib.EnterHandler = function(e, callback) {    
    e = e || window.event || {};
    var charCode = e.keyCode || e.charCode || 0;
    if (Sys.UI.Key.enter == charCode) {
        callback();
        if (e.stopPropagation) {
            e.stopPropagation();
        } else {
            e.cancelBubble = true;
        }
    }
}