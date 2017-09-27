using System;
using System.Collections;

namespace Dijkstra201530161135
{
    class Program
    {
        const int inf = 100000;//定义无穷大inf
        static double[,] QMatrix = new double[12, 12]//Q矩阵
        {
            {inf,0.02,inf,0.01,inf,inf,inf,inf,inf,inf,inf,inf},
            {0.02,inf,0.03,inf,0.05,inf,inf,inf,inf,inf,inf,inf},
            {inf,0.03,inf,inf,inf,0.06,inf,inf,inf,inf,inf,inf},
            {0.01,inf,inf,inf,0.04,inf,0.03,inf,inf,inf,inf,inf},
            {inf,0.05,inf,inf,inf,0.06,inf,0.04,inf,inf,inf,inf},
            {inf,inf,0.06,inf,inf,inf,inf,inf,0.05,inf,inf,inf},
            {inf,inf,inf,0.03,inf,inf,inf,inf,inf,0.02,inf,inf},
            {inf,inf,inf,inf,0.04,inf,0.06,inf,inf,inf,0.01,inf},
            {inf,inf,inf,inf,inf,0.05,inf,0.04,inf,inf,inf,0.01},
            {inf,inf,inf,inf,inf,inf,0.02,inf,inf,inf,0.05,inf},
            {inf,inf,inf,inf,inf,inf,inf,0.01,inf,0.05,inf,0.03},
            {inf,inf,inf,inf,inf,inf,inf,inf,0.01,inf,0.03,inf}
        };
        static double[,] Matrix = new double[12, 12]//权矩阵
        {
            {0,3,inf,2,inf,inf,inf,inf,inf,inf,inf,inf},
            {3,0,2,inf,1.5,inf,inf,inf,inf,inf,inf,inf},
            {inf,2,0,inf,inf,1,inf,inf,inf,inf,inf,inf},
            {2,inf,inf,0,2.5,inf,3,inf,inf,inf,inf,inf},
            {inf,1.5,inf,inf,0,1.5,inf,4,inf,inf,inf,inf},
            {inf,inf,1,inf,inf,0,inf,inf,2,inf,inf,inf},
            {inf,inf,inf,3,inf,inf,0,inf,inf,1,inf,inf},
            {inf,inf,inf,inf,4,inf,0.5,0,inf,inf,3,inf},
            {inf,inf,inf,inf,inf,2,inf,3.5,0,inf,inf,3},
            {inf,inf,inf,inf,inf,inf,1,inf,inf,0,1,inf},
            {inf,inf,inf,inf,inf,inf,inf,3,inf,1,0,3.5},
            {inf,inf,inf,inf,inf,inf,inf,inf,3,inf,3.5,0}
        };
        static double[,] Matrix0 = new double[12, 12]//权矩阵
        {
            {0,3,inf,2,inf,inf,inf,inf,inf,inf,inf,inf},
            {3,0,2,inf,1.5,inf,inf,inf,inf,inf,inf,inf},
            {inf,2,0,inf,inf,1,inf,inf,inf,inf,inf,inf},
            {2,inf,inf,0,2.5,inf,3,inf,inf,inf,inf,inf},
            {inf,1.5,inf,inf,0,1.5,inf,4,inf,inf,inf,inf},
            {inf,inf,1,inf,inf,0,inf,inf,2,inf,inf,inf},
            {inf,inf,inf,3,inf,inf,0,inf,inf,1,inf,inf},
            {inf,inf,inf,inf,4,inf,0.5,0,inf,inf,3,inf},
            {inf,inf,inf,inf,inf,2,inf,3.5,0,inf,inf,3},
            {inf,inf,inf,inf,inf,inf,1,inf,inf,0,1,inf},
            {inf,inf,inf,inf,inf,inf,inf,3,inf,1,0,3.5},
            {inf,inf,inf,inf,inf,inf,inf,inf,3,inf,3.5,0}
        };
        static string[,] ShortestPathSubscriptStringTemp = new string[12, 12];//OD分配的所需路径临时储存矩阵
        static double[,] OdOrigine = new double[12, 12]//OD源数据
        {
            {0,0,300,0,0,0,0,0,0,400,0,500},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {300,0,0,0,0,0,0,0,0,100,0,250},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {400,0,100,0,0,0,0,0,0,0,0,600},
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {500,0,250,0,0,0,0,0,0,600,0,0},
        };
        static double[,] OdDistributeTemp = new double[12, 12];//OD分配缓存
        static double[,] OdDistributedTemp = new double[12, 12];//OD已分配缓存
        static double[,] OdDistributed = new double[12, 12];//OD已分配 
        static int n = 12;//n表示顶点个数
        public static ArrayList S = new ArrayList(n);//S表示已经标号的顶点的集合
        public static ArrayList U = new ArrayList(n);//U表示未标号的顶点的集合     
        public static double[] distance = new double[12];//distance存储每次迭代后的最短路的值     
        public static int[] previousVertex = new int[12];//previousVertex存储上一次的最短路的顶点   
        public static bool[] judgement = new bool[12] { false, false, false, false, false, false, false, false, false, false, false, false };//判断顶点是否已经发生移动
        static void Main(string[] args)//主程序入口
        {
            for (int i = 0; i < n; i++)
            {
                ClearAll();//清除缓存
                FindPath(i);//寻路
                PrintPath(i);//打印
                Console.WriteLine("");
            }
            Console.WriteLine("*****************************************************************************************");
            Console.WriteLine("通过Dijkstra算法已经成功找到从任意单源点出发到达其余各点的最短路径和最短路，最短路径如上");
            Console.WriteLine("接下来进行OD分配");
            Console.WriteLine("*****************************************************************************************");
            Console.WriteLine("请按任意键继续……");
            Console.ReadKey();
            Console.WriteLine();
            int K = new int();//OD分配次数
            Console.WriteLine("请输入OD分配次数：（就本次项目而言，分配次数为4，则输入\"4\"）");
            K = int.Parse(Console.ReadLine());
            double[] Percentage = new double[K];//OD分配百分比
            Console.WriteLine("就本次项目而言，分配百分比依次为【40】 【30】 【20】 【10】");
            for (int i = 0; i < K; i++)
            {
                Console.Write("请输入第{0}次分配百分比，并按回车：", i + 1);
                Percentage[i] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("*****************************************************************************************");
            Console.WriteLine("参数设置完成");
            Console.Write("分配次数为：");
            Console.WriteLine("{0}次", K);
            Console.WriteLine("每次分配的百分比为：");
            for (int i = 0; i < K; i++)
            { Console.Write("【{0}】 ", Percentage[i]); }
            Console.WriteLine();
            Console.WriteLine("*****************************************************************************************");
            Console.WriteLine("请按任意键开始分配……");
            Console.ReadKey();
            for (int i = 0; i < K; i++)//循环K次分配
            {
                Console.WriteLine("第{0}次分配：", i + 1);
                //更新权重,清除缓存
                for (int j = 0; j < 12; j++)
                {
                    for (int k = 0; k < 12; k++)
                    {
                        Matrix[j, k] = QMatrix[j, k] * OdDistributed[j, k] + Matrix0[j, k];
                        ShortestPathSubscriptStringTemp[j, k] = null;//清除缓存
                    }
                }
                for (int j = 0; j < 12; j++)//寻路
                {
                    ClearAll();
                    FindPath(j);
                    PrintPath(j);
                    Console.WriteLine("");
                }
                for (int j = 0; j < 12; j++)//待分配的OD量存入缓存
                {
                    for (int k = 0; k < 12; k++)
                    { OdDistributeTemp[j, k] = (Percentage[i] / 100) * OdOrigine[j, k]; }
                }
                for (int j = 0; j < 12; j++)
                {
                    for (int k = 0; k < 12; k++)
                    {
                        int x = 0;
                        for (int t = 0; t < ShortestPathSubscriptStringTemp[j, k].Length; t++)//找到“/”的个数以推断节点个数
                        {
                            if (ShortestPathSubscriptStringTemp[j, k].Substring(t, 1) == "/")
                                x = x + 1;
                        }
                        string[] temp = new string[x + 1];
                        int y = 0;
                        for (int t = 0; t < ShortestPathSubscriptStringTemp[j, k].Length; t++)//将节点存入缓存
                        {
                            if (ShortestPathSubscriptStringTemp[j, k].Substring(t, 1) != "/")
                                temp[y] = temp[y] + ShortestPathSubscriptStringTemp[j, k].Substring(t, 1);
                            else
                                y = y + 1;
                        }
                        for (int t = 0; t < temp.Length - 1; t++)//OD量存入缓存矩阵
                        { OdDistributedTemp[int.Parse(temp[t]), int.Parse(temp[t + 1])] += OdDistributeTemp[j, k]; }
                    }
                }
                for (int j = 0; j < 12; j++)//OD分配
                {
                    for (int k = 0; k < 12; k++)
                    {
                        OdDistributed[j, k] += OdDistributedTemp[j, k];
                        OdDistributedTemp[j, k] = 0;//清除缓存
                        OdDistributeTemp[j, k] = 0;
                        ShortestPathSubscriptStringTemp[j, k] = null;//清除缓存
                    }
                }
                //打印
                Console.WriteLine("*****************************************************************************************");
                Console.WriteLine("打印分配结果如下：");
                for (int j = 0; j < 12; j++)
                {
                    for (int k = 0; k < 12; k++)
                    { Console.Write("{0} ", OdDistributed[j, k]); }
                    Console.WriteLine();
                }
                Console.WriteLine("*****************************************************************************************");
            }
            Console.WriteLine("请按任意键继续……");
            Console.ReadKey();
        }
        static void FindPath(int origin)//寻找最短路
        {
            S.Add(origin);//把起始点加入S集合
            judgement[origin] = true;//将其标记改为true
            for (int i = 0; i < n; i++)//把除起始点之外的点加入U集合
            {
                if (i != origin)
                    U.Add(i);
            }
            for (int i = 0; i < n; i++)//生成第一次寻路之前的distance和previousVertex参数
            {
                distance[i] = Matrix[origin, i];//源点到i的距离
                previousVertex[i] = -1;
            }
            while (U.Count > 0)//当U中还有未标号的点则继续循环，否则退出循环
            {
                int minWeightSubscript = (int)U[0];//找到当前点到U中距离最短的点对应的下标，minWeightSubscript为最短路径终点的下标
                foreach (int vertex in U)
                {
                    if ((!judgement[vertex]) && (distance[vertex] < distance[minWeightSubscript]))
                        minWeightSubscript = vertex;
                }
                S.Add(minWeightSubscript);//将这个点从集合U转移到集合S
                judgement[minWeightSubscript] = true;
                U.Remove(minWeightSubscript);
                foreach (int vertex in U)//比较源点不经过中间点的距离和源点经过中间点的距离，取最小值
                {
                    if (distance[minWeightSubscript] + Matrix[minWeightSubscript, vertex] < distance[vertex])
                    {
                        distance[vertex] = distance[minWeightSubscript] + Matrix[minWeightSubscript, vertex];
                        previousVertex[vertex] = minWeightSubscript;
                    }
                }
            }
        }
        static void PrintPath(int origin)//打印最短路
        {
            int previousPoint;
            string s;
            for (int i = 0; i < n; i++)
            {
                Console.Write("V{0}到V{1}的最短路径为：     V{0}→", origin, i);
                previousPoint = previousVertex[i];
                s = "";
                while (previousPoint > -1)//回溯并将点添加到s中     
                {
                    ShortestPathSubscriptStringTemp[origin, i] = previousPoint.ToString() + "/" + ShortestPathSubscriptStringTemp[origin, i];//OD分配的所需路径临时储存
                    s = "V" + previousPoint + "→" + s;
                    previousPoint = previousVertex[previousPoint];
                }
                ShortestPathSubscriptStringTemp[origin, i] = origin.ToString() + "/" + ShortestPathSubscriptStringTemp[origin, i] + i.ToString();
                Console.Write(s);
                Console.Write("V{0}", i);
                Console.WriteLine("     距离为：{0}", distance[i]);
            }
        }
        //清除数据
        static void ClearAll()
        {
            S.Clear();
            U.Clear();
            Array.Clear(distance, 0, 12);
            Array.Clear(previousVertex, 0, 12);
            Array.Clear(judgement, 0, 12);
        }
    }
}