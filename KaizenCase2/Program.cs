using KaizenCase2.Models;
using System.Text.Json;

string filePath = Path.Combine("Data", "response.json");

string response = File.ReadAllText(filePath);

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var responseItems = JsonSerializer.Deserialize<List<Item>>(response, options);

string result = string.Empty;

List<LineItem> lineItems = new List<LineItem>();

foreach (var responseItem in responseItems)
{
    if (!string.IsNullOrEmpty(responseItem.Locale))
        continue;

    var avarageY = ((responseItem.BoundingPoly.Vertices[0].Y + responseItem.BoundingPoly.Vertices[3].Y) / 2.0);

    if (!lineItems.Any())
    {
        lineItems.Add(new LineItem()
        {
            Line = 1,
            LineText = responseItem.Description + " ",
            LineYAverage = ((responseItem.BoundingPoly.Vertices[2].Y + responseItem.BoundingPoly.Vertices[1].Y) / 2.0),
            LineHeight = (responseItem.BoundingPoly.Vertices[2].Y - responseItem.BoundingPoly.Vertices[1].Y)
        });
        continue;
    }

    foreach (var lineItem in lineItems)
    {
        if ((avarageY - lineItem.LineYAverage) < (lineItem.LineHeight * 0.8))
        {
            lineItem.LineText += responseItem.Description + " ";
            lineItem.LineYAverage = ((responseItem.BoundingPoly.Vertices[2].Y + responseItem.BoundingPoly.Vertices[1].Y) / 2.0);
            lineItem.LineHeight = (responseItem.BoundingPoly.Vertices[2].Y - responseItem.BoundingPoly.Vertices[1].Y);
        }
        else
        {
            if (lineItems.Count > lineItem.Line)
                continue;

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

Console.WriteLine("line | text");
Console.WriteLine("");

for (int i = 0; i < lineItems.Count; i++)
    Console.WriteLine("{0,2}     {1}", i + 1, lineItems[i].LineText);


Console.ReadLine();