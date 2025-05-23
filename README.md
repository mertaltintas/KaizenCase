# KaizenCase

Proje içerisinde KaizenCase isimli solution altında iki farklı .NET 8 Console uygulaması yer almaktadır. 

- `KaizenCase1`: 8 haneli algoritmik kampanya kodları üreten uygulama
- `KaizenCase2`: Fiş tarama sistemi için response.json dosyasının uygun şekilde text'e dönüştüren uygulama

## KaizenCase1  (Kampanya kodları)
Çalıştırmak için adımlar  :  
-`git clone https://github.com/mertaltintas/KaizenCase.git`   
-`cd KaizenCase`   
-`dotnet run --project KaizenCase1`

Bu case'deki amaç algoritmik olarak 8 haneli kampanya kodlarının oluşturulması ve aynı algoritmayla bu kodların geçerliliğinin sağlanabilmesiydi.    
Kampanya kodlarını oluştururken kodların 6 hanesini random olarak oluşturup, kalan 2 hanesini ise bu 6 hanelik random değerin index karşılıklarının belli bir denkleme göre işlenip elde edilen çıktı ile oluşturdum.       
6 haneli random kod : RRRRRR      
Belirlenmiş bir denklem ile bu 6 hanenin charSet'deki indexleri üzerinden elde edilen iki farklı char   X   Y   
son olarak   X ve Y   4. ve 8. karaktere konularak kod tamamlanır   >    CODE : RRRXRRRY

Denklem algoritması açıklaması : 
6 haneli random kodun her bir karakterinin sırasıyla charSet'deki indexlerinin karşılığının bir fazlası ile koddaki indexinin 1 fazlası birbiriyle çarpılır.
Her karakter için elde edilen bi çarpım birbiriyle toplanır.   sum += (randomIndex + 1) * (i + 1);

[4]. karakter X =  toplamın 101 fazlasının charsete göre modu    (sum + 101) % codeChars.Length               
[8]. karakter Y = toplamın yarısının charsete göre modu  ((sum / 2)) % codeChars.Length

CheckCode kısmında aynı işlemler ile 4. ve 8. karakterin sağlaması yapılır ve kod valid kabul edilir.

SONUÇ : Tahmin edilemez bir algoritmayla kodların geçerliliği sağlanmış oldu.
6 Adet random kullanılarak kodların unique'liği büyük ölçüde sağlandı.

## KaizenCase2 (OCR Response Parse)
Çalıştırmak için adımlar  :  
-`git clone https://github.com/mertaltintas/KaizenCase.git`   
-`cd KaizenCase`   
-`dotnet run --project KaizenCase2`

Case'deki amaç response.json'daki itemlerin hangi satıra ait olduğunun tespit edilmesiydi.     
Her bir itemin vertice koordinatlarının sırayla sol üst, sağ üst, sağ alt, sol alt olduğunu anladım.    
İlk karşılaşılan problem aynı satırsaki itemlerin tepe noktalarının koordinatlarının lineer bir x eksenine oturmamasıydı.     
Diğer bir problem ise her bir itemin fiş görselindeki sırayla gelmemesi.       
Bu iki problem göz önünde bulundurularak her bir itemin sırasıyla yukardan aşağı hangi satıra denk geldiği tespit edildi.        
Bunu tespit ederken de yeni satıra yerleştirilen en son itemin sağ kenarındaki y ekseni ortalaması (sağ üst y ~ sağ alt y) ve y yüksekliği, o satırın mevcut eksen ve yükselik özelliği olarak kabul edildi.        
Eklenecek sıradaki item'in sol eksen ve yüksekliği de bu değerler ile karşılaştırılarak o satıra ait olup olmadığı tespit edildi.        
Bu karşılaştırmadaki satır eksen farkı, itemin yüksekliğinin en az %80inden küçük ise çok yüksek ihtimalle aynı satırdadır olarak kabul edildi.         
Karşılaştırırken %20lik bir pay bırakmak istedim çünkü fişteki görselden kaynaklı yükseklik ve eksen farkı aynı satırda olmasına rağmen birebir ölçüşmeyebilir.   


**Mert Altıntaş**  
[GitHub](https://github.com/mertaltintas)
