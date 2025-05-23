using KaizenCase2.Models;
using System.Text.Json;

//Projedeki mevcut json dosyası okunup uygun bir modelde listeye deserialize ediliyor
string filePath = Path.Combine("Data", "response.json");
string response = File.ReadAllText(filePath);

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var responseItems = JsonSerializer.Deserialize<List<ResponseItem>>(response, options);

string result = string.Empty;

List<LineItem> lineItems = new List<LineItem>();

//reponse listesinin her bir itemi, hangi uygun satıra konulacağının belirlenmesi için işleme alınıyor
foreach (var responseItem in responseItems)
{
    //tüm değerlerin bulunduğu ilk satır ignore ediliyor
    if (!string.IsNullOrEmpty(responseItem.Locale))
        continue;

    //işleme alınan itemin sol kenarının y eksenindeki ortalaması hesaplanıyor
    var avarageY = ((responseItem.BoundingPoly.Vertices[0].Y + responseItem.BoundingPoly.Vertices[3].Y) / 2.0);

    //henüz hiç bir satır dolu değilse ilk satıra ilk eleman ekleniyor.
    if (!lineItems.Any())
    {
        //satıra eklenen elemanın sağ kenarının y eksen ortalaması ve yükseliği,
        //o satırın y ortalaması ve y yüksekliği olarak olarak kabul ediliyor.
        lineItems.Add(new LineItem()
        {
            Line = 1,
            LineText = responseItem.Description + " ",
            LineYAverage = ((responseItem.BoundingPoly.Vertices[2].Y + responseItem.BoundingPoly.Vertices[1].Y) / 2.0),
            LineHeight = (responseItem.BoundingPoly.Vertices[2].Y - responseItem.BoundingPoly.Vertices[1].Y)
        });
        continue;
    }

    //işleme alınan response item, oluşturulan her bir satır ve o satıra ait yükseklik ve eksen ile karşılaştırılıyor.
    foreach (var lineItem in lineItems)
    {
        //aslında satırların sonundaki itemin sağ kenarının ortalama y eksenleri ile işleme alınan itemin sol kenarının y ekseni karşılaştırılmış oluyor.
        //eksenler arasındaki fark mevcut satır yüksekliğinin yaklaşık %80'inden küçükse item mevcut satıra ekleniyor.
        if ((avarageY - lineItem.LineYAverage) < (lineItem.LineHeight * 0.8))
        {
            lineItem.LineText += responseItem.Description + " ";
            lineItem.LineYAverage = ((responseItem.BoundingPoly.Vertices[2].Y + responseItem.BoundingPoly.Vertices[1].Y) / 2.0);
            lineItem.LineHeight = (responseItem.BoundingPoly.Vertices[2].Y - responseItem.BoundingPoly.Vertices[1].Y);
        }
        //eğer değilse bir sonraki satıra ait olmalıdır.
        else 
        {
            //eğer aşağıda daha fazla satır var ise aşağıdakiyle de karşılaştırılmak üzere diğer satıra geçiliyor.
            if (lineItems.Count > lineItem.Line)
                continue;

            //daha fazla item bulunan satır kalmadığına göre bu item yeni bir satır oluşturmalıdır.
            //ve bu satırın yükseklik ve y ekseni ortalaması, satıra eklenen elemana göre belirleniyor.
            lineItems.Add(new LineItem()
            {
                LineText = responseItem.Description + " ",
                LineYAverage = ((responseItem.BoundingPoly.Vertices[2].Y + responseItem.BoundingPoly.Vertices[1].Y) / 2.0),
                LineHeight = (responseItem.BoundingPoly.Vertices[2].Y - responseItem.BoundingPoly.Vertices[1].Y),
                Line = (short)(lineItem.Line + 1)
            });
        }
        break;
    }

}

//Sonuçları konsol ekranına yazdırıyoruz
Console.WriteLine("line | text");
Console.WriteLine("");

for (int i = 0; i < lineItems.Count; i++)
    Console.WriteLine("{0,2}     {1}", i + 1, lineItems[i].LineText);


Console.ReadLine();