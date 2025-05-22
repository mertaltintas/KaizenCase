Random rnd = new Random(Guid.NewGuid().GetHashCode());
string codeChars = "ACDEFGHKLMNPRTXYZ234579";

GeneratesCodes();
Console.ReadLine();

//1000 adet kod oluşturur ve aynı yöntemle doğrulama yaparak kod geçerliliği algoritmik olarak sağlanır.
//Console ekranına kodları sırasıyla yazdırır
void GeneratesCodes()
{
    for (int i = 0; i < 1000; i++)
    {
        var code = GenerateCode();
        string isValid = CheckCode(code) ? "Valid" : "Invalid";
        Console.WriteLine($"Code: {code} - {isValid}");
    }
}

// Rastgele 6 karakterli kod oluşturur ve 2 karakteri belirli bir denkleme göre ekler
string GenerateCode()
{
    string randomCode = string.Empty;
    int sum = 0;

    //6 karakterli rastgele kod oluşturur
    for (int i = 0; i < 6; i++)
    {
        var randomIndex = rnd.Next(0, codeChars.Length);
        char randomChar = codeChars[randomIndex];

        //belli bir denkleme göre indexleri toplar
        sum += (randomIndex + 1) * (i + 1);
        randomCode += randomChar;
    }

    //6 karakterli random koddan elde edlilen toplamdan 2 karakter için anahtar oluşturur
    int firstKeyIndex = (sum + 101) % codeChars.Length;
    int secondKeyIndex = ((sum / 2)) % codeChars.Length;

    //2 karakteri random kodun 4. ve 8. karakterine ekler
    return $"{randomCode.Substring(0, 3)}" +
           $"{codeChars[firstKeyIndex]}" +
           $"{randomCode.Substring(3, 3)}" +
           $"{codeChars[secondKeyIndex]}";
}

// 8 karakterli kodun algoritmik olarak doğrulanması yapılır.
bool CheckCode(string code)
{
    int sum = 0;
    string randomCode = code.Substring(0, 3) + code.Substring(4, 3);

    for (int i = 0; i < randomCode.Length; i++)
    {
        int charIndex = codeChars.IndexOf(randomCode[i]);

        //belli bir denkleme göre indexleri toplar
        sum += (charIndex + 1) * (i + 1);
    }

    //2 karakter için doğrulama
    int firstKeyIndex = (sum + 101) % codeChars.Length;
    int secondKeyIndex = ((sum / 2)) % codeChars.Length;

    //kodun 4. ve 8. karakteri ile toplamdan elde edilen anahtarların karşılaştırılması
    if (code[3] == codeChars[firstKeyIndex] && code[7] == codeChars[secondKeyIndex])
        return true;

    return false;
}
