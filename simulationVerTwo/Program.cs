using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace simulationVerTwo
{
    class Program
    {
        //variables
        public static int[,] path = new int[16, 32]; //consist of our cell
        public static int[,] tempPath = new int[16, 32];//değişen kısımları buraya aktarıp kontrolden sonra path dizisine değişmiş halini aktaracağız
        public static int steps = 0;//adım sayısını tutmak için oluşturuldu
        public static int[,] Q = new int[3, 3];
        public static int[,] W = new int[3, 3];
        public static int[,] E = new int[3, 3];
        public static int[,] R = new int[3, 3];
        public static ConsoleKeyInfo myKey;
        public static int xCursor = 4;//setcursor positionunu belirliyor
        public static int yCursor = 8;//setcursor positionunu belirliyor
        public static Random rnd = new Random();//random sayı oluşturmak için oluşturuldu
        //end of veriables
        static void Main(string[] args)
        {
            fillInPath();//yolu başlangıçda noktalar ile dolduruyoruz
            creatingQWE();
            //for R-Random
            creatingRandom();//random kısmı oluşturduk sonrakı randomları R harfi ile oluşturabilirsiniz
            drawFrame();//çerçeve çizmek için fonksiyonumuzu çağırıyoruz

            drawRightSide();//sağ kısmı yazdırıyoruz
            drawingTwoDimensionMatrix(4, 8, path, 0);//yolumuzu yani simulasyon alanını çizdirmek için kullanıldı.
            while (true)//simulasyon hiç bitmeyeceği için sürekli dönmesini sağlıyoruz
            {
                Console.SetCursorPosition(yCursor, xCursor);
                myKey = Console.ReadKey(true);//bastığımız tuşun görünürlüğünü kapattık(true yazarak)
                switch (myKey.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (yCursor < 69)
                        {
                            yCursor += 2;
                            Console.SetCursorPosition(yCursor, xCursor);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (yCursor > 8)
                        {
                            yCursor -= 2;
                            Console.SetCursorPosition(yCursor, xCursor);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (xCursor > 4)
                        {
                            xCursor--;
                            Console.SetCursorPosition(yCursor, xCursor);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (xCursor < 19)
                        {
                            xCursor++;
                            Console.SetCursorPosition(yCursor, xCursor);
                        }
                        break;
                    case ConsoleKey.Q:
                        drawingTwoDimensionMatrix(xCursor, yCursor, Q, 1);//ekrana yazdırmak ve path dizisine işlemek için bu produce'u kullandık
                        break;
                    case ConsoleKey.W:
                        drawingTwoDimensionMatrix(xCursor, yCursor, W, 1);//ekrana yazdırmak ve path dizisine işlemek için bu produce'u kullandık
                        break;
                    case ConsoleKey.E://E blogunu yerleştirir
                        drawingTwoDimensionMatrix(xCursor, yCursor, E, 1);//ekrana yazdırmak ve path dizisine işlemek için bu produce'u kullandık
                        break;
                    case ConsoleKey.R:
                        drawingTwoDimensionMatrix(xCursor, yCursor, R, 1);//ekrana yazdırmak ve path dizisine işlemek için bu produce'u kullandık
                        break;
                    case ConsoleKey.Y://randomu rasgele yerleştirme
                        creatingRandom();//tekrar random kısmını oluşturuyoruz
                        int randomX = rnd.Next(4, 18);//X konumu için rastgele bir sayı oluşturuyoruz(4-18 arası)
                        int randomY;
                        do
                        {
                            randomY = rnd.Next(8, 67);
                        } while (randomY % 2 == 1);//Y'nin çift sayılar gelmesi gerekiyor çünkü arada boşluk bıraktık.
                        drawingTwoDimensionMatrix(randomX, randomY, R, 1);//ekrana yazdırmak için
                        break;
                    case ConsoleKey.T:
                        creatingRandom();//random kısmını oluşturuyoruz
                        break;
                    case ConsoleKey.Spacebar:
                        steps++;//adımı arttırıyoruz
                        execSimulation();//simulasyonu çalıştırıyoruz
                        drawRightSide();//sağ kısmı yazdırıyoruz
                        drawingTwoDimensionMatrix(4, 8, path, 0);//yolumuzu yani simulasyon alanını çizdirmek için kullanıldı.
                        break;
                    case ConsoleKey.D0:
                        fillInPath();//diziyi temizledik
                        drawingTwoDimensionMatrix(4, 8, path, 0);//diziyi ekrana yazdırdık
                        break;
                    case ConsoleKey.NumPad0://numpad kısmından 0'a basarsa çalışsın diye bu kısmı ekledik
                        fillInPath();//diziyi temizledik
                        drawingTwoDimensionMatrix(4, 8, path, 0);//diziyi ekrana yazdırdık
                        break;
                    case ConsoleKey.D1://Q'yu dondur
                        rotatingMatrix(Q);
                        break;
                    case ConsoleKey.NumPad1://Q'yu dondur numpad kısmı 1 ise
                        rotatingMatrix(Q);
                        break;
                    case ConsoleKey.D2://W'yi döndür
                        rotatingMatrix(W);
                        break;
                    case ConsoleKey.NumPad2://W'yi döndür numpad kısmı 2 ise
                        rotatingMatrix(W);
                        break;
                    case ConsoleKey.D3://Tek bir hücre siler
                        path[(xCursor - 4), (yCursor - 8) / 2] = 46;
                        Console.Write(Convert.ToChar(46));
                        break;
                    case ConsoleKey.NumPad3://Tek bir hücre siler numpad kısmı 3 ise
                        path[(xCursor - 4), (yCursor - 8) / 2] = 46;
                        Console.Write(Convert.ToChar(46));
                        break;
                    case ConsoleKey.D4://R'yi döndürür
                        rotatingMatrix(R);
                        break;
                    case ConsoleKey.NumPad4://R'yi döndürür numpad kısmı 4 ise
                        rotatingMatrix(R);
                        break;
                    default:
                        break;
                }

                drawRightSide();//sağ bölümü yazdırıyoruz
            }
        }
        //alt kısımda fonksiyonlar bulunur
        public static void drawFrame()//çerçeve kısmını çizdiriyoruz
        {
           // Console.SetCursorPosition(20,0);

            Console.SetCursorPosition(6, 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("╔");
            for (int i = 0; i < 65; i++)
            {
                Console.Write("═");
            }
            Console.Write("╗");

            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(6, 4 + i);
                Console.Write("║");
                for (int j = 0; j < 65; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("║");
            }
            Console.SetCursorPosition(6, 20);
            Console.Write("╚");
            for (int i = 0; i < 65; i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");
            Console.SetCursorPosition(75,3);
            Console.Write("╔══════════════╗");
            Console.SetCursorPosition(75,4);
            Console.Write("║              ║");
            Console.SetCursorPosition(75,5);
            Console.Write("║══════════════║");
            Console.SetCursorPosition(75, 6);
            Console.Write("║              ║");
            Console.SetCursorPosition(75, 7);
            Console.Write("╚══════════════╝");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void fillInPath()//yolu nokta ile doldurur (0 için de bu fonksiyonu kullanacağız)
        {
            for (int i = 0; i < path.GetLength(0); i++)
            {
                for (int j = 0; j < path.GetLength(1); j++)
                {
                    path[i, j] = 46;//default values of the path are 46.(46 ascii kod üzerinde noktaya denk gelir)
                }
            }
        }
        //----------------------------------------------------------------
        public static void drawingTwoDimensionMatrix(int cellX, int cellY, int[,] matrix,int isOr)//iki boyutlu dizileri çizdirmek için kullanılır(ilk ve ikinci degerler x,y konumu ücüncü deger dizimiz,son deger eğer 1 ise or işlemi yapar 0 ise yapmadan yazar)
        {
            int startPointX = 0;//bu değişken köşelere gelen matrixlerin duruma göre yazdırmak stillerini değiştirmek için kullanılmıştır
            int startPointY = 0;//bu değişken köşelere gelen matrixlerin duruma göre yazdırmak stillerini değiştirmek için kullanılmıştır
            int endPointX = matrix.GetLength(0);//bu değişken varsayılan olarak matrix boyutuna göre ayarlanmıştır duruma göre değişecektir
            int endPointY = matrix.GetLength(1);//bu değişken varsayılan olarak matrix boyutuna göre ayarlanmıştır duruma göre değişecektir
            if (cellX > 4 && cellX < 19 && cellY > 8 && cellY < 70 && isOr == 1)//sınırları taşmasını engelledik
            {
                cellX -= 1;
                cellY -= 2;
            }
            else if (cellX == 4&&cellY==8 &&isOr == 1)//sol üst köşe yerleştirme kontrolü
            {
                startPointX = 1;
                startPointY = 1;
            }
            else if (cellX == 4 && cellY == 70&& isOr == 1)//sağ üst köşe yerleştirme kontrolü
            {
                cellY -= 2;
                startPointX = 1;
                endPointY = 2;
            }
            else if (cellX == 19 && cellY == 8 && isOr == 1)//sol alt köşe yerleştirme kontrolü
            {
                cellX -= 1;
                startPointY = 1;
                endPointX = 2;
            }
            else if (cellX == 19 && cellY == 70 && isOr == 1)//sağ alt köşe yerleştirme kontrolü
            {
                cellX -= 1;
                cellY -= 2;
                endPointX = 2;
                endPointY = 2;
            }
            else if (cellX == 4&& isOr == 1)//üst yerleştirme kontrolü
            {
                cellY -= 2;
                startPointX = 1;
            }
            else if (cellX > 4 && cellY == 8&&isOr == 1)//sağ yerleştirme kontrolü
            {
                cellX -= 1;
                startPointY = 1;
            }
            else if (cellX > 4 && cellY == 70 && isOr == 1)//sol yerleştirme kontrolü
            {
                cellX -= 1;
                cellY -= 2;
                endPointY = 2;
            }
            else if (cellX == 19 && isOr == 1)//alt yerleştirme kontrolü
            {
                cellX -= 1;
                cellY -= 2;
                endPointX = 2;
            }

            int firstcellY = cellY;
            for (int i = startPointX; i < endPointX; i++)
            {
                for (int j = startPointY; j < endPointY; j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (isOr == 1)
                    {
                        Console.SetCursorPosition(cellY, cellX);
                        Console.Write(Convert.ToChar(path[cellX - 4, (cellY - 8) / 2] | matrix[i, j])); //or işlemini yapıyoruz
                        path[cellX - 4, (cellY - 8) / 2] = path[cellX - 4, (cellY - 8) / 2] | matrix[i, j];//yapılan işlemleri yola işledik
                    }
                    else
                    {
                        Console.SetCursorPosition(cellY, cellX);
                        Console.Write(Convert.ToChar(matrix[i, j]));
                    }
                    cellY += 2;
                }
                cellY = firstcellY;
                cellX++;
            }
        }
        //----------------------------------------------------------------
        public static void creatingQWE()//QWER matrislerin içeriği oluşturulmuştur
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Q[i, j] = 46;
                    W[i, j] = 46;
                    E[i, j] = 46;
                }
            }
            //for Q
            for (int i = 0; i < Q.GetLength(0); i++)//Q'nun içerisinde dikey şekilde o'ları yerleştiriyorum
            {
                Q[i, 1] = 111;
            }
            //for W 
            W[1, 0] = 111;
            W[2, 1] = 111;
            for (int i = 0; i < W.GetLength(0); i++)
            {
                W[i, 2] = 111;
            }
            //for E
            E[1, 1] = 111;
            
        }
        //----------------------------------------------------------------
        public static void creatingRandom()//Random matrix'in içeriği oluşturulmuştur
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    R[i, j] = 46;
                }
            }
            int random = rnd.Next(4, 7);//canlı hücre sayısını (4,7) arası rastgele oluşturuyoruz
            int randomX;
            int randomY;
            for (int i = 0; i < random; i++)
            {
                do
                {
                    randomX = rnd.Next(0, 3);//matrix'in içerisinde x pozisyonunu belirliyoruz
                    randomY = rnd.Next(0, 3);//matrix'in içerisinde y pozisyonunu belirliyoruz
                    if (R[randomX, randomY] == 46)//eğer canlı hücrenin konulacağı yerde nokta(ölü hücre) varsa öyle koymasını istiyoruz
                    {
                        R[randomX, randomY] = 111;
                        break;
                    }
                    
                    
                } while (true);
            }
        }
        //----------------------------------------------------------------
        public static void drawRightSide()//drawing the right side on screen(steps,live,Q,W,R)
        {
            int cellX = 4, cellY = 77;
            Console.SetCursorPosition(cellY, cellX);
            Console.Write("Steps: " + steps);
            cellX += 2;
            Console.SetCursorPosition(cellY, cellX);
            Console.Write("Live: " + countOfLivesCell()+"    ");
            cellX += 3;
            Console.SetCursorPosition(cellY, cellX + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Q");
            Console.SetCursorPosition(cellY, cellX + 5);
            Console.Write("W");
            Console.SetCursorPosition(cellY, cellX + 9);
            Console.Write("R");
            Console.SetCursorPosition(cellY, cellX + 13);
            Console.Write("E");
            Console.ForegroundColor = ConsoleColor.Gray;
            cellY += 3;
            drawingTwoDimensionMatrix(cellX, cellY, Q,0);
            cellX += 4;
            drawingTwoDimensionMatrix(cellX, cellY, W,0);
            cellX += 4;
            drawingTwoDimensionMatrix(cellX, cellY, R,0);
            cellX += 4;
            drawingTwoDimensionMatrix(cellX, cellY, E, 0);
        }
        //----------------------------------------------------------------
        public static void rotatingMatrix(int[,] matrix)//matrisleri döndürmek için kullanıldı
        {
            int[,] tempMatrix = new int[3,3];//döndürme işlemi için gecici bir matrix oluşturduk
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tempMatrix[i, j] = 46;//gecici matrixin içerisini nokta ile doldurduk
                }
            }
            int k = 0;
            for (int i = 2; i >= 0; i--)
            {
                for (int j = 0; j < 3; j++)
                {
                    tempMatrix[j,i] = matrix[k, j];
                }
                k++;
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = tempMatrix[i, j];//gecici matrixden normal matriximize aktarıyoruz
                }
            }
        }
        //-------------------------------------------
        public static int countOfNeighbours(int cellX,int cellX2,int cellY,int cellY2)//komşuları saydırmak için kullanıldı ve 4 değer alır(cellx başlangıç cellx2 bitiş noktası aynı şey y içinde geçerli)
        {
            int liveNeighbours = 0;
            for (int i = cellX; i <= cellX2; i++)//yaşayan komşuları saydıracağız
            {
                for (int j = cellY; j <= cellY2; j++)
                {
                    if (path[i, j] == 111)//eğer o hücre yaşıyorsa +1 arttır.
                    {
                        liveNeighbours++;
                    }
                }
            }
            return liveNeighbours;
        }
        //-------------------------------------------
        public static void controlOfRules(int cellX,int cellY,int liveNeighbours)//kuralları kontrol ettiriyoruz(hangisi ölecek hangisi kalacak bunlara karar veriyoruz)
        {
            if (path[cellX, cellY] == 46 && liveNeighbours == 3)//eğer hücre ölü ve 3 tane komşusu yaşıyorsa hücre canlanır
            {
                tempPath[cellX, cellY] = 111;
            }
            else if (path[cellX, cellY] == 111 && liveNeighbours == 3 || path[cellX, cellY] == 111 && liveNeighbours == 2)//hücre canlıysa ve 2 veya 3 komşusu varsa yaşamaya devam eder
            {
                tempPath[cellX, cellY] = 111;
            }
            else if (path[cellX, cellY] == 111 && liveNeighbours < 2)//ikiden az komşusu varsa yaşayan hücre ölür
            {
                tempPath[cellX, cellY] = 46;
            }
            else if (path[cellX, cellY] == 111 && liveNeighbours > 3)//3'den fazla komşusu olan yaşayan hücre ölür
            {
                tempPath[cellX, cellY] = 46;
            }
            
        }
        //-------------------------------------------
        public static void execSimulation()
        {
            for (int i = 0; i < tempPath.GetLength(0); i++)//geçici olarak path dizisini tutacağımız dizinin içeriğini 46(nokta) ile dolduruyoruz
            {
                for (int j = 0; j < tempPath.GetLength(1); j++)
                {
                    tempPath[i, j] = 46;
                }
            }
            for (int i = 0; i < path.GetLength(0); i++)//path içerisinde kontrollere başlıyoruz
            {
                for (int j = 0; j < path.GetLength(1); j++)
                {
                    int liveNeighbours = 0;//komşu sayısını tutuyoruz
                    if (i > 0 && j > 0 && i < 15 && j < 31)//orta kısımdaki kontroller
                    {
                        liveNeighbours = countOfNeighbours(i - 1, i + 1, j - 1, j + 1);//komşuları saydırmak için fonksiyonu kullanıyoruz
                        if (path[i, j] == 111)//eğer kontrol edilen hücre canlı ise komşuların içinden çıkarmamız gerekiyor bu yüzden bunu kullandık
                        {
                            liveNeighbours--;
                        }
                        controlOfRules(i, j, liveNeighbours);//gerekli kurallar kontrol ettiriliyor buna göre hücrelerin durumu belli olacaktır.
                    }
                    else if (i == 0&&j==0)//başlangıç noktası(0,0) kısmındaki kontrol
                    {
                        liveNeighbours = countOfNeighbours(i, i + 1, j, j + 1);//komşuları saydırmak için fonksiyonu kullanıyoruz
                        if (path[i, j] == 111)//eğer kontrol edilen hücre canlı ise komşuların içinden çıkarmamız gerekiyor bu yüzden bunu kullandık
                        {
                            liveNeighbours--;
                        }
                        controlOfRules(i, j, liveNeighbours);//gerekli kurallar kontrol ettiriliyor buna göre hücrelerin durumu belli olacaktır.
                    }
                    else if (i==0&&j>0&&j<30)//i'nin sıfır olduğu durumların kontrolü(birinci kolon)
                    {
                        liveNeighbours = countOfNeighbours(i, i + 1, j-1, j + 1);//komşuları saydırmak için fonksiyonu kullanıyoruz
                        if (path[i, j] == 111)//eğer kontrol edilen hücre canlı ise komşuların içinden çıkarmamız gerekiyor bu yüzden bunu kullandık
                        {
                            liveNeighbours--;
                        }
                        //---------------------------
                        controlOfRules(i, j, liveNeighbours);//gerekli kurallar kontrol ettiriliyor buna göre hücrelerin durumu belli olacaktır.
                    }
                    else if (j == 0 && i > 0 && i < 15)//j'nin sıfır olduğu kısım(ilk satır)
                    {
                        liveNeighbours = countOfNeighbours(i-1, i + 1, j, j + 1);//komşuları saydırmak için fonksiyonu kullanıyoruz
                        if (path[i, j] == 111)//eğer kontrol edilen hücre canlı ise komşuların içinden çıkarmamız gerekiyor bu yüzden bunu kullandık
                        {
                            liveNeighbours--;
                        }
                        //---------------------------
                        controlOfRules(i, j, liveNeighbours);//gerekli kurallar kontrol ettiriliyor buna göre hücrelerin durumu belli olacaktır.
                    }
                    else if (i==15&& j > 0 && j < 30)//son kolon
                    {
                        liveNeighbours = countOfNeighbours(i-1, i, j - 1, j + 1);//komşuları saydırmak için fonksiyonu kullanıyoruz
                        if (path[i, j] == 111)//eğer kontrol edilen hücre canlı ise komşuların içinden çıkarmamız gerekiyor bu yüzden bunu kullandık
                        {
                            liveNeighbours--;
                        }
                        //---------------------------
                        controlOfRules(i, j, liveNeighbours);//gerekli kurallar kontrol ettiriliyor buna göre hücrelerin durumu belli olacaktır.
                    }
                    else if (j == 31 && i > 0 && i < 15)//son satırlar
                    {
                        liveNeighbours = countOfNeighbours(i - 1, i+1, j - 1, j);//komşuları saydırmak için fonksiyonu kullanıyoruz
                        if (path[i, j] == 111)//eğer kontrol edilen hücre canlı ise komşuların içinden çıkarmamız gerekiyor bu yüzden bunu kullandık
                        {
                            liveNeighbours--;
                        }
                        //---------------------------
                        controlOfRules(i, j, liveNeighbours);//gerekli kurallar kontrol ettiriliyor buna göre hücrelerin durumu belli olacaktır.
                    }

                }
            }
            for (int i = 0; i < path.GetLength(0); i++)//gerekli değişiklikleri temp'den path'e aktarıyoruz
            {
                for (int j = 0; j < path.GetLength(1); j++)
                {
                    path[i, j] = tempPath[i, j];
                }
            }
        }//similasyon kısmı bu kısımda yapılmıştır
        public static int countOfLivesCell()//tüm alandaki canlı hücre sayısını verir
        {
            int livesCell = 0;
            for (int i = 0; i < path.GetLength(0); i++)
            {
                for (int j = 0; j < path.GetLength(1); j++)
                {
                    if (path[i,j]==111)
                    {
                        livesCell++;
                    }
                }
            }
            return livesCell;
        }
    }
}
